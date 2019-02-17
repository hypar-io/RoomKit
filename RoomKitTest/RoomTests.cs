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
    }
}
