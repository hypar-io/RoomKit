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
        /// <summary>
        /// The calculated angle of the Row in degrees.
        /// </summary>
        private readonly double angle = 0.0;

        /// <summary>
        /// The circulation envelope around the row.
        /// </summary>
        private Polygon circulation = null;

        /// <summary>
        /// A position indicating the limit of placed Rooms along the Row.
        /// </summary>
        private Vector3 mark = null;

        /// <summary>
        /// The circulation envelope around the row.
        /// </summary>
        public Polygon Circulation
        {
            get
            {
                return circulation;
            }
        }

        /// <summary>
        /// The depth of the deepest room along the Row.
        /// </summary>
        public double Depth { get; private set; } = 0.0;

        /// <summary>
        /// Arbitrary string identifier for this RoomRow.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The list of Rooms placed along the Row.
        /// </summary>
        public IList<Room> Rooms { get; }

        /// <summary>
        /// The Line along which Rooms can be placed.
        /// </summary>
        public Line Row { get; }

        /// <summary>
        /// Constructor initializes the RoomRow with default values.
        /// </summary>
        public RoomRow()
        {
            Name = "";
            Depth = 0.0;
            Rooms = new List<Room>();
            Row = null;
        }

        /// <summary>
        /// Constructor initializes the RoomRow with a new Line and an optional name.
        /// </summary>
        public RoomRow(Line row, string name = "")
        {
            Row = new Line(row.Start, row.End);
            Name = name;
            Depth = 0.0;
            Rooms = new List<Room>();
            mark = new Vector3(Row.Start.X, Row.Start.Y);
            angle = Math.Atan2(Row.End.Y - Row.Start.Y, Row.End.X - Row.Start.X) * (180 / Math.PI);
        }

        /// <summary>
        /// Constructor initializes the RoomRow with line endpoints and an optional name.
        /// </summary>
        public RoomRow(Vector3 start, Vector3 end, string name = "")
        {
            Row = new Line(start, end);
            Name = name;
            Depth = 0.0;
            Rooms = new List<Room>();
            mark = new Vector3(Row.Start.X, Row.Start.Y);
            angle = Math.Atan2(Row.End.Y - Row.Start.Y, Row.End.X - Row.Start.X) * (180 / Math.PI);
        }

        /// <summary>
        /// The unallocated length of the RoomRow.
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
        /// The aggregate area of the Rooms placed on this Row.
        /// </summary>
        public double AreaPlaced
        {
            get
            {
                var area = 0.0;
                foreach(Room room in Rooms)
                {
                    area += room.Perimeter.Area;
                }
                return area;
            }
        }

        /// <summary>
        /// A list of all placed Room perimeters.
        /// </summary>
        public IList<Polygon> PerimetersRooms
        {
            get
            {
                var perimeters = new List<Polygon>(); 
                foreach(Room room in Rooms)
                {
                    perimeters.Add(room.Perimeter);
                }
                return perimeters;
            }
        }

        /// <summary>
        /// Attempts to place a Room perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the Room's perimeter must fit and a list of Polygons which it cannot intersect.
        /// </summary>
        /// <param name="room">The Room from which to derive the Polygon to place.</param>
        /// <param name="within">The optional Polygon perimeter within which a new Room must fit.</param>
        /// <param name="among">The optional list of Polygon perimeters the new Room cannot intersect.</param>
        /// <param name="circ">The optional additional allowance opposite the Row to allow for circulation to the Rooms.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        public bool AddRoom(Room room, Polygon within = null, IList<Polygon> among = null, double circ = 2.0)
        {
            if (room == null)
            {
                return false;
            }
            circ = Math.Abs(circ);
            var polygon = room.MakePerimeter();
            var box = new TopoBox(polygon);
            var delta = 0.0;
            var newDepth = 0.0;
            var circDepth = 0.0;
            var rotation = angle;
            if (box.SizeX <= AvailableLength)
            {
                delta = box.SizeX;
                newDepth = box.SizeY;
                circDepth = box.SizeY + circ;
            }
            else if (box.SizeY <= AvailableLength)
            {
                polygon = polygon.MoveFromTo(box.NW, box.SW);
                delta = box.SizeY;
                newDepth = box.SizeX;
                circDepth = box.SizeX + circ;
                rotation += 90;
            }
            else
            {
                return false;
            }
            var t = new Transform();
            t.Rotate(Vector3.ZAxis, rotation);
            var chkCirc =
                new Polygon(
                    new []
                    {
                        new Vector3(),
                        new Vector3(delta, 0.0),
                        new Vector3(delta, circDepth),
                        new Vector3(0.0, circDepth)
                    });
            chkCirc = t.OfPolygon(chkCirc).MoveFromTo(new Vector3(), mark);
            if (!chkCirc.Fits(within, among))
            {
                if (Rooms.Count == 0)
                {
                    mark = Row.PointAt(Math.Abs(mark.DistanceTo(Row.Start) + delta) / Row.Length());
                }
                return false;
            }
            polygon = t.OfPolygon(polygon).MoveFromTo(new Vector3(), mark);
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
            var dpt = Depth + circ;
            var line1 = new Line(box.SW, new Vector3(0.0, dpt));
            line1 = line1.Rotate(box.SW, rotation).MoveFromTo(new Vector3(), Row.Start);
            var line2 = new Line(box.SW, new Vector3(0.0, dpt));
            line2 = line2.Rotate(box.SW, rotation).MoveFromTo(new Vector3(), mark);
            circulation = new Polygon(new [] { Row.Start, mark, line2.End, line1.End });
            room.Perimeter = polygon;
            Rooms.Add(room);
            return true;
        }
    }
}
