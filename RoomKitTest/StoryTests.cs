using System.Collections.Generic;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using GeometryEx;
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
                            new[]
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
                    Perimeter =
                        new Polygon(
                            new[]
                            {
                                new Vector3(25.0, 15.0),
                                new Vector3(35.0, 15.0),
                                new Vector3(35.0, 25.0),
                                new Vector3(25.0, 25.0),
                            })
                });
            story.AddOpening(
                new Room()
                {
                    Perimeter =
                        new Polygon(
                            new[]
                            {
                                new Vector3(25.0, 15.0),
                                new Vector3(35.0, 15.0),
                                new Vector3(35.0, 25.0),
                                new Vector3(25.0, 25.0),
                            })
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
            Assert.Equal(1555.0, story.AreaAvailable, 10);
        }

        [Fact]
        public void AreaByName()
        {
            var story = MakeStory();
            Assert.Equal(620.0, story.AreaByName("Retail"));
        }

        [Fact]
        public void AreaPlaced()
        {
            var story = MakeStory();
            Assert.Equal(720.0, story.AreaPlaced);
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
        public void CorridorsAsMasses()
        {
            var story = MakeStory();
            Assert.IsType<Mass>(story.CorridorsAsMasses[0]);
            Assert.Equal(4.0, story.CorridorsAsMasses.Count);
        }

        [Fact]
        public void CorridorsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.CorridorsAsPolygons[0]);
            Assert.Equal(4.0, story.CorridorsAsPolygons.Count);
        }

        [Fact]
        public void CorridorsAsSpaces()
        {
            var story = MakeStory();
            Assert.IsType<Space>(story.CorridorsAsSpaces[0]);
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
        public void EnvelopeAsMass()
        {
            var story = MakeStory();
            var envelope = story.EnvelopeAsMass;
            Assert.IsType<Mass>(envelope);
            Assert.Equal(2400.0, envelope.Profile.Area());
        }

        [Fact]
        public void EnvelopeAsPolygon()
        {
            var story = MakeStory();
            var envelope = story.EnvelopeAsPolygon;
            Assert.IsType<Polygon>(envelope);
            Assert.Equal(2400.0, envelope.Area());
        }

        [Fact]
        public void EnvelopeAsSpace()
        {
            var story = MakeStory();
            var envelope = story.EnvelopeAsSpace;
            Assert.IsType<Space>(envelope);
            Assert.Equal(2400.0, envelope.Profile.Area());
        }

        [Fact]
        public void Exclusions()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.Exclusions.Count);
        }

        [Fact]
        public void ExclusionsAsMasses()
        {
            var story = MakeStory();
            Assert.IsType<Mass>(story.ExclusionsAsMasses[0]);
            Assert.Equal(1.0, story.ExclusionsAsMasses.Count);
        }

        [Fact]
        public void ExclusionsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.ExclusionsAsPolygons[0]);
            Assert.Equal(1.0, story.ExclusionsAsPolygons.Count);
        }

        [Fact]
        public void ExclusionsAsSpaces()
        {
            var story = MakeStory();
            Assert.IsType<Space>(story.ExclusionsAsSpaces[0]);
            Assert.Equal(1.0, story.ExclusionsAsSpaces.Count);
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
        public void InteriorsAsMasses()
        {
            var story = MakeStory();
            Assert.IsType<Mass>(story.InteriorsAsMasses[0]);
            Assert.Equal(13.0, story.InteriorsAsMasses.Count);
        }

        [Fact]
        public void InteriorsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.InteriorsAsPolygons[0]);
            Assert.Equal(13.0, story.InteriorsAsPolygons.Count);
        }

        [Fact]
        public void InteriorsAsSpaces()
        {
            var story = MakeStory();
            Assert.IsType<Space>(story.InteriorsAsSpaces[0]);
            Assert.Equal(13.0, story.InteriorsAsSpaces.Count);
        }

        [Fact]
        public void MoveFromTo()
        {
            var story = MakeStory();
            story.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20));
            var vertices = story.Slab.Profile.Perimeter.Vertices;
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 20.0 && p.Z == 0.0);
            Assert.Contains(vertices, p => p.X == 80.0 && p.Y == 20.0 && p.Z == 0.0);
            Assert.Contains(vertices, p => p.X == 80.0 && p.Y == 60.0 && p.Z == 0.0);
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 60.0 && p.Z == 0.0);
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
        public void RoomsAsMasses()
        {
            var story = MakeStory();
            Assert.IsType<Mass>(story.RoomsAsMasses[0]);
            Assert.Equal(8.0, story.RoomsAsMasses.Count);
        }

        [Fact]
        public void RoomsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.RoomsAsPolygons[0]);
            Assert.Equal(8.0, story.RoomsAsPolygons.Count);
        }

        [Fact]
        public void RoomsAsSpaces()
        {
            var story = MakeStory();
            Assert.IsType<Space>(story.RoomsAsSpaces[0]);
            Assert.Equal(8.0, story.RoomsAsSpaces.Count);
        }

        [Fact]
        public void RoomsByName()
        {
            var story = MakeStory();
            Assert.Equal(8.0, story.RoomsByName("Retail").Count);
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
        public void Rotate()
        {
            var story = MakeStory();
            var model = new Model();
            foreach (Space space in story.InteriorsAsSpaces)
            {
                model.AddElement(space);
            }
            story.Rotate(Vector3.Origin, 60);
            foreach (Space space in story.InteriorsAsSpaces)
            {
                model.AddElement(space);
            }
            model.ToGlTF("../../../../StoryRotate.glb");
        }

        [Fact]
        public void Services()
        {
            var story = MakeStory();
            Assert.Equal(1.0, story.Services.Count);
        }

        [Fact]
        public void ServicesAsMasses()
        {
            var story = MakeStory();
            Assert.IsType<Mass>(story.ServicesAsMasses[0]);
            Assert.Equal(1.0, story.ServicesAsMasses.Count);
        }

        [Fact]
        public void ServicesAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.ServicesAsPolygons[0]);
            Assert.Equal(1.0, story.ServicesAsPolygons.Count);
        }

        [Fact]
        public void ServicesAsSpaces()
        {
            var story = MakeStory();
            Assert.IsType<Space>(story.ServicesAsSpaces[0]);
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
            var model = new Model();
            model.AddElement(story.Slab);
            model.ToGlTF("../../../../StorySlab.glb");
        }

        [Fact]
        public void SlabType()
        {
            var story = MakeStory();
            Assert.Equal(0.1, story.SlabType.Thickness());
            var newFloor = new FloorType("newFloor", 0.5);
            story.SlabType = newFloor;
            Assert.Equal(0.5, story.SlabThickness);
            Assert.Equal(0.5, story.Slab.Thickness());
        }

        [Fact]
        public void SlabThickness()
        {
            var story = MakeStory();
            Assert.Equal(0.1, story.SlabThickness);
            var newSlab = new FloorType("newSlab", 0.5);
            story.SlabType = newSlab;
            Assert.Equal(0.5, story.SlabThickness);
        }
    }
}
