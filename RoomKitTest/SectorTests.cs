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
    public class SectorTests
    {
        [Fact]
        public void Create()
        {
            var perimeter = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0, 0.0),
                    new Vector3(30.0, 0.0, 0.0),
                    new Vector3(30.0, 20.0, 0.0),
                    new Vector3(0.0, 20.0, 0.0)
                }
            );
            var sector = new Sector(perimeter, 4.0, 20.0, 5.0, 2.0, 0.0, GridPosition.CenterXY);
            var model = new Model();
            model.AddElement(new Panel(perimeter, BuiltInMaterials.Concrete));
            foreach (var corridor in sector.Corridors)
            {
                model.AddElement(new Space(new Profile(corridor.Perimeter), corridor.Height, BuiltInMaterials.Concrete));
            }
            foreach (var roomRow in sector.RoomRows)
            {
                model.AddElement(new Space(new Profile(roomRow.Perimeter), 3.0, BuiltInMaterials.Glass));
                for (var i = 0; i < 10; i++)
                {
                    var room = new Room(new Vector3(5.0, 5.0, 4.0))
                    {
                        Color = Palette.Aqua
                    };
                    roomRow.AddRoom(room);
                }
                foreach (var rm in roomRow.Rooms)
                {
                    model.AddElement(new Space(rm.PerimeterAsProfile, 3.0, rm.ColorAsMaterial));
                }
            }
            model.ToGlTF("../../../../sectorCreate.glb");
        }
    }
}
