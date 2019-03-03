using System;
using System.Collections.Generic;
using System.Linq;
using Elements.Geometry;
using ClipperLib;

namespace RoomKit
{
    public static class PolygonEx
    {
        /// <summary>
        /// The ratio of the longer side to the shorter side of the Polygon's bounding box.
        /// </summary>
        public static double AspectRatio(this Polygon polygon)
        {
            var box = polygon.Box();
            if (box.SizeX >= box.SizeY)
            {
                return box.SizeX / box.SizeY;
            }
            else
            {
                return box.SizeY / box.SizeX;
            }
        }

        /// <summary>
        /// Returns a TopoBox representation of the Polygon's bounding box.
        /// </summary>
        public static TopoBox Box(this Polygon polygon)
        {
            return new TopoBox(polygon);
        }

        /// <summary>
        /// Returns a list of Vector3 points representig the corners of the Polygon's orthogonal bounding box.
        /// </summary>
        public static List<Vector3> BoxCorners(this Polygon polygon)
        {
            var box = new TopoBox(polygon);
            return new List<Vector3>
            {
                box.SW,
                box.SE,
                box.NE,
                box.NW
            };
        }

        /// <summary>
        /// Tests if the supplied Polygon resides in a corner of a Polygon perimeter.
        /// </summary>
        /// <param name="polygon">The Polygon to test.</param>
        /// <param name="perimeter">The Polygon to test against.</param>
        /// <returns>
        /// Returns true if exactly three of the polygon bounding box points fall on the Polygon perimeter bounding box.
        /// </returns>
        public static bool AtCorner(this Polygon polygon, Polygon perimeter)
        {
            var count = 0;
            var box = new TopoBox(perimeter);
            var boundary = Shaper.PolygonBox(box.SizeX, box.SizeY).MoveFromTo(Vector3.Origin, box.SW);
            foreach (Vector3 vertex in BoxCorners(polygon))
            {
                if (boundary.Touches(vertex))
                {
                    count++;
                }
            }
            if (count != 3)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tests if the supplied Polygon resides against an edge of a Polygon perimeter.
        /// </summary>
        /// <param name="polygon">The Polygon to test.</param>
        /// <param name="perimeter">The Polygon to test against.</param>
        /// <returns>
        /// Returns true if exactly two of the Polygon bounding box points fall on or outside the perimeter and exactly two bounding box points fall inside the perimeter.
        /// </returns>
        public static bool AtEdge(this Polygon polygon, Polygon perimeter)
        {
            var countOn = 0;
            var countIn = 0;
            foreach (Vector3 vertex in BoxCorners(polygon))
            {
                if (perimeter.Contains(vertex))
                {
                    countIn++;
                }
                else if (perimeter.Touches(vertex) || perimeter.Disjoint(vertex))
                {
                    countOn++;
                }
            }
            if (countOn != countIn)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tests if the bounding box of the supplied Polygon fills a side of the perimeter.
        /// </summary>
        /// <param name="perimeter">The Polygon to test against.</param>
        /// <returns>
        /// Returns true if all Polygon bounding box points fall on the perimeter or on its bounding box.
        /// </returns>
        public static bool AtSide(this Polygon polygon, Polygon perimeter)
        {
            var count = 0;
            var box = new TopoBox(perimeter);
            var boundary = Shaper.PolygonBox(box.SizeX, box.SizeY).MoveFromTo(Vector3.Origin, box.SW);
            foreach (Vector3 vertex in BoxCorners(polygon))
            {
                if (boundary.Touches(vertex) || perimeter.Touches(vertex))
                {
                    count++;
                }
            }
            if (count != 4)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tests if the supplied Vector3 point is within this Polygon without coincidence with an edge when compared on a shared plane.
        /// </summary>
        /// <param name="point">The Vector3 point to compare to this Polygon.</param>
        /// <returns>
        /// Returns true if the supplied Vector3 point is within this Polygon when compared on a shared plane. Returns false if the Vector3 point is outside this Polygon or if the supplied Vector3 point is null.
        /// </returns>
        public static bool Contains(this Polygon polygon, Vector3 point)
        {
            if (point == null)
            {
                return false;
            }
            var thisPath = ToClipperPath(polygon);
   
            var intPoint = new IntPoint(point.X * scale, point.Y * scale);
            if (Clipper.PointInPolygon(intPoint, thisPath) != 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tests if the supplied Vector3 point is within this Polygon or coincident with an edge when compared on a shared plane.
        /// </summary>
        /// <param name="point">The Vector3 point to compare to this Polygon.</param>
        /// <returns>
        /// Returns true if the supplied Vector3 point is within this Polygon or coincident with an edge when compared on a shared plane. Returns false if the supplied point is outside this Polygon, or if the supplied Vector3 point is null.
        /// </returns>
        public static bool Covers(this Polygon polygon, Vector3 point)
        {
            if (point == null)
            {
                return false;
            }
            var thisPath = ToClipperPath(polygon);
            var intPoint = new IntPoint(point.X * scale, point.Y * scale);
            if (Clipper.PointInPolygon(intPoint, thisPath) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tests if the supplied Polygon is within this Polygon with or without edge coincident vertices when compared on a shared plane.
        /// </summary>
        /// <param name="polygon">The Polygon to compare to this Polygon.</param>
        /// <returns>
        /// Returns true if every vertex of the supplied Polygon is within this Polygon or coincident with an edge when compared on a shared plane. Returns false if any vertex of the supplied Polygon is outside this Polygon, or if the supplied Polygon is null.
        /// </returns>
        private static bool Covers(Polygon cover, Polygon polygon)
        {
            if (polygon == null)
            {
                return false;
            }
            var clipper = new Clipper();
            var solution = new List<List<IntPoint>>();
            clipper.AddPath(cover.ToClipperPath(), PolyType.ptSubject, true);
            clipper.AddPath(polygon.ToClipperPath(), PolyType.ptClip, true);
            clipper.Execute(ClipType.ctUnion, solution);
            if (solution.Count != 1)
            {
                return false;
            }
            return Math.Abs(solution.First().ToPolygon().Area - cover.ToClipperPath().ToPolygon().Area) <= 0.0001;
        }

        /// <summary>
        /// Constructs the geometric difference between this Polygon and the supplied Polygons.
        /// </summary>
        /// <param name="difPolys">The list of intersecting Polygons.</param>
        /// <returns>
        /// Returns a list of Polygons representing the subtraction of the supplied Polygons from this Polygon.
        /// Returns null if the area of this Polygon is entirely subtracted.
        /// Returns a list containing a representation of the perimeter of this Polygon if the two Polygons do not intersect.
        /// </returns>
        public static IList<Polygon> Difference(this Polygon polygon, IList<Polygon> difPolys)
        {
            var thisPath = ToClipperPath(polygon);
            var polyPaths = new List<List<IntPoint>>();
            foreach (Polygon poly in difPolys)
            {
                polyPaths.Add(poly.ToClipperPath());
            }
            Clipper clipper = new Clipper();
            clipper.AddPath(thisPath, PolyType.ptSubject, true);
            clipper.AddPaths(polyPaths, PolyType.ptClip, true);
            var solution = new List<List<IntPoint>>();
            clipper.Execute(ClipType.ctDifference, solution);
            if (solution.Count == 0)
            {
                return null;
            }
            var polygons = new List<Polygon>();
            foreach (List<IntPoint> path in solution)
            {
                polygons.Add(ToPolygon(path.Distinct().ToList()));
            }
            return polygons;
        }

        /// <summary>
        /// Tests if the supplied Vector3 point is outside this Polygon when compared on a shared plane.
        /// </summary>
        /// <param name="point">The Vector3 point to compare to this Polygon.</param>
        /// <returns>
        /// Returns true if the supplied Vector3 point is outside this Polygon when compared on a shared plane or if the supplied Vector3 point is null.
        /// </returns>
        public static bool Disjoint(this Polygon polygon, Vector3 point)
        {
            if (point == null)
            {
                return true;
            }
            var thisPath = ToClipperPath(polygon);
            var intPoint = new IntPoint(point.X * scale, point.Y * scale);
            if (Clipper.PointInPolygon(intPoint, thisPath) != 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tests whether a Polygon is covered by a Polygon perimeter and doesn't intersect with a list of Polygons.
        /// </summary>
        /// <param name="polygon">The Polygon to test.</param>
        /// <param name="perimeter">The covering perimeter.</param>
        /// <param name="among">The list of Polygons to check for intersection.</param>
        /// <returns>
        /// True if the Polygon is covered by the perimeter and does not intersect with any Polygon in the supplied list.
        /// </returns>
        public static bool Fits(this Polygon polygon, Polygon within = null, IList<Polygon> among = null)
        {
            if (within != null && !Covers(within, polygon))
            {
                return false;
            }
            return !polygon.Intersects(among);
        }

        /// <summary>
        /// Tests if any of the supplied Polygons share one or more areas with this Polygon when compared on a shared plane.
        /// </summary>
        /// <param name="polygons">The Polygon to compare with this Polygon.</param>
        /// <returns>
        /// Returns true if any of the supplied Polygons share one or more areas with this Polygon when compared on a shared plane or if the list of supplied Polygons is null. Returns false if the none of the supplied Polygons share an area with this Polygon or if the supplied list of Polygons is null.
        /// </returns>
        public static bool Intersects(this Polygon polygon, IList<Polygon> polygons)
        {
            if (polygons == null)
            {
                return false;
            }
            foreach (Polygon poly in polygons)
            {
                if (polygon.Intersects(poly))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a new Polygon displaced along a 2D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="polygon">Polygon instance to be copied.</param>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// A new Polygon.
        /// </returns>
        public static Polygon MoveFromTo(this Polygon polygon, Vector3 from, Vector3 to)
        {
            var t = new Transform();
            t.Move(new Vector3(to.X - from.X, to.Y - from.Y));
            return t.OfPolygon(polygon);
        }

        /// <summary>
        /// Tests if the supplied Vector3 point is coincident with an edge of this Polygon when compared on a shared plane.
        /// </summary>
        /// <param name="point">The Vector3 point to compare to this Polygon.</param>
        /// <returns>
        /// Returns true if the supplied Vector3 point coincides with an edge of this Polygon when compared on a shared plane. Returns false if the supplied Vector3 point is not coincident with an edge of this Polygon, or if the supplied Vector3 point is null.
        /// </returns>
        public static bool Touches(this Polygon polygon, Vector3 point)
        {
            if (point == null)
            {
                return false;
            }
            var thisPath = ToClipperPath(polygon);
            var intPoint = new IntPoint(point.X * scale, point.Y * scale);
            if (Clipper.PointInPolygon(intPoint, thisPath) != -1)
            {
                return false;
            }
            return true;
        }

        private const double scale = 1024.0;

        /// <summary>
        /// Construct a clipper path from a Polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static List<IntPoint> ToClipperPath(this Polygon p)
        {
            var path = new List<IntPoint>();
            foreach (var v in p.Vertices)
            {
                path.Add(new IntPoint(v.X * scale, v.Y * scale));
            }
            return path.Distinct().ToList();
        }

        /// <summary>
        /// Construct a Polygon from a clipper path 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static Polygon ToPolygon(this List<IntPoint> p)
        {
            return new Polygon(p.Select(v => new Vector3(v.X / scale, v.Y / scale)).Distinct().ToArray());
        }
    }
}
