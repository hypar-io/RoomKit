using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class RoomTests
    {
        [Fact]
        public void AdjacentTo()
        {
            var room = new Room
            {
                AdjacentTo = new[] { 5, 10, 18 }
            };
            Assert.Equal(5, room.AdjacentTo[0]);
            Assert.Equal(10, room.AdjacentTo[1]);
            Assert.Equal(18, room.AdjacentTo[2]);
        }

        [Fact]
        public void Area()
        {
            var room = new Room
            {
                Perimeter =
                    new Polygon(
                        new[]
                        {
                            new Vector3(0.0, 0.0),
                            new Vector3(10.0, 0.0),
                            new Vector3(10.0, 10.0),
                            new Vector3(0.0, 10.0)
                        })
            };
            Assert.Equal(100.0, room.Area);
        }

        [Fact]
        public void AreaVariance()
        {
            var room = new Room
            {
                DesignArea = 50.0,
                Perimeter =
                    new Polygon(
                        new[]
                        {
                            new Vector3(0.0, 0.0),
                            new Vector3(10.0, 0.0),
                            new Vector3(10.0, 10.0),
                            new Vector3(0.0, 10.0)
                        })
            };
            Assert.Equal(2.0, room.AreaVariance);
        }

        [Fact]
        public void AsSpace()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(20.0, 0.0),
                    new Vector3(20.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );
            var room = new Room()
            {
                Color = Palette.Green,
                Elevation = 0.0,
                Height = 5.0,
                Perimeter = polygon,
            };
            var model = new Model();
            model.AddElement(room.AsSpace);
            model.SaveGlb("../../../../RoomAsSpace.glb");
        }

        [Fact]
        public void Color()
        {
            var room = new Room
            {
                Color = Palette.Green
            };
            Assert.Equal(Palette.Green, room.Color);
            room.Color = Palette.Yellow;
            Assert.Equal(Palette.Yellow, room.Color);
            room.Color = null;
            Assert.Equal(Palette.Yellow, room.Color);
        }

        [Fact]
        public void DesignArea()
        {
            var room = new Room
            {
                DesignArea = 20
            };
            Assert.Equal(20, room.DesignArea);
            room.DesignArea = -20;
            Assert.Equal(20, room.DesignArea);
        }

        [Fact]
        public void DesignXYZ()
        {
            var room = new Room
            {
                DesignXYZ = new Vector3(5.0, 6.0, 3.0)
            };
            Assert.Equal(5.0, room.DesignLength);
            Assert.Equal(6.0, room.DesignWidth);
            Assert.Equal(3.0, room.Height);
            Assert.Equal(30, room.DesignArea);
        }

        [Fact]
        public void Elevation()
        {
            var room = new Room
            {
                Elevation = 15
            };
            Assert.Equal(15, room.Elevation);
            room.Elevation = -15;
            Assert.Equal(-15, room.Elevation);
        }

        [Fact]
        public void Height()
        {
            var room = new Room();
            room.Height = 5.1;
            Assert.Equal(5.1, room.Height, 10);
            room.Height = -5;
            Assert.Equal(5.1, room.Height, 10);
        }

        [Fact]
        public void Name()
        {
            var room = new Room
            {
                Name = "Test"
            };
            Assert.Equal("Test", room.Name);
        }

        [Fact]
        public void Perimeter()
        {
            var room = new Room
            {
                Perimeter =
                    new Polygon(
                        new[]
                        {
                            new Vector3(0.0, 0.0),
                            new Vector3(10.0, 0.0),
                            new Vector3(10.0, 10.0),
                            new Vector3(0.0, 10.0)
                        })
            };
            Assert.Contains(new Vector3(0.0, 10.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(10.0, 10.0), room.Perimeter.Vertices);
            room.Perimeter = null;
            Assert.Contains(new Vector3(0.0, 0.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(10.0, 0.0), room.Perimeter.Vertices);
        }

        [Fact]
        public void Placed()
        {
            var room = new Room
            {
                Placed = true
            };
            Assert.True(room.Placed);
        }

        [Fact]
        public void SizeXY()
        {
            var room = new Room
            {
                Perimeter =
                    new Polygon(
                        new[]
                        {
                            new Vector3(3.0, 0.0),
                            new Vector3(6.0, 3.0),
                            new Vector3(3.0, 6.0),
                            new Vector3(0.0, 3.0)
                        })
            };
            Assert.Equal(6.0, room.SizeX);
            Assert.Equal(6.0, room.SizeY);
        }

        [Fact]
        public void TypeID()
        {
            var room = new Room
            {
                TypeID = 12
            };
            Assert.Equal(12, room.TypeID);
        }

        [Fact]
        public void UniqueID()
        {
            var room = new Room();
            Assert.NotNull(room.UniqueID);
        }


        [Fact]
        public void SetDimensions()
        {
            var room = new Room();
            Assert.True(room.SetDimensions(new Vector3(10.0, 10.0, 4.0), new Vector3(10.0, 10.0)));
            Assert.Equal(4.0, room.Height);
            Assert.Contains(new Vector3(10.0, 10.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(20.0, 20.0), room.Perimeter.Vertices);
        }


        [Fact]
        public void SetPerimeter()
        {
            var room = new Room();
            Assert.True(room.SetPerimeter());
            Assert.Contains(Vector3.Origin, room.Perimeter.Vertices);
            Assert.Contains(new Vector3(1.0, 1.0), room.Perimeter.Vertices);

            Assert.True(room.SetPerimeter(new Vector3(10.0, 10.0)));
            Assert.Contains(new Vector3(10.0, 10.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(11.0, 11.0), room.Perimeter.Vertices);

            Assert.True(room.SetPerimeter(9.0, 1.0, new Vector3(10.0, 10.0)));
            Assert.Equal(9.0, room.Area, 10);
            Assert.Equal(3.0, room.SizeX, 10);
            Assert.Contains(new Vector3(10.0, 10.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(13.0, 13.0), room.Perimeter.Vertices);

            Assert.True(room.SetPerimeter(new Line(Vector3.Origin, new Vector3(10.0, 0.0)), 5.0));
            Assert.Equal(50.0, room.Area, 10);
            Assert.Equal(10.0, room.SizeX, 10);
            Assert.Equal(5.0, room.SizeY, 10);

            Assert.True(room.SetPerimeter(Vector3.Origin, new Vector3(10.0, 0.0), 5.0));
            Assert.Equal(50.0, room.Area, 10);
            Assert.Equal(10.0, room.SizeX, 10);
            Assert.Equal(5.0, room.SizeY, 10);
        }
    }
}
