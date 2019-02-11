using System;
using System.Collections.Generic;
using System.Text;
using Hypar.Elements;
using Hypar.Geometry;

namespace RoomKit
{
    /// <summary>
    /// A data structure recording room characteristics.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Constructor setting all internal variables to default values.
        /// </summary>
        public Room()
        {
            AdjacentTo = null;
            Color = Palette.White;
            DesignArea = 1.0;
            DesignX = 1.0;
            DesignY = 1.0;
            Elevation = 0.0;
            Height = 1.0;
            Name = "";
            Perimeter = null;
            ResourceID = 0;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// A list of Resource ID integers indicating the desired adjacencies of this Room type to other Room types.
        /// </summary>
        public int[] AdjacentTo { get; set; }

        /// <summary>
        /// Color of the Space returned by AsSpace.
        /// </summary>
        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                if (value == null)
                {
                    color = Palette.White;
                }
                else
                { 
                    color = value;
                }
            }
        }
        /// <summary>
        /// The desired area of this Room. Overridden if values of DesignX and DesignY are set to positive values.
        /// </summary>
        private double designArea;
        public double DesignArea
        {
            get { return designArea; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
                }
                designArea = value;
            }
        }

        /// <summary>
        /// The desired x-axis dimension of this Room. Overrides DesignArea if DesignY is also set to a positive value.
        /// </summary>
        private double designX;
        public double DesignX
        {
            get { return designX; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
                }
                designX = value;
            }
        }

        /// <summary>
        /// The desired y-axis dimension of this Room. Overrides DesignArea if DesignX is also set to a positive value.
        /// </summary>
        private double designY;
        public double DesignY
        {
            get { return designY; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
                }
                designY = value;
            }
        }

        /// <summary>
        /// The vertical position of the Room's lowest plane, parallel to the ground plane.
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// Public property of the height of the Room prism. Required to allow error checking for new heights.
        /// </summary>
        private double height;
        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
                }
                height = value;
            }
        }
        /// <summary>
        /// Arbitrary string identifier for this Room instance. Has no effect on Room operations.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Polygon perimeter of the Room. Required to allow error checking for a non-null perimeter.
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
        /// Arbitrary integer identifier of this Room type. Can be used to identify desired adjacencies.
        /// </summary>
        public int ResourceID { get; set; }

        /// <summary>
        /// A UUID for this Room instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

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
                if (Perimeter != null)
                {
                    return Perimeter.Area / DesignArea;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// A Space created from Room characteristics.
        /// </summary>
        public Space AsSpace
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                var space = new Space(Perimeter, Elevation, Height, new Material(Guid.NewGuid().ToString(), Color));
                space.AddParameter("Name", new Parameter(this.Name, ParameterType.Text));
                space.AddParameter("Area", new Parameter(this.Perimeter.Area, ParameterType.Area));
                return space;
            }
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin.
        /// </summary>
        /// <returns>
        /// A new rectilinear Polygon derived either from fixed DesignX and DesignY dimensions or as a rectilinear target area of a random ratio between 1 and 2 of the Room's X to Y dimensions.
        /// </returns>
        public Polygon MakePerimeter(Vector3 moveTo = null)
        {
            if (DesignX > 0.0 && DesignY > 0.0)
            {
                Perimeter = Shaper.PolygonBox(DesignX, DesignY);
            }
            else
            {
                Perimeter = Shaper.AreaFromCorner(DesignArea, Shaper.RandomDouble(1, 2));
            }
            if (moveTo != null)
            {
                Perimeter = Perimeter.MoveFromTo(new Vector3(), moveTo);
            }
            return Perimeter;
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors.
        /// </summary>
        /// <returns>
        /// A new rectilinear Polygon derived from the axis and the width.
        /// </returns>
        public Polygon MakePerimeter(Line axis, double width)
        {
            if (width <= 0.0)
            {
                throw new ArgumentException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
            }
            Perimeter = axis.Thicken(width);
            return Perimeter;
        }

        /// <summary>
        /// Creates and sets a rectangular Room Perimeter with dimensions derived from two points and a width. Intended for creating corridors.
        /// </summary>
        /// <returns>
        /// A new rectilinear Polygon derived from the axis and the width.
        /// </returns>
        public Polygon MakePerimeter(Vector3 start, Vector3 end, double width)
        {
            if (width <= 0.0)
            {
                throw new ArgumentException(Messages.NONPOSITIVE_VALUE_EXCEPTION);
            }
            Perimeter = new Line(start, end).Thicken(width);
            return Perimeter;
        }
    }
}
