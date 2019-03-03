using System.Collections.Generic;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class ArcExTests
    {
        [Fact]
        public void Divide()
        {
            var arc = new Arc(new Plane(Vector3.Origin, Vector3.ZAxis), 150.0, 0.0, 180.0);
            Assert.Equal(25.0, arc.Divide(24).Count);
        }
    }
}
