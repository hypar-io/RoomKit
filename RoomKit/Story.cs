using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages Corridors, Rooms, and Services.
    /// </summary>
    public class Story
    {

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
        /// The area of the perimeter.
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
        /// The area allocated to Corridors, Rooms, and Services.
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
        /// The unallocated area.
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

        public List<Room> Corridors { get; }

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
        /// A Space created from Story characteristics.
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

        public string Name { get; set; }

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

        public List<Room> Rooms { get; }

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
        /// Sets the Rooms color.
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

        public List<Room> Services { get; }

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
        /// Sets the Services color.
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

        public Floor Slab
        {
            get
            {
                return new Floor(Perimeter, new FloorType("slab", SlabThickness), Elevation,BuiltInMaterials.Concrete);
            }
        }

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



        public void AddCorridor(Line axis, 
                                double width = 2.0, 
                                double height = 3.0, 
                                Color color = null)
        {
            var corridor = new Room()
            {
                Color = color,
                Elevation = Elevation,
                Height = height
            };
            corridor.MakePerimeter(axis, width);
            Corridors.Add(corridor);
            FitCorridorsToServices();
            FitRoomsToCorridors();
        }

        public void AddCorridor(Vector3 start, 
                                Vector3 end, 
                                double width = 2.0, 
                                double height = 3.0, 
                                Color color = null)
        {
            var corridor = new Room()
            {
                Color = color,
                Elevation = elevation,
                Height = height
            };
            corridor.MakePerimeter(start, end, width);
            Corridors.Add(corridor);
            FitCorridorsToServices();
            FitRoomsToCorridors();
        }

        public void AddCorridor(Polygon perimeter,
                                double height = 3.0,
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
                Height = height
            };
            corridor.Perimeter = perimeter;
            Corridors.Add(corridor);
            FitCorridorsToServices();
            FitRoomsToCorridors();
        }

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

        public void AddService(Polygon perimeter, Color color = null)
        {
            var service = new Room()
            {
                Color = color,
                Elevation = Elevation,
                Height = Height,
                Perimeter = perimeter
            };
            Services.Add(service);
            FitCorridorsToServices();
            FitRoomsToServices();
        }

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

        public void RoomsByDivision(int xRooms = 1,
                                    int yRooms = 1,
                                    double height = 3.0,
                                    double setback = 0.0,
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
            var roomGroup = new RoomGroup(polygon, "", xRooms, yRooms);
            roomGroup.SetElevation(Elevation);
            roomGroup.SetHeight(height);
            Rooms.AddRange(roomGroup.Rooms);
            RoomsColor = color;
            FitRoomsToServices();
            FitRoomsToCorridors();
        }
    }
}
