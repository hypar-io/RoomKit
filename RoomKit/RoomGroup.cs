using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages Rooms within a perimeter.
    /// </summary>
    public class RoomGroup
    {
        public string Name { get; set; }
        public Polygon Perimeter { get; }
        public IList<Room> Rooms { get; }

        private TopoBox Box;


        public RoomGroup(Polygon perimeter, string name = "", int xRooms = 1, int yRooms = 1)
        {
            Perimeter = new Polygon(perimeter.Vertices);
            Name = name;
            Box = new TopoBox(perimeter);
            Rooms = new List<Room>();

            var sizeX = Box.SizeX / xRooms;
            var sizeY = Box.SizeY / yRooms;
            var count = xRooms * yRooms;

            for (int xIdx = 0; xIdx < xRooms; xIdx++)
            {
                var xCoord = Box.SW.X + (xIdx * sizeX);
                for (int yIdx = 0; yIdx < yRooms; yIdx++)
                {
                    var yCoord = Box.SW.Y + (yIdx * sizeY);
                    var polygon = Shaper.PolygonBox(sizeX, sizeY);
                    polygon = polygon.MoveFromTo(new Vector3(), new Vector3(xCoord, yCoord));
                    var room = new Room()
                    {
                        Color = Palette.Aqua,
                        Perimeter = polygon

                    };
                    Rooms.Add(room);
                }
            }
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
        /// The area allocated within the RoomGroup.
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
        /// A list of all placed Room perimeters.
        /// </summary>
        public IList<Polygon> PerimetersRooms
        {
            get
            {
                var perimeters = new List<Polygon>();
                foreach (Room room in Rooms)
                {
                    perimeters.Add(room.Perimeter);
                }
                return perimeters;
            }
        }

        /// <summary>
        ///Sets the elevation of all Rooms.
        /// </summary>
        public void SetElevation(double elevation)
        {
            foreach(Room room in Rooms)
            {
                room.Elevation = elevation;
            }
        }

        /// <summary>
        ///Sets the height of all Rooms.
        /// </summary>
        public void SetHeight(double height)
        {
            foreach (Room room in Rooms)
            {
                room.Height = height;
            }
        }

    }
}
