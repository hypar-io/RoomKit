using System;
using System.Collections.Generic;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Extends Elements.Geometry.Line with utility methods.
    /// </summary>
    public static class LineEx
    {
        /// <summary>
        /// Creates a collection of Vector3 points representing the division of the linear geometry into the supplied number of segments.
        /// </summary>
        /// <param name="segments">The quantity of desired segments.</param>
        /// <returns>
        /// A List of Vector3 points.
        /// </returns>
        public static IList<Vector3> Divide(this Line line, int segments)
        {
            var pointList = new List<Vector3>()
            {
                line.Start
            };
            var percent = 1.0 / segments;
            var factor = 1;
            var at = percent * factor;
            for (int i = 0; i < segments; i++)
            {
                pointList.Add(line.PointAt(at));
                at = percent * ++factor;
            }
            return pointList;
        }

        /// <summary>
        /// Creates a new line from the supplied line rotated around the supplied pivot point by the specified angle in degrees.
        /// </summary>
        /// <param name="line">The Line instance to be copied.</param>
        /// <param name="pivot">The Vector3 base point of the rotation.</param>
        /// <param name="angle">The desired rotation angle in degrees.</param>
        /// <returns>
        /// A new Line.
        /// </returns>
        public static Line Rotate(this Line line, Vector3 pivot, double angle)
        {
            var theta = angle * (Math.PI / 180);
            var sX = (Math.Cos(theta) * (line.Start.X - pivot.X)) - (Math.Sin(theta) * (line.Start.Y - pivot.Y)) + pivot.X;
            var sY = (Math.Sin(theta) * (line.Start.X - pivot.X)) + (Math.Cos(theta) * (line.Start.Y - pivot.Y)) + pivot.Y;
            var eX = (Math.Cos(theta) * (line.End.X - pivot.X)) - (Math.Sin(theta) * (line.End.Y - pivot.Y)) + pivot.X;
            var eY = (Math.Sin(theta) * (line.End.X - pivot.X)) + (Math.Cos(theta) * (line.End.Y - pivot.Y)) + pivot.Y;
            return new Line(new Vector3(sX, sY), new Vector3(eX, eY));
        }

        /// <summary>
        /// Returns a new line displaced from the supplied line along a vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="line">The Line instance to be copied.</param>
        /// <param name="from">The Vector3 base point of the move.</param>
        /// <param name="to">The Vector3 target point of the move.</param>
        /// <returns>
        /// A new Line.
        /// </returns>
        public static Line MoveFromTo(this Line line, Vector3 from, Vector3 to)
        {
            var v = new Vector3(to.X - from.X, to.Y - from.Y);
            var start = line.Start + v;
            return new Line(line.Start + v, line.End + v);
        }
    }
}
