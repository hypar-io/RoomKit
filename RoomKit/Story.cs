using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.
    /// </summary>
    public class Story
    {
        #region Constructors
        /// <summary>
        /// By default creates a Story at a 3.0 Height on the zero plane with new lists for Corridors, Rooms, and Services.
        /// Perimeter is set to null, Name is "", and SlabThickness is 0.1.
        /// </summary>
        /// <returns>
        /// A new Story.
        /// </returns>

        public Story(double elevation = 0.0, 
                     double height = 3.0,
                     bool isBasement = false,
                     string name = "",
                     Polygon perimeter = null,
                     double slabThick = 0.1,
                     int typeID = -1)
        {
            Corridors = new List<Room>();
            Exclusions = new List<Room>();
            Rooms = new List<Room>();
            Services = new List<Room>();
            UniqueID = Guid.NewGuid().ToString();

            Color = Palette.White;
            Elevation = elevation;
            Height = height;
            IsBasement = isBasement;
            Name = name;
            Perimeter = perimeter;
            slabThickness = slabThick;
            SlabType = new FloorType(new Guid().ToString(), slabThickness);
            TypeID = typeID;
        }

        /// <summary>
        /// Area of the perimeter.
        /// </summary>
        public double Area
        {
            get {  return (Perimeter == null) ? 0.0 : Perimeter.Area(); }
        }

        /// <summary>
        /// Unallocated area within the Story.
        /// </summary>
        public double AreaAvailable
        {
            get
            {
                if (Perimeter == null)
                {
                    return 0.0;
                }
                var area = Perimeter.Area();
                var rooms = new List<Room>(Corridors);
                rooms.AddRange(Exclusions);
                rooms.AddRange(Rooms);
                rooms.AddRange(Services);
                foreach (Room room in rooms)
                {
                    if (room.Perimeter != null)
                    {
                        area -= room.Perimeter.Area();
                    }
                }
                if (area < 0.0)
                {
                    area = 0.0;
                }
                return area;
            }
        }

        /// <summary>
        /// Area allocated to Corridors, Rooms, and Services.
        /// </summary>
        public double AreaPlaced
        {
            get
            {
                var area = 0.0;
                var placed = new List<Room>(Corridors);
                placed.AddRange(Rooms);
                placed.AddRange(Services);
                foreach (Room room in placed)
                {
                    area += room.Perimeter.Area();
                }
                return area;
            }
        }

        /// <summary>
        /// Rendering color of the Story returned as a Space.
        /// </summary>
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value ?? color; }
        }

        /// <summary>
        /// List of Rooms designated as cooridors.
        /// </summary>
        public List<Room> Corridors { get; private set;  }

        /// <summary>
        /// List of Masses created from Room characteristics within the Corridors list.
        /// </summary>
        public List<Mass> CorridorsAsMasses
        {
            get
            {
                var masses = new List<Mass>();
                foreach (Room room in Corridors)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                return masses;
            }
        }

        /// <summary>
        /// Polygons representing Corridors.
        /// Rooms Perimeters in the Story conform to Corridor Perimeters.
        /// </summary>
        public List<Polygon> CorridorsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Corridors)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of Spaces created from Room characteristics within the Corridors list.
        /// </summary>
        public List<Space> CorridorsAsSpaces
        {
            get
            {
                var spaces = new List<Space>();
                foreach (Room room in Corridors)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                return spaces;
            }
        }

        /// <summary>
        /// Sets the Corridors color.
        /// </summary>
        public Color CorridorsColor
        {
            set
            {
                foreach (Room room in Corridors)
                {
                    room.Color = value;
                }
            }
        }

        /// <summary>
        /// Elevation of the Story relative to the zero plane. A change in this value will also change the elevations of all Rooms in all Story lists.
        /// </summary>
        private double elevation;
        public double Elevation
        {
            get { return elevation; }
            set
            {
                elevation = value;
                foreach (Room room in Corridors)
                {
                    room.Elevation = value;
                }
                foreach (Room room in Rooms)
                {
                    room.Elevation = value;
                }
                foreach (Room room in Services)
                {
                    room.Elevation = value;
                }
            }
        }

        /// <summary>
        /// Room representing the Story envelope.
        /// </summary>
        public Room Envelope
        {
            get
            {
                return (Perimeter == null) ? null : 
                    new Room()
                    {
                        Color = Color,
                        Height = Height,
                        Name = Name,
                        Perimeter = Perimeter
                    };
            }
        }

        /// <summary>
        /// Polygon representation of the Story Perimeter.
        /// </summary>
        public Polygon EnvelopeAsPolygon
        {
            get { return Envelope.Perimeter; }
        }

        /// <summary>
        /// Mass created from Story characteristics.
        /// </summary>
        public Mass EnvelopeAsMass
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                var t = new Transform(0.0, 0.0, Elevation);
                var mass = new Mass(new Profile(Perimeter), Height, new Material(Guid.NewGuid().ToString(), Color), t)
                {
                    Name = "Name"
                };
                mass.AddProperty("Name", new StringProperty(Name, UnitType.Text));
                mass.AddProperty("Area", new NumericProperty(Perimeter.Area(), UnitType.Area));
                mass.AddProperty("Elevation", new NumericProperty(Elevation, UnitType.Distance));
                mass.AddProperty("Height", new NumericProperty(Height, UnitType.Distance));
                return mass;
            }
        }

        /// <summary>
        /// Space created from Story characteristics.
        /// </summary>
        public Space EnvelopeAsSpace
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                var space = new Space(Perimeter, Height, Elevation, new Material(Guid.NewGuid().ToString(), Color));
                space.AddProperty("Name", new StringProperty(Name, UnitType.Text));
                space.AddProperty("Area", new NumericProperty(Perimeter.Area(), UnitType.Area));
                space.AddProperty("Elevation", new NumericProperty(Elevation, UnitType.Distance));
                space.AddProperty("Height", new NumericProperty(Height, UnitType.Distance));
                return space;
            }
        }

        /// <summary>
        /// Rooms representing areas that must not be intersected, but which will not be available as Spaces.
        /// All other Room Perimeters in the Story conform to Exclusion Room Perimeters.
        /// </summary>
        public List<Room> Exclusions { get; }

        /// <summary>
        /// List of Masses created from Exclusion characteristics within the Exclusions list.
        /// </summary>
        public List<Mass> ExclusionsAsMasses
        {
            get
            {
                var masses = new List<Mass>();
                foreach (Room room in Exclusions)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                return masses;
            }
        }

        /// <summary>
        /// Polygons representing areas that must not be intersected.
        /// All other Room Perimeters in the Story conform to Exclusion Room Perimeters.
        /// </summary>
        public List<Polygon> ExclusionsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Exclusions)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of Spaces created from Exclusion characteristics within the Exclusions list.
        /// </summary>
        public List<Space> ExclusionsAsSpaces
        {
            get
            {
                var spaces = new List<Space>();
                foreach (Room room in Exclusions)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                return spaces;
            }
        }

        /// <summary>
        /// Height of the Story relative to its elevation.
        /// </summary>
        private double height;
        public double Height
        {
            get { return height; }
            set { height = value > 0.0 ? value : height; }
        }

        /// <summary>
        /// Sets the height of all Corridors, Rooms, and Services.
        /// </summary>
        public double HeightInteriors
        {
            set
            {
                foreach (Room room in Corridors)
                {
                    room.Height = value;
                }
                foreach (Room room in Rooms)
                {
                    room.Height = value;
                }
                foreach (Room room in Services)
                {
                    room.Height = value;
                }
            }
        }

        /// <summary>
        /// Returns all Corridors, Rooms, and Services as Masses.
        /// </summary>
        public IList<Mass> InteriorsAsMasses
        {
            get
            {
                var masses = new List<Mass>();
                foreach (Room room in Corridors)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                foreach (Room room in Rooms)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                foreach (Room room in Services)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                return masses;
            }
        }

        /// <summary>
        /// Returns all Corridors, Exclusions, Rooms, and Services as Polygons.
        /// </summary>
        public IList<Polygon> InteriorsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Corridors)
                {
                    polygons.Add(room.Perimeter);
                }
                foreach (Room room in Rooms)
                {
                    polygons.Add(room.Perimeter);
                }
                foreach (Room room in Services)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// Returns all Corridors, Rooms, and Services as Spaces.
        /// </summary>
        public IList<Space> InteriorsAsSpaces
        {
            get
            {
                var spaces = new List<Space>();
                foreach (Room room in Corridors)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                foreach (Room room in Rooms)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                foreach (Room room in Services)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                return spaces;
            }
        }

        /// <summary>
        /// Identifies whether this story represents a base ment level.
        /// </summary>
        public bool IsBasement { get; set; }

        /// <summary>
        /// Arbitrary string identifier.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The perimeter of the Story.
        /// </summary>
        private Polygon perimeter;
        public Polygon Perimeter
        {
            get { return perimeter; }
            set { perimeter = value ?? perimeter; }
        }

        /// <summary>
        /// List of Rooms designated as occupiable rooms.
        /// </summary>
        public List<Room> Rooms { get; private set; }

        /// <summary>
        /// List of Masses created from Room characteristics within the Rooms list.
        /// </summary>
        public List<Mass> RoomsAsMasses
        {
            get
            {
                var masses = new List<Mass>();
                foreach (Room room in Rooms)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                return masses;
            }
        }

        /// <summary>
        /// Polygons representing Services.
        /// Corridors and Rooms Perimeters in the Story conform to Service Room Perimeters.
        /// </summary>
        public List<Polygon> RoomsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Rooms)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of Spaces created from Room characteristics within the Rooms list.
        /// </summary>
        public List<Space> RoomsAsSpaces
        {
            get
            {
                var spaces = new List<Space>();
                foreach (Room room in Rooms)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                return spaces;
            }
        }

        /// <summary>
        /// Sets the Rooms rendering color.
        /// </summary>
        public Color RoomsColor
        {
            set
            {
                foreach (Room room in Rooms)
                {
                    room.Color = value;
                }
            }
        }

        /// <summary>
        /// A list of Rooms designated as building services.
        /// </summary>
        public List<Room> Services { get; private set;  }

        /// <summary>
        /// List of Masses created from Room characteristics within the Services list.
        /// </summary>
        public List<Mass> ServicesAsMasses
        {
            get
            {
                var masses = new List<Mass>();
                foreach (Room room in Services)
                {
                    if (room.Perimeter != null)
                    {
                        masses.Add(room.AsMass);
                    }
                }
                return masses;
            }
        }

        /// <summary>
        /// Polygons representing Services.
        /// Corridors and Rooms Perimeters in the Story conform to Service Room Perimeters.
        /// </summary>
        public List<Polygon> ServicesAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Services)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of Spaces created from Room characteristics within the Services list.
        /// </summary>
        public List<Space> ServicesAsSpaces
        {
            get
            {
                var spaces = new List<Space>();
                foreach (Room room in Services)
                {
                    if (room.Perimeter != null)
                    {
                        spaces.Add(room.AsSpace);
                    }
                }
                return spaces;
            }
        }

        /// <summary>
        /// Sets the Services Space rendering color.
        /// </summary>
        public Color ServicesColor
        {
            set
            {
                foreach (Room room in Services)
                {
                    room.Color = value;
                }
            }
        }

        /// <summary>
        /// Concrete Floor created from Story and Slab characteristics.
        /// </summary>
        public Floor Slab
        {
            get
            {
                return new Floor(Perimeter, SlabType, Elevation);
            }
        }

        /// <summary>
        /// FloorType of the Story's floor slab.
        /// </summary>
        private FloorType slabType;
        public FloorType SlabType
        {
            get
            {
                return slabType;
            }
            set
            {
                slabType = value;
                slabThickness = slabType.Thickness();
            }
        }

        /// <summary>
        /// Thickness of the Story's floor slab.
        /// </summary>
        private double slabThickness;
        public double SlabThickness
        {
            get { return slabThickness; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
                }
                slabThickness = value;
                slabType = new FloorType(slabType.Name, slabThickness);
            }
        }

        /// <summary>
        /// Arbitrary integer identifier of this instance..
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Private Methods
        /// <summary>
        /// Private function conforming a list of Rooms to another list of Rooms.
        /// </summary>
        /// <param name="fitRooms">Rooms that will conform the 'to' list of Rooms.</param>
        /// <param name="toRooms">List of Rooms to which the 'fitted' Rooms will conform.</param>
        /// <param name="toStory">Indicates whether the new Room should conform to the Story Perimeter. True by default.</param>     
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        private List<Room> FitRooms(List<Room> fitRooms, List<Room> toRooms, bool toStory = true)
        {
            if (toRooms.Count == 0)
            {
                return fitRooms;
            }
            var addRooms = new List<Room>();
            var toPolygons = new List<Polygon>();
            foreach (Room toRoom in toRooms)
            {
                toPolygons.Add(toRoom.Perimeter);
            }
            Polygon storyPerimeter = Perimeter;
            if (!toStory)
            {
                storyPerimeter = null;
            }
            foreach (Room fitRoom in fitRooms)
            {
                var perimeters = Shaper.FitTo(fitRoom.Perimeter, storyPerimeter, toPolygons);
                for (int i = 0; i < perimeters.Count; i++)
                {
                    var addRoom = new Room()
                    {
                        Color = fitRoom.Color,
                        Elevation = fitRoom.Elevation,
                        Height = fitRoom.Height,
                        Name = fitRoom.Name,
                        Perimeter = perimeters[i]
                    };
                    addRooms.Add(addRoom);
                }
            }
            return addRooms;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a Room to the Corridors list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <param name="fit">Indicates whether the new corridor should mutually fit to other Story features.</param>
        /// <param name="merge">Indicates whether the new corridor should merge with other corridors.</param>
        /// <returns>
        /// True if one or more rooms were added to the Story.
        /// </returns>
        public bool AddCorridor(Room room, bool fit = true, bool merge = true)
        {
            if (Perimeter == null || room.Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            if (!Perimeter.Covers(perimeter))
            {
                throw new ArgumentNullException(Messages.PERIMETER_PLACEMENT_EXCEPTION);
            }
            var newRoom =
                new Room()
                {
                    Color = room.Color,
                    Elevation = Elevation,
                    Height = room.Height,
                    Name = room.Name,
                    Perimeter = room.Perimeter
                };
            var fitRooms = new List<Room> { newRoom };
            if (fit)
            {
                var toRooms = new List<Room>(Exclusions);
                toRooms.AddRange(Services);
                fitRooms = FitRooms(fitRooms, toRooms, false);
                Rooms = FitRooms(Rooms, fitRooms);
            }
            if (fitRooms.Count == 0)
            {
                return false;
            }
            Corridors.AddRange(fitRooms);
            var merged = Shaper.Merge(CorridorsAsPolygons);
            if (merge && merged.Count < Corridors.Count)
            {
                Corridors.Clear();
                foreach (var corridor in merged)
                {
                    Corridors.Add(
                        new Room()
                        {
                            Color = room.Color,
                            Elevation = Elevation,
                            Height = room.Height,
                            Name = room.Name,
                            Perimeter = corridor
                        });
                }
            }
            return true;
        }

        /// <summary>
        /// Adds a Room to the Exclusions list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <param name="fit">Indicates whether the new room should mutually fit to other Story features. Default is true.</param>
        /// <returns>
        /// True if one or more rooms were added to the Story.
        /// </returns>
        public bool AddExclusion(Room room, bool fit = true)
        {
            if (Perimeter == null || room.Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            if (!Perimeter.Covers(perimeter))
            {
                throw new ArgumentNullException(Messages.PERIMETER_PLACEMENT_EXCEPTION);
            }
            var newRoom =
                new Room()
                {
                    Elevation = Elevation,
                    Name = room.Name,
                    Perimeter = room.Perimeter
                };
            var toRooms = new List<Room> { newRoom };
            if (fit)
            {
                Services = FitRooms(Services, toRooms);
                toRooms.AddRange(Services);
                Corridors = FitRooms(Corridors, toRooms);
                toRooms.AddRange(Corridors);
                Rooms = FitRooms(Rooms, toRooms);
            }
            Exclusions.Add(newRoom);
            return true;
        }

        /// <summary>
        /// Adds a Room to the Rooms list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <param name="fit">Indicates whether the new Room should mutually fit to other Story features. Default is true.</param>
        /// <returns>
        /// True if one or more Rooms were added to the Story.
        /// </returns>
        public bool AddRoom(Room room, bool fit = true)
        {
            if (Perimeter == null || room.Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            if (!Perimeter.Covers(perimeter))
            {
                throw new ArgumentNullException(Messages.PERIMETER_PLACEMENT_EXCEPTION);
            }
            var newRoom =
                new Room()
                {
                    Color = room.Color,
                    Elevation = Elevation,
                    Height = room.Height,
                    Name = room.Name,
                    Perimeter = room.Perimeter
                };
            var fitRooms = new List<Room> { newRoom };
            if (fit)
            {
                var toRooms = new List<Room>(Exclusions);
                toRooms.AddRange(Services);
                toRooms.AddRange(Corridors);
                fitRooms = FitRooms(fitRooms, toRooms);
            }
            if (fitRooms.Count == 0)
            {
                return false;
            }
            Rooms.AddRange(fitRooms);
            return true;
        }

        /// <summary>
        /// Adds a Room to the Services list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <param name="fit">Indicates whether the new Room should mutually fit to other Story features. Default is true.</param>
        /// <returns>
        /// True if one or more Rooms were added to the Story.
        /// </returns>
        public bool AddService(Room room, bool fit = true)
        {
            if (Perimeter == null || room.Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            if (!Perimeter.Covers(perimeter))
            {
                throw new ArgumentNullException(Messages.PERIMETER_PLACEMENT_EXCEPTION);
            }
            var newRoom =
                new Room()
                {
                    Color = room.Color,
                    Elevation = Elevation,
                    Height = Height,
                    Name = room.Name,
                    Perimeter = room.Perimeter
                };
            var fitRooms = new List<Room> { newRoom };
            if (fit)
            {
                var toRooms = new List<Room>(Exclusions);
                fitRooms = FitRooms(fitRooms, toRooms);
                toRooms.AddRange(fitRooms);
                Corridors = FitRooms(Corridors, toRooms);
                toRooms.AddRange(Corridors);
                Rooms = FitRooms(Rooms, toRooms);
            }
            if (fitRooms.Count == 0)
            {
                return false;
            }
            Services.AddRange(fitRooms);
            return true;
        }

        /// <summary>
        /// Returns the aggregate area of all Rooms with a supplied name.
        /// </summary>
        /// <param name="name">Name of the Rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public double AreaByName(string name)
        {
            var area = 0.0;
            foreach (var room in Rooms)
            {
                if (room.Name == name)
                {
                    area += room.Area;
                }
            }
            return area;
        }

        /// <summary>
        /// Moves all Rooms in the Story and the Story Envelope along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            if (Perimeter != null)
            {
                Perimeter = Perimeter.MoveFromTo(from, to);
            }
            foreach (Room room in Corridors)
            {
                room.MoveFromTo(from, to);
            }
            foreach (Room room in Exclusions)
            {
                room.MoveFromTo(from, to);
            }
            foreach (Room room in Rooms)
            {
                room.MoveFromTo(from, to);
            }
            foreach (Room room in Services)
            {
                room.MoveFromTo(from, to);
            }
        }

        /// <summary>
        /// Creates Rooms by orthogonally dividing the interior of the Story perimeter by a quantity of x-axis and y-axis intervals.
        /// Adds the new Rooms to the Rooms list.
        /// New Rooms conform to Corridor and Service perimeters.
        /// </summary>
        /// <param name="xRooms">Quantity Rooms along the orthogonal x-axis.</param>
        /// <param name="yRooms">Quantity Rooms along the orthogonal y-axis.</param>
        /// <param name="height">Height of the new Rooms.</param>
        /// <param name="setback">Offset from the Story perimeter.</param>
        ///  <param name="name">String identifier applied to every new Room.</param>
        /// <param name="color">Rendering color of the Room as a Space.</param>
        /// <returns>
        /// None.
        /// </returns>
        public bool RoomsByDivision(int xRooms = 1,
                                    int yRooms = 1,
                                    double height = 3.0,
                                    double setback = 0.0,
                                    string name = "",
                                    Color color = null,
                                    bool fit = true)
        {
            if (Perimeter == null || height < 0.0 || setback < 0.0 || xRooms < 1 || yRooms < 1)
            {
                return false;
            }
            var polygon = Perimeter.Offset(setback * -1.0).First();
            var roomGroup =
                new RoomGroup()
                {
                    Name = name,
                    Perimeter = polygon
                };
            roomGroup.RoomsByDivision(xRooms, yRooms, height, name);
            roomGroup.Elevation = Elevation;
            roomGroup.SetHeight(height);
            roomGroup.SetColor(color);
            var fitRooms = new List<Room>(roomGroup.Rooms);
            if (fit)
            {
                var toRooms = new List<Room>(Exclusions);
                toRooms.AddRange(Services);
                toRooms.AddRange(Corridors);
                fitRooms = FitRooms(fitRooms, toRooms);
            }
            if (fitRooms.Count == 0)
            {
                return false;
            }
            Rooms.Clear();
            Rooms.AddRange(fitRooms);
            return true;
        }

        /// <summary>
        /// Returns a list of Rooms with a specific name.
        /// </summary>
        /// <param name="name">Name of the rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public List<Room> RoomsByName (string name)
        {
            var rooms = new List<Room>();
            foreach (var room in Rooms)
            {
                if (room.Name == name)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }

        /// <summary>
        /// Rotates the Story Perimeter and Rooms in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
        /// <param name="angle">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void Rotate(Vector3 pivot, double angle)
        {
            if (Perimeter != null)
            {
                Perimeter = Perimeter.Rotate(pivot, angle);
            }
            foreach (Room room in Corridors)
            {
                room.Rotate(pivot, angle);
            }
            foreach (Room room in Exclusions)
            {
                room.Rotate(pivot, angle);
            }
            foreach (Room room in Rooms)
            {
                room.Rotate(pivot, angle);
            }
            foreach (Room room in Services)
            {
                room.Rotate(pivot, angle);
            }
        }
        #endregion
    }
}
