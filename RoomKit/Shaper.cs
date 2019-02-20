using System;
using System.Collections.Generic;
using System.Text;
using ClipperLib;
using System.Linq;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Utilities for creating and editing Polygons.
    /// </summary>
    public static class Shaper
    {
        /// <summary>
        /// Creates a rectangular Polygon of the supplied length to width proportion at the supplied area with its southwest corner at the origin.
        /// </summary>
        /// <param name="ratio">The ratio of width to depth</param>
        /// <param name="area">The required area of the Polygon.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon AreaFromCorner(double area, double ratio)
        {
            var x = Math.Sqrt(area * ratio);
            var y = area / x;
            return PolygonBox(x, y);
        }

        /// <summary>
        /// Attempts to expand a Polygon horizontally until coming within the tolerance percentage of the target area.
        /// </summary>
        /// <param name="polygon">The Polygon to expand to the specified area.</param>
        /// <param name="area">The target area of the Polygon.</param>
        /// <param name="within">The optional Polygon acting as a constraining outer boundary.</param>
        /// <param name="among">The optional list of Polygons to avoid intersecting.</param>
        /// <param name="tolerance">The area total tolerance.</param>
        /// <param name="trials">The number of times to attempt to scale the Polygon to the desired area.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon ExpandtoArea(Polygon polygon,
                                           double area,
                                           Polygon within = null,
                                           List<Polygon> among = null,
                                           double tolerance = 0.1,
                                           int trials = 20)
        {
            var i = 0;
            var position = polygon.Centroid;
            Polygon tryPoly = null;
            do
            {
                var factor = Math.Sqrt(area / polygon.Area);
                var t = new Transform();
                t.Scale(new Vector3(factor, factor));
                tryPoly = t.OfPolygon(polygon);
                var centroid = tryPoly.Centroid;
                tryPoly = tryPoly.MoveFromTo(centroid, position);
                if (within != null && tryPoly.Intersects(within))
                {
                    tryPoly = within.Intersection(tryPoly).First();
                }
                if (among != null && tryPoly.Intersects(among))
                {
                    tryPoly = tryPoly.Diff(among).First();
                }
                i++;
            }
            while ((tryPoly.Area < area - (area * tolerance) || tryPoly.Area > area + (area * tolerance)) && i < trials);
            if (i == trials)
            {
                return polygon;
            }
            return tryPoly;
        }

        /// <summary>
        /// Creates a new Polygon fitted within a supplied perimeter and conforming to supplied intersecting Polygons.
        /// </summary>
        /// <param name="polygon">The Polygon to fit to the context.</param>
        /// <param name="within">The optional Polygon acting as a constraining outer boundary.</param>
        /// <param name="among">The optional list of Polygons against which this Polygon must conform.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon FitTo(Polygon polygon,
                                    Polygon within = null,
                                    List<Polygon> among = null)
        {
            if (within != null && polygon.Intersects(within))
            {
                var polygons = within.Intersection(polygon);
                if (polygons != null && polygons.Count > 0)
                {
                    polygon = polygons.First();
                }
            }
            if (among != null && polygon.Intersects(among))
            {
                var polygons = polygon.Difference(among);
                if (polygons != null && polygons.Count > 0)
                {
                    polygon = polygons.First();
                }
            }
            return polygon;
        }

        /// <summary>
        /// Tests whether the supplied Vector3 point falls within or on any Polygon in the supplied collection when compared on a shared plane.
        /// </summary>
        /// <param name="point">The Vector3 point to test.</param>
        /// <param name="polygons">The collection of Polygons to test for point coincidence.</param>
        /// <returns>
        /// True if the supplied Vector3 point falls within or on the perimeter of any of supplied Polygon.
        /// </returns>
        public static bool PointWithin(Vector3 point, IList<Polygon> polygons)
        {
            foreach(Polygon polygon in polygons)
            {
                if (polygon.Covers(point))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates an orthogonal box Polygon with the southwest corner at the origin.
        /// </summary>
        /// <param name="sizeX">The x dimension of the Polygon.</param>
        /// <param name="sizeY">The y dimension of the Polygon.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonBox(double sizeX, double sizeY)
        {
            if (sizeX <= 0 || sizeY <= 0)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            return
                new Polygon
                (
                    new []
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(sizeX, 0.0),
                        new Vector3(sizeX, sizeY),
                        new Vector3(0.0, sizeY)
                    }
                );
        }

        /// <summary>
        /// Creates an C-shaped Polygon within a specified rectangle with its southwest corner at the origin.
        /// </summary>
        /// <param name="origin">The southwest enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonC(Vector3 origin, Vector3 size, double width)
        {
            if (size.X <= 0 || size.Y <= 0 || width >= size.X || width * 3 >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var halfWidth = width * 0.5;
            var xAxis = size.Y * 0.5;
            var polygon =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(),
                        new Vector3(size.X, 0),
                        new Vector3(size.X, width),
                        new Vector3(width, width),
                        new Vector3(width, size.Y - width),
                        new Vector3(size.X, size.Y - width),
                        new Vector3(size.X, size.Y),
                        new Vector3(0, size.Y),
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates an E-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The southwest enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonE(Vector3 origin, Vector3 size, double width)
        {
            if(size.X <= 0 || size.Y <= 0 || width >= size.X || width * 3 >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var halfWidth = width * 0.5;
            var xAxis = size.Y * 0.5;
            var polygon = 
                new Polygon
                (
                    new []
                    {
                        new Vector3(),
                        new Vector3(size.X, 0),
                        new Vector3(size.X, width),
                        new Vector3(width, width),
                        new Vector3(width, xAxis - halfWidth),//
                        new Vector3(size.X, xAxis - halfWidth),
                        new Vector3(size.X, xAxis + halfWidth),
                        new Vector3(width, xAxis + halfWidth),
                        new Vector3(width, size.Y - width),//
                        new Vector3(size.X, size.Y - width),
                        new Vector3(size.X, size.Y),
                        new Vector3(0, size.Y),
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates an F-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The initial enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonF(Vector3 origin, Vector3 size, double width)
        {
            if (size.X <= 0 || size.Y <= 0 || width >= size.X || width * 2 >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var halfWidth = width * 0.5;
            var xAxis = size.Y * 0.5;
            var polygon =
                new Polygon
                (
                    new []
                    {
                        new Vector3(),
                        new Vector3(width, origin.Y),
                        new Vector3(width, xAxis - halfWidth),
                        new Vector3(size.X, xAxis - halfWidth),
                        new Vector3(size.X, xAxis + halfWidth),
                        new Vector3(width, xAxis + halfWidth),
                        new Vector3(width, size.Y - width),
                        new Vector3(size.X, size.Y - width),
                        new Vector3(size.X, size.Y),
                        new Vector3(0, size.Y),
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates an H-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The initial enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <param name="offset">Positive or negative displacement of the H crossbar from the shape meridian.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonH(Vector3 origin, Vector3 size, double width)
        {
            if (size.X <= 0 || size.Y <= 0 || width * 2 >= size.X || width >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var halfWidth = width * 0.5;
            var xAxis = size.Y * 0.5;
            var rightWest = size.X - width;
            var polygon = 
                new Polygon
                (
                    new []
                    {
                        new Vector3(),
                        new Vector3(width, 0),
                        new Vector3(width, xAxis - halfWidth),
                        new Vector3(rightWest, xAxis - halfWidth),
                        new Vector3(rightWest, 0),
                        new Vector3(size.X, 0),
                        new Vector3(size.X, size.Y),
                        new Vector3(rightWest, size.Y),
                        new Vector3(rightWest, xAxis + halfWidth),
                        new Vector3(width, xAxis + halfWidth),
                        new Vector3(width, size.Y),
                        new Vector3(0, size.Y),
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates an L-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The initial enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonL(Vector3 origin, Vector3 size, double width)
        {
            if (size.X <= 0 || size.Y <= 0 || width >= size.X || width >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var polygon =
                new Polygon
                (
                    new []
                    {
                        origin,
                        new Vector3(size.X, 0),
                        new Vector3(size.X, width),
                        new Vector3(width, width),
                        new Vector3(width, size.Y),
                        new Vector3(0, size.Y)
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates a T-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The initial enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonT(Vector3 origin, Vector3 size, double width)
        {
            if (size.X <= 0 || size.Y <= 0 || width >= size.X || width >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var halfWidth = width * 0.5;
            var yAxis = origin.X + (size.X * 0.5);
            var polygon =
                new Polygon
                (
                    new []
                    {
                        new Vector3(yAxis - halfWidth, 0),
                        new Vector3(yAxis + halfWidth, 0),
                        new Vector3(yAxis + halfWidth, size.Y - width),
                        new Vector3(size.X, size.Y - width),
                        new Vector3(size.X, size.Y),
                        new Vector3(0, size.Y),
                        new Vector3(0, size.Y - width),
                        new Vector3(yAxis - halfWidth, size.Y - width)
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates U-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The initial enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonU(Vector3 origin, Vector3 size, double width)
        {
            if (size.X <= 0 || size.Y <= 0 || width * 2 >= size.X || width >= size.Y)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var polygon =
                new Polygon
                (
                    new []
                    {
                        origin,
                        new Vector3(size.X, origin.Y),
                        new Vector3(size.X, origin.Y + size.Y),
                        new Vector3(size.X - width, origin.Y + size.Y),
                        new Vector3(size.X - width, origin.Y + width),
                        new Vector3(width, origin.Y + width),
                        new Vector3(width, origin.Y + size.Y),
                        new Vector3(origin.X, origin.Y + size.Y)
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates an X-shaped Polygon within a specified rectangle.
        /// </summary>
        /// <param name="origin">The initial enclosing box corner.</param>
        /// <param name="size">The positive x and y delta defining the size of the enclosing box.</param>
        /// <param name="width">Width of each stroke of the shape.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonX(Vector3 origin, Vector3 size, double width)
        {
            if (width >= Math.Abs(size.X - origin.X) || width >= Math.Abs(size.Y - origin.Y))
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var halfWidth = width * 0.5;
            var xAxis = origin.Y + (size.Y * 0.5);
            var yAxis = origin.X + (size.X * 0.5);
            var polygon = 
                new Polygon
                (               
                    new []
                    {
                        new Vector3(yAxis - halfWidth, 0),
                        new Vector3(yAxis + halfWidth, 0),
                        new Vector3(yAxis + halfWidth, xAxis - halfWidth),
                        new Vector3(size.X, xAxis - halfWidth),
                        new Vector3(size.X, xAxis + halfWidth),
                        new Vector3(yAxis + halfWidth, xAxis + halfWidth),
                        new Vector3(yAxis + halfWidth, size.Y),
                        new Vector3(yAxis - halfWidth, size.Y),
                        new Vector3(yAxis - halfWidth, xAxis + halfWidth),
                        new Vector3(0, xAxis + halfWidth),
                        new Vector3(0, xAxis - halfWidth),
                        new Vector3(yAxis - halfWidth, xAxis - halfWidth)
                    }
                );
            Transform movTrans = new Transform();
            movTrans.Move(origin);
            return movTrans.OfPolygon(polygon);
        }

        /// <summary>
        /// Creates a regular Polygon inscribed within the supplied radius from the supplied center.
        /// </summary>
        /// <param name="center">The Vector3 center point of the Polygon.</param>
        /// <param name="radius">The radius of the inscribed Polygon.</param>
        /// <param name="sides">The number of sides of the inscribed Polygon.</param>
        /// <returns>
        /// A new regular Polygon.
        /// </returns>

        public static Polygon PolygonRegular(Vector3 center, double radius, int sides = 3)
        {
            if (radius <= 0 || sides < 3)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var vertices = new List<Vector3>();
            var angle = Math.PI * 0.5;
            var nxtAngle = Math.PI * 2 / sides;
            for (int i = 0; i < sides; i++)
            {
                var x = center.X + (radius * Math.Cos(angle));
                var y = center.Y + (radius * Math.Sin(angle));
                vertices.Add(new Vector3(x, y));
                angle += nxtAngle;
            }
            return new Polygon(vertices.ToArray());
        }

        /// <summary>
        /// Returns a random double within the supplied range.
        /// </summary>
        /// <param name="minValue">The lower bound of the random range.</param>
        /// <param name="minValue">The upper bound of the random range.</param>
        /// <returns>
        /// A random double within the range.
        /// </returns>
        public static double RandomDouble(double minvalue, double maxvalue)
        {
            var scale = 1000000.0;
            var rnd = new Random();
            double next = rnd.Next((int)Math.Round(minvalue * scale), (int)Math.Round(maxvalue * scale));
            return next / scale;
        }
    }
}
