﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using RoomKit;
using GeometryEx;

namespace RoomKitTest
{
    public class RoomTests
    {
        //[Fact]
        //public void AdjacentTo()
        //{
        //    var room = new Room
        //    {
        //        AdjacentTo = new List<string>{ "5", "10", "18" }
        //    };
        //    Assert.Equal("5", room.AdjacentTo[0]);
        //    Assert.Equal("10", room.AdjacentTo[1]);
        //    Assert.Equal("18", room.AdjacentTo[2]);
        //}

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

        //[Fact]
        //public void AreaVariance()
        //{
        //    var room = new Room
        //    {
        //        DesignArea = 50.0,
        //        Perimeter =
        //            new Polygon(
        //                new[]
        //                {
        //                    new Vector3(0.0, 0.0),
        //                    new Vector3(10.0, 0.0),
        //                    new Vector3(10.0, 10.0),
        //                    new Vector3(0.0, 10.0)
        //                })
        //    };
        //    Assert.Equal(2.0, room.AreaVariance);
        //}



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
        public void Rotate()
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
            room.Rotate(Vector3.Origin, 90);
            Assert.Contains(new Vector3(-10.0, 0.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(-10.0, 10.0), room.Perimeter.Vertices);
        }

        //[Fact]
        //public void SizeXY()
        //{
        //    var room = new Room
        //    {
        //        Perimeter =
        //            new Polygon(
        //                new[]
        //                {
        //                    new Vector3(3.0, 0.0),
        //                    new Vector3(6.0, 3.0),
        //                    new Vector3(3.0, 6.0),
        //                    new Vector3(0.0, 3.0)
        //                })
        //    };
        //    Assert.Equal(6.0, room.SizeX);
        //    Assert.Equal(6.0, room.SizeY);
        //}

        [Fact]
        public void Number()
        {
            var room = new Room
            {
                Number = "12"
            };
            Assert.Equal("12", room.Number);
        }

        [Fact]
        public void UniqueID()
        {
            var room = new Room();
            Assert.NotNull(room.UniqueID);
        }

        //[Fact]
        //public void MoveFromTo()
        //{
        //    var room = new Room();
        //    Assert.True(room.SetDimensions(new Vector3(10.0, 10.0, 4.0), new Vector3(10.0, 10.0)));
        //    Assert.Equal(4.0, room.Height);
        //    room.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20.0));
        //    Assert.Contains(new Vector3(30.0, 30.0, 0.0), room.Perimeter.Vertices);
        //    Assert.Equal(0.0, room.Elevation);
        //}


        //[Fact]
        //public void SetDimensions()
        //{
        //    var room = new Room();
        //    Assert.True(room.SetDimensions(new Vector3(10.0, 10.0, 4.0), new Vector3(10.0, 10.0)));
        //    Assert.Equal(4.0, room.Height);
        //    Assert.Contains(new Vector3(10.0, 10.0), room.Perimeter.Vertices);
        //    Assert.Contains(new Vector3(20.0, 20.0), room.Perimeter.Vertices);
        //}

    }
}
