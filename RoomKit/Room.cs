using System;
using System.Linq;
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
        /// <summary>
        /// Sets the dimension precision of all RoomKit operations, (3 = 1 millimeter).
        /// </summary>
        public const int PRECISION = 3;

        #region Constructors

        /// <summary>
        /// Default constructor creates a 1.0 x 1.0 x 1.0 white cube on the zero plane with the SW corner at the origin.
        /// </summary>
        public Room()
        {
            Color = Palette.White;
            Department = "";
            DesignArea = 1.0;
            DesignRatio = 1.0;
            Height = 1.0;
            Name = "";
            Number = "";
            Perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(1.0, 1.0));
            elevation = 0.0;
            Placed = false;
            Suite = "";
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a positive sizing vector with the SW corner at the origin.
        /// </summary>
        public Room(Vector3 size)
        {
            var sizeX = Math.Abs(size.X).NearEqual(0.0) ? 1.0 : Math.Abs(size.X);
            var sizeY = Math.Abs(size.Y).NearEqual(0.0) ? 1.0 : Math.Abs(size.Y);
            var sizeZ = Math.Abs(size.Z).NearEqual(0.0) ? 1.0 : Math.Abs(size.Z);
            Color = Palette.White;
            Department = "";
            DesignArea = sizeX * sizeY;
            DesignRatio = sizeX / sizeY;
            Height = sizeZ;
            Name = "";
            Number = "";
            Perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(sizeX, sizeY));
            elevation = 0.0;
            Placed = false;
            Suite = "";
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a positive area and a positive x : y dimension ratio and a positive height with the SW corner at the origin.
        /// </summary>
        public Room(double area, double ratio, double height)
        {
            area = area.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(area), PRECISION);
            ratio = ratio.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(ratio), PRECISION);
            height = height.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(height), PRECISION);
            Color = Palette.White;
            Department = "";
            DesignArea = area;
            DesignRatio = ratio;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = Shaper.RectangleByArea(area, ratio);
            elevation = 0.0;
            Placed = false;
            Suite = "";
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by two points, a positive width, and a positive height.
        /// </summary>
        public Room(Vector3 start, Vector3 end, double width, double height)
        {
            width = width.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(width), PRECISION);
            height = height.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(height), PRECISION);
            Color = Palette.White;
            Department = "";
            DesignArea = start.DistanceTo(end) * width;
            DesignRatio = start.DistanceTo(end) / width;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = new Line(start, end).Thicken(width);
            elevation = Math.Round(start.Z, PRECISION);
            Placed = false;
            Suite = "";
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a line, a positive width, and a positive height.
        /// </summary>
        public Room(Line line, double width, double height)
        {
            width = width.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(width), PRECISION);
            height = height.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(height), PRECISION);
            Color = Palette.White;
            Department = "";
            DesignArea = line.Length() * width;
            DesignRatio = line.Length() / width;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = line.Thicken(width);
            elevation = Math.Round(line.Start.Z, PRECISION);
            Placed = false;
            Suite = "";
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room by a Polygon and a positive height.
        /// </summary>
        public Room(Polygon polygon, double height)
        {
            polygon = polygon.IsClockWise() ? polygon.Reversed() : polygon;
            height = height.NearEqual(0.0) ? 1.0 : Math.Round(Math.Abs(height), PRECISION);
            Color = Palette.White;
            Department = "";
            DesignArea = polygon.Area();
            var compass = polygon.Compass();
            DesignRatio = compass.SizeX / compass.SizeY;
            Height = height;
            Name = "";
            Number = "";
            Perimeter = polygon;
            Elevation = Math.Round(polygon.Vertices.First().Z, PRECISION);
            Placed = false;
            Suite = "";
            SuiteID = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor creates a Room from another Room.
        /// </summary>
        public Room(Room room)
        {
            Color = room.Color;
            Department = room.Suite;
            DesignArea = room.DesignArea;
            DesignRatio = room.DesignRatio;
            Height = room.Height;
            Name = room.Name;
            Number = room.Number;
            Perimeter = room.Perimeter;
            Elevation = room.Elevation;
            Placed = false;
            Suite = room.Suite;
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
                return Math.Round(Perimeter.Area(), PRECISION);
            }
        }

        /// <summary>
        /// Color of the Room. 
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Material of the Room. 
        /// </summary>
        public Material ColorAsMaterial
        {
            get
            {
                return new Material(Color, 0.0, 0.0, false, null, false, Guid.NewGuid(), Guid.NewGuid().ToString());
            }
        }

        /// <summary>
        /// Arbitrary string identifier to link this Room to a department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Intended area for this Room after placement.
        /// </summary>
        private double designArea;
        public double DesignArea 
        { 
            get { return designArea; }
            set 
            {
                value = Math.Round(Math.Abs(value));
                designArea = value.NearEqual(0.0) ? designArea : value;
            }
        }

        /// <summary>
        /// Intended x : y ratio for this Room after placement.
        /// </summary>
        private double designRatio;
        public double DesignRatio 
        { 
            get { return designRatio; }
            set
            {
                value = Math.Round(Math.Abs(value), PRECISION);
                designRatio = value.NearEqual(0.0) ? designRatio : value;
            }
        }

        private double elevation;
        /// <summary>
        /// Vertical position of the Room's lowest plane relative to the ground plane.
        /// </summary> 
        public double Elevation
        {
            get
            {
                return elevation;
            }
            set
            {
                elevation = Math.Round(value, PRECISION);
            }
        }

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
                value = Math.Round(Math.Abs(value), PRECISION);
                height = value.NearEqual(0.0) ? height : value;
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
                return perimeter.MoveFromTo(Vector3.Origin, new Vector3(0.0, 0.0, Elevation));
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                perimeter = value.IsClockWise() ? new Polygon(value.Reversed().Vertices) : new Polygon(value.Vertices);
            }
        }

        /// <summary>
        /// Profile perimeter of the Room.
        /// </summary>
        public Profile PerimeterAsProfile
        {
            get
            {
                return new Profile(perimeter);
            }
        }


        /// <summary>
        /// Whether the Room has been placed in a location with a perimeter.
        /// </summary>
        public bool Placed { get; set; }

        /// <summary>
        /// Intended x : y ratio for this Room's current Perimeter.
        /// </summary>
        public double Ratio
        {
            get
            {
                var compass = Perimeter.Compass();
                return Math.Round(compass.SizeX / compass.SizeY, PRECISION);
            }
        }

        /// <summary>
        /// Arbitrary string identifier to link this Room to a Suite instance.
        /// </summary>
        public string Suite { get; set; }

        /// <summary>
        /// Arbitrary string identifier to link this Room to a Suite instance.
        /// </summary>
        public string SuiteID { get; set; }

        //public Transform Transform { get; set; }

        /// <summary>
        /// UUID for this instance set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Moves the Room Perimeter between the specifed relative points.
        /// </summary>
        /// <param name="from">Vector3 anchor point.</param> 
        /// <param name="to">Vector3 destination point.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            perimeter = perimeter.MoveFromTo(from, to);
            Elevation = to.Z;
        }

        /// <summary>
        /// Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
        /// <param name="angle">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void Rotate(Vector3 pivot, double angle)
        {
            perimeter = perimeter.Rotate(pivot, angle);
        }

        #endregion
    }
}
