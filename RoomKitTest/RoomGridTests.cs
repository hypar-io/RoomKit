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
    public class RoomGridTests
    {
        [Fact]
        public void Create()
        {
            //var perimeter = new Polygon
            //(
            //    new[]
            //    {
            //        new Vector3(0.0, 0.0, 0.0),
            //        new Vector3(30.0, 0.0, 0.0),
            //        new Vector3(30.0, 20.0, 0.0),
            //        new Vector3(0.0, 20.0, 0.0)
            //    }
            //);

            var perimeter =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(22.4239, -11.4625,0.0000),
                        new Vector3(41.3779, -11.4625,0.0000),
                        new Vector3(41.3779, 35.0399, 0.0000),
                        new Vector3(-22.4239, 35.0399, 0.0000)
                    }
                );



            //var sector = new RoomGrid(perimeter, 4.0, 31.900910660932219, 11.625584573198932, 3.0, 69.394155495589274, GridPosition.CenterXY);
            var sector = new RoomGrid(perimeter, 4.0, 30.0, 6.0, 3.0, 69.394155495589274, GridPosition.CenterXY);
            var model = new Model();
            model.AddElement(new Panel(perimeter, BuiltInMaterials.Concrete));
            foreach (var corridor in sector.Corridors)
            {
               //model.AddElement(new Space(new Profile(corridor.Perimeter), corridor.Height, BuiltInMaterials.Glass));
            }
            foreach (var roomRow in sector.RoomRows)
            {
               model.AddElement(new Space(roomRow.Perimeter, 3.0, new Material(Palette.Aqua, 0.0, 0.0, Guid.NewGuid(), Guid.NewGuid().ToString())));
                //for (var i = 0; i < 10; i++)
                //{
                //    var room = new Room(new Vector3(5.0, 5.0, 4.0))
                //    {
                //        Color = Palette.Aqua
                //    };
                //    roomRow.AddRoom(room);
                //}
                //roomRow.Infill(4.0);
                //foreach (var rm in roomRow.Rooms)
                //{
                //    model.AddElement(new Space(rm.PerimeterAsProfile, 3.0, rm.ColorAsMaterial));
                //}
            }
            model.ToGlTF("../../../../roomGridCreate.glb");
        }
    }
}
