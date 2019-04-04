using System;
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
    public class RoomGroupTests
    {
        [Fact]
        public void RoomGroup()
        {
            var polygon = Shaper.PolygonBox(60.0, 20.0);
            var roomGroup = new RoomGroup()
            {
                Perimeter = polygon
            };
            roomGroup.RoomsByDivision(4, 2, 3.5);
            var model = new Model();
            foreach (Room room in roomGroup.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            model.ToGlTF("../../../../RoomGroup.glb");
        }

        [Fact]
        public void AreaAvailable()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            Assert.Equal(1200.0, roomGroup.AreaAvailable, 10);
            roomGroup.RoomsByDivision(4, 2);
            Assert.Equal(0.0, roomGroup.AreaAvailable, 10);
        }

        [Fact]
        public void AreaPlaced()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            Assert.Equal(1200.0, roomGroup.AreaPlaced, 10);
        }

        [Fact]
        public void MoveFromTo()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20.0));
            Assert.Equal(20.0, roomGroup.Elevation);
            Assert.Contains(new Vector3(20.0, 20.0, 0.0), roomGroup.Perimeter.Vertices);
            Assert.Contains(new Vector3(80.0, 20.0, 0.0), roomGroup.Perimeter.Vertices);
            Assert.Contains(new Vector3(80.0, 40.0, 0.0), roomGroup.Perimeter.Vertices);
            Assert.Contains(new Vector3(20.0, 40.0, 0.0), roomGroup.Perimeter.Vertices);
        }

        [Fact]
        public void Name()
        {
            var roomGroup = new RoomGroup()
            {
                Name = "Test"
            };
            Assert.Equal("Test", roomGroup.Name);
        }

        [Fact]
        public void Perimeter()
        {
            var room = new Room
            {
                Perimeter = Shaper.PolygonBox(10.0, 10.0)
            };
            Assert.Contains(new Vector3(0.0, 10.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(10.0, 10.0), room.Perimeter.Vertices);
            room.Perimeter = null;
            Assert.Contains(new Vector3(0.0, 0.0), room.Perimeter.Vertices);
            Assert.Contains(new Vector3(10.0, 0.0), room.Perimeter.Vertices);
        }

        [Fact]
        public void Rooms()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            Assert.Equal(8.0, roomGroup.Rooms.Count, 10);
        }

        [Fact]
        public void RoomsAsPolygons()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            Assert.Equal(8.0, roomGroup.RoomsAsPolygons.Count, 10);
        }

        [Fact]
        public void RoomsAsSpaces()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            Assert.Equal(8.0, roomGroup.RoomsAsSpaces.Count, 10);
            Assert.Equal(150.0, roomGroup.RoomsAsSpaces.First().Profile.Area(), 10);
        }

        [Fact]
        public void Rotate()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            var model = new Model();
            foreach (Room room in roomGroup.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            roomGroup.Rotate(Vector3.Origin, 180);
            foreach (Room room in roomGroup.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            model.ToGlTF("../../../../RoomGroupRotate.glb");
        }

        [Fact]
        public void SizeXY()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            Assert.Equal(60.0, roomGroup.SizeX, 10);
            Assert.Equal(20.0, roomGroup.SizeY, 10);
        }

        [Fact]
        public void UniqueID()
        {
            var roomGroup = new RoomGroup();
            Assert.NotNull(roomGroup.UniqueID);
        }

        [Fact]
        public void SetColor()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            foreach (Room room in roomGroup.Rooms)
            {
                Assert.Equal(Palette.White, room.Color);
            }
            roomGroup.SetColor(Palette.Green);
            foreach (Room room in roomGroup.Rooms)
            {
                Assert.Equal(Palette.Green, room.Color);
            }
        }

        [Fact]
        public void SetElevation()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            foreach (Room room in roomGroup.Rooms)
            {
                Assert.Equal(0.0, room.Elevation, 10);
            }
            roomGroup.Elevation = 10.2;
            foreach (Room room in roomGroup.Rooms)
            {
                Assert.Equal(10.2, room.Elevation, 10);
            }
        }

        [Fact]
        public void SetHeight()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2, 3.0);
            foreach (Room room in roomGroup.Rooms)
            {
                Assert.Equal(3.0, room.Height, 10);
            }
            roomGroup.SetHeight(4.5);
            foreach (Room room in roomGroup.Rooms)
            {
                Assert.Equal(4.5, room.Height, 10);
            }
        }

        [Fact]
        public void RoomsByDivision()
        {
            var roomGroup = new RoomGroup()
            {
                Perimeter = Shaper.PolygonBox(60.0, 20.0)
            };
            roomGroup.RoomsByDivision(4, 2);
            Assert.Equal(8.0, roomGroup.Rooms.Count, 10);
        }
    }
}
