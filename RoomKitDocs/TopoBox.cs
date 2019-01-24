using System;
using System.Collections.Generic;
using Hypar.Geometry;

namespace RoomKit
{
    public class TopoBox
    {
        /// <summary>
        /// Vector3 location identifiers corresponding to points on the box perimeter.
        /// </summary>
        public Vector3 C { get; }
        public Vector3 N { get; }
        public Vector3 NNW { get; }
        public Vector3 NW { get; }
        public Vector3 WNW { get; }
        public Vector3 W { get; }
        public Vector3 WSW { get; }
        public Vector3 SW { get; }
        public Vector3 SSW { get; }
        public Vector3 S { get; }
        public Vector3 SSE { get; }
        public Vector3 SE { get; }
        public Vector3 ESE { get; }
        public Vector3 E { get; }
        public Vector3 ENE { get; }
        public Vector3 NE { get; }
        public Vector3 NNE { get; }
        public double SizeX { get; }
        public double SizeY { get; }

        /// <summary>
        /// Constructor creates a new mathematical bounding box and populates all orientation points.
        /// </summary>
        public TopoBox(Polygon polygon)
        {
            var vertices = new List<Vector3>(polygon.Vertices);
            vertices.Sort((a, b) => a.X.CompareTo(b.X));
            var minX = vertices[0].X;
            vertices.Sort((a, b) => b.X.CompareTo(a.X));
            var maxX = vertices[0].X;
            vertices.Sort((a, b) => a.Y.CompareTo(b.Y));
            var minY = vertices[0].Y;
            vertices.Sort((a, b) => b.Y.CompareTo(a.Y));
            var maxY = vertices[0].Y;

            SizeX = Math.Abs(maxX - minX);
            SizeY = Math.Abs(maxY - minY);

            C = new Vector3(minX + (SizeX * 0.5), minY + (SizeY * 0.5));
            N = new Vector3(minX + (SizeX * 0.5), maxY);
            NNW = new Vector3(minX + (SizeX * 0.25), maxY);
            NW = new Vector3(minX, maxY);
            WNW = new Vector3(minX, minY + (SizeY * 0.75));
            W = new Vector3(minX, minY + (SizeY * 0.5));
            WSW = new Vector3(minX, minY + (SizeY * 0.25));
            SW = new Vector3(minX, minY);
            SSW = new Vector3(minX + (SizeX * 0.25), minY);
            S = new Vector3(minX + (SizeX * 0.5), minY);
            SSE = new Vector3(minX + (SizeX * 0.75), minY);
            SE = new Vector3(maxX, minY);
            ESE = new Vector3(maxX, minY + (SizeY * 0.25));
            E = new Vector3(maxX, minY + (SizeY * 0.5));
            ENE = new Vector3(maxX, minY + (SizeY * 0.75));
            NE = new Vector3(maxX, maxY);
            NNE = new Vector3(minX + (SizeX * 0.75), maxY);
        }

        /// <summary>
        /// Returns the requested bounding box location by orientation.
        /// </summary>
        /// <param name="orient">The Orient value to index point.</param>
        /// <returns>
        /// A 2D Vector3 point.
        /// </returns>
        public Vector3 PointBy(Orient orient)
        {
            switch(orient)
            {
                case Orient.C: return C;
                case Orient.N: return N;
                case Orient.NNW: return NNW;
                case Orient.NW: return NW;
                case Orient.WNW: return WNW;
                case Orient.W: return W;
                case Orient.WSW: return WSW;
                case Orient.SW: return SW;
                case Orient.SSW: return SSW;
                case Orient.S: return S;
                case Orient.SSE: return SSE;
                case Orient.SE: return SE;
                case Orient.ESE: return ESE;
                case Orient.E: return E;
                case Orient.ENE: return ENE;
                case Orient.NE: return NE;
                case Orient.NNE: return NNE;
            }
            return null;
        }
    }
}
