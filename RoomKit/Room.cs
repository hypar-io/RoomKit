using System;
using System.Collections.Generic;
using System.Text;
using Elements;
using Elements.Interfaces;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// A data structure recording room characteristics.
    /// </summary>
    public class Room
    {
        #region Constructors
        /// <summary>
        /// Constructor setting all internal variables to default values to create a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the zero plane with an empty string, null perimeter, and an integer TypeID of -1.
        /// </summary>
        public Room()
        {
            AdjacentTo = null;
            Color = Palette.White;
            DesignArea = 1.0;
            DesignRatio = 1.0;
            DesignXYZ = new Vector3(0.0, 0.0, 1.0);
            Elevation = 0.0;
            Name = "";
            Perimeter = null;
            Placed = false;
            TypeID = 0;
            UniqueID = Guid.NewGuid().ToString();
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
                if (Perimeter != null)
                {
                    return Perimeter.Area;
                }
                return -1.0;
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
                if (Perimeter != null && DesignArea > 0.0)
                {
                    return Perimeter.Area / DesignArea;
                }
                else
                {
                    return -1.0;
                }
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
                var space = new Space(Perimeter, Height, Elevation, new Material(Guid.NewGuid().ToString(), Color));
                space.AddProperty("Name", new StringProperty(Name, UnitType.Text));
                space.AddProperty("Type", new NumericProperty(TypeID, UnitType.None));
                space.AddProperty("Design Area", new NumericProperty(DesignArea, UnitType.Area));
                space.AddProperty("Design Length", new NumericProperty(DesignLength, UnitType.Distance));
                space.AddProperty("Design Width", new NumericProperty(DesignWidth, UnitType.Distance));
                space.AddProperty("Area", new NumericProperty(Perimeter.Area, UnitType.Area));
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
            set
            {
                if (value != null)
                {
                    color = value;
                }
            }
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
            set
            {
                if (value > 0.0)
                {
                    designArea = value;
                }
            }
        }

        /// <summary>
        /// Desired x-axis dimension of this Room. 
        /// </summary>
        public double DesignLength
        {
            get
            {
                if (DesignXYZ == null)
                {
                    return 0.0;
                }
                return DesignXYZ.X;
            }
            set
            {
                if (value > 0.0)
                {
                    DesignXYZ = new Vector3(value, DesignWidth, Height);
                }
            }
        }
           
        /// <summary>
        /// Desired y-axis dimension of this Room. 
        /// </summary>
        public double DesignWidth
        {
            get
            {
                if (DesignXYZ == null)
                {
                    return 0.0;
                }
                return DesignXYZ.Y;
            }
            set
            {
                if (value > 0.0)
                {
                    DesignXYZ = new Vector3(DesignLength, value, Height);
                }
            }
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
            set
            {
                if (value > 0.0)
                {
                    designRatio = value;
                }
            }
        }


        /// <summary>
        /// Returns true if both DesignLength and DesignWidth are positive values.
        /// </summary>
        public bool DesignSet
        {
            get
            {
                if (DesignLength <= 0.0 || DesignWidth <= 0.0)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Desired x, y, and z axis dimensions of this Room.
        /// Ignores x and y values if either is 0.0 or negative.
        /// Ignored z value if it is 0.0 or negative.
        /// Sets DesignArea.
        /// </summary>
        private Vector3 designXYZ;
        public Vector3 DesignXYZ
        {
            get { return designXYZ; }
            set
            {
                if (value.X >= 0.0 && value.Y >= 0.0 && value.Z > 0.0)
                {
                    designXYZ = value;
                    DesignArea = value.X * value.Y;
                }
            }
        }

        /// <summary>
        /// The vertical position of the Room's lowest plane, parallel to the ground plane.
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// Height of the Room prism.
        /// Set ignores non-positive values.
        /// </summary>
        public double Height
        {
            get
            {
                if (DesignXYZ == null)
                {
                    return 1.0;
                }
                return DesignXYZ.Z;
            }
            set
            {
                if (value > 0.0)
                {
                    DesignXYZ = new Vector3(DesignLength, DesignWidth, value);
                }
            }
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
            set
            {
                if (value != null)
                {
                    perimeter = value;
                }
            }
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
            get
            {
                if (Perimeter == null)
                {
                    return 0.0;
                }
                return new TopoBox(perimeter).SizeX;
            }
        }

        /// <summary>
        /// X dimensions of the Room Perimeter orthogonal bounding box.
        /// </summary>
        public double SizeY
        {
            get
            {
                if (Perimeter == null)
                {
                    return 0.0;
                }
                return new TopoBox(perimeter).SizeY;
            }
        }

        /// <summary>
        /// Arbitrary integer identifier of this Room type. Can be used to identify desired adjacencies.
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// UUID for this Room instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }
        #endregion

        #region Methods

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
            Perimeter = Shaper.PolygonBox(xyz.X, xyz.Y, moveTo);
            if (xyz.Z > 0.0)
            {
                Height = xyz.Z;
            }
            return true;
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the origin or at the 2D location implied by the supplied Vector3.
        /// </summary>
        /// <returns>
        /// True if the Perimeter is successfully set.
        /// </returns>
        public bool SetPerimeter(Vector3 moveTo = null)
        {
            if (DesignSet)
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
        public bool SetPerimeter(double area, double ratio = 1.5, Vector3 moveTo = null)
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
