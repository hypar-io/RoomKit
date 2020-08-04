using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages a collection of stories as a tower.
    /// </summary>
    public class Stack
    {

        #region Constructors

        /// <summary>
        /// Constructor by default sets a kilometer limit on tower height and a standard 3 meter story height.
        /// </summary>
        public Stack()
        {
            Stories = new List<Story>();
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the aggregate area of all Stories.
        /// </summary>
        public double Area
        {
            get
            {
                var area = 0.0;
                foreach (Story story in Stories)
                {
                    area += story.Area;
                }
                return Math.Round(area, Room.PRECISION);
            }
        }

        /// <summary>
        /// Sets the color for all Stories. 
        /// </summary>
        public Color ColorStories
        {
            set
            {
                foreach (var story in Stories)
                {
                    story.Color = value;
                }
            }
        }

        /// <summary>
        /// Elevation of the lowest point of the Tower. 
        /// </summary>
        private double elevation;
        public double Elevation
        {
            get
            {
                if (Stories.Count > 0)
                {
                    return Stories.First().Elevation;
                }
                return Math.Round(elevation, Room.PRECISION);
            }
            set
            {
                elevation = value;
                if (Stories.Count > 0)
                {
                    Stories.First().Elevation = elevation;
                    for (var i = 1; i < Stories.Count; i++)
                    {
                        var lwrStory = Stories[i - 1];
                        Stories[i].Elevation = lwrStory.Elevation + lwrStory.Height;
                    }
                }

            }
        }

        /// <summary>
        /// Highest point of the highest tower story above the zero plane. 
        /// </summary>
        public double Height
        {
            get { return Stories.Count <= 0 ? 0.0 : Stories.Last().Elevation + Stories.Last().Height; }
        }

        /// <summary>
        /// Arbitrary string identifier for this Tower instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of Stories. 
        /// </summary>
        public List<Story> Stories { get; }

        public string UniqueID { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the aggregate area of all Rooms with a supplied name.
        /// </summary>
        /// <param name="name">Name of the Rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public double AreaByName(string name)
        {
            var area = 0.0;
            foreach (Story story in Stories)
            {
                foreach (var room in story.Rooms)
                {
                    if (room.Name == name)
                    {
                        area += room.Area;
                    }
                }
            }
            return Math.Round(area, Room.PRECISION);
        }

        /// <summary>
        /// Returns a list of Rooms with a specific name.
        /// </summary>
        /// <param name="name">Name of the Rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public List<Room> RoomsByName(string name)
        {
            var rooms = new List<Room>();
            foreach (Story story in Stories)
            {
                foreach (var room in story.Rooms)
                {
                    if (room.Name == name)
                    {
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// Adds a new highest Story.
        /// </summary>
        /// <returns>
        /// True if the Story is added.
        /// </returns>
        public bool AddStory(Story story)
        {
            var elevation = Elevation;
            if (Stories.Count > 0)
            {
                elevation = Height;
            }
            story.Elevation = elevation;
            Stories.Add(story);
            return true;
        }

        /// <summary>
        /// Moves all Stories in the Stack along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            foreach (var story in Stories)
            {
                story.MoveFromTo(from, to);
            }
        }

        /// <summary>
        /// Rotates the Stories in the horizontal plane around the supplied pivot point.
        /// </summary>
        /// <param name="pivot">Vector3 point around which the Room Perimeter will be rotated.</param> 
        /// <param name="angle">Angle in degrees to rotate the Perimeter.</param> 
        /// <returns>
        /// None.
        /// </returns>
        public void Rotate(Vector3 pivot, double angle)
        {
            foreach (Story story in Stories)
            {
                story.Rotate(pivot, angle);
            }
        }

        /// <summary>
        /// Sets the height of an index-specified Story and relocates Stories above to accommodate the Story's new height.
        /// </summary>
        /// <param name="story">Index of the Story to affect.</param>
        /// <param name="height">Desired new height of the specified Story.</param>
        /// <returns>
        /// True if the Stack is successfully restacked.
        /// </returns>
        public bool SetStoryHeight(int story, double height)
        {
            if (story < 0 || story > Stories.Count - 1 || height <= 0.0) return false;
            var delta = height - Stories[story].Height;
            if (delta.NearEqual(0.0)) return true;
            Stories[story].Height = height;
            var index = story + 1;
            while (index < Stories.Count)
            {
                Stories[index].Elevation += delta;
                index++;
            }
            return true;
        }

        #endregion
    }
}
