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
            var line = new Line(Vector3.Origin, new Vector3(0.0, 150.0));
            Assert.Equal(25.0, line.Divide(24).Count);
        }

        [Fact]
        public void Intersection ()
        {
            var line = new Line(new Vector3(1.0, 1.0), new Vector3(6.0, 6.0));
            var intr = new Line(new Vector3(1.0, 5.0), new Vector3(5.0, 1.0));
            var point = line.Intersection(intr);
            Assert.Equal(3.0, point.X);
            Assert.Equal(3.0, point.Y);
            line = new Line(new Vector3(1.0, 3.0), new Vector3(5.0, 7.0));
            intr = new Line(new Vector3(3.0, 1.0), new Vector3(3.0, 7.0));
            point = line.Intersection(intr);
            Assert.Equal(3.0, point.X);
            Assert.Equal(5.0, point.Y);
            line = new Line(new Vector3(2.0, 4.0), new Vector3(9.0, 4.0));
            intr = new Line(new Vector3(3.0, 1.0), new Vector3(8.0, 6.0));
            point = line.Intersection(intr);
            Assert.Equal(6.0, point.X);
            Assert.Equal(4.0, point.Y);
        }

        [Fact]
        public void MoveFromTo()
        {
            var line = new Line(Vector3.Origin, new Vector3(0.0, 150));
            var moved = line.MoveFromTo(Vector3.Origin, new Vector3(0.0, 150.0));
            Assert.Equal(0.0, moved.End.X);
            Assert.Equal(300.0, moved.End.Y);
        }

        [Fact]
        public void Rotate()
        {
            var line = new Line(Vector3.Origin, new Vector3(5.0, 0.0));
            var rotated = line.Rotate(Vector3.Origin, 90.0);
            Assert.Equal(0.0, rotated.End.X, 10);
            Assert.Equal(5.0, rotated.End.Y, 10);
        }
    }
}
