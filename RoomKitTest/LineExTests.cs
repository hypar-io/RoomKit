using System.Collections.Generic;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class LineExTests
    {
        [Fact]
        public void Divide()
        {
            var line = new Line(Vector3.Origin, new Vector3(0.0, 150));
            Assert.Equal(25.0, line.Divide(24).Count);
        }

        [Fact]
        public void MoveFromTo()
        {
            var line = new Line(Vector3.Origin, new Vector3(0.0, 150));
            var moved = line.MoveFromTo(Vector3.Origin, new Vector3(0.0, 150));
            Assert.Equal(0.0, moved.End.X);
            Assert.Equal(300.0, moved.End.Y);
        }

        [Fact]
        public void Rotate()
        {
            var line = new Line(Vector3.Origin, new Vector3(5.0, 0));
            var rotated = line.Rotate(Vector3.Origin, 90);
            Assert.Equal(0.0, rotated.End.X, 10);
            Assert.Equal(5.0, rotated.End.Y, 10);
        }
    }
}
