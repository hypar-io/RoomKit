using System.Collections.Generic;
using Xunit;
using Hypar.Elements;
using Hypar.Geometry;

namespace HyparSpaces.Tests
{
    public class SpacerTests
    {
        [Fact]
        public void CopyPlace()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(),
                    new Vector3(30.0, 10.0),
                    new Vector3(20.0, 50.0),
                    new Vector3(-10.0, 5.0)
                }
            );
            var thisSpace = new Space(polygon);
            var vector = new Vector3(50.0, 0.0, 0.0);
            var copySpace = Spacer.CopyPlace(thisSpace, vector);
            var vertices = copySpace.Profile.Perimeter.Vertices;
            Assert.Contains(vertices, p => p.X == 50.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 80.0 && p.Y == 10.0);
            Assert.Contains(vertices, p => p.X == 70.0 && p.Y == 50.0);
            Assert.Contains(vertices, p => p.X == 40.0 && p.Y == 5.0);
        }

        [Fact]
        public void Stack()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(),
                    new Vector3(30, 10),
                    new Vector3(20, 50),
                    new Vector3(-10, 5)
                }
            );
            var thisSpace = new Space(polygon);
            var spaces = Spacer.Stack(thisSpace, 5, 5);
            var elevation = 0.0;
            foreach (Space space in spaces)
            {
                Assert.Equal(elevation, elevation);
                elevation += space.Height + 5;
            }
        }

        [Fact]
        public void StackToArea()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(),
                    new Vector3(20, 0),
                    new Vector3(20, 20),
                    new Vector3(0, 20)
                }
            );
            var space = new Space(polygon);
            var spaces = (List<Space>)Spacer.StackToArea(space, 4000, 0);
            Assert.True(spaces.Count == 10);
        }
    }
}
