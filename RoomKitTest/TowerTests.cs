using System;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using GeometryEx;
using RoomKit;

namespace RoomKitTest
{
    public class TowerTests
    {
        [Fact]
        public Tower MakeTower()
        {
            var tower = new Tower()
            {
                Color = Palette.Aqua,
                Elevation = -8.0,
                Floors = 20,
                HeightLimit = 80.0,
                Perimeter = Shaper.PolygonBox(60.0, 20.0),
                StoryHeight = 4.0,
                TargetArea = 24000.0
            };
            tower.Stack();
            var entry = new Room()
            {
                Height = 6.0
            };
            tower.SetStoryHeight(0, 8.0);
            tower.Stories[0].Color = Palette.Granite;
            entry.SetPerimeter(new Vector3(30.0, -0.1), new Vector3(30.0, 6.0), 2.0);
            tower.Stories[1].AddCorridor(entry);
            var coreShaft = new Room()
            {
                Perimeter = Shaper.PolygonBox(5.0, 8.0, new Vector3(27.5, 6.0))
            };
            for (int i = 1; i < 3; i++)
            {
                tower.Stories[i].Color = Palette.Green;
                tower.SetStoryHeight(i, 6.0);
                tower.Stories[i].RoomsByDivision(4, 1, 5.5, 0.5, "Retail", Palette.Lime);
                tower.Stories[i].AddExclusion(coreShaft);
            }
            var corridor = new Room()
            {
                Height = 3.5
            };
            corridor.SetPerimeter(new Vector3(0.5, 10.0), new Vector3(59.5, 10), 2.0);
            for (int i = 3; i < tower.Stories.Count; i++)
            {
                tower.Stories[i].RoomsByDivision(4, 2, 3.5, 0.5, "Office", Palette.Teal);
                tower.Stories[i].AddCorridor(corridor);
                tower.Stories[i].AddExclusion(coreShaft);
            }
            tower.AddCore(coreShaft.Perimeter, 0, 3.0, Palette.Granite);
            var model = new Model();
            foreach (Space space in tower.Spaces)
            {
                model.AddElement(space);
            }
            model.ToGlTF("../../../../Tower.glb");
            return tower;
        }

        [Fact]
        public void Area()
        {
            var tower = MakeTower();
            Assert.Equal(24000.0, tower.Area);
        }

        [Fact]
        public void AreaByName()
        {
            var tower = MakeTower();
            Assert.Equal(2151.0, tower.AreaByName("Retail"));
        }

        [Fact]
        public void Color()
        {
            var tower = new Tower()
            {
                Color = Palette.Aqua
            };
            Assert.Equal(Palette.Aqua, tower.Color);
            tower.Color = Palette.Yellow;
            Assert.Equal(Palette.Yellow, tower.Color);
            tower.Color = null;
            Assert.Equal(Palette.Yellow, tower.Color);
        }

        [Fact]
        public void Cores()
        {
            var tower = MakeTower();
            Assert.Equal(1.0, tower.Cores.Count);
            Assert.Equal(91.0, tower.Cores.First().Height);
            Assert.Contains(new Vector3(27.5, 6.0), tower.Cores.First().Perimeter.Vertices);
            Assert.Contains(new Vector3(32.5, 6.0), tower.Cores.First().Perimeter.Vertices);
            Assert.Contains(new Vector3(27.5, 14.0), tower.Cores.First().Perimeter.Vertices);
            Assert.Contains(new Vector3(32.5, 14.0), tower.Cores.First().Perimeter.Vertices);
        }

        [Fact]
        public void Elevation()
        {
            var tower = MakeTower();
            tower.Elevation = 20.0;
            var column = new Column(Vector3.Origin, 20.0, new Profile(Shaper.PolygonBox(1.0, 1.0)));
            var model = new Model();
            model.AddElement(column);
            foreach (Space space in tower.Spaces)
            {
                model.AddElement(space);
            }
            model.ToGlTF("../../../../TowerElevation.glb");
        }

        [Fact]
        public void Envelope()
        {
            var tower = MakeTower();
            var envelope = tower.Envelope;
            Assert.Equal(1200.0, envelope.Area);
        }

