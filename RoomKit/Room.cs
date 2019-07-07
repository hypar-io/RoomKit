using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Interfaces;
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
        /// Constructor by default sets all internal variables to values to creating a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the zero plane with an empty string, null perimeter, and an integer TypeID of -1.
        /// </summary>
        public Room(int[] adjacentTo = null,
                    double designArea = 1.0,
                    double designRatio = 1.0,
                    double elevation = 0.0,
                    double height = 3.0,
                    string name = "",
                    Polygon perimeter = null,
                    int typeID = -1)
        {
            Color = Palette.White;
            Placed = false;
            UniqueID = Guid.NewGuid().ToString();

            AdjacentTo = adjacentTo;
            DesignArea = designArea;
            DesignRatio = designRatio;
            Elevation = elevation;
            Height = height;
            Name = name;
            Perimeter = perimeter;
            TypeID = typeID;
        }

        #endregion

        #region Properties
        /// <summary>
        /// A list of Resource ID integers indicating the desired adjacencies of this Room type to other Room types.
        /// </summary>
        public int[] AdjacentTo { get; set; }

        /// <summary>
        /// The area of the room's perimeter Polygon. 
        /// Returns -1.0 if the Room's Perimeter is null.
        /// </summary>
        public double Area
        {
            get
            {
                return Perimeter == null ? -1.0 : Perimeter.Area();
            }
        }

        /// <summary>
        /// The ratio between the intended area and the actual area of the Room. 
        /// Returns a negative value if the Room has no Perimeter value.
        /// </summary>
        public double AreaVariance
        {
            get
            {
                return (Perimeter != null && DesignArea > 0.0) ? Perimeter.Area() / DesignArea : -1.0;
            }
        }

        /// <summary>
        /// A Space created from Room characteristics.
        /// Adds properties to the Space recording
        /// Name
        /// TypeID as Type
        /// DesignArea as Design Area
        /// DesignX as Design Length
        /// DesignY as Design Width
        /// Perimeter.Area as Area
        /// Elevation
        /// Height
        /// </summary>
        public Space AsSpace
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                var space = new Space(Perimeter, Height, Elevation, new Material(Guid.NewGuid().ToString(), Color))
                {
                    Name = "Name"
                };
                space.AddProperty("Name", new StringProperty(Name, UnitType.Text));
                space.AddProperty("Type", new NumericProperty(TypeID, UnitType.None));
                space.AddProperty("Design Area", new NumericProperty(DesignArea, UnitType.Area));
                space.AddProperty("Design Length", new NumericProperty(DesignLength, UnitType.Distance));
                space.AddProperty("Design Width", new NumericProperty(DesignWidth, UnitType.Distance));
                space.AddProperty("Area", new NumericProperty(Perimeter.Area(), UnitType.Area));
                space.AddProperty("Elevation", new NumericProperty(Elevation, UnitType.Distance));
                space.AddProperty("Height", new NumericProperty(Height, UnitType.Distance));
                return space;
            }
        }

        /// <summary>
        /// Color of the Space returned by AsSpace. 
        /// Ignores null values.
        /// </summary>
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value ?? color; }
        }

        /// <summary>
        /// Desired area of this Room. 
        /// Overridden by positive values of DesignX and DesignY. 
        /// Ignores negative values.
        /// Set to 1.0 by default.
        /// </summary>
        private double designArea;
        public double DesignArea
        {
            get { return designArea; }
            set { designArea = value > 0.0 ? value : designArea; }
        }

        /// <summary>
        /// Desired x-axis dimension of this Room. 
        /// </summary>
        private double designLength;
        public double DesignLength
        {
            get { return designLength; }
            set { designLength = value > 0.0 ? value : designLength; }
        }

        /// <summary>
        /// Desired y-axis dimension of this Room. 
        /// </summary>
        private double designWidth;
        public double DesignWidth
        {
            get { return designWidth; }
            set { designWidth = value > 0.0 ? value : designLength; }
        }

        /// <summary>
        /// Desired ratio of x to y dimensions of the Room. 
        /// Overridden by positive values of DesignX and DesignY. 
        /// Ignores negative values.
        /// Set to 1.0 by default.
        /// </summary>
        private double designRatio;
        public double DesignRatio
        {
            get { return designRatio; }
            set { designRatio = value > 0.0 ? value : designRatio; }
        }


        /// <summary>
        /// Returns true if both DesignLength and DesignWidth are positive values.
        /// </summary>
        public bool DesignSet
        {
            get { return (DesignLength <= 0.0 || DesignWidth <= 0.0) ? false : true; }
        }

        /// <summary>
        /// Desired x, y, and z axis dimensions of this Room.
        /// Ignores x and y values if either is 0.0 or negative.
        /// Ignored z value if it is 0.0 or negative.
        /// Sets DesignArea.
        /// </summary>
        public Vector3 DesignXYZ
        {
            get { return new Vector3(designLength, designWidth, height); }
            set
            {
                designLength = value.X > 0.0 ? value.X : designLength;
                designWidth = value.Y > 0.0 ? value.Y : designWidth;
                height = value.Z > 0.0 ? value.Z : height;
                DesignArea = designLength * designWidth;
            }
        }

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
            get { return height; }
            set { height = value > 0.0 ? value : height; }
        }

        /// <summary>
        /// Arbitrary string identifier for this Room instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Polygon perimeter of the Room.
        /// </summary>
        private Polygon perimeter;
        public Polygon Perimeter
        {
            get { return perimeter; }
            set { perimeter = value ?? perimeter; }
        }

        /// <summary>
        /// Manual flag to record if the Room has been placed in its final position.
        /// </summary>
        public bool Placed { get; set; }

        /// <summary>
        /// X dimensions of the Room Perimeter orthogonal bounding box.
        /// </summary>
        public double SizeX
        {
            get { return Perimeter == null ? 0.0 : new TopoBox(perimeter).SizeX;  }
        }

        /// <summary>
        /// X dimensions of the Room Perimeter orthogonal bounding box.
        /// </summary>
        public double SizeY
        {
            get { return Perimeter == null ? 0.0 : new TopoBox(perimeter).SizeY; }
        }

        /// <summary>
        /// Arbitrary integer identifier of this instance..
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }
        #endregion

        #region Methods

        /// <summary>
        /// Moves the Room along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// A Polygon represeting the Room's new Perimeter.
        /// </returns>
        public Polygon MoveFromTo(Vector3 from, Vector3 to)
        {
            if (Perimeter != null)
            {
                Perimeter = Perimeter.MoveFromTo(from, to);
            }
            return Perimeter;
        }

        /// <summary>
        /// Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
        /// <param name="angle">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// True if the Perimeter is successfully rotated.
        /// </returns>
        public bool Rotate(Vector3 pivot, double angle)
        {
            if (Perimeter == null)
            {
                return false;
            }
            Perimeter = Perimeter.Rotate(pivot, angle);
            return true;
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter, Height, and southwest corner location with a supplied vectors.
        /// Sets the DesignX and DesignY properties.
        /// </summary>
        /// <param name="xyz">Vector3 dimensions of a new Polygon Perimeter. If xy.Z is > 0.0, sets the height of the Room.</param> 
        /// <param name="moveTo">Vector3 location of the new Polygon's southwest corner.</param> 
        /// <returns>
        /// True if the Perimeter is successfully set.
        /// </returns>
        public bool SetDimensions(Vector3 xyz, Vector3 moveTo = null)
        {
            if (xyz.X <= 0.0 || xyz.Y <= 0.0)
            {
                return false;
            }
            perimeter = Shaper.PolygonBox(xyz.X, xyz.Y, moveTo);
            height = xyz.Z > 0.0 ? xyz.Z : height;
            return true;
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the origin or at the 2D location specified by the supplied Vector3.
        /// </summary>
        /// <returns>
        /// True if the Perimeter is successfully set.
        /// </returns>
        public bool SetPerimeter(Vector3 moveTo = null, double width = 0.0)
        {
            if (width > 0.0)
            {
                if (DesignSet)
                {
                    Perimeter = Shaper.PolygonBox(DesignLength * DesignWidth / width, width, moveTo);
                    return true;
                }
                else if (DesignArea > 0.0)
                {
                    Perimeter = Shaper.PolygonBox(DesignArea / width, width, moveTo);
                    return true;
                }
            }
            else if (DesignSet)
            {
                Perimeter = Shaper.PolygonBox(DesignLength, DesignWidth, moveTo);
                return true;
            }
            else if (DesignArea > 0.0 && DesignRatio > 0.0)
            {
                Perimeter = Shaper.PolygonByArea(DesignArea, DesignRatio, moveTo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin.
        /// </summary>
        /// <param name="area">Area override for the new Room Perimeter. If zero, defaults to the value of DesignArea.</param>
        /// <param name="ratio">Desired ratio of X to Y Room dimensions.</param> 
        /// <param name="moveTo">Vector3 location of the new Polygon's southwest corner.</param> 
        /// <returns>
        /// True if the Perimeter is successfully set.
        /// </returns>
        public bool SetPerimeter(double area, double ratio = 1.0, Vector3 moveTo = null)
        {
            if (area <= 0.0 || ratio <= 0.0)
            {
                return false;
            }
            Perimeter = Shaper.PolygonByArea(area, ratio, moveTo);
            return true;
        }

        /// <summary>
        /// Creates and sets a rectangular Room perimeter with dimensions derived from a supplied Line and a width. 
        /// Intended for creating corridors.
        /// </summary>
        /// <param name="axis">The Line defining the centerline of the perimeter.</param> 
        /// <param name="width">The width of the perimeter along the axis Line.</param> 
        /// <returns>
        /// True if the Perimeter is successfully set.
        /// </returns>
        public bool SetPerimeter(Line axis, double width)
        {
            if (width <= 0.0)
            {
                return false;
            }
            Perimeter = axis.Thicken(width);
            return true;
        }

        /// <summary>
        /// Creates and sets a rectangular Room perimeter with dimensions derived from two points and a width. 
        /// Intended for creating corridors.
        /// </summary>
        /// <param name="start">The start point of an axis defining centerline of the perimeter.</param> 
        /// <param name="end">The end point of an axis defining centerline of the perimeter.</param> 
        /// <param name="width">The width of the perimeter along the axis Line.</param> 
        /// <returns>
        ///True if the Perimeter is successfully set.
        /// </returns>
        public bool SetPerimeter(Vector3 start, Vector3 end, double width)
        {
            if (width <= 0.0)
            {
                return false;
            }
            Perimeter = new Line(start, end).Thicken(width);
            return true;
        }

        #endregion
    }
}
