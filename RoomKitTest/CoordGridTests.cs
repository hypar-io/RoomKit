using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class CoordGridTests
    {
        [Fact]
        public void CoordGrid()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(60, 0),
                    new Vector3(60, 36),
                    new Vector3(0, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            Assert.Equal(2257, grid.Available.Count);
        }

        [Fact]
        public void Allocate()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(60, 0),
                    new Vector3(60, 36),
                    new Vector3(0, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            Assert.Equal(2257, grid.Available.Count);
            var allocate1 = new Polygon
            (
                new[]
                {
                    new Vector3(10, 10),
                    new Vector3(20, 10),
                    new Vector3(20, 20),
                    new Vector3(10, 20)
                }
            );
            grid.Allocate(allocate1);
            Assert.Equal(2136, grid.Available.Count);
            var allocate2 = new Polygon
            (
                new[]
                {
                    new Vector3(30, 10),
                    new Vector3(40, 10),
                    new Vector3(40, 30),
                    new Vector3(30, 30)
                }
            );
            grid = new CoordGrid(perimeter);
            var allocate = new List<Polygon> { allocate1, allocate2 };
            grid.Allocate(allocate);
            Assert.Equal(1905, grid.Available.Count);
        }

        [Fact]
        public void AllocatedNearTo()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(60, 0),
                    new Vector3(60, 36),
                    new Vector3(0, 36)
                }
            );
            var allocated = new List<Polygon>
            {
                new Polygon
                (
                    new []
                    {
                        Vector3.Origin,
                        new Vector3(8, 0),
                        new Vector3(8, 9),
                        new Vector3(0, 9)
                    }
                ),
                new Polygon
                (
                    new []
                    {
                        new Vector3(52, 0),
                        new Vector3(60, 0),
                        new Vector3(60, 6),
                        new Vector3(52, 6)
                    }
                ),
                new Polygon
                (
                    new []
                    {
                        new Vector3(24, 33),
                        new Vector3(32, 33),
                        new Vector3(32, 36),
                        new Vector3(24, 36)
                    }
                )
            };
            var grid = new CoordGrid(perimeter);
            foreach(Polygon polygon in allocated)
            {
                grid.Allocate(polygon);
            }
            var nearPoint = grid.AllocatedNearTo(new Vector3(26.6, 34.1));
            Assert.Equal(27, nearPoint.X);
            Assert.Equal(36, nearPoint.Y);
        }

        [Fact]
        public void AllocatedRandom()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(60, 0),
                    new Vector3(60, 36),
                    new Vector3(0, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            var allocate = new Polygon
            (
                new[]
                {
                    new Vector3(10, 10),
                    new Vector3(20, 10),
                    new Vector3(20, 20),
                    new Vector3(10, 20)
                }
            );
            grid.Allocate(allocate);
            var point = grid.AllocatedRandom();
            Assert.Contains(point, grid.Allocated);
            Assert.Equal(17.0, point.X);
            Assert.Equal(12.0, point.Y);
        }

        [Fact]
        public void AvailableMax()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(5, 5),
                    new Vector3(60, 5),
                    new Vector3(60, 36),
                    new Vector3(5, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            var max = grid.AvailableMax();
            Assert.Equal(60, max.X);
            Assert.Equal(36, max.Y);
        }

        [Fact]
        public void AvailableMin()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(5, 5),
                    new Vector3(60, 5),
                    new Vector3(60, 36),
                    new Vector3(5, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            var min = grid.AvailableMin();
            Assert.Equal(5, min.X);
            Assert.Equal(5, min.Y);
        }

        [Fact]
        public void AvailableNearTo()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(60, 0),
                    new Vector3(60, 36),
                    new Vector3(0, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            var nearPoint = grid.AvailableNearTo(new Vector3(50.6, 40.1));
            Assert.Equal(51, nearPoint.X);
            Assert.Equal(36, nearPoint.Y);
        }

        [Fact]
        public void AvailableRandom()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0, 0),
                    new Vector3(60, 0),
                    new Vector3(60, 36),
                    new Vector3(0, 36)
                }
            );
            var grid = new CoordGrid(perimeter);
            var allocate = new Polygon
            (
                new[]
                {
                    new Vector3(10, 10),
                    new Vector3(20, 10),
                    new Vector3(20, 20),
                    new Vector3(10, 20)
                }
            );
            grid.Allocate(allocate);
            var point = grid.AvailableRandom();
            Assert.Contains(point, grid.Available);
            Assert.Equal(42.0, point.X);
            Assert.Equal(8.0, point.Y);
        }
    }
}
