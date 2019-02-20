using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.
    /// </summary>
    public class Story
    {
        /// <summary>
        /// Creates a Story at a 1.0 Height on the zero plane with new lists for Corridors, Rooms, and Services.
        /// Perimeter is set to null, Name is blank, and SlabThickness is s0.1.
        /// </summary>
        /// <param name="ratio">The ratio of width to depth</param>
        /// <param name="area">The required area of the Polygon.</param>
        /// <returns>
        /// A new Story.
        /// </returns>

        public Story()
        {
            Corridors = new List<Room>();
            Rooms = new List<Room>();
            Services = new List<Room>();
            Color = Palette.White;
            Elevation = 0.0;
            Height = 1.0;
            Name = "";
            Perimeter = null;
            SlabThickness = 0.1;
        }

        /// <summary>
        /// Area of the perimeter.
        /// </summary>
        public double Area
        {
            get
            {
                if (Perimeter == null)
                {
                    return 0.0;
                }
                return Perimeter.Area;
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
                foreach (Room room in Corridors)
                {
                    area += room.Perimeter.Area;
                }
                foreach (Room room in Rooms)
                {
                    area += room.Perimeter.Area;
                }
                foreach (Room room in Services)
                {
                    area += room.Perimeter.Area;
                }
                return area;
            }
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
                var area = Perimeter.Area;
                foreach (Room room in Rooms)
                {
                    if (room.Perimeter != null)
                    {
                        area -= room.Perimeter.Area;
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
        /// Rendering color of the Story returned as a Space.
        /// </summary>
        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                if (value == null)
                {
                    color = Palette.White;
                }
                else
                {
                    color = value;
                }
            }
        }

        /// <summary>
        /// List of Rooms designated as cooridors.
        /// </summary>
        public List<Room> Corridors { get; }

        /// <summary>
        /// List of Spaces created from Room characteristics within the Corridors list.
        /// </summary>
        public List<Space> CorridorsAsSpaces
        {
            get
            {
                List<Space> spaces = new List<Space>();
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
                    room.Color = color;
                }
            }
        }

        /// <summary>
        /// Elevation of the Story relative to the zero plane. A change in this value will also change the elevations of all Rooms in all Story lists.
        /// </summary>
        private double elevation;
        public double Elevation
        {
            get
            {
                return elevation;
            }
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
        /// Space created from Story characteristics.
        /// </summary>
        public Space Envelope
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                var space = new Space(Perimeter, Height, Elevation, new Material(Guid.NewGuid().ToString(), Color));
                space.AddProperty("Name", new StringProperty(Name, UnitType.Text));
                space.AddProperty("Area", new NumericProperty(Perimeter.Area, UnitType.Area));
                return space;
            }
        }

        /// <summary>
        /// Height of the Story relative to its elevation.
        /// </summary>
        private double height;
        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
                }
                height = value;
            }
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
        /// Returns all Corridors, Rooms, and Services as Spaces.
        /// </summary>
        public IList<Space> InteriorsAsSpaces
        {
            get
            {
                List<Space> spaces = new List<Space>
                {
                    Envelope
                };
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
            set
            {
                if (value != null)
                {
                    perimeter = value;
                }
            }
        }

        /// <summary>
        /// List of Rooms designated as occupiable rooms.
        /// </summary>
        public List<Room> Rooms { get; }

        /// <summary>
        /// List of Spaces created from Room characteristics within the Rooms list.
        /// </summary>
        public List<Space> RoomsAsSpaces
        {
            get
            {
                List<Space> spaces = new List<Space>
                {
                    Envelope
                };
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
        /// Sets the Rooms Space rendering color.
        /// </summary>
        public Color RoomsColor
        {
            set
            {
                foreach (Room room in Rooms)
                {
                    room.Color = color;
                }
            }
        }

        /// <summary>
        /// A list of Rooms designated as building services.
        /// </summary>
        public List<Room> Services { get; }

        /// <summary>
        /// List of Spaces created from Room characteristics within the Services list.
        /// </summary>
        public List<Space> ServicesAsSpaces
        {
            get
            {
                List<Space> spaces = new List<Space>();
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
                    room.Color = color;
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
                return new Floor(Perimeter, new FloorType("slab", SlabThickness), Elevation,BuiltInMaterials.Concrete);
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
            }
        }

        /// <summary>
        /// Creates a rectangular corridor Room from a centerline axis, width, and height, at the Story elevation.
        /// Adds the new Room to the Corrdors list.
        /// Corridors conform to Service perimeters.
        /// Corridors change intersecting Room perimeters to conform to the corridor's perimeter.
        /// </summary>
        /// <param name="axis">Center Line of the corridor.</param>
        /// <param name="width">Width of the corridor.</param>
        /// <param name="height">Height of the corridor.</param>
        /// <param name="name">String identifier.</param>
        /// <param name="color">Rendering color of the Room as a Space.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void AddCorridor(Line axis, 
                                double width = 2.0, 
                                double height = 3.0,
                                string name = "",
                                Color color = null)
        {
            var corridor = new Room()
            {
                Color = color,
                Elevation = Elevation,
                Height = height,
                Name = name
            };
            corridor.MakePerimeter(axis, width);
            Corridors.Add(corridor);
            FitCorridorsToServices();
            FitRoomsToCorridors();
        }

        /// <summary>
        /// Creates a rectangular corridor Room from a centerline axis, width, and height, at the Story elevation.
        /// Adds the new Room to the Corrdors list.
        /// Corridors conform to Service perimeters.
        /// Corridors change intersecting Room perimeters to conform to the corridor's perimeter.
        /// </summary>
        /// <param name="start">First endpoint of the centerline of the corridor.</param>
        /// <param name="end">Second endpoint of the centerline of the corridor.</param>
        /// <param name="width">Width of the corridor.</param>
        /// <param name="height">Height of the corridor.</param>
        /// <param name="name">String identifier.</param>
        /// <param name="color">Rendering color of the Room as a Space.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void AddCorridor(Vector3 start, 
                                Vector3 end, 
                                double width = 2.0, 
                                double height = 3.0,
                                string name = "",
                                Color color = null)
        {
            var corridor = new Room()
            {
                Color = color,
                Elevation = Elevation,
                Height = height,
                Name = name
            };
            corridor.MakePerimeter(start, end, width);
            Corridors.Add(corridor);
            FitCorridorsToServices();
            FitRoomsToCorridors();
        }

        /// <summary>
        /// Creates a corridor Room from a perimeter and height, at the Story elevation.
        /// Adds the new Room to the Corrdors list.
        /// Corridors conform to Service perimeters.
        /// Corridors change intersecting Room perimeters to conform to the corridor's perimeter.
        /// </summary>
        /// <param name="perimeter">Polygon perimeter of the corridor.</param>
        /// <param name="height">Height of the corridor.</param>
        /// <param name="name">String identifier.</param>
        /// <param name="color">Rendering color of the Room as a Space.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void AddCorridor(Polygon perimeter,
                                double height = 3.0,
                                string name = "",
                                Color color = null)
        {
            if (Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            var corridor = new Room()
            {
                Color = color,
                Elevation = elevation,
                Height = height,
                Name = name,
                Perimeter = perimeter
            };
            Corridors.Add(corridor);
            FitCorridorsToServices();
            FitRoomsToCorridors();
        }

        /// <summary>
        /// Creates an occupied Room from a perimeter and height, at the Story elevation.
        /// Adds the new Room to the Rooms list.
        /// Rooms conform to corridor and service perimeters.
        /// </summary>
        /// <param name="perimeter">Polygon perimeter of the corridor.</param>
        /// <param name="height">Height of the corridor.</param>
        /// <param name="name">String identifier.</param>
        /// <param name="color">Rendering color of the Room as a Space.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void AddRoom(Polygon perimeter,
                            double height = 3.0,
                            string name = "",
                            Color color = null)
        {
            if (Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            Rooms.Add(
                new Room()
                {
                    Color = color,
                    Elevation = Elevation,
                    Height = height,
                    Name = name,
                    Perimeter = perimeter,
                });
            FitRoomsToServices();
            FitRoomsToCorridors();
        }

        /// <summary>
        /// Creates a Service from a perimeter at the Story's height and elevation.
        /// Adds the new Room to the Services list.
        /// Corridors and Rooms conform to Service perimeters.
        /// </summary>
        /// <param name="perimeter">Polygon perimeter of the corridor.</param>
        /// <param name="name">String identifier.</param>
        /// <param name="color">Rendering color of the Room as a Space.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void AddService(Polygon perimeter, string name = "", Color color = null)
        {
            var service = new Room()
            {
                Color = color,
                Elevation = Elevation,
                Height = Height,
                Name = name,
                Perimeter = perimeter
            };
            Services.Add(service);
            FitCorridorsToServices();
            FitRoomsToServices();
        }

        /// <summary>
        /// Private function configuring all Corridor perimeters to conform to all Service perimeters.
        /// </summary>
        /// <returns>
        /// None.
        /// </returns>
        private void FitCorridorsToServices()
        {
            foreach (Room service in Services)
            {
                if (service.Perimeter == null)
                {
                    continue;
                }
                int index = 0;
                List<int> indices = new List<int>();
                var addRooms = new List<Room>();
                foreach (Room room in Corridors)
                {
                    var perimeters = room.Perimeter.Difference(service.Perimeter);
                    if (perimeters != null && perimeters.Count > 0)
                    {
                        room.Perimeter = perimeters.First();
                        for (int i = 1; i < perimeters.Count; i++)
                        {
                            var addRoom = new Room()
                            {
                                Color = room.Color,
                                Elevation = room.Elevation,
                                Height = room.Height,
                                Name = room.Name,
                                Perimeter = perimeters[i]
                            };
                            addRooms.Add(addRoom);
                        }
                    }
                    else
                    {
                        indices.Add(index);
                    }
                    index++;
                }
                foreach (int remove in indices)
                {
                    Corridors.RemoveAt(remove);
                }
                if (addRooms.Count > 0)
                {
                    Corridors.AddRange(addRooms);
                }
            }
        }

        /// <summary>
        /// Private function configuring all Room perimeters to conform to all Corridor perimeters.
        /// </summary>
        /// <returns>
        /// None.
        /// </returns>
        private void FitRoomsToCorridors()
        {
            foreach (Room corridor in Corridors)
            {
                if (corridor.Perimeter == null)
                {
                    continue;
                }
                int index = 0;
                List<int> indices = new List<int>();
                var addRooms = new List<Room>();
                foreach (Room room in Rooms)
                {
                    var perimeters = room.Perimeter.Difference(corridor.Perimeter);
                    if (perimeters != null && perimeters.Count > 0)
                    {
                        room.Perimeter = perimeters.First();
                        for (int i = 1; i < perimeters.Count; i++)
                        {
                            var addRoom = new Room()
                            {
                                Color = room.Color,
                                Elevation = room.Elevation,
                                Height = room.Height,
                                Name = room.Name,
                                Perimeter = perimeters[i]
                            };
                            addRooms.Add(addRoom);
                        }
                    }
                    else
                    {
                        indices.Add(index);
                    }
                    index++;
                }
                foreach (int remove in indices)
                {
                    Rooms.RemoveAt(remove);
                }
                if (addRooms.Count > 0)
                {
                    Rooms.AddRange(addRooms);
                }
            }
        }

        /// <summary>
        /// Private function configuring all Room perimeters to conform to all Service perimeters.
        /// </summary>
        /// <returns>
        /// None.
        /// </returns>
        private void FitRoomsToServices()
        {
            foreach (Room service in Services)
            {
                if (service.Perimeter == null)
                {
                    continue;
                }
                int index = 0;
                List<int> indices = new List<int>();
                var addRooms = new List<Room>();
                foreach (Room room in Rooms)
                {
                    var perimeters = room.Perimeter.Difference(service.Perimeter);
                    if (perimeters != null && perimeters.Count > 0)
                    {
                        room.Perimeter = perimeters.First();
                        for (int i = 1; i < perimeters.Count; i++)
                        {
                            var addRoom = new Room()
                            {
                                Color = room.Color,
                                Elevation = room.Elevation,
                                Height = room.Height,
                                Name = room.Name,
                                Perimeter = perimeters[i]
                            };
                            addRooms.Add(addRoom);
                        }
                    }
                    else
                    {
                        indices.Add(index);
                    }
                    index++;
                }
                foreach (int remove in indices)
                {
                    Rooms.RemoveAt(remove);
                }
                if (addRooms.Count > 0)
                {
                    Rooms.AddRange(addRooms);
                }
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
        public void RoomsByDivision(int xRooms = 1,
                                    int yRooms = 1,
                                    double height = 3.0,
                                    double setback = 0.0,
                                    string name = "",
                                    Color color = null)
        {
            if (Perimeter == null)
            {
                throw new ArgumentNullException(Messages.PERIMETER_NULL_EXCEPTION);
            }
            if (setback < 0.0)
            {
                throw new ArgumentNullException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
            }
            Rooms.Clear();
            var polygon = Perimeter.Offset(setback * -1.0).First();
            var roomGroup = new RoomGroup(polygon, xRooms, yRooms, name);
            roomGroup.SetElevation(Elevation);
            roomGroup.SetHeight(height);
            Rooms.AddRange(roomGroup.Rooms);
            RoomsColor = color;
            FitRoomsToServices();
            FitRoomsToCorridors();
        }
    }
}
