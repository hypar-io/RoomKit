using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;

namespace RoomKitTest
{
    public class StoryTests
    {
        [Fact]
        public void AddCorridor()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(48.0, 0.0),
                    new Vector3(48.0, 12.0),
                    new Vector3(0.0, 12.0)
                }
            );
            var story = new Story()
            {
                Color = Palette.Green,
                Elevation = 0.0,
                Height = 6.0,
                Perimeter = polygon,
            };
            story.RoomsByDivision(4, 1, 5.5, 0.5, "", Palette.Lime);
            story.AddCorridor(new Vector3(24.0, -0.01),
                              new Vector3(24.0, 6.0),
                              2.0, 5.5, "", Palette.White);
            story.AddCorridor(new Vector3(0.5, 10.75),
                              new Vector3(47.5, 10.75),
                              1.5, 5.5, "", Palette.White);
            story.SlabThickness = 0.3;
            var model = new Model();
            foreach (Space space in story.InteriorsAsSpaces)
            {
                model.AddElement(space);
            }
            model.AddElement(story.Slab);
            model.SaveGlb("../../../../AddCorridor.glb");
        }

        [Fact]
        public void AddService()
        {
            var polygon = new Polygon
            (
                new[]
                {
                    new Vector3(0.0, 0.0),
                    new Vector3(48.0, 0.0),
                    new Vector3(48.0, 12.0),
                    new Vector3(0.0, 12.0)
                }
            );
            var story = new Story()
            {
                Color = Palette.Green,
                Elevation = 0.0,
                Height = 6.0,
                Perimeter = polygon,
                SlabThickness = 0.3
            };
            story.RoomsByDivision(4, 1, 5.5, 0.5, "", Palette.Lime);
            story.AddCorridor(new Vector3(24.0, -0.01),
                              new Vector3(24.0, 10.0),
                                2.0, 5.5, "", Palette.White);
            story.AddCorridor(new Vector3(0.5, 10.75),
                                new Vector3(47.5, 10.75),
                                1.5, 5.5, "", Palette.White);
            polygon = new Line(new Vector3(24.0, 7.0), new Vector3(24.0, 11.5)).Thicken(5.0);
            story.AddService(polygon, "", Palette.Gray);
            var model = new Model();
            foreach (Space space in story.InteriorsAsSpaces)
            {
                model.AddElement(space);
            }
            model.AddElement(story.Slab);
            model.SaveGlb("../../../../AddService.glb");
        }
    }
}
