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
            if (polygon.IsClockWise())
            {
                polygon = polygon.Reversed();
            }
            Row = polygon.Segments().First();
            Angle = Math.Atan2(Row.End.Y - Row.Start.Y, Row.End.X - Row.Start.X) * (180 / Math.PI);
            perimeterJig = polygon.MoveFromTo(Row.Start, Vector3.Origin).Rotate(Vector3.Origin, Angle * -1);
            insert = Vector3.Origin;
            Name = name;
            Origin = Row.Start;
            Perimeter = polygon;
            Rooms = new List<Room>();
            Tolerance = 0.1;
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Private

        private readonly Polygon perimeterJig;
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
                return Perimeter.Area();
            }
        }

        /// <summary>
        /// Unallocated area within the boundary.
        /// </summary>
        public double AreaAvailable
        {
            get
            {
                return Area - AreaOfRooms;
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
        /// Position indicating the beginning of the row.
        /// </summary>
        public Vector3 Origin { get; private set; }

        /// <summary>
        /// Boundary of the RoomRow.
        /// </summary>
        public Polygon Perimeter { get; private set; }

        /// <summary>
        /// List of Rooms placed along the row.
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
        public bool AddRoom(Room room, bool fit = true)
        {
            if (room == null || room.Area > AreaAvailable)
            {
                return false;
            }
            var ratio = room.DesignRatio;
            if (ratio < 1.0)
            {
                ratio = 1 / ratio;
            }
            Polygon polygon = null;
            if (fit)
            {
                var compass = perimeterJig.Compass();
                var length = room.Area / compass.SizeY;
                var polygons = Polygon.Rectangle(insert, new Vector3(insert.X + length, insert.Y + compass.SizeY)).Intersection(perimeterJig);
                if (polygons != null)
                {
                    polygon = polygons.First();
                }
            }
            if (polygon == null)
            {
                polygon = Shaper.RectangleByRatio(ratio).MoveFromTo(Vector3.Origin, insert)
                            .ExpandtoArea(room.Area, Tolerance, Orient.SW, perimeterJig, RoomsAsPolygons);
            }
            insert = polygon.Compass().SE;
            room.Perimeter = polygon.MoveFromTo(Vector3.Origin, Origin).Rotate(Origin, Angle);
            room.Placed = true;
            Rooms.Add(room);
            return true;
        }

        /// <summary>
        /// Attempts to place a list of Rooms on the Row in list order. Returns a list of Rooms that failed placement.
        /// </summary>
        /// <param name="rooms">List of Rooms to place along the Row.</param>
        /// <returns>List of unplaced Rooms.</returns>
        public List<Room> AddRooms(List<Room> rooms)
        {
            var unplaced = new List<Room>();
            foreach (var room in rooms)
            {
                if(!AddRoom(room))
                {
                    unplaced.Add(room);
                }
            }
            return unplaced;
        }

        public void Infill (double height)
        {
            if (AreaAvailable == 0.0)
            {
                return;
            }
            var ratio = insert.DistanceTo(Row.End) / perimeterJig.Compass().SizeY;
            AddRoom(new Room(AreaAvailable, ratio, height));
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
            foreach (Room room in Rooms)
            {
                room.MoveFromTo(from, to);
            }
            Perimeter = Perimeter.MoveFromTo(from, to);
            Row = Row.MoveFromTo(from, to);
        }

        /// <summary>
        /// Rotates all Rooms, the Perimeter and the Row in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the RoomRowr will be rotated.</param> 
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
            Row = Row.Rotate(pivot, angle);
            Origin = Row.Start;
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
            foreach (Room room in Rooms)
            {
                room.Height = height;
            }
        }
    } 
    #endregion
}
