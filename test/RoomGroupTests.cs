using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Hypar.Elements;
using Hypar.Geometry;

namespace HyparSpaces.Tests
{
    public class RoomGroupTests
    {
        [Fact]
        public void RoomRow()
        {
            var rooms = new List<Room>();
            //{
            //    new Room("Reception", 0, 27.0, Palette.Sand),
            //    new Room("Conf Large", 2, 4.5, 9.5, Palette.Purple),
            //    new Room("Kitchen", 3, 14.0, Palette.Yellow),
            //    new Room("Kitchen", 3, 14.0, Palette.Yellow),
            //    new Room("Break", 4, 56.0, Palette.Aqua),
            //    new Room("IT", 5, 14.0, Palette.White),
            //    new Room("Shipping", 6, 19.0, Palette.Orange),
            //    new Room("Servers", 7, 38.0, Palette.Beige),
            //};
            for (int i = 0; i < 1; i++)
            {
                rooms.Add(new Room("Office Large", 7, 4.25, 3.65, Palette.Green));
            }
            for (int i = 0; i < 1; i++)
            {
                rooms.Add(new Room("Office Medium", 8, 4.25, 3.35, Palette.Lime));
            }
            for (int i = 0; i < 1; i++)
            {
                rooms.Add(new Room("Office Small", 9, 3.65, 2.75, Palette.Mint));
            }
            for (int i = 0; i < 1; i++)
            {
                rooms.Add(new Room("Conference Large", 10, 9.5, 4.5, Palette.Purple));
            }
            for (int i = 0; i < 1; i++)
            {
                rooms.Add(new Room("Conference Medium", 11, 6.0, 4.25, Palette.Magenta));
            }
            for (int i = 0; i < 1; i++)
            {
                rooms.Add(new Room("Conference Small", 12, 5, 4.5, Palette.Lavender));
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
            model.AddElement(new Space(roomRow.Circulation, 0.0, 0.01, BuiltInMaterials.Concrete));
            model.SaveGlb("../../../../RoomGroup.glb");
        }
    }
}
