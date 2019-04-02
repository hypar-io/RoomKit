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
        /// Finds the implied intersection of this line with a supplied line.
        /// </summary>
        /// <param name="intr">Line to find intersection with this Line.</param>
        /// <returns>
        /// A Vector3 point or null if the lines are parallel.
        /// </returns>
        public static Vector3 Intersection(this Line line, Line intr)
        {
            var lineSlope = (line.End.Y - line.Start.Y) / (line.End.X - line.Start.X);
            var intrSlope = (intr.End.Y - intr.Start.Y) / (intr.End.X - intr.Start.X);
            if (lineSlope == intrSlope)
            {
                return null;
            }
            if (lineSlope == double.PositiveInfinity && intrSlope == 0.0)
            {
                return new Vector3(line.Start.X, intr.Start.Y);
            }
            if (lineSlope == 0.0 && intrSlope == double.PositiveInfinity)
            {
                return new Vector3(intr.Start.X, line.Start.Y);
            }
            double lineB;
            double intrB;
            if (lineSlope == double.PositiveInfinity)
            {
                intrB = intr.End.Y - (intrSlope * intr.End.X);
                return new Vector3(line.End.X, intrSlope * line.End.X + intrB);
            }
            if (intrSlope == double.PositiveInfinity)
            {
                lineB = line.End.Y - (lineSlope * line.End.X);
                return new Vector3(intr.End.X, lineSlope * intr.End.X + lineB);
            }
            lineB = line.End.Y - (lineSlope * line.End.X);
            intrB = intr.End.Y - (intrSlope * intr.End.X);
            var x = (intrB - lineB) / (lineSlope - intrSlope);
            var y = lineSlope * x + lineB;
            return new Vector3(x, y);
        }

        /// <summary>
        /// Returns a new line displaced from the supplied line along a 2D vector calculated between the supplied Vector3 points.
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
            return new Line(line.Start + v, line.End + v);
        }

        /// <summary>
        /// Creates a new line from the supplied line rotated around the supplied pivot point by the specified angle in degrees.
        /// </summary>
        /// <param name="line">Line instance to be copied and rotated.</param>
        /// <param name="pivot">Vector3 base point of the rotation.</param>
        /// <param name="angle">Desired rotation angle in degrees.</param>
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
    }
}
