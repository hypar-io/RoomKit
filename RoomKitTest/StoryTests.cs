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
        private Story MakeStory()
        {
            var story = new Story()
            {
                Color = Palette.Green,
                Elevation = 0.0,
                Height = 6.0,
                Name = "First Floor",
                Perimeter = new Polygon(
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(60.0, 0.0),
                        new Vector3(60.0, 40.0),
                        new Vector3(0.0, 40.0)
                    }),
                SlabThickness = 0.1
            };
            story.RoomsByDivision(4, 2, 5.5, 10.0, "Retail", Palette.Lime);
            story.AddCorridor(
                new Room()
                {
                    Color = Palette.White,
                    Height = 5.5,
                    Perimeter = new Line(new Vector3(10.0, 20.0), new Vector3(50.0, 20.0)).Thicken(2.0)
                });
            story.AddCorridor(
                new Room()
                {
                    Color = Palette.White,
                    Height = 5.5,
                    Perimeter = new Line(new Vector3(30.0, 10.0), new Vector3(30.0, 30.0)).Thicken(2.0)
                });
            story.AddExclusion(
                new Room()
                {
                    Perimeter = 
                        new Polygon(
                            new []
                            {
                                Vector3.Origin,
                                new Vector3(5.0, 0.0),
                                new Vector3(5.0, 5.0),
                                new Vector3(0.0, 5.0),
                            })
                });
            story.AddService(
                new Room()
                {
                    Color = BuiltInMaterials.Concrete.Color,
                    Height = 6.0,
                    Perimeter = new Line(new Vector3(30.0, 15.0), new Vector3(30.0, 25.0)).Thicken(2.0)
                });
            return story;
        }

        [Fact]
        public void Area()
        {
            var story = MakeStory();
            Assert.Equal(2400.0, story.Area);
        }

        [Fact]
        public void AreaAvailable()
        {
            var story = MakeStory();
            Assert.Equal(1575.0, story.AreaAvailable, 10);
        }

        [Fact]
        public void AreaPlaced()
        {
            var story = MakeStory();
            Assert.Equal(800.0, story.AreaPlaced);
        }

        [Fact]
        public void Color()
        {
            var story = MakeStory();
            Assert.Equal(Palette.Green, story.Color);
        }

        [Fact]
        public void Corridors()
        {
            var story = MakeStory();
            Assert.Equal(4.0, story.Corridors.Count);
        }

        [Fact]
        public void CorridorsAsPolygons()
        {
            var story = MakeStory();
            Assert.Equal(4.0, story.CorridorsAsPolygons.Count);
        }

        [Fact]
        public void CorridorsAsSpaces()
        {
            var story = MakeStory();
            Assert.Equal(4.0, story.CorridorsAsSpaces.Count);
        }

        [Fact]
        public void CorridorsColor()
        {
            var story = MakeStory();
            story.CorridorsColor = Palette.Yellow;
            foreach(Room room in story.Corridors)
            {
                Assert.Equal(Palette.Yellow, room.Color);
            }
        }

        [Fact]
        public void Elevation()
        {
            var story = MakeStory();
            story.Elevation = 10.0;
            var rooms = new List<Room>(story.Corridors);
            rooms.AddRange(story.Rooms);
            rooms.AddRange(story.Services);
            foreach(Room room in rooms)
            {
                Assert.Equal(10.0, room.Elevation);
            }
        }

        [Fact]
        public void Envelope()
        {
            var story = MakeStory();
            var envelope = story.Envelope;
            Assert.Equal(2400.0, envelope.Area);
        }

        [Fact]
        public void EnvelopeAsPolygon()
        {
            var story = MakeStory();
            var envelope = story.EnvelopeAsPolygon;
            Assert.Equal(2400.0, envelope.Area);
        }

        [Fact]
        public void EnvelopeAsSpace()
        {
            var story = MakeStory();
            var envelope = story.EnvelopeAsSpace;
            Assert.Equal(2400.0, envelope.Profile.Area());
        }

        [Fact]
        public void Exclusions()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.Exclusions.Count);
        }

        [Fact]
        public void ExclusionsAsPolygons()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.ExclusionsAsPolygons.Count);
        }

        [Fact]
        public void Height()
        {
            var story = MakeStory();
            Assert.Equal(6.0, story.Height);
        }

        [Fact]
        public void HeightInteriors()
        {
            var story = MakeStory();
            story.HeightInteriors = 4.0;
            var rooms = new List<Room>(story.Corridors);
            rooms.AddRange(story.Rooms);
            rooms.AddRange(story.Services);
            foreach (Room room in rooms)
            {
                Assert.Equal(4.0, room.Height);
            }
        }

        [Fact]
        public void InteriorsAsPolygons()
        {
            var story = MakeStory();
            Assert.Equal(14.0, story.InteriorsAsPolygons.Count);
        }

        [Fact]
        public void InteriorsAsSpaces()
        {
            var story = MakeStory();
            Assert.Equal(13.0, story.InteriorsAsSpaces.Count);
        }

        [Fact]
        public void Name()
        {
            var story = MakeStory();
            Assert.Equal("First Floor", story.Name);
        }

        [Fact]
        public void Perimeter()
        {
            var story = MakeStory();
            var vertices = story.Perimeter.Vertices;
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 60.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 60.0 && p.Y == 40.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 40.0);
        }

        [Fact]
        public void Rooms()
        {
            var story = MakeStory();
            Assert.Equal(8.0, story.Rooms.Count);
        }

        [Fact]
        public void RoomsAsPolygons()
        {
            var story = MakeStory();
            Assert.Equal(8.0, story.RoomsAsPolygons.Count);
        }

        [Fact]
        public void RoomsAsSpaces()
        {
            var story = MakeStory();
            Assert.Equal(8.0, story.RoomsAsSpaces.Count);
        }

        [Fact]
        public void RoomsColor()
        {
            var story = MakeStory();
            story.RoomsColor = Palette.Magenta;
            foreach (Room room in story.Rooms)
            {
                Assert.Equal(Palette.Magenta, room.Color);
            }
        }

        [Fact]
        public void Services()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.Services.Count);
        }

        [Fact]
        public void ServicesAsPolygons()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.ServicesAsPolygons.Count);
        }

        [Fact]
        public void ServicesAsSpaces()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.ServicesAsSpaces.Count);
        }

        [Fact]
        public void ServicesColor()
        {
            var story = MakeStory();
            story.RoomsColor = Palette.Red;
            foreach (Room room in story.Rooms)
            {
                Assert.Equal(Palette.Red, room.Color);
            }
        }

        [Fact]
        public void Slab()
        {
            var story = MakeStory();
            var vertices = story.Slab.Profile.Perimeter.Vertices;
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 60.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 60.0 && p.Y == 40.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 40.0);
        }

        [Fact]
        public void SlabThickness()
        {
            var story = MakeStory();
            Assert.Equal(0.1, story.SlabThickness);
        }

        [Fact]
        public void MoveFromTo()
        {
            var story = MakeStory();
            story.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20));
            Assert.Equal(20.0, story.Elevation);
            var vertices = story.Slab.Profile.Perimeter.Vertices;
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 20.0 && p.Z == 0.0);
            Assert.Contains(vertices, p => p.X == 80.0 && p.Y == 20.0 && p.Z == 0.0);
            Assert.Contains(vertices, p => p.X == 80.0 && p.Y == 60.0 && p.Z == 0.0);
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 60.0 && p.Z == 0.0);
        }

        [Fact]
        public void Rotate()
        {
            var story = MakeStory();
            var model = new Model();
            foreach (Space space in story.InteriorsAsSpaces)
            {
                model.AddElement(space);
            }
            model.AddElement(story.EnvelopeAsSpace);
            story.Rotate(Vector3.Origin, 180);
            foreach (Space space in story.InteriorsAsSpaces)
            {
                model.AddElement(space);
            }
            model.AddElement(story.EnvelopeAsSpace);
            model.SaveGlb("../../../../StoryRotate.glb");
        }
    }
}
