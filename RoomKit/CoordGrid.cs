using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClipperLib;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.
    /// </summary>
    public class CoordGrid
    {

        /// <summary>
        /// Providing a random seed value ensures reproducible results.
        /// </summary>
        private Random random;

        /// <summary>
        /// The list of vector3 allocated points.
        /// </summary>
        public List<Vector3> Allocated { get; }

        /// <summary>
        /// The list of Vector3 points available for allocation.
        /// </summary>
        public List<Vector3> Available { get; }

        /// <summary>
        /// Polygon perimeter of the grid. 
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
        /// Creates an orthogonal 2D grid of Vector3 points from the supplied Polygon and axis intervals.
        /// </summary>
        /// <param name="perimeter">The Polygon boundary of the point grid.</param>
        /// <param name="xInterval">The spacing of the grid along the x-axis.</param>
        /// <param name="yInterval">The spacing of the grid along the y-axis.</param>
        /// <returns>
        /// A new CoordGrid.
        /// </returns>
        public CoordGrid(Polygon polygon, double xInterval = 1,  double yInterval = 1, int randomSeed = 1)
        {
            random = new Random(randomSeed);
            Allocated = new List<Vector3>();
            Available = new List<Vector3>();
            Perimeter = new Polygon(polygon.Vertices);
            var box = new TopoBox(polygon);
            var x = box.SW.X;
            var y = box.SW.Y;
            while (y <= box.NW.Y)
            {
                while (x <= box.SE.X)
                {
                    var point = new Vector3(x, y);
                    if (polygon.Contains(point) || polygon.Touches(point))
                    {
                        Available.Add(point);
                    }
                    x += xInterval;
                }
                x = box.SW.X;
                y += yInterval;
            }
        }

        /// <summary>
        /// Allocates the points in the grid falling within or on the supplied Polygon.
        /// </summary>
        /// <param name="polygon">The Polygon bounding the points to be allocated.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void Allocate(Polygon polygon)
        {
            var rmvPoints = new List<int>();
            var index = 0;
            foreach (Vector3 point in Available)
            {
                if (polygon.Covers(point))
                {
                    rmvPoints.Add(index);
                    Allocated.Add(point);
                }
                index++;
            }
            rmvPoints.Reverse();
            foreach(int rmv in rmvPoints)
            {
                Available.RemoveAt(rmv);
            }
        }

        /// <summary>
        /// Allocates points in the grid falling within the supplied Polygons.
        /// </summary>
        /// <param name="polygon">The Polygon bounding the points to be allocated.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void Allocate(IList<Polygon> polygons)
        {
            var rmvPoints = new List<int>();
            var index = 0;
            var allocate = new List<Vector3>();
            foreach (Vector3 point in Available)
            {
                foreach (Polygon polygon in polygons)
                {
                    if (polygon.Covers(point))
                    {
                        allocate.Add(point);
                    }
                    index++;
                }
            }
            foreach (Vector3 point in allocate)
            {
                Available.Remove(point);
                Allocated.Add(point);
            }
        }

        /// <summary>
        /// Returns the allocated grid point nearest to the supplied point.
        /// </summary>
        /// <param name="point">The Vector3 point to compare.</param>
        /// <returns>
        /// A Vector3 point.
        /// </returns>
        public Vector3 AllocatedNearTo(Vector3 point)
        {
            var x = Allocated.First().X;
            var y = Allocated.First().Y;
            foreach (Vector3 aPoint in Allocated)
            {
                var xDelta = Math.Abs(point.X - aPoint.X);
                var yDelta = Math.Abs(point.Y - aPoint.Y);
                if (xDelta <= Math.Abs(x - aPoint.X) ||
                    yDelta <= Math.Abs(y - aPoint.Y))
                {
                    x = aPoint.X;
                    y = aPoint.Y;
                }
            }
            return new Vector3(x, y);
        }

        /// <summary>
        /// Returns a random allocated point.
        /// </summary>
        /// <returns>
        /// A Vector3 point.
        /// </returns>
        public Vector3 AllocatedRandom()
        {
            return Allocated[random.Next(0, Allocated.Count - 1)];
        }

        /// <summary>
        /// Returns the maximum available grid point.
        /// </summary>
        /// <returns>
        /// A Vector3 point.
        /// </returns>

        public Vector3 AvailableMax()
        {
            var maxX = Available.First().X;
            var maxY = Available.First().Y;
            foreach (Vector3 point in Available)
            {
                if (point.X + point.Y > maxX + maxY)
                {
                    maxX = point.X;
                    maxY = point.Y;
                }
            }
            return new Vector3(maxX, maxY);
        }

        /// <summary>
        /// Returns the minimum available grid point.
        /// </summary>
        /// <returns>
        /// A Vector3 point.
        /// </returns>

        public Vector3 AvailableMin()
        {
            var minX = Available.First().X;
            var minY = Available.First().Y;
            foreach (Vector3 point in Available)
            {
                if (point.X + point.Y < minX + minY)
                {
                    minX = point.X;
                    minY = point.Y;
                }
            }
            return new Vector3(minX, minY);
        }

        /// <summary>
        /// Returns the available grid point nearest to the supplied Vector3 point.
        /// </summary>
        /// <param name="point">The Vector3 point to compare.</param>
        /// <returns>
        /// A Vector3 point.
        /// </returns>
        public Vector3 AvailableNearTo(Vector3 point)
        {
            var x = Available.First().X;
            var y = Available.First().Y;
            foreach (Vector3 aPoint in Available)
            {
                var xDelta = Math.Abs(point.X - aPoint.X);
                var yDelta = Math.Abs(point.Y - aPoint.Y);
                if (xDelta <= Math.Abs(x - aPoint.X) ||
                    yDelta <= Math.Abs(y - aPoint.Y))
                {
                    x = aPoint.X;
                    y = aPoint.Y;
                }

            }
            return new Vector3(x, y);
        }

        /// <summary>
        /// Returns a random available grid point.
        /// </summary>
        /// <returns>
        /// A Vector3 point.
        /// </returns>
        public Vector3 AvailableRandom()
        {
            return Available[random.Next(0, Available.Count - 1)];
        }
    }
}
