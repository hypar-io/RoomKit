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
    public class StackTests
    {
        [Fact]
        public Stack MakeStack()
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
            var stack = new Stack();
            for (var i = 0; i < 10; i++)
            {
                stack.AddStory(new Story(perimeter) { Color = Palette.Aqua });
            }
            foreach (var story in stack.Stories)
            {
                story.AddRoom(new Room() { Name = "Retail" });
            }
            var model = new Model();
            foreach (var story in stack.Stories)
            {
                model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/makeStack.glb");
            return stack;
        }

        [Fact]
        public void Area()
        {
            var stack = MakeStack();
            Assert.Equal(4000.0, stack.Area);
        }

        [Fact]
        public void AreaByName()
        {
            var stack = MakeStack();
            Assert.Equal(10.0, stack.AreaByName("Retail"));
        }

        [Fact]
        public void ColorStories()
        {
            var stack = MakeStack();
            stack.ColorStories = Palette.Aqua;
            var model = new Model();
            foreach (var story in stack.Stories)
            {
                model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/stackColor.glb");
        }

        [Fact]
        public void Height()
        {
            var stack = MakeStack();
            Assert.Equal(40.0, stack.Height);
        }

        [Fact]
        public void MoveFromTo()
        {
            var stack = MakeStack();
            stack.MoveFromTo(Vector3.Origin, new Vector3(100.0, 100.0, 100.0));
            var vertices = stack.Stories.First().Perimeter.Vertices;
            Assert.Contains(new Vector3(100.0, 100.0, 100.0), vertices);

            var model = new Model();
            foreach (var story in stack.Stories)
            {
                model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/stackMoveFromTo.glb");
        }

        [Fact]
        public void RoomsByName()
        {
            var stack = MakeStack();
            Assert.Equal(10.0, stack.RoomsByName("Retail").Count);
        }

        [Fact]
        public void Rotate()
        {
            var stack = MakeStack();
            stack.Rotate(Vector3.Origin, 45.0);
            stack.Stories.First().Rotate(Vector3.Origin, -45.0);
            var model = new Model();
            foreach (var story in stack.Stories)
            {
                model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/stackRotate.glb");
        }

        [Fact]
        public void SetStoryHeight()
        {
            var stack = MakeStack();
            stack.SetStoryHeight(3, 10.0);
            var model = new Model();
            foreach (var story in stack.Stories)
            {
                model.AddElement(new Space(story.PerimeterAsProfile, story.Height, story.ColorAsMaterial));
            }
            model.ToGlTF("../../../../RoomKitTest/output/stackStoryHeight.glb");
        }

    }
}
