﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using RoomKit;
using GeometryEx;

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
                var room = new Room(new Vector3(5.0, 4.0, 3.0))
                {
                    Color = Palette.Green,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room(new Vector3(6.0, 6.0, 3.0))
                {
                    Color = Palette.Lime,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room(new Vector3(5.0, 4.0, 3.0))
                {
                    Color = Palette.Mint,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room(new Vector3(6.0, 6.0, 3.0))
                {
                    Color = Palette.Purple,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room(new Vector3(5.0, 4.0, 3.0))
                {
                    Color = Palette.Magenta,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 3; i++)
            {
                var room = new Room(new Vector3(6.0, 6.0, 3.0))
                {
                    Color = Palette.Lavender,
                };
                rooms.Add(room);
            }
            var polygon =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(10.0, 10.0),
                        new Vector3(200.0, 30.0),
                        new Vector3(200.0, 33.0),
                        new Vector3(10.0, 13.0)
                    }
                );
            var roomRow = new RoomRow(polygon);
            foreach (Room room in rooms)
            {
                roomRow.AddRoom(room);
            }
            Assert.Equal(18, roomRow.Rooms.Count);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.AddElement(new Space(new Profile(polygon), 0.2, new Material(Colors.Aqua, 0.0, 0.0, false, null, false, Guid.NewGuid(), Guid.NewGuid().ToString())));
            model.ToGlTF("../../../../RoomKitTest/output/RoomRow.glb");
        }

        [Fact]
        public void AddRooms()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(Vector3.Origin, new Vector3(9.0, 3.0)));
            var rooms = new List<Room>();
            for (int i = 0; i < 5; i++)
            {
                rooms.Add(new Room(new Vector3(3.0, 3.0, 3.0)));
            }
            Assert.Equal(2, roomRow.AddRooms(rooms).Count);
        }

        [Fact]
        public void AreaPlaced()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 3; i++)
            {
                roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0)));
            }
            Assert.Equal(27.0, roomRow.Area, 10);
        }

        [Fact]
        public void Footprint()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0))));
            }
            var footprint = roomRow.Footprint;
            Assert.Equal(27.0, footprint.Area(), 10);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.AddElement(new Space(new Profile(footprint), 0.5, BuiltInMaterials.Concrete));
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowFootprint.glb");
        }

        [Fact]
        public void JoinSmallRooms()
        {
            var roomRow =
                new RoomRow(
                    new Polygon
                    (
                        new[]
                        {
                            Vector3.Origin,
                            new Vector3(10.0, 0.0),
                            new Vector3(8.0, 2.0),
                            new Vector3(2.0, 2.0)
                        }
                    ));
            for (var i = 0; i < 5; i++)
            {
                roomRow.AddRoom(
                    new Room(new Vector3(2.0, 2.0))
                    {
                        Color = Palette.Aqua,
                        Height = 1.0
                    });
            }
            Assert.Equal(5, roomRow.Rooms.Count);
            roomRow.JoinSmallEndRooms(3.0);
            Assert.Equal(3, roomRow.Rooms.Count);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowSmallRooms.glb");
        }

        [Fact]
        public void JoinTriangleEndRooms()
        {
            var roomRow =
                new RoomRow(
                    new Polygon
                    (
                        new[]
                        {
                            Vector3.Origin,
                            new Vector3(10.0, 0.0),
                            new Vector3(8.0, 2.0),
                            new Vector3(2.0, 2.0)
                        }
                    ));
            for (var i = 0; i < 5; i++)
            {
                roomRow.AddRoom(
                    new Room(new Vector3(2.0, 2.0))
                    {
                        Color = Palette.Aqua,
                        Height = 1.0
                    });
            }
            Assert.Equal(5, roomRow.Rooms.Count);
            Assert.Equal(3, roomRow.Rooms.First().Perimeter.Vertices.Count);
            Assert.Equal(3, roomRow.Rooms.Last().Perimeter.Vertices.Count);
            roomRow.JoinTriangleEndRooms();
            Assert.Equal(3, roomRow.Rooms.Count);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowJoinTriangleEndRooms.glb");
        }

        [Fact]
        public void LengthAvailable()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 2; i++)
            {
                roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0)));
            }
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.AddElement(new Space(new Profile(roomRow.Perimeter), 0.2, new Material(Colors.Aqua, 0.0, 0.0, false, null, false, Guid.NewGuid(), Guid.NewGuid().ToString())));
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowLengthAvailable.glb");
            Assert.Equal(3.0, roomRow.LengthAvailable, 10);
        }

        [Fact]
        public void MoveFromTo()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(Vector3.Origin, new Vector3(9.0, 3.0)));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0))));
            }
            roomRow.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20.0));
            Assert.Equal(20.0, roomRow.Row.Start.X);
            Assert.Equal(20.0, roomRow.Row.Start.Y);
            Assert.Equal(0.0, roomRow.Row.Start.Z);
        }

        [Fact]
        public void Populate()
        {
            var polygon =
             new Polygon
             (
                 new[]
                 {
                    new Vector3(0.0, 0.0),
                    new Vector3(30.0, 0.0),
                    new Vector3(25.0, 5.0),
                    new Vector3(5.0, 5.0)
                 }
             );
            var roomRow = new RoomRow(polygon);
            roomRow.Populate(20.0, 4.0);
            roomRow.SetColor(Palette.Aqua);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.AddElement(new Space(new Profile(polygon), 0.2, new Material(Colors.Granite, 0.0, 0.0, false, null, false, Guid.NewGuid(), Guid.NewGuid().ToString())));
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowPopulate.glb");
            Assert.Equal(7, roomRow.Rooms.Count);
        }

        [Fact]
        public void PopulateLine()
        {
            var line = new Line(new Vector3(0.0, 0.0), new Vector3(25.0, 5.0));
            var roomRow = new RoomRow(line, 5.0);
            roomRow.Populate(20.0, 4.0);
            roomRow.SetColor(Palette.Aqua);
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.AddElement(new Space(new Profile(roomRow.Perimeter), 0.2, new Material(Colors.Granite, 0.0, 0.0, false, null, false, Guid.NewGuid(), Guid.NewGuid().ToString())));
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowPopulateLine.glb");
            Assert.Equal(6, roomRow.Rooms.Count);
        }

        [Fact]
        public void Rooms()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 2; i++)
            {
                roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0)));
            }
            Assert.Equal(2, roomRow.Rooms.Count);
            Assert.Equal(9.0, roomRow.Rooms.First().Area, 10);
        }

        [Fact]
        public void RoomsAsPolygons()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0))));
            }
            Assert.Equal(3.0, roomRow.RoomsAsPolygons.Count);
            Assert.Equal(9.0, roomRow.RoomsAsPolygons.First().Area(), 10);
        }

        [Fact]
        public void Rotate()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(Vector3.Origin, new Vector3(9.0, 3.0)));
            for (int i = 0; i < 3; i++)
            {
                var room = new Room(new Vector3(3.0, 3.0, 3.0))
                {
                    Color = Palette.Aqua
                };
                Assert.True(roomRow.AddRoom(room));
            }
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            roomRow.Rotate(Vector3.Origin, 180);
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/RoomRowRotate.glb");
        }

        [Fact]
        public void SetColor()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0))));
            }
            roomRow.SetColor(Palette.Blue);
            foreach (Room room in roomRow.Rooms)
            {
                Assert.Equal(Palette.Blue, room.Color);
            }
        }

        [Fact]
        public void SetHeight()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            for (int i = 0; i < 3; i++)
            {
                Assert.True(roomRow.AddRoom(new Room(new Vector3(3.0, 3.0, 3.0))));
            }
            roomRow.SetHeight(10.0);
            foreach (Room room in roomRow.Rooms)
            {
                Assert.Equal(10.0, room.Height);
            }
        }

        [Fact]
        public void UniqueID()
        {
            var roomRow = new RoomRow(Polygon.Rectangle(9.0, 3.0));
            Assert.NotNull(roomRow.UniqueID);
        }
    }
}
