using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using Elements.Spatial;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages the geometry of a floor with openings and Rooms representing excluded areas, corridors, occupied rooms, and services.
    /// </summary>
    public class Story
    {
        #region Constructors

        /// <summary>
        /// By default creates a Story at a 4.0 Height on the zero plane with the supplied Polygon perimeter.
        /// </summary>
        /// <returns>
        /// A new Story.
        /// </returns>
        public Story(Polygon perimeter)
        {
            if (perimeter == null)
            {
                return;
            }
            Perimeter = perimeter.IsClockWise() ? perimeter.Reversed() : perimeter;
      
            Openings = new List<Room>();
            Exclusions = new List<Room>();
            Services = new List<Room>();
            Corridors = new List<Room>();
            Rooms = new List<Room>();

            Color = Palette.White;
            Elevation = 0.0;
            Height = 4.0;
            Name = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Area of the perimeter.
        /// </summary>
        public double Area
        {
            get { return Perimeter.Area(); }
        }

        /// <summary>
        /// Unallocated area within the Story.
        /// </summary>
        public double AreaAvailable
        {
            get
            {
                var area = Perimeter.Area() - AreaPlaced;
                foreach (var opening in Openings)
                {
                    area -= opening.Area;
                }
                foreach (var exclusion in Exclusions)
                {
                    area -= exclusion.Area;
                }
                area = area < 0.0 ? 0.0 : area;
                return Math.Round(area, Room.PRECISION);
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
                    area += room.Area;
                }
                return Math.Round(area, Room.PRECISION);
            }
        }

        /// <summary>
        /// Rendering color of the Story returned as a Space.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Material of the Story. 
        /// </summary>
        public Material ColorAsMaterial
        {
            get
            {
                return new Material(Color, 0.0, 0.0, false, null, false, Guid.NewGuid(), Guid.NewGuid().ToString());
            }
        }

        /// <summary>
        /// List of Rooms designated as cooridors.
        /// </summary>
        public List<Room> Corridors { get; }

        /// <summary>
        /// Polygons representing Corridors.
        /// </summary>
        public List<Polygon> CorridorsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (var room in Corridors)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// Profiles representing Corridors.
        /// </summary>
        public List<Profile> CorridorsAsProfiles
        {
            get
            {
                var profiles = new List<Profile>();
                foreach (var room in Corridors)
                {
                    profiles.Add(new Profile(room.Perimeter));
                }
                return profiles;
            }
        }

        /// <summary>
        /// Sets the Corridors color.
        /// </summary>
        public Color ColorCorridors
        {
            set
            {
                foreach (var room in Corridors)
                {
                    room.Color = value;
                }
            }
        }

        /// <summary>
        /// Sets the Rooms color.
        /// </summary>
        public Color ColorRooms
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
        /// Sets the Services color.
        /// </summary>
        public Color ColorServices
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
        /// Elevation of the Story relative to the zero plane. 
        /// A change in this value will also change the elevations of all Rooms in all Story lists.
        /// </summary>
        private double elevation;
        public double Elevation
        {
            get { return Math.Round(elevation, Room.PRECISION); }
            set
            {
                elevation = value;
                Perimeter = Perimeter.MoveFromTo(Vector3.Origin, new Vector3(0.0, 0.0, elevation));

                var rooms = new List<Room>(Corridors);
                rooms.AddRange(Exclusions);
                rooms.AddRange(Openings);
                rooms.AddRange(Rooms);
                rooms.AddRange(Services);
                foreach (Room room in rooms)
                {
                    room.Elevation = elevation;
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
                return
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
        /// Rooms representing areas that must not be intersected..
        /// </summary>
        public List<Room> Exclusions { get; }

        /// <summary>
        /// Polygons representing areas that must not be intersected.
        /// </summary>
        public List<Polygon> ExclusionsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (var room in Exclusions)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// Profiles representing areas that must not be intersected.
        /// </summary>
        public List<Profile> ExclusionsAsProfiles
        {
            get
            {
                var profiles = new List<Profile>();
                foreach (var room in Exclusions)
                {
                    profiles.Add(new Profile(room.Perimeter));
                }
                return profiles;
            }
        }

        /// <summary>
        /// Height of the Story relative to its elevation.
        /// </summary>
        private double height;
        public double Height
        {
            get { return Math.Round(height, Room.PRECISION); }
            set
            {
                if (value <= 0.0)
                {
                    return;
                }
                height = value;
                foreach (var room in Services)
                {
                    room.Height = height;
                }
                var rooms = new List<Room>(Rooms);
                rooms.AddRange(Corridors);
                foreach (var room in Rooms)
                {
                    if (room.Height > height)
                    {
                        room.Height = height;
                    }
                }
                
            }
        }

        /// <summary>
        /// Sets the height of all Corridors, Rooms, and Services.
        /// </summary>
        public double HeightInteriors
        {
            set
            {
                height = height < value ? value : height;
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
        /// Returns all Corridors, Exclusions, Rooms, and Services as Polygons.
        /// </summary>
        public List<Polygon> InteriorsAsPolygons
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
        /// Arbitrary string identifier.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of Rooms designated as floor openings.
        /// </summary>
        public List<Room> Openings { get; }

        /// <summary>
        /// Polygons representing Rooms.
        /// </summary>
        public List<Polygon> OpeningsAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room opening in Openings)
                {
                    polygons.Add(opening.Perimeter);
                }
                return polygons;
            }
        }

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
        /// The perimeter of the Story as a Profile.
        /// </summary>
        public Profile PerimeterAsProfile
        {
            get { return new Profile(perimeter); }
        }

        /// <summary>
        /// List of Rooms designated as occupiable.
        /// </summary>
        public List<Room> Rooms { get; }

        /// <summary>
        /// Polygons representing Rooms.
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
        /// Profiles representing Rooms.
        /// </summary>
        public List<Profile> RoomsAsProfiles
        {
            get
            {
                var profiles = new List<Profile>();
                foreach (Room room in Rooms)
                {
                    profiles.Add(new Profile(room.Perimeter));
                }
                return profiles;
            }
        }

        /// <summary>
        /// List of RoomRows. Can be a zero-length list if no RoomRows have been created.
        /// </summary>
        public List<RoomRow> RoomRows { get; }

        /// <summary>
        /// A list of Rooms designated as building services.
        /// </summary>
        public List<Room> Services { get; }

        /// <summary>
        /// Polygons representing Services.
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
        /// Profiles representing Services.
        /// </summary>
        public List<Profile> ServicesAsProfiles
        {
            get
            {
                var profiles = new List<Profile>();
                foreach (Room room in Services)
                {
                    profiles.Add(new Profile(room.Perimeter));
                }
                return profiles;
            }
        }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion
        #region Private Methods

        /// <summary>
        /// Private function conforming the Corridors to other Story Room lists.
        /// </summary>
        private void FitCorridors()
        {
            var corridors = Corridors.ToList();
            Corridors.Clear();
            foreach (var corridor in corridors)
            {
                AddCorridor(corridor);
            }
        }

        /// <summary>
        /// Private function conforming the Rooms all other Story Room lists.
        /// </summary>
        private void FitRooms()
        {
            var rooms = Rooms.ToList();
            Rooms.Clear();
            foreach (var room in rooms)
            {
                AddRoom(room);
            }
        }

        /// <summary>
        /// Private function conforming the Rooms all other Story Room lists.
        /// </summary>
        private void FitServices()
        {
            var services = Services.ToList();
            Services.Clear();
            foreach (var service in services)
            {
                AddService(service);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a Room to the Corridors list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <returns>
        /// True if one or more Rooms were added.
        /// </returns>
        public bool AddCorridor(Room room, bool merge = true, double tolerance = 0.0)
        {
            var perimeters = Shaper.FitWithin(room.Perimeter, Perimeter);
            if (perimeters.Count == 0)
            {
                return false;
            }
            var fitAmong = new List<Polygon>(OpeningsAsPolygons);
            fitAmong.AddRange(ExclusionsAsPolygons);
            fitAmong.AddRange(ServicesAsPolygons);
            var corridors = Shaper.Differences(perimeters, fitAmong).ToList();
            if (corridors.Count() == 0)
            {
                return false;
            }
            corridors.AddRange(CorridorsAsPolygons);
            if (merge)
            {
                corridors = Shaper.Merge(corridors, Shaper.FillType.NonZero, tolerance);
            }
            Corridors.Clear();
            foreach (var corridor in corridors)
            {
                Corridors.Add(
                    new Room(room)
                    {
                        Elevation = Elevation,
                        Height = room.Height,
                        Perimeter = corridor 
                    });
            }
            FitRooms();
            return true;
        }

        /// <summary>
        /// Adds a Room to the Exclusions list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <returns>
        /// True if one or more Rooms were added.
        /// </returns>
        public bool AddExclusion(Room room)
        {
            Exclusions.Add(new Room(room) { Elevation = Elevation });
            FitServices();
            FitCorridors();
            FitRooms();
            return true;
        }

        /// <summary>
        /// Adds a Room to the Openings list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <returns>
        /// True if one or more Rooms were added.
        /// </returns>
        public bool AddOpening(Room room)
        {
            Openings.Add(new Room(room) { Elevation = Elevation });
            FitServices();
            FitCorridors();
            FitRooms();
            return true;
        }

        /// <summary>
        /// Adds a Room to the Rooms list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <returns>
        /// True if one or more Rooms were added.
        /// </returns>
        public bool AddRoom(Room room)
        {
            var perimeter = room.Perimeter.FitMost(Perimeter);
            if (perimeter == null)
            {
                return false;
            }
            var fitAmong = new List<Polygon>(OpeningsAsPolygons);
            fitAmong.AddRange(ExclusionsAsPolygons);
            fitAmong.AddRange(ServicesAsPolygons);
            fitAmong.AddRange(CorridorsAsPolygons);
            perimeter = perimeter.FitAmong(fitAmong);
            if (perimeter == null)
            {
                return false;
            }
            Rooms.Add(
                new Room(room) 
                { 
                    Elevation = Elevation,
                    Perimeter = perimeter 
                });
            return true;
        }

        /// <summary>
        /// Adds a Room to the Services list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <returns>
        /// True if one or more Rooms were added.
        /// </returns>
        public bool AddService(Room room)
        {
            var perimeter = room.Perimeter.FitMost(Perimeter);
            if (perimeter == null)
            {
                return false;
            }
            var fitAmong = new List<Polygon>(OpeningsAsPolygons);
            fitAmong.AddRange(ExclusionsAsPolygons);
            fitAmong.AddRange(ServicesAsPolygons);
            perimeter = perimeter.FitAmong(fitAmong);
            if (perimeter == null)
            {
                return false;
            }
            Services.Add(new Room(room) 
            { 
                Elevation = Elevation,
                Height = height,
                Perimeter = perimeter 
            });
            FitCorridors();
            FitRooms();
            return true;
        }

        /// <summary>
        /// Returns the aggregate area of all Rooms with a supplied name.
        /// </summary>
        /// <param name="name">Name of the Rooms to retrieve.</param>
        /// <returns>
        /// A double.
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
            return Math.Round(area, Room.PRECISION);
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
            Perimeter = Perimeter.MoveFromTo(from, to);
            foreach (var room in Corridors)
            {
                room.MoveFromTo(from, to);
            }
            foreach (var room in Exclusions)
            {
                room.MoveFromTo(from, to);
            }
            foreach (var room in Openings)
            {
                room.MoveFromTo(from, to);
            }
            foreach (var room in Rooms)
            {
                room.MoveFromTo(from, to);
            }
            foreach (var room in Services)
            {
                room.MoveFromTo(from, to);
            }
        }

        /// <summary>
        /// Creates a Room plan from a Polygon centerline as the double-loaded corridor. Assumes sides of the Story Perimeter are parallel to the centerline.
        /// </summary>
        /// <param name="ctrLine">Polyline centerline of the Corridor.</param>
        /// <param name="roomArea">Desired area of each Room.</param>
        /// <param name="corridorWidth">Width of the Corridor.</param>
        /// <param name="corridorOffset">Offset of the Corridor end from the Perimeter.</param>
        public void PlanByCenterline (Polyline ctrLine, 
                                      double roomArea, 
                                      double corridorWidth, 
                                      double corridorOffset)
        {
            var perLines = Perimeter.Segments();
            var ctrLines = ctrLine.Segments();
            var rows = new List<Polygon>();
            foreach (var line in ctrLines)
            {
                var sides = new List<Line>();
                foreach (var perLine in perLines)
                {
                    if (perLine.IsParallelTo(line))
                    {
                        sides.Add(perLine);
                    }
                }
                if (sides.Count() < 2)
                {
                    continue;
                }
                sides = sides.OrderBy(s => s.Midpoint().DistanceTo(line.Midpoint())).ToList();
                sides = new List<Line>() { sides[0], sides[1] };
                foreach (var side in sides)
                {
                    var points = new List<Vector3>();
                    foreach (var vertex in new List<Vector3>() { line.Start, line.End, side.Start, side.End })
                    {
                        points.Add(new Vector3(vertex.X, vertex.Y, 0.0));
                    }
                    Polygon polygon;
                    try
                    {
                        polygon = new Polygon(points);
                    }
                    catch (Exception)
                    {
                        polygon = new Polygon(new[] { points[0], points[1], points[3], points[2] });
                    }
                    rows.Add(polygon);
                }
            }
            foreach (var row in rows)
            {
                var roomRow = new RoomRow(row);
                roomRow.Populate(roomArea, Height);
                foreach (var room in roomRow.Rooms)
                {
                    AddRoom(room);
                }
            }
            var ctrPnts = new List<Vector3>() { ctrLines.First().PositionAt(corridorOffset) };
            foreach (var line in ctrLines)
            {
                ctrPnts.Add(line.End);
            }
            ctrPnts.Reverse();
            ctrPnts = ctrPnts.Skip(1).ToList();
            ctrPnts.Reverse();
            ctrPnts.Add(ctrLines.Last().PositionAt(ctrLines.Last().Length() - corridorOffset));
            var ctrCorridor = new Polyline(ctrPnts);
            var corridor = new Room(ctrCorridor.Offset(corridorWidth * 0.5, EndType.Square).First(), Height);
            AddCorridor(corridor);
        }

        /// <summary>
        /// Creates a grid network of corridors within the Story and returns a list of spatially sorted RoomRows ready for population.
        /// </summary>
        /// <param name="rowLength">Distance between cross corridors.</param>
        /// <param name="roomDepth">Desired depth of Rooms.</param>
        /// <param name="corridorWidth">Width of all corridors.</param>
        /// <returns>A List of RoomRow</returns>
        public List<RoomRow> PlanGrid(double rowLength, double rowDepth, double corridorWidth = 3.0, bool split = true)
        {
            Corridors.Clear();
            Rooms.Clear();
            rowLength = rowLength.NearEqual(0.0) ? 1.0 : Math.Abs(rowLength);
            rowDepth = rowLength.NearEqual(0.0) ? 1.0 : Math.Abs(rowDepth);
            corridorWidth = rowLength.NearEqual(0.0) ? 1.0 : Math.Abs(corridorWidth);

            var row = Perimeter.Segments().OrderByDescending(s => s.Length()).First();
            var ang = Math.Atan2(row.End.Y - row.Start.Y, row.End.X - row.Start.X) * (180 / Math.PI);
            var perimeterJig = Perimeter.Rotate(Vector3.Origin, ang * -1);
            var grid = new Grid2d(perimeterJig);
            grid.U.DivideByFixedLength(rowLength, FixedDivisionMode.RemainderAtBothEnds);
            grid.V.DivideByFixedLength(rowDepth, FixedDivisionMode.RemainderAtBothEnds);
            var uLines = grid.GetCellSeparators(GridDirection.U).Skip(1).Reverse().Skip(1).Reverse();
            var vLines = grid.GetCellSeparators(GridDirection.V).Skip(1).Reverse().Skip(1).Reverse();
            var ctrLines = new List<Line>();
            foreach(var curve in uLines)
            {
                ctrLines.Add((Line)curve);
            }
            foreach (var curve in vLines)
            {
                ctrLines.Add((Line)curve);
            }
            foreach (var line in ctrLines)
            {
                var corridor = line.Thicken(corridorWidth);
                if (perimeterJig.Compass().Box.Covers(corridor))
                {
                    AddCorridor(new Room(corridor.Rotate(Vector3.Origin, ang), Height));
                }
            }
            foreach (var cell in grid.CellsFlat)
            {
                var polygon = (Polygon)cell.GetCellGeometry();
                var compass = polygon.Compass();
                if (split)
                {
                    var north = Polygon.Rectangle(compass.W, compass.NE).Rotate(Vector3.Origin, ang);
                    var south = Polygon.Rectangle(compass.SW, compass.E).Rotate(Vector3.Origin, ang);
                    AddRoom(new Room(north, Height));
                    AddRoom(new Room(south, Height));
                    continue;
                }
                else if (Math.Abs(compass.SizeY - rowDepth) <= 0.0001)
                {
                    AddRoom(new Room(polygon.Rotate(Vector3.Origin, ang), Height));
                }
            }
            var roomRows = new List<RoomRow>();
            foreach (var room in Rooms)
            {
                roomRows.Add(new RoomRow(room.Perimeter));
            }
            Rooms.Clear();
            return roomRows;
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
            foreach (var room in Corridors)
            {
                room.Rotate(pivot, angle);
            }
            foreach (var room in Exclusions)
            {
                room.Rotate(pivot, angle);
            }
            foreach (var room in Openings)
            {
                room.Rotate(pivot, angle);
            }
            foreach (var room in Rooms)
            {
                room.Rotate(pivot, angle);
            }
            foreach (var room in Services)
            {
                room.Rotate(pivot, angle);
            }
        }
        #endregion

    }
}
