using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages Rooms placed along one segment of a containing perimeter.
    /// </summary>
    public class RoomRow
    {
        #region Contructors
        /// <summary>
        /// Constructor initializes the RoomRow with a new Line derived from the first segment of the provided quadrilateral polygon.
        /// </summary>
        public RoomRow(Polygon polygon, string name = "")
        {
            if (polygon == null) return;
            polygon = polygon.IsClockWise() ? polygon.Reversed() : polygon;
            var ang = polygon.Segments().OrderByDescending(s => s.Length()).ToList().First();
            Angle = Math.Atan2(ang.End.Y - ang.Start.Y, ang.End.X - ang.Start.X) * (180 / Math.PI);
            perimeterJig = polygon.Rotate(Vector3.Origin, Angle * -1);
            compass = perimeterJig.Compass();
            insert = compass.SW;
            Name = name;
            Perimeter = polygon;
            Rooms = new List<Room>();
            Tolerance = 0.1;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor initializes the RoomRow with a supplied Line and a depth.
        /// </summary>
        public RoomRow(Line row, double width, string name = "")
        {
            if (row == null) return;
            width = width <= 0.0 ? 1.0 : width;
            var angRads = Math.Atan2(row.End.Y - row.Start.Y, row.End.X - row.Start.X);
            Angle = Math.Atan2(row.End.Y - row.Start.Y, row.End.X - row.Start.X) * (180 / Math.PI);
            var direction = angRads + Math.PI * 0.5;
            var start = new Vector3(row.End.X + (width * Math.Cos(direction)),
                                    row.End.Y + (width * Math.Sin(direction)));
            var end = new Vector3(row.Start.X + (width * Math.Cos(direction)),
                                  row.Start.Y + (width * Math.Sin(direction)));
            var vertices = new[] { row.Start, row.End, start, end };
            var polygon = new Polygon(vertices);
            perimeterJig = polygon.Rotate(Vector3.Origin, Angle * -1);
            compass = perimeterJig.Compass();
            insert = perimeterJig.Compass().SW;
            Name = name;
            Perimeter = polygon;
            Rooms = new List<Room>();
            Tolerance = 0.1;
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Private

        private readonly Polygon perimeterJig;
        private readonly CompassBox compass;
        private Vector3 insert;

        #endregion

        #region Properties
        /// <summary>
        /// Calculated angle of the row in degrees.
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        /// boundary area.
        /// </summary>
        public double Area
        {
            get
            {
                return Math.Round(Perimeter.Area(), Room.PRECISION);
            }
        }

        /// <summary>
        /// Unallocated area within the boundary.
        /// </summary>
        public double AreaAvailable
        {
            get
            {
                if (Area - AreaOfRooms <= 0.0)
                {
                    return 0.0;
                }
                return Math.Round(Area - AreaOfRooms, Room.PRECISION);
            }
        }

        /// <summary>
        /// Unallocated areas within the Perimeter.
        /// </summary>
        public List<Polygon> AreasAvailable
        {
            get
            {
                return Shaper.Differences(Perimeter.ToList(), Footprint.ToList()).ToList();
            }
        }

        /// <summary>
        /// Aggregate area of the Rooms placed on this row.
        /// </summary>
        public double AreaOfRooms
        {
            get
            {
                var area = 0.0;
                foreach (Room room in Rooms)
                {
                    area += room.Area;
                }
                return Math.Round(area, Room.PRECISION);
            }
        }

        /// <summary>
        /// Returns a single Polygon representing the merged perimeters of all Rooms. 
        /// If more than one polygon emerges from the merging process a Polygon representing the convex hull is returned instead.
        /// </summary>
        public Polygon Footprint
        {
            get
            {
                if (Rooms.Count() == 0)
                {
                    return null;
                }
                var polygons = Shaper.Merge(RoomsAsPolygons);
                if (polygons.Count() == 1)
                {
                    return polygons.First();
                }
                var points = new List<Vector3>();
                foreach (var polygon in polygons)
                {
                    points.AddRange(polygon.Vertices);
                }
                return new Polygon(ConvexHull.MakeHull(points));
            }
        }

        /// <summary>
        /// Unallocated length of the RoomRow.
        /// </summary>
        public double LengthAvailable
        {
            get
            {
                return insert.DistanceTo(perimeterJig.Compass().SE);
            }
        }

        /// <summary>
        /// Arbitrary string identifier for this RoomRow.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Boundary of the RoomRow.
        /// </summary>
        public Polygon Perimeter { get; private set; }

        /// <summary>
        /// List of Rooms placed along the row.
        /// </summary>
        public List<Room> Rooms { get; private set;  }

        /// <summary>
        /// List of all Room perimeters as Polygons.
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
        /// List of all Room perimeters as Profiles.
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
        /// Line along which Rooms will be placed.
        /// </summary>
        public Line Row { get; private set; }

        /// <summary>
        /// Tolerated room area variance.
        /// </summary>
        public double Tolerance { get; set; }

        /// <summary>
        /// UUID for this RoomRow instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Attempts to place a Room perimeter on the next open segment of the row.
        /// </summary>
        /// <param name="room">Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        public bool AddRoom(Room room, bool within = true)
        {
            if (room == null ||
                insert.DistanceTo(compass.SE) < 1.0 ||
                compass.SW.DistanceTo(insert) >= compass.SW.DistanceTo(compass.SE))
            {
                return false;
            }
            var ratio = room.DesignRatio > 1.0 ? 1 / room.DesignRatio : room.DesignRatio;
            var length = Math.Sqrt(room.Area * ratio);
            Polygon polygon;
            if (within)
            {
                var polygons = Shaper.Intersections(
                    Polygon.Rectangle(insert, new Vector3(insert.X + length, insert.Y + compass.SizeY)).ToList(),
                    perimeterJig.ToList());
                if (polygons.Count == 0)
                {
                    return false;
                }
                polygon = polygons.First();
            }
            else
            {
                polygon = Polygon.Rectangle(insert, new Vector3(insert.X + length, insert.Y + compass.SizeY));
            }
            insert = polygon.Compass().SE;        
            room.Perimeter = polygon.Rotate(Vector3.Origin, Angle);
            room.Placed = true;
            Rooms.Add(room);
            return true;
        }

        /// <summary>
        /// Attempts to place a list of Rooms on the Row in list order. Returns a list of Rooms that failed placement.
        /// </summary>
        /// <param name="rooms">List of Rooms to place along the Row.</param>
        /// <returns>List of unplaced Rooms.</returns>
        public List<Room> AddRooms(List<Room> rooms, bool fitted = true)
        {
            var unplaced = new List<Room>();
            foreach (var room in rooms)
            {
                if (fitted)
                {
                    if (!AddRoomFitted(room))
                    {
                        unplaced.Add(room);
                    }
                }
                else
                {
                    if (!AddRoom(room))
                    {
                        unplaced.Add(room);
                    }
                }

            }
            return unplaced;
        }

        /// <summary>
        /// Creates and attempts to add a Room to the RoomRow from the supplied characteristics.
        /// </summary>
        /// <param name="area">Room area.</param>
        /// <param name="height">Room height.</param>
        /// <param name="elevation">Room elevation.</param>
        /// <returns>
        /// True if the Room was successfully placed.
        /// </returns>
        public bool AddRoomByArea(double area, double height, double elevation)
        {
            if (area <= 0.0 || AreaAvailable < area || height <= 0.0) return false;
            var ratio = area / (compass.SizeY * compass.SizeY);
            var room = new Room(area, ratio, height)
            {
                Elevation = elevation,
                DesignArea = area,
                DesignRatio = ratio,
            };
            return AddRoom(room, true);
        }

        /// <summary>
        /// Attempts to place a Room perimeter on the next open segment of the row.
        /// </summary>
        /// <param name="room">Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// True if the Room was successfully placed.
        /// </returns>
        public bool AddRoomFitted(Room room, bool within = true)
        {
            if (room == null ||
                insert.DistanceTo(compass.SE) < 1.0 ||
                compass.SW.DistanceTo(insert) >= compass.SW.DistanceTo(compass.SE))
            {
                return false;
            }
            var ratio = room.DesignRatio;
            if (ratio > 1.0)
            {
                ratio = 1 / ratio;
            }
            Polygon boundary = null;
            if (within)
            {
                boundary = new Polygon(perimeterJig.Vertices);
            }
            var polygon = Shaper.RectangleByRatio(ratio).MoveFromTo(Vector3.Origin, insert)
                            .ExpandToArea(room.DesignArea, ratio, Tolerance, Orient.SW, boundary, RoomsAsPolygons);
            insert = polygon.Compass().SE;
            room.Perimeter = polygon.Rotate(Vector3.Origin, Angle);
            room.Placed = true;
            Rooms.Add(room);
            return true;
        }

        /// <summary>
        /// Creates or expands a final Room in the remaining RoomRow area.
        /// </summary>
        /// <param name="height">Desired height of the Room if a new Room must be created.</param>
        /// <param name="tolerance">Minimum area of a infill Room. Smaller rooms will be joined to the last Room.</param>
        public void Infill(double height, bool join = false, double tolerance = 1.0)
        {
            if (AreaAvailable <= tolerance) return;
            height = height <= 0.0 ? 1.0 : height;
            var polygons = Shaper.Intersections(Polygon.Rectangle(insert, compass.NE).ToList(), perimeterJig.ToList());
            if (polygons.Count == 0) return;
            if (Rooms.Count > 0 && (join || polygons.First().Area() < tolerance))
            {
                Rooms.Last().Perimeter = polygons.First().Rotate(Vector3.Origin, Angle).Union(Rooms.Last().Perimeter);
                return;
            }
            if (Rooms.Count > 0)
            {
                Rooms.Add(new Room(polygons.First().Rotate(Vector3.Origin, Angle), height));
                return;
            }
            Rooms.Add(new Room(perimeterJig.Rotate(Vector3.Origin, Angle), height));
            insert = compass.SE;
        }

        /// <summary>
        /// Moves all Rooms along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            foreach (Room room in Rooms)
            {
                room.MoveFromTo(from, to);
            }
            Perimeter = Perimeter.MoveFromTo(from, to);
        }

        /// <summary>
        /// Create Rooms of the specified area along the Row.
        /// </summary>
        /// <param name="area">Desired area of each Room.</param>
        /// <param name="height">Desired height of the Rooms.</param>
        public void Populate (double area, double height)
        {
            area = Math.Abs(area);
            height = Math.Abs(height);
            if (area.NearEqual(0.0) || height.NearEqual(0.0)) return;
            var room = new Room(area, 1.0, height);
            while (AddRoom(room))
            {
                room = new Room(area, 1.0, height);
            }
            Infill(height, true);
        }

        /// <summary>
        /// Rotates all Rooms, the Perimeter and the Row in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the RoomRow will be rotated.</param> 
        /// <param name="angle">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void Rotate(Vector3 pivot, double angle)
        {
            foreach (Room room in Rooms)
            {
                room.Rotate(pivot, angle);
            }
            Angle = angle;
            Perimeter = Perimeter.Rotate(pivot, angle);
        }

        /// <summary>
        /// Uniformly sets the color of all Rooms in the RoomRow.
        /// </summary>
        /// <param name="color">New color of the Rooms.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void SetColor(Color color)
        {
            foreach (Room room in Rooms)
            {
                room.Color = color;
            }
        }

        /// <summary>
        /// Uniformly sets the height of all Rooms in the RoomRow.
        /// </summary>
        /// <param name="elevation">New height of the Rooms.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void SetHeight(double height)
        {
            height = height <= 0.0 ? 1.0 : height;
            foreach (Room room in Rooms)
            {
                room.Height = height;
            }
        }
    } 
    #endregion
}
