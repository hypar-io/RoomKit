using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages Rooms placed along a line.
    /// </summary>
    public class RoomRow
    {
        #region Contructors
        /// <summary>
        /// Constructor initializes the RoomRow with a new Line.
        /// </summary>
        public RoomRow(Line row)
        {
            CirculationWidth = 0.0;
            Depth = 0.0;
            Name = "";
            Rooms = new List<Room>();
            Row = new Line(row.Start, row.End);
            UniqueID = Guid.NewGuid().ToString();
            angle = Math.Atan2(Row.End.Y - Row.Start.Y, Row.End.X - Row.Start.X) * (180 / Math.PI);
            mark = new Vector3(Row.Start.X, Row.Start.Y);
        }

        /// <summary>
        /// Constructor initializes the RoomRow with line endpoints.
        /// </summary>
        public RoomRow(Vector3 start, Vector3 end)
        {
            CirculationWidth = 0.0;
            Depth = 0.0;
            Name = "";
            Rooms = new List<Room>();
            Row = new Line(start, end);
            UniqueID = Guid.NewGuid().ToString();
            angle = Math.Atan2(Row.End.Y - Row.Start.Y, Row.End.X - Row.Start.X) * (180 / Math.PI);
            mark = new Vector3(Row.Start.X, Row.Start.Y);
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Calculated angle of the Row in degrees.
        /// </summary>
        private readonly double angle = 0.0;

        /// <summary>
        /// Position indicating the limit of placed Rooms along the Row.
        /// </summary>
        private Vector3 mark = null;

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
                    area += room.Perimeter.Area;
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
                return Math.Sqrt(Math.Pow(Row.End.X - mark.X, 2) + Math.Pow(Row.End.Y - mark.Y, 2));
            }
        }

        /// <summary>
        /// Circulation envelope around the row.
        /// </summary>
        public Polygon Circulation
        {
            get
            {
                var lineS = new Line(Vector3.Origin, new Vector3(Depth + CirculationWidth, 0.0));
                lineS = lineS.Rotate(Vector3.Origin, angle + 90).MoveFromTo(Vector3.Origin, Row.Start);
                var lineE = lineS.MoveFromTo(Row.Start, mark);
                var sPnt = lineS.End;
                var ePnt = lineE.End;
                return new Polygon(new[] { Row.Start, mark, ePnt, sPnt });
            }
            private set => Circulation = value;
        }

        /// <summary>
        /// Additional depth added to the deepest Room in the RoomRow allowing for circulation adjacent to the Rooms.
        /// </summary>
        private double circulationWidth;
        public double CirculationWidth
        {
            get
            {
                return circulationWidth;
            }
            set
            {
                if (value >= 0.0)
                {
                    circulationWidth = value;
                }
            }
        }

        /// <summary>
        /// Depth of the deepest room along the Row.
        /// </summary>
        public double Depth { get; private set; } = 0.0;

        /// <summary>
        /// Arbitrary string identifier for this RoomRow.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of Rooms placed along the Row.
        /// </summary>
        public IList<Room> Rooms { get; }

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
        /// List of all Rooms as Spaces.
        /// </summary>
        public List<Space> RoomsAsSpaces
        {
            get
            {
                var spaces = new List<Space>();
                foreach (Room room in Rooms)
                {
                    spaces.Add(room.AsSpace);
                }
                return spaces;
            }
        }

        /// <summary>
        /// Line along which Rooms can be placed.
        /// </summary>
        public Line Row { get; }

        /// <summary>
        /// X dimension of the Circulation orthogonal bounding box.
        /// </summary>
        public double SizeX
        {
            get
            {
                if (Circulation == null)
                {
                    return 0.0;
                }
                return new TopoBox(Circulation).SizeX;
            }
        }

        /// <summary>
        /// Y dimension of the Circulation orthogonal bounding box.
        /// </summary>
        public double SizeY
        {
            get
            {
                if (Circulation == null)
                {
                    return 0.0;
                }
                return new TopoBox(Circulation).SizeY;
            }
        }

        /// <summary>
        /// UUID for this RoomRow instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Methods
        /// <summary>
        /// Attempts to place a Room perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the Room's perimeter must fit and a list of Polygons with which it cannot intersect.
        /// </summary>
        /// <param name="room">Room from which to derive the Polygon to place.</param>
        /// <param name="within">Polygon perimeter within which a new Room must fit.</param>
        /// <param name="among">List of Polygon perimeters the new Room cannot intersect.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        public bool AddRoom(Room room, Polygon within = null, IList<Polygon> among = null)
        {
            if (room == null)
            {
                return false;
            }
            if (room.Perimeter == null)
            {
                room.SetPerimeter();
            }
            var polygon = room.Perimeter;
            var box = new TopoBox(polygon);
            var delta = 0.0;
            var newDepth = 0.0;
            var rotation = angle;
            if (box.SizeX <= AvailableLength)
            {
                delta = box.SizeX;
                newDepth = box.SizeY;
            }
            else if (box.SizeY <= AvailableLength)
            {
                polygon = polygon.MoveFromTo(box.NW, box.SW);
                delta = box.SizeY;
                newDepth = box.SizeX;
                rotation += 90;
            }
            else
            {
                return false;
            }
            var circDepth = newDepth + CirculationWidth;
            var chkCirc =
                new Polygon(
                    new[]
                    {
                        Vector3.Origin,
                        new Vector3(delta, 0.0),
                        new Vector3(delta, circDepth),
                        new Vector3(0.0, circDepth)
                    });
            var t = new Transform();
            t.Rotate(Vector3.ZAxis, rotation);
            chkCirc = t.OfPolygon(chkCirc).MoveFromTo(Vector3.Origin, mark);
            if (!chkCirc.Fits(within, among))
            {
                if (Rooms.Count == 0)
                {
                    mark = Row.PointAt(Math.Abs(mark.DistanceTo(Row.Start) + delta) / Row.Length());
                }
                return false;
            }
            polygon = t.OfPolygon(polygon).MoveFromTo(Vector3.Origin, mark);
            if (!polygon.Fits(within, among))
            {
                if (Rooms.Count == 0)
                {
                    mark = Row.PointAt(Math.Abs(mark.DistanceTo(Row.Start) + delta) / Row.Length());
                }
                return false;
            }
            mark = Row.PointAt(Math.Abs(mark.DistanceTo(Row.Start) + delta) / Row.Length());
            if (newDepth > Depth)
            {
                Depth = newDepth;
            }
            room.Perimeter = polygon;
            Rooms.Add(room);
            return true;
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
        /// Uniformly sets the elevation of all Rooms in the RoomRow.
        /// </summary>
        /// <param name="elevation">New elevation of the Rooms.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void SetElevation(double elevation)
        {
            foreach (Room room in Rooms)
            {
                room.Elevation = elevation;
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
