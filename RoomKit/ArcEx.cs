using System;
using System.Collections.Generic;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Extends Elements.Geometry.Arc with utility methods.
    /// </summary>
    public static class ArcEx
    {
        /// <summary>
        /// Creates a collection of Vector3 points representing the division of the linear geometry into the supplied number of segments.
        /// </summary>
        /// <param name="segments">The quantity of desired segments.</param>
        /// <returns>
        /// A List of Vector3 points.
        /// </returns>
        public static IList<Vector3> Divide(this Arc arc, int segments)
        {
            var pointList = new List<Vector3>()
            {
                arc.Start
            };
            var percent = 1.0 / segments;
            var factor = 1;
            var at = percent * factor;
            for (int i = 0; i < segments; i++)
            {
                pointList.Add(arc.PointAt(at));
                at = percent * ++factor;
            }
            return pointList;
        }
    }
}
