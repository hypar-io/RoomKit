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
            Name = "";
            Number = "";
            Perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(1.0, 1.0));
            Position = Vector3.Origin;
            Size = new Vector3(1.0, 1.0, 1.0);
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a sizing vector with the SW corner at the origin.
        /// </summary>
        public Room(Vector3 size)
        {

            Color = Palette.White;
            Name = "";
            Number = "";
            Perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(size.X, size.Y));
            Position = Vector3.Origin;
            Size = size;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by an area and a x : y dimension ratio and height with the SW corner at the origin.
        /// </summary>
        public Room(double area, double ratio, double height)
        {

            Color = Palette.White;
            Name = "";
            Number = "";
            Perimeter = Shaper.RectangleByArea(area, ratio);
            Position = Vector3.Origin;
            Size = new Vector3(Perimeter.Compass().SizeX, Perimeter.Compass().SizeY, height);
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by two points, a width, and a height.
        /// </summary>
        public Room(Vector3 start, Vector3 end, double width, double height)
        {

            Color = Palette.White;
            Name = "";
            Number = "";
            Perimeter = new Line(start, end).Thicken(width);
            Position = Vector3.Origin;
            Size = new Vector3(Perimeter.Compass().SizeX, Perimeter.Compass().SizeY, height);
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a line, a width, and a height.
        /// </summary>
        public Room(Line line, double width, double height)
        {

            Color = Palette.White;
            Name = "";
            Number = "";
            Perimeter = line.Thicken(width);
            Position = Vector3.Origin;
            Size = new Vector3(Perimeter.Compass().SizeX, Perimeter.Compass().SizeY, height);
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a Polygon and a height.
        /// </summary>
        public Room(Polygon polygon, double height)
        {

            Color = Palette.White;
            Name = "";
            Number = "";
            Perimeter = polygon;
            Position = Vector3.Origin;
            Size = new Vector3(Perimeter.Compass().SizeX, Perimeter.Compass().SizeY, height);
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room from another Room.
        /// </summary>
        public Room(Room room)
        {

            Color = room.Color;
            Name = room.Name;
            Number = room.Number;
            Perimeter = room.Perimeter;
            Position = room.Position;
            Size = room.Size;
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The area of the room's perimeter Polygon. 
        /// Returns -1.0 if the Room's Perimeter is null.
        /// </summary>
        public double Area
        {
            get
            {
                return Math.Abs(Perimeter.Area());
            }
        }

        /// <summary>
        /// A Mass created from Room characteristics.
        /// </summary>
        public Mass AsMass
        {
            get
            {
                return new Mass(new Profile(Perimeter),
                                Height,
                                new Material(Guid.NewGuid().ToString(), Color),
                                new Transform(Position),
                                null,
                                Guid.NewGuid(),
                                Name);
            }
        }

        /// <summary>
        /// A Panel created from Room characteristics.
        /// </summary>
        public Panel AsPanel
        {
            get
            {
                return new Panel(Perimeter,
                                 new Material(Guid.NewGuid().ToString(), Color),
                                 new Transform(Position),
                                 null,
                                 Guid.NewGuid(),
                                 Name);
            }
        }

        /// <summary>
        /// A Space created from Room characteristics.
        /// </summary>
        public Space AsSpace
        {
            get
            {
                return new Space(new Profile(Perimeter),
                                 Height,
                                 new Material(Guid.NewGuid().ToString(), Color),
                                 new Transform(Position),
                                 null,
                                 Guid.NewGuid(),
                                 Name);
            }
        }

        /// <summary>
        /// Color of the Room. 
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Vertical position of the Room's lowest plane relative to the ground plane.
        /// </summary> 
        public double Elevation { get; }

        /// <summary>
        /// Vertical height of the room above its elevation.
        /// </summary> 
        public double Height { get; }

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
        public Polygon Perimeter { get; private set; }

        /// <summary>
        /// Polygon perimeter of the Room.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Dimensions of the Room Perimeter orthogonal bounding box.
        /// </summary>
        public Vector3 Size { get; private set; }

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
        /// <param name="angle">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// True if the Perimeter is successfully rotated.
        /// </returns>
        public void Rotate(Vector3 pivot, double angle)
        {
            Perimeter = Perimeter.Rotate(pivot, angle);
            Size = new Vector3(Perimeter.Compass().SizeX, Perimeter.Compass().SizeY, Size.Z);
        }

        #endregion
    }
}
