using System;
using System.Collections.Generic;
using System.Linq;
using Hypar.Elements;
using Hypar.Geometry;

namespace RoomKit
{
    /// <summary>
    /// A data structure recording room characteristics.
    /// </summary>
    public class RoomGroup
    {
        private readonly double angle = 0.0;
        private Vector3 mark = null;

        public string Name { get; set; }
        public Polygon Perimeter { get; }
        public IList<Room> Rooms { get; set; }
        public Line Row { get; }

        private TopoBox Box;

        public RoomGroup()
        {
            Name = "";
            Perimeter =
                new Polygon
                (
                    new List<Vector3>
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(0.0, 10.0),
                        new Vector3(10.0, 10.0),
                        new Vector3(0.0, 10.0)
                    }
                );
            Box = new TopoBox(Perimeter);
            Rooms = new List<Room>();
            Row = null;
        }

        public RoomGroup(Polygon perimeter, string name = "")
        {
            Perimeter = new Polygon(perimeter.Vertices);
            Name = name;
            Box = new TopoBox(perimeter);
            Rooms = new List<Room>();
            Row = null;
        }

        public RoomGroup(Line row, string name = "")
        {
            Row = new Line(row.Start, row.End);
            Name = name;
            Perimeter = null;
            Rooms = new List<Room>();
            mark = new Vector3(Row.Start.X, Row.Start.Y);
            angle = Math.Atan2(Row.End.Y - Row.Start.Y, Row.End.X - Row.Start.X) * (180 / Math.PI);
        }

        /// <summary>
        /// The unallocated area of the RoomGroup Perimeter.
        /// </summary>
        public double AvailableArea
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
        /// The unallocated area of the RoomGroup Row.
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
        /// The area allocated within the RoomGroup.
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
        /// A list of all placed room perimeters.
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
        /// Attempts to place a room within the perimeter of the group or on its row line, depending on the grooup's initial geometry.
        /// </summary>
        /// <param name="room">The Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        public bool AddRoom(Room room, IList<Polygon> among = null)
        {
            if (Perimeter != null)
            {
                return AddToPerimeter(room, among);
            }
            if (Row != null)
            {
                return AddToRow(room, among);
            }
            return false;
        }

        /// <summary>
        /// Attempts to place a Room perimeter in an open area of the RoomGroup.
        /// </summary>
        /// <param name="room">The Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        private bool AddToPerimeter(Room room, IList<Polygon> among)
        {
            var polygon = RoomPerimeter(room);
            if (polygon.Area > AvailableArea)
            {
                return false;
            }
            if (Rooms.Count == 0)
            {
                polygon = polygon.MoveFromTo(new Vector3(), Box.SW);
                polygon = Perimeter.Intersection(polygon).First();
                if (among != null && polygon.Intersects(among))
                {
                    return false;
                }
                room.Perimeter = polygon;
                Rooms.Add(room);
                return true;
            }
            foreach (Polygon adjacentTo in PerimetersRooms)
            {
                var adjPolygon = Place.Adjacent(polygon, adjacentTo, Perimeter, PerimetersRooms);
                if (adjPolygon != null)
                {
                    room.Perimeter = adjPolygon;
                    Rooms.Add(room);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Attempts to place a Room perimeter on the next open segment of the Row.
        /// </summary>
        /// <param name="room">The Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// True if the room was successfully placed.
        /// </returns>
        private bool AddToRow(Room room, IList<Polygon> among)
        {
            var polygon = RoomPerimeter(room);
            var box = new TopoBox(polygon);
            var t = new Transform();
            var delta = 0.0;
            if (box.SizeX <= AvailableLength)
            {
                t.Rotate(Vector3.ZAxis, angle);
                delta = box.SizeX;
            }
            else if (box.SizeY <= AvailableLength)
            {
                t.Rotate(Vector3.ZAxis, angle + 90);
                delta = box.SizeY;
            }
            else
            {
                return false;
            }
            polygon = polygon.Transform(t);
            polygon = polygon.MoveFromTo(new Vector3(), mark);
            if (among != null && polygon.Intersects(among))
            {
                return false;
            }
            mark = Row.PointAt((mark.DistanceTo(Row.Start) + delta) / Row.Length);
            room.Perimeter = polygon;
            Rooms.Add(room);
            return true;
        }

        /// <summary>
        /// Creates a Polygon perimeter derived from the supplied Room characteristics.
        /// </summary>
        /// <param name="room">The Room from which to derive the Polygon to place.</param>
        /// <returns>
        /// A new Polygon derived either from fixed dimensions or as a variably proportioned area.
        /// </returns>
        private Polygon RoomPerimeter(Room room)
        {
            if (room.Perimeter != null)
            {
                return room.Perimeter;
            }
            if (room.DesignX > 0.0 && room.DesignY > 0.0)
            {
               return Shaper.PolygonBox(room.DesignX, room.DesignY);
            }
            else
            {
               return Shaper.AreaFromCorner(room.DesignArea, Shaper.RandomDouble(1, 2));
            }
        }
    }
}
