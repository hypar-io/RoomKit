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
            var row = lines[0];
            Placed = new List<Polygon>();
            Rooms = new List<Room>();
            Angle = Math.Atan2(row.End.Y - row.Start.Y, row.End.X - row.Start.X) * (180 / Math.PI);
            Boundary = polygon.MoveFromTo(row.Start, Vector3.Origin).Rotate(Vector3.Origin, Angle * -1);
            Elevation = 0.0;
            Limit = Boundary.Segments()[2];
            Name = "";
            Origin = row.Start;
            Row = Boundary.Segments()[0];
            Mark = new Vector3(Row.Start.X, Row.Start.Y);
            UniqueID = Guid.NewGuid().ToString();
            Tolerance = 0.1;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Calculated angle of the Row in degrees.
        /// </summary>
        public double Angle { get; private set; }

        /// <summary>
        /// Aggregate area of the Rooms placed on this Row.
        /// </summary>
        public double AreaPlaced
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
                if (Row == null)
                {
                    return 0.0;
                }
                return Mark.DistanceTo(Row.End);
            }
        }

        /// <summary>
        /// Polygon within which to fit all Room perimeters.
        /// </summary>
        public Polygon Boundary { get; private set; }

        /// <summary>
        /// Elevation of all Rooms in the RoomRow.
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
                foreach (Room room in Rooms)
                {
                    room.Elevation = elevation;
                }
            }
        }

        /// <summary>
        /// Line along which Rooms can be placed.
        /// </summary>
        public Line Limit { get; private set; }

        /// <summary>
        /// Position indicating the limit of placed Rooms along the Row.
        /// </summary>
        public Vector3 Mark { get; private set; }

        /// <summary>
        /// Arbitrary string identifier for this RoomRow.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Position indicating the beginning of the Row.
        /// </summary>
        public Vector3 Origin { get; private set; }

        /// <summary>
        /// List of all placed Polygons.
        /// </summary>
        public List<Polygon> Placed { get; }

        /// <summary>
        /// List of Rooms placed along the Row.
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
        /// Line along which Rooms can be placed.
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
        /// Attempts to place a Room perimeter on the next open segment of the Row.
        /// </summary>
        /// <param name="room">Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        public bool AddRoom(Room room, bool atRatio = true)
        {
            if (room == null || room.Area > (Boundary.Area() - AreaPlaced))
            {
                return false;
            }
            var ratio = room.DesignRatio;
            if (ratio < 1.0)
            {
                ratio = 1 / ratio;
            }
            Polygon polygon;
            if (atRatio)
            {
                polygon = Shaper.RectangleByRatio(ratio).MoveFromTo(Vector3.Origin, Mark);
            }
            else
            {
                polygon = Polygon.Rectangle(Mark, new Vector3(Mark.X + 0.1, Mark.Y + Boundary.Compass().SizeY));
            }
            polygon = polygon.ExpandtoArea(room.Area, Tolerance, Orient.SW, Boundary, Placed);
            Mark = polygon.Compass().SE;
            Placed.Add(polygon);
            room.Elevation = Elevation;
            room.Perimeter = polygon.MoveFromTo(Vector3.Origin, Origin).Rotate(Origin, Angle);
            room.Placed = true;
            Rooms.Add(room);
            return room.Placed;
        }

        /// <summary>
        /// Moves all Rooms in the RoomRow and the RoomRow Row along a 3D vector calculated between the supplied Vector3 points.
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
            Row = Row.MoveFromTo(from, to);
            Elevation = to.Z - from.Z;
        }

        /// <summary>
        /// Rotates the RoomRow Row and Rooms in the horizontal plane around the supplied pivot point.
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
            Row = Row ?? Row.Rotate(pivot, angle);
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
