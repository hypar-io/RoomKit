using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class ShaperTests
    {
        [Fact]
        public void ExpandToArea()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(),
                    new Vector3(4, 0),
                    new Vector3(4, 4),
                    new Vector3(0, 4)
                }
            );
            var within = new Polygon
            (
                new[]
                {
                    new Vector3(-5, -5),
                    new Vector3(20, -5),
                    new Vector3(20, 30),
                    new Vector3(-5, 30)
                }
            );
            var among = new List<Polygon>
            {
                new Polygon(
                    new []
                    {
                        new Vector3(3, 1),
                        new Vector3(7, 1),
                        new Vector3(7, 5),
                        new Vector3(3, 5)
                    }),
                new Polygon(
                    new[]
                    {
                        new Vector3(1, 3),
                        new Vector3(2, 3),
                        new Vector3(2, 6),
                        new Vector3(1, 6),
                    })
            };
            var coordGrid = new CoordGrid(within);
            polygon = Shaper.ExpandtoArea(polygon, 20, within, among);
            Debug.WriteLine(polygon.Area);
            var spaces = new List<Space>
            {
                new Space(polygon, 3.0, 2, new Material("blue", Palette.Blue)),
                new Space(within, 3.0, 0.1, new Material("aqua", Palette.Aqua)),
                new Space(among[0], 3.0, 2, new Material("yellow", Palette.Yellow)),
                new Space(among[1], 3.0, 2, new Material("green", Palette.Green))
            };
            var model = new Model();
            foreach (Space space in spaces)
            {
                model.AddElement(space);
            }
            model.SaveGlb("../../../../expandToArea.glb");
        }

        [Fact]
        public void PointWithin()
        {
            var polygons = new List<Polygon>
            {
                new Polygon
                (
                    new []
                    {
                        new Vector3(),
                        new Vector3(8.0, 0.0),
                        new Vector3(8.0, 9.0),
                        new Vector3(0.0, 9.0)
                    }
                ),
                new Polygon
                (
                    new []
                    {
                        new Vector3(52.0, 0.0),
                        new Vector3(60.0, 0.0),
                        new Vector3(60.0, 6.0),
                        new Vector3(52.0, 6.0)
                    }
                ),
                new Polygon
                (
                    new []
                    {
                        new Vector3(24.0, 33.0),
                        new Vector3(32.0, 33.0),
                        new Vector3(32.0, 36.0),
                        new Vector3(24.0, 36.0)
                    }
                )
            };
            var point1 = new Vector3(-1.1, -1.1);
            var point2 = new Vector3(26.2, 34.2);
            var point3 = new Vector3(54.5, 3.3);

            Assert.False(Shaper.PointWithin(point1, polygons));
            Assert.True(Shaper.PointWithin(point2, polygons));
            Assert.True(Shaper.PointWithin(point3, polygons));
        }

        [Fact]
        public void PolygonBox()
        {
            var vertices = Shaper.PolygonBox(10.0, 10.0).Vertices;

            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 10.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 10.0 && p.Y == 10.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 10.0);
        }

        [Fact]
        public void PolygonC()
        {
            var polygon = Shaper.PolygonC(new Vector3(2.0, 2.0), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 6.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 6.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 7.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 7.0);
        }

        [Fact]
        public void PolygonE()
        {
            var polygon = Shaper.PolygonE(new Vector3(2.0, 2.0), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 4.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 6.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 6.0);
            Assert.Contains(vertices, p => p.X == 5.0 && p.Y == 7.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 7.0);
        }

        [Fact]
        public void PolygonF()
        {
            var polygon = Shaper.PolygonF(new Vector3(), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 4.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 4.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 5.0);
        }

        [Fact]
        public void PolygonH()
        {
            var polygon = Shaper.PolygonH(new Vector3(), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 5.0);
        }

        [Fact]
        public void PolygonL()
        {
            var polygon = Shaper.PolygonL(new Vector3(), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 1.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 1.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 5.0);
        }

        [Fact]
        public void PolygonT()
        {
            var polygon = Shaper.PolygonT(new Vector3(), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 4.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 4.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 4.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 4.0);
        }

        [Fact]
        public void PolygonU()
        {
            var polygon = Shaper.PolygonU(new Vector3(), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 1.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 1.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 5.0);
        }

        [Fact]
        public void PolygonX()
        {
            var polygon = Shaper.PolygonX(new Vector3(), new Vector3(3.0, 5.0), 1.0);
            var vertices = polygon.Vertices;
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 3.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 2.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 5.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 3.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 2.0);
            Assert.Contains(vertices, p => p.X == 1.0 && p.Y == 2.0);
        }
    }
}
