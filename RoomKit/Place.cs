using System;
using System.Collections.Generic;
using System.Linq;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Rooms 2D Polygons in various spatial relationships to each other.
    /// </summary>
    public static class Place
    {
        /// <summary>
        /// Attempts to place a supplied Polygon adjacent to another Polygon, aligning bounding box corners at the orthogonal bounding box axis. Optionally restricts Polygon placement within a perimeter and/or avoiding intersection with a supplied list of Polygons.
        /// </summary>
        /// <param name="polygon">The Polygon to be placed adjacent to another Polygon.</param>
        /// <param name="adjTo">The Polygon adjacent to which the new Polygon will be located.</param>
        /// <param name="within">The Polygon that must cover the resulting Polygon.</param>
        /// <param name="among">The collection of Polygons that must not intersect the resulting Polygon.</param>
        /// <returns>
        /// A new Polygon or null if the conditions of placement cannot be satisfied.
        /// </returns>
        public static Polygon Adjacent(Polygon polygon,
                                       Polygon adjTo,
                                       Polygon within = null,
                                       IList<Polygon> among = null)
        {
            var tryPolygon = N(polygon, adjTo, within, among);
            if (tryPolygon == null)
            {
                tryPolygon = E(polygon, adjTo, within, among);
            }
            if (tryPolygon == null)
            {
                tryPolygon = S(polygon, adjTo, within, among);
            }
            if (tryPolygon == null)
            {
                tryPolygon = W(polygon, adjTo, within, among);
            }
            return tryPolygon;
        }

        /// <summary>
        /// Attempts to place a supplied Polygon in a position relative to another Polygon, using specified paired bounding box orientation points on each Polygon. Optionally restricts Polygon placement within a perimeter and/or avoiding intersection with a supplied list of Polygons.
        /// </summary>
        /// <param name="polygon">The Polygon to be placed adjacent to another Polygon.</param>
        /// <param name="oPolygon">The Polygon TopoBox orientation to use as an insertion point.</param>
        /// <param name="adjTo">The Polygon adjacent to which the new Polygon will be located.</param>
        /// <param name="oAdjTo">The Polygon TopoBox orientation to use as a placement point.</param>
        /// <param name="within">The Polygon that must cover the resulting Polygon.</param>
        /// <param name="among">The collection of Polygons that must not intersect the resulting Polygon.</param>
        /// <param name="rotateToFit">Boolean indicating whether the Polygon should be rotated to fit.</param>
        /// <returns>
        ///  A new Polygon or null if the conditions of placement cannot be satisfied.
        /// </returns>
        public static Polygon ByOrient(Polygon polygon, 
                                       Orient oPolygon, 
                                       Polygon adjTo,   
                                       Orient oAdjTo,
                                       Polygon within = null,
                                       IList<Polygon> among = null,
                                       bool rotateToFit = false)
        {
            var tryPolygon = polygon.MoveFromTo(polygon.Box().PointBy(oPolygon), adjTo.Box().PointBy(oAdjTo));
            if (tryPolygon.Fits(within, among))
            {
                return tryPolygon;
            }
            else if (rotateToFit)
            {
                var t = new Transform();
                t.Rotate(Vector3.ZAxis, 90);
                polygon = t.OfPolygon(polygon);
                tryPolygon = polygon.MoveFromTo(polygon.Box().PointBy(oPolygon), adjTo.Box().PointBy(oAdjTo));
                if (tryPolygon.Fits(within, among))
                {
                    return tryPolygon;
                }
            }
            return null;
        }

        /// <summary>
        /// Places a Polygon north of another Polygon, attempting to align first the S and N bounding box points, then SW and NW corners, and finally SE to NE points.
        /// </summary>
        /// <param name="polygon">The Polygon to be placed adjacent to another Polygon.</param>
        /// <param name="adjTo">The Polygon adjacent to which the new Polygon will be located.</param>
        /// <param name="within">The Polygon that must cover the resulting Polygon.</param>
        /// <param name="among">The collection of Polygons that must not intersect the resulting Polygon.</param>
        /// <returns>
        ///  A new Polygon or null if the conditions of placement cannot be satisfied.
        /// </returns>
        public static Polygon N(Polygon polygon, 
                                Polygon adjTo,
                                Polygon within = null,
                                IList<Polygon> among = null)
        {
            var tryPolygon = ByOrient(polygon, Orient.S, adjTo, Orient.N, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            tryPolygon = ByOrient(polygon, Orient.SW, adjTo, Orient.NW, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            return ByOrient(polygon, Orient.SE, adjTo, Orient.NE, within, among, true);
        }

        /// <summary>
        /// Places a Polygon south of another Polygon, attempting to align first the N and S bounding box points, then NW and SW corners, and finally NE to SE points.
        /// </summary>
        /// <param name="polygon">The Polygon to be placed adjacent to another Polygon.</param>
        /// <param name="adjTo">The Polygon adjacent to which the new Polygon will be located.</param>
        /// <param name="within">The Polygon that must cover the resulting Polygon.</param>
        /// <param name="among">The collection of Polygons that must not intersect the resulting Polygon.</param>
        /// <returns>
        ///  A new Polygon or null if the conditions of placement cannot be satisfied.
        /// </returns>
        public static Polygon S(Polygon polygon,
                                Polygon adjTo,
                                Polygon within = null,
                                IList<Polygon> among = null)
        {
            var tryPolygon = ByOrient(polygon, Orient.N, adjTo, Orient.S, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            tryPolygon = ByOrient(polygon, Orient.NW, adjTo, Orient.SW, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            return ByOrient(polygon, Orient.NE, adjTo, Orient.SE, within, among, true);
        }

        /// <summary>
        /// Places a Polygon west of another Polygon, attempting to align first the E and W bounding box points, then NE and NW corners, and finally SE to SW points.
        /// </summary>
        /// <param name="polygon">The Polygon to be placed adjacent to another Polygon.</param>
        /// <param name="adjTo">The Polygon adjacent to which the new Polygon will be located.</param>
        /// <param name="within">The Polygon that must cover the resulting Polygon.</param>
        /// <param name="among">The collection of Polygons that must not intersect the resulting Polygon.</param>
        /// <returns>
        ///  A new Polygon or null if the conditions of placement cannot be satisfied.
        /// </returns>
        public static Polygon W(Polygon polygon,
                                Polygon adjTo,
                                Polygon within = null,
                                IList<Polygon> among = null)
        {
            var tryPolygon = ByOrient(polygon, Orient.E, adjTo, Orient.W, within, among, true); ;
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            tryPolygon = ByOrient(polygon, Orient.NE, adjTo, Orient.NW, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            return ByOrient(polygon, Orient.SE, adjTo, Orient.SW, within, among, true);
        }

        /// <summary>
        /// Places a Polygon east of another Polygon, attempting to align first the W and E bounding box points, then NW and NE corners, and finally SW to SE points.
        /// </summary>
        /// <param name="polygon">The Polygon to be placed adjacent to another Polygon.</param>
        /// <param name="adjTo">The Polygon adjacent to which the new Polygon will be located.</param>
        /// <param name="within">The Polygon that must cover the resulting Polygon.</param>
        /// <param name="among">The collection of Polygons that must not intersect the resulting Polygon.</param>
        /// <returns>
        ///  A new Polygon or null if the conditions of placement cannot be satisfied.
        /// </returns>
        public static Polygon E(Polygon polygon,
                                Polygon adjTo,
                                Polygon within = null,
                                IList<Polygon> among = null)
        {
            var tryPolygon = ByOrient(polygon, Orient.W, adjTo, Orient.E, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            tryPolygon = ByOrient(polygon, Orient.NW, adjTo, Orient.NE, within, among, true);
            if (tryPolygon != null)
            {
                return tryPolygon;
            }
            return ByOrient(polygon, Orient.SW, adjTo, Orient.SE, within, among, true);
        }
    }

}
