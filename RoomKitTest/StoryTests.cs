using System.Collections.Generic;
using System;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using Elements.Spatial;
using GeometryEx;
using RoomKit;


namespace RoomKitTest
{
    public class StoryTests
    {
        [Fact]
        public Story MakeStory()
        {
            var perimeter =
                new Polygon(
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(20.0, 0.0),
                        new Vector3(20.0, 20.0),
                        new Vector3(0.0, 20.0)
                    });
            var story = new Story(perimeter)
            { 
                Color = Palette.Green,
                Elevation = 0.0,
                Height = 8.0,
                Name = "First Floor"
            };
            perimeter = Polygon.Rectangle(Vector3.Origin, new Vector3(10.0, 10.0));
            story.AddRoom(
                new Room(perimeter, 5.0)
                {
                    Color = Palette.Aqua
                });
            perimeter = perimeter.MoveFromTo(Vector3.Origin, new Vector3(10.0, 0.0));
            story.AddRoom(
                new Room(perimeter, 5.0)
                {
                    Color = Palette.Blue
                });
            perimeter = perimeter.MoveFromTo(Vector3.Origin, new Vector3(-10.0, 10.0));
            story.AddRoom(
                new Room(perimeter, 5.0)
                {
                    Color = Palette.Cobalt
                });
            perimeter = perimeter.MoveFromTo(Vector3.Origin, new Vector3(10.0, 0.0));
            story.AddRoom(
                new Room(perimeter, 5.0)
                {
                    Color = Palette.Teal,
                    Name = "Retail"
                });
            perimeter = Polygon.Rectangle(new Vector3(0.0, 8.5), new Vector3(20.0, 11.5));
            story.AddCorridor(
                new Room(perimeter, 6.0)
                {
                    Color = Palette.White
                });
            perimeter = Polygon.Rectangle(new Vector3(8.5, 0.0), new Vector3(11.5, 20.0));
            story.AddCorridor(
                new Room(perimeter, 6.0)
                {
                    Color = Palette.White
                });
            perimeter = Polygon.Rectangle(new Vector3(7.5, 7.5), new Vector3(12.5, 12.5));
            story.AddService(
                new Room(perimeter, 1.0)
                {
                    Color = Palette.Red
                });
            perimeter = Polygon.Rectangle(new Vector3(2.0, 3.0), new Vector3(18.0, 5.0));
            story.AddExclusion(new Room(perimeter, 1.0));
            perimeter = Polygon.Rectangle(new Vector3(10.0, 1.0), new Vector3(15.0, 15.0));
            story.AddOpening(new Room(perimeter, 1.0));

            var model = new Model();
            model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            foreach (Room room in story.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            foreach (Room room in story.Corridors)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            foreach (Room room in story.Services)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../story.glb");
            return story;
        }

        [Fact]
        public void Area()
        {
            var story = MakeStory();
            Assert.Equal(400.0, story.Area);
        }

        [Fact]
        public void AreaAvailable()
        {
            var story = MakeStory();
            Assert.Equal(0.0, story.AreaAvailable, 10);
        }

        [Fact]
        public void AreaByName()
        {
            var story = MakeStory();
            Assert.Equal(60.0, story.AreaByName("Retail"));
        }

        [Fact]
        public void AreaPlaced()
        {
            var story = MakeStory();
            Assert.Equal(308.0, story.AreaPlaced);
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
            Assert.Equal(5, story.Corridors.Count);
        }

