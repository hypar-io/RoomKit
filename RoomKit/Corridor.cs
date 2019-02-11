using System;
using System.Collections.Generic;
using System.Linq;
using Hypar.Elements;
using Hypar.Geometry;

namespace RoomKit
{
    /// <summary>
    /// A data structure recording room characteristics.
    /// </summary>
    public class Corridor
    {
        public const double personWidth = 0.570;

        /// <summary>
        /// Rendering color.
        /// </summary>
        private Color color;

        /// <summary>
        /// Height of the prism.
        /// </summary>
        private double height;

        /// <summary>
        /// The 2D Polygon perimeter.
        /// </summary>
        private Polygon perimeter;

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
        
        public double Elevation { get; set; }

        /// <summary>
        /// Public property of the height of the prism.
        /// </summary>
        public double Height
        {
            get { return height; }
            set
            {
                if (height <= 0.0)
                {
                    height = value;
                }
            }
        }
        /// <summary>
        /// Arbitrary string identifier for this instance.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Polyygon perimeter.
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
        /// Arbitrary integer identifier.
        /// </summary>
        public int ResourceID { get; }

        /// <summary>
        /// A UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        /// <summary>
        /// Constructor setting all internal variables to default values.
        /// </summary>
        public Corridor()
        {
            Color = Colors.White;
            Elevation = 0.0;
            Height = 3.0;
            Name = "";
            Perimeter = null;
            ResourceID = -1;
            UniqueID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor setting the path of the corridor using a Line. A supplied list of Rooms will be altered to accommodate the intersecting corridor.
        /// </summary>
        public Corridor(Line axis,
                        double width = 2.0,
                        double height = 3.0,
                        double elevation = 0.0,
                        IList<Room> rooms = null,
                        Color color = null)
        {
            Color = color;
            Elevation = elevation;
            if (height <= 0.0)
            {
                throw new ArgumentOutOfRangeException("height", "Corridor height must be a positive value.");
            }
            Height = height;
            Name = "";
            ResourceID = -1;
            UniqueID = Guid.NewGuid().ToString();
            if (width <= 0.0)
            {
                throw new ArgumentOutOfRangeException("width", "Corridor width must be a positive value.");
            }
            Perimeter = axis.Thicken(width);
            foreach (Room room in rooms)
            {
                room.Perimeter = room.Perimeter.Difference(Perimeter).First();
            }
        }

        /// <summary>
        /// Constructor setting the path of the corridor using two Vector3 points. A supplied list of Rooms will be altered to accommodate the intersecting corridor.
        /// </summary>
        public Corridor(Vector3 start,
                        Vector3 end,
                        double width = 2.0,
                        double height = 3.0,
                        double elevation = 0.0,
                        IList<Room> rooms = null,
                        Color color = null)
        {
            Color = color;
            Elevation = elevation;
            if (height <= 0.0)
            {
                throw new ArgumentOutOfRangeException("height", "Corridor height must be a positive value.");
            }
            Height = height;
            Name = "";
            ResourceID = -1;
            UniqueID = Guid.NewGuid().ToString();
            if (width <= 0.0)
            {
                throw new ArgumentOutOfRangeException("width", "Corridor width must be a positive value.");
            }
            Perimeter = new Line(start, end).Thicken(width);
            foreach (Room room in rooms)
            {
                room.Perimeter = room.Perimeter.Difference(Perimeter).First();
            }
        }

        /// <summary>
        /// The area of the perimeter Polygon. 
        /// Returns -1.0 if the Perimeter is null.
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
        /// A Space created from instance characteristics.
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
    }
}
