using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages the geometry of a floor with openings and Rooms representing excluded areas, corridors, occupied rooms, and services.
    /// </summary>
    public class Suite
    {
        #region Constructors

        public enum SuiteLayout { Axis, Reciprocal }

        /// <summary>
        /// Default constructor creates an empty Suite.
        /// </summary>
        /// <returns>
        /// A new Suite.
        /// </returns>
        public Suite(string name = "", 
                     string number = "", 
                     List<Room> rooms = null, 
                     double ratio = 0.5, 
                     SuiteLayout suiteLayout = SuiteLayout.Reciprocal)
        {
            Rooms = rooms == null ? new List<Room>() : new List<Room>(rooms);
            Name = name ?? "";
            Number = number ?? "";
            Ratio = ratio;
            SuitePlanType = suiteLayout;
            UniqueID = Guid.NewGuid().ToString();
            if (suiteLayout == SuiteLayout.Reciprocal)
            {
                PlaceReciprocal();
            }
            else
            {
                PlaceByAxis();
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suite"></param>
        /// <returns></returns>
        private void PlaceByAxis()
        {
            if (Rooms.Count == 0)
            {
                return;
            }
            var area = 0.0;
            foreach (var room in Rooms)
            {
                area += room.Area;
            }
            var roomRows = new List<RoomRow>();
            while (RoomsPlaced.Count < Rooms.Count)
            {
                roomRows.Clear();
                Perimeter = Shaper.RectangleByArea(area * 0.5, Ratio);
                var roomRow = new RoomRow(Perimeter);
                roomRows.Add(roomRow);
                var i = 0;
                while (i < Rooms.Count)
                {
                    if (!roomRow.AddRoomFitted(Rooms[i], false))
                    {
                        break;
                    }
                    i++;
                }
                if (i == Rooms.Count)
                {
                    return;
                }
                var compass = Perimeter.Compass();
                var row = new Line(compass.SE, compass.SW);
                var matchRow = new RoomRow(row, area / row.Length());
                roomRows.Add(matchRow);
                while (i < Rooms.Count)
                {
                    if (!matchRow.AddRoomFitted(Rooms[i], false))
                    {
                        break;
                    }
                    i++;
                }
                Ratio += 0.1;
            }
            Rooms.Clear();
            foreach (var roomRow in roomRows)
            {
                foreach (var room in roomRow.Rooms)
                {
                    Rooms.Add(room);
                }
            }
            Perimeter = Footprint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suite"></param>
        /// <returns></returns>
        private void PlaceReciprocal()
        {
            if (Rooms.Count == 0)
            {
                return;
            }
            var area = 0.0;
            foreach (var room in Rooms)
            {
                area += room.Area;
            }
            var roomRows = new List<RoomRow>();
            while (RoomsPlaced.Count < Rooms.Count)
            {
                roomRows.Clear();
                Perimeter = Shaper.RectangleByArea(area, Ratio);
                var roomRow = new RoomRow(Perimeter);
                roomRows.Add(roomRow);
                var i = 0;
                while (i < Rooms.Count)
                {
                    if (!roomRow.AddRoomFitted(Rooms[i], false))
                    {
                        break;
                    }
                    i++;
                }
                if (i == Rooms.Count)
                {
                    return;
                }
                var perimeter = roomRow.Perimeter.Difference(roomRow.Footprint);
                if (perimeter.Count == 0)
                {
                    break;
                }
                var matchRow = new RoomRow(perimeter.First());
                roomRows.Add(matchRow);
                while (i < Rooms.Count)
                {
                    if (!matchRow.AddRoomFitted(Rooms[i], true))
                    {
                        break;
                    }
                    i++;
                }
                Ratio += 0.1;
            }
            var rooms = roomRows.Last().RoomsAsPolygons;
            var diffs = roomRows.Last().Perimeter.Difference(rooms);
            if (diffs != null)
            {
                foreach (var diff in diffs)
                {
                    foreach (var room in roomRows.Last().Rooms)
                    {
                        var merge = Shaper.Merge(new[] { room.Perimeter, diff }.ToList());
                        if (merge.Count == 1)
                        {
                            room.Perimeter = merge.First();
                            break;
                        }
                    }
                }
            }
            Rooms.Clear();
            foreach (var roomRow in roomRows)
            {
                foreach (var room in roomRow.Rooms)
                {
                    Rooms.Add(room);
                }
            }
            Perimeter = Footprint;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Aggregate area of all Room Perimeters.
        /// </summary>
        public double AreaOfRooms
        {
            get 
            {
                var area = 0.0;
                foreach (var room in Rooms)
                {
                    area += room.Area;
                }
                return area;
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
                if (polygons.Count == 0)
                {
                    return null;
                }
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
        /// Arbitrary identifier.
        /// </summary>
        private string name;
        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                name = value;
                foreach (Room room in Rooms)
                {
                    room.Suite = value;
                }
            }
        }

        /// <summary>
        /// Arbitrary identifier.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Polygon perimeter of the Suite.
        /// </summary>
        public Polygon Perimeter { get; private set; }

        /// <summary>
        /// Ratio of the Perimeter sides.
        /// </summary>
        public double Ratio { get; private set;  }

        /// <summary>
        /// List of Rooms designated as occupiable rooms.
        /// </summary>
        public List<Room> Rooms { get; }

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
        /// Placed Rooms.
        /// </summary>
        public List<Room> RoomsPlaced
        {
            get
            {
                var rooms = new List<Room>();
                foreach (var room in Rooms)
                {
                    if (room.Placed)
                    {
                        rooms.Add(room);
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// Unplaced Rooms.
        /// </summary>
        public List<Room> RoomsUnplaced
        {
            get
            {
                var rooms = new List<Room>();
                foreach (var room in Rooms)
                {
                    if (!room.Placed)
                    {
                        rooms.Add(room);
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// Returns the layour style of the Suite;
        /// </summary>
        public SuiteLayout SuitePlanType { get; private set;  }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a Room to the Rooms list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <param name="fit">Indicates whether the new Room should mutually fit to other Story features. Default is true.</param>
        /// <returns>
        /// True if one or more Rooms were added to the Story.
        /// </returns>
        public void AddRoom(Room room, int position = int.MaxValue)
        {
            room.Suite = Name;
            room.SuiteID = UniqueID;
            if (position == int.MaxValue)
            {
                Rooms.Add(room);
                return;
            }
            if (position <= 0)
            {
                Rooms.Insert(0, room);
                return;
            }
            try
            {
                Rooms.Insert(position, room);
            }
            catch
            {
                Rooms.Add(room);
            }
            foreach (var entry in Rooms)
            {
                entry.Placed = false;
            }
            if (SuitePlanType == SuiteLayout.Reciprocal)
            {
                PlaceReciprocal();
            }
            else
            {
                PlaceByAxis();
            }
        }

        /// <summary>
        /// Returns the aggregate area of all Rooms with a supplied name.
        /// </summary>
        /// <param name="name">Name of the Rooms to retrieve.</param>
        /// <returns>
        /// Aggregate area of all Rooms with the specified name.
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
        /// Moves all Rooms, the Boundary and the Row along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            foreach (var room in Rooms)
            {
                room.MoveFromTo(from, to);
            }
            Perimeter = Perimeter.MoveFromTo(from, to);
        }

        /// Returns a Rooms with a specific UniqueID.
        /// </summary>
        /// <param name="name">Name of the rooms to retrieve.</param>
        /// <returns>
        /// A Room.
        /// </returns>/// 
        public Room RoomByID(string uniqueID)
        {
            return Rooms.Find(r => r.UniqueID == uniqueID);
        }

        /// <summary>
        /// Returns a list of Rooms with an area within a tolerance.
        /// </summary>
        /// <param name="area">Area of the rooms to retrieve.</param>
        /// <returns>
        /// A List of Rooms.
        /// </returns>
        public List<Room> RoomsByArea (double area, double tolerance = 0.01)
        {
            var rooms = new List<Room>();
            foreach (var room in Rooms)
            {
                if (Math.Abs(room.Area - area) <= tolerance)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }

        /// <summary>
        /// Returns a list of Rooms with a specific name.
        /// </summary>
        /// <param name="name">Name of the rooms to retrieve.</param>
        /// <returns>
        /// A List of Rooms.
        /// </returns>
        public List<Room> RoomsByName(string name)
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
        /// Rotates all Rooms and the Perimeter in the horizontal plane around the supplied pivot point.
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
            Perimeter = Perimeter.Rotate(pivot, angle);
        }

        /// <summary>
        /// Uniformly sets the color of all Rooms.
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
        /// Uniformly sets the height of all Rooms.
        /// </summary>
        /// <param name="elevation">New height of the Rooms.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void SetHeight(double height)
        {
            foreach (Room room in Rooms)
            {
                room.Height = height;
            }
        }

        #endregion
    }
}
