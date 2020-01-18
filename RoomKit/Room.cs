using System;
using System.Collections.Generic;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// A data structure recording room characteristics.
    /// </summary>
    public class Room
    {
        #region Constructors

        /// <summary>
        /// Default constructor creates a 1.0 x 1.0 x 1.0 white cube on the zero plane with the SW corner at the origin.
        /// </summary>
        public Room()
        {

            Color = Palette.White;
            Elevation = 0.0;
            Height = 1.0;
            Name = "";
            Number = "";
            Perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(1.0, 1.0));
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a sizing vector with the SW corner at the origin.
        /// </summary>
        public Room(Vector3 size)
        {
            Color = Palette.White;
            Elevation = 0.0;
            Height = size.Z;
            Name = "";
            Number = "";
            Perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(size.X, size.Y));
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by an area and a x : y dimension ratio and height with the SW corner at the origin.
        /// </summary>
        public Room(double area, double ratio, double height)
        {
            Color = Palette.White;
            Elevation = 0.0;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = Shaper.RectangleByArea(area, ratio);
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by two points, a width, and a height.
        /// </summary>
        public Room(Vector3 start, Vector3 end, double width, double height)
        {
            Color = Palette.White;
            Elevation = 0.0;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = new Line(start, end).Thicken(width);
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a line, a width, and a height.
        /// </summary>
        public Room(Line line, double width, double height)
        {
            Color = Palette.White;
            Elevation = 0.0;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = line.Thicken(width);
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a Polygon and a height.
        /// </summary>
        public Room(Polygon polygon, double height)
        {

            Color = Palette.White;
            Elevation = 0.0;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = polygon;
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room from another Room.
        /// </summary>
        public Room(Room room)
        {

            Color = room.Color;
            Elevation = room.Elevation;
            Height = room.Height;
            Name = room.Name;
            Number = room.Number;
            Perimeter = room.Perimeter;
            SuiteID = room.SuiteID;
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The area of the Room's Perimeter;
        /// </summary>
        public double Area
        {
            get
            {
                return Math.Abs(Perimeter.Area());
            }
        }

        /// <summary>
        /// Color of the Room. 
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Vertical position of the Room's lowest plane relative to the ground plane.
        /// </summary> 
        public double Elevation { get; set; }

        /// <summary>
        /// Vertical height of the room above its elevation.
        /// </summary>
        private double height;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value > 0.0)
                {
                    height = value;
                }
                else
                {
                    height = 1.0;
                }
            }
        }

        /// <summary>
        /// Arbitrary string identifier for this Room instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Arbitrary string identifier for this Room instance.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Polygon perimeter of the Room.
        /// </summary>
        private Polygon perimeter;
        public Polygon Perimeter
        {
            get
            {
                return perimeter;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                if (value.IsClockWise())
                {
                    perimeter = value.Reversed();
                }
                else
                {
                    perimeter = value;
                }
            }
        }

        /// <summary>
        /// Arbitrary string identifier for this Room instance.
        /// </summary>
        public string SuiteID { get; set; }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
        /// <param name="rotation">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            Perimeter = perimeter.MoveFromTo(from, to);
        }

        /// <summary>
        /// Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
        /// <param name="rotation">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void Rotate(Vector3 pivot, double rotation)
        {
            Perimeter = perimeter.Rotate(pivot, rotation);
        }

        #endregion
    }
}
