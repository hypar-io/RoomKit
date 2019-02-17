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
    public class RoomGroupTests
    {
        [Fact]
        public void RoomGroup()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(60.0, 0.0),
                    new Vector3(60.0, 20.0),
                    new Vector3(0.0, 20.0)
                }
            );

            var roomGroup = new RoomGroup(polygon, "", 4, 2);
            var model = new Model();
            foreach (Room room in roomGroup.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            model.SaveGlb("../../../../RoomGroup.glb");
        }

        [Fact]
        public void RoomRow()
        {
            var rooms = new List<Room>();
            for (int i = 0; i < 1; i++)
            {
                var room = new Room()
                {
                    Name = "Office Large",
                    ResourceID = 7,
                    DesignX = 4.25,
                    DesignY = 3.65,
                    Color = Palette.Green
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 1; i++)
            {
                var room = new Room()
                {
                    Name = "Office Medium",
                    ResourceID = 8,
                    DesignX = 4.25,
                    DesignY = 3.35,
                    Color = Palette.Lime
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 1; i++)
            {
                var room = new Room()
                {
                    Name = "Office Small",
                    ResourceID = 9,
                    DesignX = 3.65,
                    DesignY = 2.75,
                    Color = Palette.Mint
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 1; i++)
            {
                var room = new Room()
                {
                    Name = "Conference Large",
                    ResourceID = 10,
                    DesignX = 9.5,
                    DesignY = 4.5,
                    Color = Palette.Purple
                };
                rooms.Add(room);

            }
            for (int i = 0; i < 1; i++)
            {
                var room = new Room()
                {
                    Name = "Conference Medium",
                    ResourceID = 11,
                    DesignX = 6.0,
                    DesignY = 4.25,
                    Color = Palette.Magenta
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 1; i++)
            {
                var room = new Room()
                {
                    Name = "Conference Small",
                    ResourceID = 12,
                    DesignX = 5.0,
                    DesignY = 4.5,
                    Color = Palette.Lavender
                };
                rooms.Add(room);
            }
            var line = new Line(new Vector3(30.0, 30.0), new Vector3(60.0, 40.0));
            var roomRow = new RoomRow(line);
            foreach (Room room in rooms)
            {
                roomRow.AddRoom(room, null, null, 10.0);
            }
            var model = new Model();
            foreach (Room room in roomRow.Rooms)
            {
                model.AddElement(room.AsSpace);
            }
            model.AddElement(new Space(roomRow.Circulation, 0.1, 0.01, BuiltInMaterials.Concrete));
            model.SaveGlb("../../../../RoomRow.glb");
        }
    }
}
