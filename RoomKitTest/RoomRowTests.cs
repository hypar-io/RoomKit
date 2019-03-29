using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using RoomKit;

namespace RoomKitTest
{
    public class RoomRowTests
    {
        [Fact]
        public void AddRoom()
        {
            var rooms = new List<Room>();
            for (int i = 0; i < 3; i++)
            {
                var room = new Room()
                {
                    Color = Palette.Green,
                    DesignXYZ = new Vector3(5.0, 4.0, 3.0),
                    Height = 3.0,
                };
                room.SetPerimeter();
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room()
                {
                    Color = Palette.Lime,
                    DesignArea = 12.0,
                    Height = 3.0,
                };
                room.SetPerimeter();
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room()
                {
                    Color = Palette.Mint,
                    DesignXYZ = new Vector3(4.0, 3.0, 3.0)
                };
                room.SetPerimeter();
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room()
                {
                    Color = Palette.Purple,
                    DesignArea = 16.0,
                    Height = 5.0,
                };
                room.SetPerimeter();
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room()
                {
                    Color = Palette.Magenta,
                    DesignXYZ = new Vector3(6.0, 4.0, 5.0)
                };
                room.SetPerimeter();
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room()
                {
                    Color = Palette.Lavender,
                    DesignXYZ = new Vector3(5.0, 4.5, 5.0)
                };
                room.SetPerimeter();
                rooms.Add(room);
            }
            var line = new Line(new Vector3(30.0, 30.0), new Vector3(200.0, 40.0));
            var roomRow = new RoomRow(line)
            {
                CirculationWidth = 2.0
            };
            foreach (Room room in rooms)
            {
                roomRow.AddRoom(room, null, null);
            }
            Assert.Equal(18, roomRow.Rooms.Count);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            model.AddElement(new Floor(roomRow.Circulation, new FloorType("slab", 0.05)));
            model.ToGlTF("../../../../RoomRow.glb");
        }

        [Fact]
        public void AreaPlaced()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                roomRow.AddRoom(new Room() { DesignArea = 9.0 });
            }
            Assert.Equal(27.0, roomRow.AreaPlaced, 10);
        }

        [Fact]
        public void AvailableLength()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                roomRow.AddRoom(new Room() { DesignArea = 9.0 });
            }
            Assert.Equal(1.0, roomRow.AvailableLength, 10);
        }

        [Fact]
        public void Circulation()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0))
            {
                CirculationWidth = 2.0
            };
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            Assert.Contains(new Vector3(0.0, 5.0), roomRow.Circulation.Vertices);
            Assert.Contains(new Vector3(9.0, 5.0), roomRow.Circulation.Vertices);
        }

        [Fact]
        public void CirculationWidth()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0))
            {
                CirculationWidth = 2.0
            };
            Assert.Equal(2.0, roomRow.CirculationWidth);
        }

        [Fact]
        public void Depth()
        {
            var roomRow = new RoomRow(new Line(Vector3.Origin, new Vector3(20.0, 0.0)));
            Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            Assert.True(roomRow.AddRoom(new Room() { DesignArea = 16.0 }));
            Assert.True(roomRow.AddRoom(new Room() { DesignArea = 25.0 }));
            Assert.Equal(5.0, roomRow.Depth);
        }

        [Fact]
        public void Name()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0))
            {
                Name = "Test"
            };
            Assert.Equal("Test", roomRow.Name);
        }

        [Fact]
        public void Rooms()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            Assert.Equal(3.0, roomRow.Rooms.Count);
            Assert.Equal(9.0, roomRow.Rooms.First().Area, 10);
        }

        [Fact]
        public void RoomsAsPolygons()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            Assert.Equal(3.0, roomRow.RoomsAsPolygons.Count);
            Assert.Equal(9.0, roomRow.RoomsAsPolygons.First().Area, 10);
        }

        [Fact]
        public void RoomsAsSpaces()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            Assert.Equal(3.0, roomRow.RoomsAsSpaces.Count);
            Assert.Equal(9.0, roomRow.RoomsAsSpaces.First().Profile.Area(), 10);
        }

        [Fact]
        public void SizeXY()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            Assert.Equal(9.0, roomRow.SizeX, 10);
            Assert.Equal(3.0, roomRow.SizeY, 10);
        }

        [Fact]
        public void UniqueID()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            Assert.NotNull(roomRow.UniqueID);
        }

        [Fact]
        public void MoveFromTo()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            roomRow.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20.0));
            Assert.Equal(20.0, roomRow.Elevation);
            Assert.Equal(20.0, roomRow.Row.Start.X);
            Assert.Equal(20.0, roomRow.Row.Start.Y);
            Assert.Equal(0.0, roomRow.Row.Start.Z);

            Assert.Equal(30.0, roomRow.Row.End.X);
            Assert.Equal(20.0, roomRow.Row.Start.Y);
            Assert.Equal(0.0, roomRow.Row.Start.Z);
        }

        [Fact]
        public void Rotate()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            roomRow.Rotate(Vector3.Origin, 180);
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            model.ToGlTF("../../../../RoomRowRotate.glb");
        }

        [Fact]
        public void SetColor()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            roomRow.SetColor(Palette.Blue);
            foreach(Room room in roomRow.Rooms)
            {
                Assert.Equal(Palette.Blue, room.Color);
            }
        }

        [Fact]
        public void SetElevation()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            roomRow.Elevation = 20.0;
            foreach (Room room in roomRow.Rooms)
            {
                Assert.Equal(20.0, room.Elevation);
            }
        }

        [Fact]
        public void SetHeight()
        {
            var roomRow = new RoomRow(Vector3.Origin, new Vector3(10.0, 0.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room() { DesignArea = 9.0 }));
            }
            roomRow.SetHeight(10.0);
            foreach (Room room in roomRow.Rooms)
            {
                Assert.Equal(10.0, room.Height);
            }
        }
    }
}
