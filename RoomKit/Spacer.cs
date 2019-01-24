using System;
using System.Collections.Generic;
using Hypar.Elements;
using Hypar.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Copies and places Hypar Spaces in various spatial relationships to each other.
    /// </summary>
    public static class Spacer
    {
        /// <summary>
        /// Returns a copy of the supplied Space displaced by the supplied Vector3.
        /// </summary>
        /// <param name="space">The Space instance to be copied.</param>
        /// <param name="moveBy">Displacement of each Space from the previous instance.</param>
        /// <returns>
        /// A new space.
        /// </returns>
        public static Space CopyPlace(Space space, Vector3 moveBy)
        {
            var vertices = new List<Vector3>();
            foreach (Vector3 vertex in space.Profile.Perimeter.Vertices)
            {
                vertices.Add(new Vector3(vertex.X + moveBy.X, vertex.Y + moveBy.Y));
            }
            var polygon = new Polygon(vertices);
            var elevation = space.Elevation + moveBy.Z;
            var movedSpace = new Space(new Profile(polygon), elevation, space.Height, space.Material);
            foreach(string key in space.Parameters.Keys)
            {
                movedSpace.AddParameter(key, (Parameter)space.Parameters[key]);
            }
            return movedSpace;
        }

        /// <summary>
        /// Returns the supplied Space and one or more copies displaced by the supplied vector from the previously created Space as many times as necessary for the aggregate area of created spaces to equal or exceed the supplied area value.
        /// </summary>
        /// <param name="space">The Space instance to be copied.</param>
        /// <param name="area">The target area to be reached by the aggregate total areas of all created Spaces.</param>
        /// <param name="moveBy">Displacement of each Space from the previous instance.</param>
        /// <returns>
        /// IEnumerable<Space>
        /// </returns>
        public static IList<Space> CopyToArea(Space space, double area, Vector3 moveBy)
        {
            if (area <= 0)
            {
                throw new ArgumentOutOfRangeException("Area value must be greater than zero.", "area");
            }
            var spaces = new List<Space> { space };
            var spaceArea = space.Profile.Area;
            if (spaceArea >= area)
            {
                return spaces;
            }
            for(int i = 0; i < (int)Math.Round(area / spaceArea); i++)
            {
                var moveNxt = moveBy * (i + 1);
                spaces.Add(CopyPlace(space, moveNxt));
            }
            return spaces;
        }

        /// <summary>
        /// Returns the supplied Space and one or more copies stacked vertically by the Space.Height + gap.
        /// </summary>
        /// <param name="space">The Space instance to be copied.</param>
        /// <param name="copies">The quantity of new Spaces to be created.</param>
        /// <param name="gap">Additional vertical displacement added to the supplied Space.Height.</param>
        /// <returns>
        /// IEnumerable<Space>
        /// </returns>
        public static IList<Space> Stack(Space space, int copies, double gap = 0.0)
        {
            if (copies < 1)
            {
                throw new ArgumentOutOfRangeException("Copies value must be greater than zero.", "copies");
            }
            var spaces = new List<Space>()
            {
                space
            };
            var moveBy = new Vector3(0.0, 0.0, space.Height + gap);
            for (int i = 0; i < copies; i++)
            {
                var moveNxt = moveBy * (i + 1);
                spaces.Add(CopyPlace(space, moveNxt));
            }
            return spaces;
        }

        /// <summary>
        /// Returns the supplied Space and one or more copies stacked vertically from the previously created Space as many times as necessary for the aggregate area of created spaces to equal or exceed the supplied area value.
        /// </summary>
        /// <param name="space">The Space instance to be copied.</param>
        /// <param name="copies">The quantity of new Spaces to be created.</param>
        /// <param name="gap">Additional vertical displacement added to the supplied Space.Height.</param>
        /// <returns>
        /// IEnumerable<Space>
        /// </returns>
        public static IList<Space> StackToArea(Space space, double area, double gap = 0.0)
        {
            if (area <= 0)
            {
                throw new ArgumentOutOfRangeException("Area value must be greater than zero.", "area");
            }
            var spaceArea = space.Profile.Area;
            var spaces = new List<Space>()
            {
                space
            };
            if (spaceArea >= area)
            {
                return spaces;
            }
            int copies = (int)Math.Round(area / spaceArea) - 1;
            var moveBy = new Vector3(0.0, 0.0, space.Height + gap);
            for (int i = 0; i < copies; i++)
            {
                var moveNxt = moveBy * (i + 1);
                spaces.Add(CopyPlace(space, moveNxt));
            }
            return spaces;
        }
    }
}