        [Fact]
        public void CorridorsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.CorridorsAsPolygons[0]);
            Assert.Equal(5, story.CorridorsAsPolygons.Count);
        }

        [Fact]
        public void ColorCorridors()
        {
            var story = MakeStory();
            story.ColorCorridors = Palette.Yellow;
            foreach (Room room in story.Corridors)
            {
                Assert.Equal(Palette.Yellow, room.Color);
            }
        }

        [Fact]
        public void ColorRooms()
        {
            var story = MakeStory();
            story.ColorRooms = Palette.Magenta;
            foreach (Room room in story.Rooms)
            {
                Assert.Equal(Palette.Magenta, room.Color);
            }
        }

        [Fact]
        public void ColorServices()
        {
            var story = MakeStory();
            story.ColorServices = Palette.Teal;
            foreach (Room room in story.Services)
            {
                Assert.Equal(Palette.Teal, room.Color);
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
            foreach (Room room in rooms)
            {
                Assert.Equal(10.0, room.Elevation);
            }
            var model = new Model();
            model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            foreach (Room room in story.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            story = MakeStory();
            story.Elevation = 0.0;
            model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            foreach (Room room in story.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../storyElevation.glb");
        }

        [Fact]
        public void Exclusions()
        {
            var story = MakeStory();
            Assert.Equal(2, story.Exclusions.Count);
        }

        [Fact]
        public void ExclusionsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.ExclusionsAsPolygons[0]);
            Assert.Equal(2, story.ExclusionsAsPolygons.Count);
        }

        [Fact]
        public void Height()
        {
            var story = MakeStory();
            Assert.Equal(8.0, story.Height);
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
            Assert.IsType<Polygon>(story.InteriorsAsPolygons[0]);
            Assert.Equal(10.0, story.InteriorsAsPolygons.Count);
        }

        [Fact]
        public void MoveFromTo()
        {
            var story = MakeStory();
            story.MoveFromTo(Vector3.Origin, new Vector3(20.0, 20.0, 20.0));
            var vertices = story.Perimeter.Vertices;
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 20.0 && p.Z == 20.0);
            Assert.Contains(vertices, p => p.X == 40.0 && p.Y == 40.0 && p.Z == 20.0);
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
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 0.0);
            Assert.Contains(vertices, p => p.X == 20.0 && p.Y == 20.0);
            Assert.Contains(vertices, p => p.X == 0.0 && p.Y == 20.0);
        }

        [Fact]
        public void PlanByCenterLine()
        {
            var perimeter =
                new Polygon(
                    new[]
                    {
                        new Vector3(30.0, 0.0),
                        new Vector3(70.0, 40.0),
                        new Vector3(130.0, 40.0),
                        new Vector3(170.0, 0.0),
                        new Vector3(190.0, 20.0),
                        new Vector3(140.0, 70.0),
                        new Vector3(60.0, 70.0),
                        new Vector3(10.0, 20.0)
                    });
            var stories = new List<Story>();
            var elevation = -8.0;
            for (var i = 0; i < 1; i++)
            {
                stories.Add(
                    new Story(perimeter)
                    {
                        Color = Palette.Aqua,
                        Elevation = elevation,
                        Height = 6.0,
                    });
                elevation += 8.0;
            }
            var ctrLine =
                new Polyline(
                    new[]
                    {
                        new Vector3(20.0, 10.0),
                        new Vector3(65.0, 55.0),
                        new Vector3(135.0, 55.0),
                        new Vector3(180.0, 10.0)
                    });
            var model = new Model();
            foreach (var story in stories)
            {
                story.PlanByCenterline(ctrLine, 100.0, 2.0, 4.0);
                foreach (Room room in story.Rooms)
                {
                    model.AddElement(new Space(room.PerimeterAsProfile,
                                               room.Height,
                                               room.ColorAsMaterial));
                }
                foreach (Room room in story.Corridors)
                {
                    model.AddElement(new Space(room.PerimeterAsProfile,
                                               room.Height,
                                               room.ColorAsMaterial));
                }
            }
            model.ToGlTF("../../../../storyPlanByCenterline.glb");
        }

        [Fact]
        public void PlanGrid()
        {
            var perimeter =
                new Polygon(
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(100.0, 0.0),
                        new Vector3(100.0, 100.0),
                        new Vector3(0.0, 100.0)
                    });
            var story =
                new Story(perimeter)
                {
                    Color = Palette.Aqua,
                    Height = 5.0
                };
            var model = new Model();
            var roomRows = story.PlanGrid(10.0, 10.0, 2.0, false);
            foreach (var room in story.Corridors)
            {
                model.AddElement(new Space(room.PerimeterAsProfile,
                                           room.Height,
                                           room.ColorAsMaterial));
            }
            foreach (var row in roomRows)
            {
                model.AddElement(new Space(row.Perimeter,
                                           story.Height,
                                           new Material(Palette.Aqua, 0.0, 0.0, Guid.NewGuid(), "")));
            }
            model.ToGlTF("../../../../storyPlanGrid.glb");
        }

        [Fact]
        public void Rooms()
        {
            var story = MakeStory();
            Assert.Equal(4.0, story.Rooms.Count);
        }

        [Fact]
        public void RoomsAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.RoomsAsPolygons[0]);
            Assert.Equal(4, story.RoomsAsPolygons.Count);
        }

        [Fact]
        public void RoomsByName()
        {
            var story = MakeStory();
            Assert.Single(story.RoomsByName("Retail"));
        }

        [Fact]
        public void Rotate()
        {
            var story = MakeStory();
            story.Rotate(Vector3.Origin, 45.0);
            var model = new Model();
            foreach (var room in story.Rooms)
            {
                model.AddElement(new Space(room.PerimeterAsProfile, room.Height, room.ColorAsMaterial));
            }
            model.ToGlTF("../../../../StoryRotate.glb");
        }

        [Fact]
        public void Services()
        {
            var story = MakeStory();
            Assert.Single(story.Services);
        }

        [Fact]
        public void ServicesAsPolygons()
        {
            var story = MakeStory();
            Assert.IsType<Polygon>(story.ServicesAsPolygons[0]);
            Assert.Single(story.ServicesAsPolygons);
        }
    }
}
