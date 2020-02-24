using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;
using GeometryEx;
using Elements.Serialization.glTF;

namespace RoomKitTest
{
    public class SuiteTests
    {
        [Fact]
        public void Suite()
        {
            var rooms = new List<Room>();
            for (int i = 0; i < 2; i++)
            {
                var room = new Room(new Vector3(5.0, 4.0, 3.0))
                {
                    Color = Palette.Green,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 2; i++)
            {
                var room = new Room(new Vector3(6.0, 6.0, 3.0))
                {
                    Color = Palette.Aqua,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 2; i++)
            {
                var room = new Room(new Vector3(5.0, 4.0, 3.0))
                {
                    Color = Palette.Coral,
                };
                rooms.Add(room);
            }
            for (int i = 0; i < 2; i++)
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
                    Color = Palette.Amber,
                };
                rooms.Add(room);
            }
            var suite = new Suite("", "", rooms, 0.5, RoomKit.Suite.SuiteLayout.Axis);
            var model = new Model();
            foreach (Room room in suite.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../Suite.glb");
        }
    }
}

 