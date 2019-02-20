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
        /// <summary>
        /// An arbitrary string identifier for this RoomGroup.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Polygon within which all Rooms are placed.
        /// </summary>
        public Polygon Perimeter { get; }

        /// <summary>
        /// The list of Rooms placed within the Perimeter.
        /// </summary>
        public IList<Room> Rooms { get; }

        /// <summary>
        /// A private bounding box used for placing new Rooms.
        /// </summary>
        private TopoBox Box;

        /// <summary>
        /// Creates a group of rooms by dividing the supplied Polygon perimeter by the quantity of supplied divisions along the orthogonal x and y axes. Room perimeters conform to fit within the supplied Polygon.
        /// </summary>
        /// <param name="perimeter">The Polygon to divide with a number of Room perimeters.</param> 
        /// <param name="xRooms">The quantity of Rooms along the x axis.</param> 
        /// <param name="yRooms">The quantity of Rooms along the y axis.</param> 
        /// <param name="name">An arbitrary string identifier for this RoomGroup.</param> 
        /// <returns>
        /// A new RoomGroup.
        /// </returns>
        public RoomGroup(Polygon perimeter, int xRooms = 1, int yRooms = 1, string name = "")
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
                    polygon = polygon.MoveFromTo(new Vector3(), new Vector3(xCoord, yCoord)).Intersection(perimeter).First();

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
        /// The unallocated area of the RoomGroup perimeter.
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
        /// Uniformly sets the elevation of all Rooms in the RoomGroup.
        /// </summary>
        /// <param name="elevation">The new elevation of the Rooms.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void SetElevation(double elevation)
        {
            foreach(Room room in Rooms)
            {
                room.Elevation = elevation;
            }
        }

        /// <summary>
        /// Uniformly sets the height of all Rooms in the RoomGroup.
        /// </summary>
        /// <param name="elevation">The new height of the Rooms.</param> 
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
}
