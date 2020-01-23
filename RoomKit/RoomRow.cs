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
        public RoomRow(Polygon polygon)
        {
            if (polygon.IsClockWise())
            {
                polygon = polygon.Reversed();
            }
            var lines = polygon.Segments();
            if (lines.Count() != 4)
            {
                throw new ArgumentOutOfRangeException(Messages.NON_QUADRILATERAL_EXCEPTION);
            }
            
            var ang = lines[0];
            
            Angle = Math.Atan2(ang.End.Y - ang.Start.Y, ang.End.X - ang.Start.X) * (180 / Math.PI);
            boundary = polygon.MoveFromTo(ang.Start, Vector3.Origin).Rotate(Vector3.Origin, Angle * -1);
            Mark = Vector3.Origin;
            Name = "";
            Origin = ang.Start;
            Rooms = new List<Room>();
            row = boundary.Segments()[0];
            Tolerance = 0.1;
            UniqueID = Guid.NewGuid().ToString();
        }

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
                return boundary.Area();
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
        /// Unallocated length of the RoomRow.
        /// </summary>
        public double AvailableLength
        {
            get
            {
                return Mark.DistanceTo(Row.End);
            }
        }

        /// <summary>
        /// Boundary of the RoomRow.
        /// </summary>
        private Polygon boundary;
        public Polygon Boundary
        {
            get
            {
                return boundary.Rotate(Vector3.Origin, Angle).MoveFromTo(Vector3.Origin, Origin);
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
        /// Position indicating the limit of placed Rooms along the row.
        /// </summary>
        public Vector3 Mark { get; private set; }

        /// <summary>
        /// Arbitrary string identifier for this RoomRow.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Position indicating the beginning of the row.
        /// </summary>
        public Vector3 Origin { get; private set; }

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
        private Line row;
        public Line Row
        {
            get
            {
                return row.Rotate(Vector3.Origin, Angle).MoveFromTo(Vector3.Origin, Origin);
            }
        }

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
        public bool AddRoom(Room room)
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
            var polygon = Shaper.RectangleByRatio(ratio).MoveFromTo(Vector3.Origin, Mark)
                .ExpandtoArea(room.Area, Tolerance, Orient.SW, boundary, RoomsAsPolygons);
            Mark = polygon.Compass().SE;
            room.Perimeter = polygon.MoveFromTo(Vector3.Origin, Origin).Rotate(Origin, Angle);
            room.Placed = true;
            Rooms.Add(room);
            return true;
        }

        /// <summary>
        /// Moves all Rooms in the RoomRow and the RoomRow row along a 3D vector calculated between the supplied Vector3 points.
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
            Origin = Origin.MoveFromTo(from, to);
        }

        /// <summary>
        /// Rotates the RoomRow row and Rooms in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
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
            Angle += angle;
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
