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
        /// Creates a rectilinear Polygon in the specified adjacent quadrant to the supplied Polygon's bounding box.
        /// </summary>
        /// <param name="area">The area of the new Polygon.</param>
        /// <param name="orient">The relative cardinal direction in which the new Polygon will be placed.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon AdjacentArea(Polygon polygon, double area, Orient orient)
        {
            var box = new TopoBox(polygon);
            double sizeX = 0.0;
            double sizeY = 0.0;
            if (orient == Orient.N || orient == Orient.S)
            {
                sizeX = box.SizeX;
                sizeY = area / box.SizeX;
            }
            else
            {
                sizeX = area / box.SizeY;
                sizeY = box.SizeY;
            }
            Vector3 origin = null;
            switch (orient)
            {
                case Orient.N:
                    origin = box.NW;
                    break;
                case Orient.E:
                    origin = box.SE;
                    break;
                case Orient.S:
                    origin = new Vector3(box.SW.X, box.SW.Y - sizeY);
                    break;
                case Orient.W:
                    origin = new Vector3(box.SW.X - sizeX, box.SW.Y);
                    break;
            }
            return
                new Polygon
                (
                    new[]
                    {
                        origin,
                        new Vector3(origin.X + sizeX, origin.Y),
                        new Vector3(origin.X + sizeX, origin.Y + sizeY),
                        new Vector3(origin.X, origin.Y + sizeY)
                    }
                );
        }

        /// <summary>
        /// Attempts to expand a Polygon horizontally until coming within the tolerance percentage of the target area.
        /// </summary>
        /// <param name="polygon">Polygon to expand to the specified area.</param>
        /// <param name="area">Target area of the Polygon.</param>
        /// <param name="within">Polygon acting as a constraining outer boundary.</param>
        /// <param name="among">Llist of Polygons to avoid intersecting.</param>
        /// <param name="tolerance">Area total tolerance.</param>
        /// <param name="trials">Number of times to attempt to scale the Polygon to the desired area.</param>
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
                    tryPoly = tryPoly.Difference(among).First();
                }
                i++;
            }
            while ((tryPoly.Area < area - (area * tolerance) || tryPoly.Area > area + (area * tolerance)) && i < trials);
            return tryPoly;
        }

        /// <summary>
        /// Creates a new list of Polygons fitted within a supplied perimeter and conforming to supplied intersecting Polygons.
        /// </summary>
        /// <param name="polygon">Polygon to fit to the context.</param>
        /// <param name="within">Polygon acting as a constraining outer boundary.</param>
        /// <param name="among">List of Polygons against which this Polygon must conform.</param>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public static List<Polygon> FitTo(Polygon polygon,
                                          Polygon within = null,
                                          List<Polygon> among = null)
        {
            var polyWithin = new List<Polygon>();
            if (within != null && polygon.Intersects(within))
            {
                polyWithin.AddRange(within.Intersection(polygon));
            }
            else
            {
                polyWithin.Add(polygon);
            }
            if (among == null)
            {
                return polyWithin;
            }
            var polyAmong = new List<Polygon>();
            foreach (Polygon poly in polyWithin)
            {
                var polygons = poly.Difference(among);
                if (polygons != null)
                {
                    polyAmong.AddRange(polygons);
                }
                else
                {
                    polyAmong.Add(poly);
                }
            }
            return polyAmong;
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
        public static Polygon PolygonBox(double sizeX, double sizeY, Vector3 moveTo = null)
        {
            if (sizeX <= 0 || sizeY <= 0)
            {
                throw new ArgumentOutOfRangeException(Messages.POLYGON_SHAPE_EXCEPTION);
            }
            var polygon = 
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
            if (moveTo != null)
            {
                return polygon.MoveFromTo(Vector3.Origin, moveTo);
            }
            return polygon;
        }

        /// <summary>
        /// Creates a rectangular Polygon of the supplied length to width proportion at the supplied area with its southwest corner at the origin.
        /// </summary>
        /// <param name="area">Required area of the Polygon.</param>
        /// <param name="ratio">Ratio of width to depth.</param>
        /// <param name="moveTo">Location of the southwest corner of the new Polygon.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon PolygonByArea(double area, double ratio, Vector3 moveTo = null)
        {
            var x = Math.Sqrt(area * ratio);
            var y = area / x;
            if (moveTo != null)
            {
                return PolygonBox(x, y).MoveFromTo(Vector3.Origin, moveTo);
            }
            return PolygonBox(x, y);
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
                        Vector3.Origin,
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
                        Vector3.Origin,
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
                        Vector3.Origin,
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
                        Vector3.Origin,
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
            if (radius <= 0.0 || sides < 3)
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
