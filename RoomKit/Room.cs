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
        /// Rendering color of the room.
        /// </summary>
        private Color color;
        /// <summary>
        /// Height of the room prism.
        /// </summary>
        private double height;
        /// <summary>
        /// The 2D Polygon perimeter of the Room. When the perimeter has been set, the Room is assumed to be placed in its desired location.
        /// </summary>
        private Polygon perimeter;
        /// <summary>
        /// A list of Resource ID integers indicating the desired adjacencies of this Room type to other Room types.
        /// </summary>

        public int[] AdjacentTo { get; }
        /// <summary>
        /// Public property of color, required to allow setting an initial value.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set
            {
                if (value == null)
                {
                    color = Colors.White;
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
        public double DesignArea { get; }
        /// <summary>
        /// The desired x-axis dimension of this Room. Overrides DesignArea if DesignY is also set to a positive value.
        /// </summary>
        public double DesignX { get; }
        /// <summary>
        /// The desired y-axis dimension of this Room. Overrides DesignArea if DesignX is also set to a positive value.
        /// </summary>
        public double DesignY { get; }
        /// <summary>
        /// The vertical position of the Room's lowest plane, parallel to the ground plane.
        /// </summary>
        public double Elevation { get; set; }
        /// <summary>
        /// Public property of the height of the Room prism. Required to allow error checking for new heights.
        /// </summary>
        public double Height
        {
            get { return height; }
            set
            {
                if (value > 0.0)
                {
                    height = value;
                }
            }
        }
        /// <summary>
        /// Arbitrary string identifier for this Room instance. Has no effect on Room operations.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Public property of the 2D Polygon perimeter of the Room. Required to allow error checking for a non-null perimeter.
        /// </summary>
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
        public int ResourceID { get; }
        /// <summary>
        /// A UUID for this Room instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        /// <summary>
        /// Constructor setting all internal variables to default values.
        /// </summary>
        public Room()
        {
            AdjacentTo = null;
            Color = Colors.White;
            DesignArea = 1.0;
            DesignX = 0.0;
            DesignY = 0.0;
            Elevation = 0.0;
            Height = 3.0;
            Name = "";
            Perimeter = null;
            ResourceID = 0;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor setting the area of the Room.
        /// </summary>
        public Room(string name = "",
                    int resourceID = -1,
                    double designArea = 1.0,
                    Color color = null,
                    int[] adjacentTo = null)
        {
            if (designArea <= 0)
            {
                throw new ArgumentException("Design area must be greater that zero.");
            }
            AdjacentTo = adjacentTo;
            Color = color;
            DesignArea = designArea;
            DesignX = 0.0;
            DesignY = 0.0;
            Elevation = 0.0;
            Height = 3.0;
            Name = name;
            ResourceID = resourceID;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor setting the perimeter of the Room.
        /// </summary>
        public Room(Polygon perimeter,
                    string name = "",
                    Color color = null)
        {
            AdjacentTo = null;
            Color = color;
            DesignArea = perimeter.Area;
            DesignX = 0.0;
            DesignY = 0.0;
            Elevation = 0.0;
            Height = 3.0;
            Name = name;
            Perimeter = perimeter;
            ResourceID = -1;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor setting the X and Y diemsions of a Room.
        /// </summary>
        public Room(string name = "",
                    int resourceID = -1,
                    double designX = 1.0,
                    double designY = 1.0,
                    Color color = null,
                    int[] adjacentTo = null)
        {
            if (designX <= 0)
            {
                throw new ArgumentException("DesignX dimension must be greater that zero.");
            }
            if (designY <= 0)
            {
                throw new ArgumentException("DesignY dimension must be greater that zero.");
            }
            AdjacentTo = adjacentTo;
            Color = color;
            DesignArea = 0.0;
            DesignX = designX;
            DesignY = designY;
            Elevation = 0.0;
            Height = 3.0;
            Name = name;
            ResourceID = resourceID;
            UniqueID = Guid.NewGuid().ToString();
        }

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
        /// Creates a Polygon perimeter at the origin with dimensions derived from Room characteristics. Assumes the Perimeter will be relocated and so omits setting the Room's Perimeter. 
        /// </summary>
        /// <returns>
        /// A new rectilinear Polygon derived either from fixed dimensions or as a rectilinear target area of a randomly determined ratio between 1 and 2 between the Room's X and Y dimensions.
        /// </returns>
        public Polygon MakePerimeter()
        {
            if (Perimeter != null)
            {
                return Perimeter;
            }
            if (DesignX > 0.0 && DesignY > 0.0)
            {
                return Shaper.PolygonBox(DesignX, DesignY);
            }
            else
            {
                return Shaper.AreaFromCorner(DesignArea, Shaper.RandomDouble(1, 2));
            }
        }
    }
}
