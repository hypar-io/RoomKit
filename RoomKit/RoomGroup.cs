using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages Rooms within a perimeter.
    /// </summary>
    public class RoomGroup
    {

        #region Constructors
        /// <summary>
        /// Creates an empty group of Rooms.
        /// </summary>
        /// <returns>
        /// A new RoomGroup.
        /// </returns>
        public RoomGroup()
        {
            Name = "";
            Perimeter = null;
            Rooms = new List<Room>();
            UniqueID = Guid.NewGuid().ToString();
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Unallocated area of the RoomGroup perimeter.
        /// </summary>
        public double AreaAvailable
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
        /// Area allocated within the RoomGroup.
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
        /// Elevation of all Rooms in the RoomGroup.
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
        /// Arbitrary string identifier for this RoomGroup.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Polygon within which all Rooms are placed.
        /// </summary>
        private Polygon perimeter;
        public Polygon Perimeter
        {
            get { return perimeter; }
            set
            {
                if (value != null)
                {
                    perimeter = value;
                }
            }
        }

        /// <summary>
        /// List of Rooms placed within the Perimeter.
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
        /// X dimension of the Perimeter orthogonal bounding box.
        /// </summary>
        public double SizeX
        {
            get
            {
                if (Perimeter == null)
                {
                    return 0.0;
                }
                return new TopoBox(Perimeter).SizeX;
            }
        }

        /// <summary>
        /// Y dimension of the Perimeter orthogonal bounding box.
        /// </summary>
        public double SizeY
        {
            get
            {
                if (Perimeter == null)
                {
                    return 0.0;
                }
                return new TopoBox(Perimeter).SizeY;
            }
        }

        /// <summary>
        /// UUID for this RoomGroup instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Moves all Rooms in the RoomGroup and the RoomGroup Perimeter along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            foreach(Room room in Rooms)
            {
                room.MoveFromTo(from, to);
            }
            if (Perimeter != null)
            {
                Perimeter = Perimeter.MoveFromTo(from, to);
            }
            Elevation = to.Z - from.Z;
        }

        /// <summary>
        /// Rotates the RoomGroup Perimeter and Rooms in the horizontal plane around the supplied pivot point.
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
            if (Perimeter != null)
            {
                Perimeter = Perimeter.Rotate(pivot, angle);
            }
        }

        /// <summary>
        /// Uniformly sets the color of all Rooms in the RoomGroup.
        /// </summary>
        /// <param name="color">The new color of the Rooms.</param> 
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

        /// <summary>
        /// Clears the current Rooms list and creates new Rooms defined by orthogonal x- and y-axis divisions of the RoomGroup Perimeter.
        /// </summary>
        /// <param name="xRooms">The quantity of Rooms along orthogonal x-axis. Must be positive.</param> 
        /// <param name="yRooms">The quantity of Rooms along orthogonal y-axis. Must be positive.</param> 
        /// <returns>
        /// True if the Rooms are created.
        /// </returns>
        public bool RoomsByDivision(int xRooms = 1, int yRooms = 1, double height = 3.0, string name = "")
        {
            if (Perimeter == null || xRooms < 1 || yRooms < 1 || height <= 0.0)
            {
                return false;
            }
            var sizeX = SizeX / xRooms;
            var sizeY = SizeY / yRooms;
            var count = xRooms * yRooms;
            var box = new TopoBox(Perimeter);
            var newRooms = new List<Room>();
            for (int xIdx = 0; xIdx < xRooms; xIdx++)
            {
                var xCoord = box.SW.X + (xIdx * sizeX);
                for (int yIdx = 0; yIdx < yRooms; yIdx++)
                {
                    var yCoord = box.SW.Y + (yIdx * sizeY);
                    var polygon = Shaper.PolygonBox(sizeX, sizeY);
                    polygon = polygon.MoveFromTo(Vector3.Origin, new Vector3(xCoord, yCoord)).Intersection(Perimeter).First();
                    var room = new Room()
                    {
                        Height = height,
                        Name = name,
                        Perimeter = polygon
                    };
                    newRooms.Add(room);
                }
            }
            if (newRooms.Count == 0)
            {
                return false;
            }
            Rooms.Clear();
            Rooms.AddRange(newRooms);
            return true;
        } 
        #endregion

    }
}
