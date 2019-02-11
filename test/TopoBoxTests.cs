using System.Collections.Generic;
using Xunit;
using Hypar.Elements;
using Hypar.Geometry;

namespace HyparSpaces.Tests
{
    public class TopoBoxTests
    {
        [Fact]
        public void TopoBoxCreate()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(4.0, 0.0),
                    new Vector3(4.0, 4.0),
                    new Vector3(0.0, 4.0)
                }
            );
            var box = new TopoBox(polygon);
            Assert.Equal(0.0, box.SW.X);
            Assert.Equal(0.0, box.SW.Y);
            Assert.Equal(1.0, box.SSW.X);
            Assert.Equal(0.0, box.SSW.Y);
            Assert.Equal(2.0, box.S.X);
            Assert.Equal(0.0, box.S.Y);
            Assert.Equal(3.0, box.SSE.X);
            Assert.Equal(0.0, box.SSE.Y);
            Assert.Equal(4.0, box.SE.X);
            Assert.Equal(0.0, box.SE.Y);
            Assert.Equal(4.0, box.ESE.X);
            Assert.Equal(1.0, box.ESE.Y);
            Assert.Equal(4.0, box.E.X);
            Assert.Equal(2.0, box.E.Y);
            Assert.Equal(4.0, box.ENE.X);
            Assert.Equal(3.0, box.ENE.Y);
            Assert.Equal(4.0, box.NE.X);
            Assert.Equal(4.0, box.NE.Y);
            Assert.Equal(3.0, box.NNE.X);
            Assert.Equal(4.0, box.NNE.Y);
            Assert.Equal(2.0, box.N.X);
            Assert.Equal(4.0, box.N.Y);
            Assert.Equal(1.0, box.NNW.X);
            Assert.Equal(4.0, box.NNW.Y);
            Assert.Equal(0.0, box.NW.X);
            Assert.Equal(4.0, box.NW.Y);
            Assert.Equal(0.0, box.WNW.X);
            Assert.Equal(3.0, box.WNW.Y);
            Assert.Equal(0.0, box.W.X);
            Assert.Equal(2.0, box.W.Y);
            Assert.Equal(0.0, box.WSW.X);
            Assert.Equal(1.0, box.WSW.Y);
        }
    }
}