        [Fact]
        public void EnvelopeAsPolygon()
        {
            var tower = MakeTower();
            var envelope = tower.EnvelopeAsPolygon;
            Assert.Equal(1200.0, envelope.Area);
        }

        [Fact]
        public void EnvelopeAsSpace()
        {
            var story = MakeTower();
            var envelope = story.EnvelopeAsSpace;
            Assert.Equal(1200.0, envelope.Profile.Area());
        }

        [Fact]
        public void Floors()
        {
            var tower = MakeTower();
            Assert.Equal(20.0, tower.Floors);
        }

        [Fact]
        public void Height()
        {
            var tower = MakeTower();
            Assert.Equal(80.0, tower.Height);
        }

        [Fact]
        public void MoveFromTo()
        {
            var tower = MakeTower();
            tower.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20.0));
            Assert.Equal(20.0, tower.Elevation);
            Assert.Contains(new Vector3(20.0, 20.0), tower.Perimeter.Vertices);
            Assert.Contains(new Vector3(80.0, 20.0), tower.Perimeter.Vertices);
            Assert.Contains(new Vector3(80.0, 40.0), tower.Perimeter.Vertices);
            Assert.Contains(new Vector3(20.0, 40.0), tower.Perimeter.Vertices);
            var column = new Column(Vector3.Origin, 20.0, new Profile(Shaper.PolygonBox(1.0, 1.0)));
            var model = new Model();
            model.AddElement(column);
            foreach (Space space in tower.Spaces)
            {
                model.AddElement(space);
            }
            model.ToGlTF("../../../../TowerMoveFromTo.glb");
        }

        [Fact]
        public void Perimeter()
        {
            var tower = MakeTower();
            Assert.Contains(new Vector3(0.0, 0.0), tower.Perimeter.Vertices);
            Assert.Contains(new Vector3(60.0, 0.0), tower.Perimeter.Vertices);
            Assert.Contains(new Vector3(60.0, 20.0), tower.Perimeter.Vertices);
            Assert.Contains(new Vector3(0.0, 20.0), tower.Perimeter.Vertices);
        }

        [Fact]
        public void RoomsByName()
        {
            var tower = MakeTower();
            Assert.Equal(136.0, tower.RoomsByName("Office").Count);
        }

        [Fact]
        public void Rotate()
        {
            var tower = MakeTower();
            var model = new Model();
            foreach (Space space in tower.Spaces)
            {
                model.AddElement(space);
            }
            tower.Rotate(Vector3.Origin, 180);
            foreach (Space space in tower.Spaces)
            {
                model.AddElement(space);
            }
            model.ToGlTF("../../../../TowerRotate.glb");
        }

        [Fact]
        public void SetStoryHeight()
        {
            var tower = MakeTower();
            Assert.Equal(8.0, tower.Stories[0].Height);
            tower.SetStoryHeight(10, 20.0, true, false);
            Assert.Equal(20.0, tower.Stories[10].Height);
            var model = new Model();
            foreach (Space space in tower.Spaces)
            {
                model.AddElement(space);
            }
            model.ToGlTF("../../../../TowerStoryHeight.glb");
        }

        [Fact]
        public void Slabs()
        {
            var tower = MakeTower();
            Assert.Equal(20.0, tower.Slabs.Count);
            var slab = tower.Slabs.First();
            Assert.Contains(new Vector3(0.0, 0.0), slab.Profile.Perimeter.Vertices);
            Assert.Contains(new Vector3(60.0, 0.0), slab.Profile.Perimeter.Vertices);
            Assert.Contains(new Vector3(60.0, 20.0), slab.Profile.Perimeter.Vertices);
            Assert.Contains(new Vector3(0.0, 20.0), slab.Profile.Perimeter.Vertices);
        }

        [Fact]
        public void Spaces()
        {
            var tower = MakeTower();
            Assert.Equal(200.0, tower.Spaces.Count);
        }

        [Fact]
        public void Stack()
        {
            var tower = MakeTower();
            Assert.Equal(20.0, tower.Floors);
        }

        [Fact]
        public void Stories()
        {
            var tower = MakeTower();
            Assert.Equal(20.0, tower.Stories.Count);
        }


    }
}
