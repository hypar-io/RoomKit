using System.Collections.Generic;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class PolygonExTests
    {
        [Fact]
        public void AspectRatio()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );
            Assert.Equal(2.0, polygon.AspectRatio());
        }

        [Fact]
        public void AtCorner()
        {
            var p1 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 10.0),
                    new Vector3(0.0, 10.0)
                }
            );
            var p2 = new Polygon
            (
                new[]
                {
                    new Vector3(5.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 10.0),
                    new Vector3(5.0, 10.0)
                }
            );
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );
            Assert.True(p1.AtCorner(perimeter));
            Assert.False(p2.AtCorner(perimeter));
        }

        [Fact]
        public void AtEdge()
        {
            var p1 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 10.0),
                    new Vector3(0.0, 10.0)
                }
            );
            var p2 = new Polygon
            (
                new[]
                {
                    new Vector3(5.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 10.0),
                    new Vector3(5.0, 10.0)
                }
            );
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );
            Assert.False(p1.AtEdge(perimeter));
            Assert.True(p2.AtEdge(perimeter));
        }

        [Fact]
        public void AtSide()
        {
            var p1 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );
            var p2 = new Polygon
            (
                new[]
                {
                    new Vector3(5.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(10.0, 10.0),
                    new Vector3(5.0, 10.0)
                }
            );
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );
            Assert.True(p1.AtSide(perimeter));
            Assert.False(p2.AtSide(perimeter));
        }

        [Fact]
        public void Covers()
        {
            var p1 = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(20, 0),
                    new Vector3(20, 20),
                    new Vector3(0, 20)
                }
            );
            var p2 = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(10, 0),
                    new Vector3(10, 10),
                    new Vector3(0, 10)
                }
            );
            Assert.True(p1.Covers(p2));
            Assert.False(p1.Covers(new Vector3(-1.1, -1.1)));
            Assert.True(p1.Covers(new Vector3(2.0, 5.0)));
            Assert.True(p1.Covers(new Vector3(2.0, 0.0)));
        }

        //[Fact]
        //public void Fits()
        //{
        //    var within = new Polygon
        //    (
        //        new[]
        //        {
        //            new Vector3(0.0, 0.0),
        //            new Vector3(60.0, 0.0),
        //            new Vector3(0.0, 60.0)
        //        }
        //    );
        //    var p1 = new Polygon
        //    (
        //        new[]
        //        {
        //            new Vector3(0.0, 0.0),
        //            new Vector3(5.0, 0.0),
        //            new Vector3(5.0, 5.0),
        //            new Vector3(0.0, 5.0)
        //        }
        //    );
        //    var p2 = new Polygon
        //    (
        //        new[]
        //        {
        //            new Vector3(5.0, 0.0),
        //            new Vector3(10.0, 0.0),
        //            new Vector3(10.0, 5.0),
        //            new Vector3(5.0, 5.0)
        //        }
        //    );
        //    var p3 = new Polygon
        //    (
        //        new[]
        //        {
        //            new Vector3(55.0, 0.0),
        //            new Vector3(60.0, 0.0),
        //            new Vector3(60.0, 10.0),
        //            new Vector3(55.0, 10.0)
        //        }
        //    );
        //    var among = new List<Polygon> { p1, p2 };
        //    Assert.False(p3.Fits(within, among));
        //}

        [Fact]
        public void Fits()
        {
            var within = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(40.0, 0.0),
                    new Vector3(40.0, 40.0),
                    new Vector3(0.0, 40.0)
                }
            );
            var room = new Polygon
            (
                new[]
                {
                    new Vector3(40.0, 40.0),
                    new Vector3(34.51511960021, 40.0),
                    new Vector3(34.51511960021, 35.0773767097941),
                    new Vector3(40.0, 35.0773767097941)
                }
            );
            Assert.True(room.Fits(within, null));
        }

        [Fact]
        public void Intersects()
        {
            var p1 = new Polygon
            (
                new[]
                {
                    new Vector3(10.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 20.0),
                    new Vector3(10.0, 20.0)
                }
            );

            var p2 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 10.0),
                    new Vector3(0.0, 10.0)
                }
            );

            var p3 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(5.0, 0.0),
                    new Vector3(5.0, 5.0),
                    new Vector3(0.0, 5.0)
                }
            );

            var p4 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(5.0, 0.0),
                    new Vector3(5.0, 5.0),
                    new Vector3(0.0, 5.0)
                }
            );

            var polygons = new List<Polygon> { p2, p3 };

            Assert.True(p1.Intersects(p2));
            Assert.True(p2.Intersects(p3));
            Assert.False(p1.Intersects(p3));
            Assert.True(p3.Intersects(p4));
            Assert.True(p1.Intersects(polygons));
        }

        [Fact]
        public void Touches()
        {
            var p1 = new Polygon
            (
                new[]
                {
                    new Vector3(10.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 20.0),
                    new Vector3(10.0, 20.0)
                }
            );

            var p2 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 10.0),
                    new Vector3(0.0, 10.0)
                }
            );

            var p3 = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(10.0, 0.0),
                    new Vector3(5.0, 5.0),
                    new Vector3(0.0, 5.0)
                }
            );

            var point1 = new Vector3(-1.1, -1.1);
            var point2 = new Vector3(0.0, 5.0);

            Assert.False(p1.Touches(p2));
            Assert.True(p1.Touches(p3));
            Assert.False(p2.Touches(p3));

            Assert.False(p1.Touches(point1));
            Assert.True(p2.Touches(point2));
        }
    }
}
