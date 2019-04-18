using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    public class Tower
    {

        #region Constructors

        public Tower()
        {
            Color = Palette.White;
            Cores = new List<Room>();
            Floors = 0;
            HeightLimit = 0.0;
            Name = "";
            Perimeter = null;
            Stories = new List<Story>();
            StoryHeight = 0.0;
            TargetArea = 0.0;
            TypeID = -1;
            UniqueID = Guid.NewGuid().ToString();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Returns the aggregate area of all Stories in the Tower.
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
                return area;
            }
        }


        /// <summary>
        /// Color imposed on the envelope of new Stories created by the Tower.. 
        /// Ignores null values.
        /// </summary>
        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                if (value != null)
                {
                    color = value;
                }
            }
        }

        /// <summary>
        /// List of all service Cores in the Tower. 
        /// </summary>
        public List<Room> Cores { get; }

        /// <summary>
        /// Elevation of the lowest point of the Tower. 
        /// </summary>
        private double elevation;
        public double Elevation
        {
            get
            {
                return elevation;
            }
            set
            {
                var oldTowerElevation = elevation;
                elevation = value;
                foreach (Room core in Cores)
                {
                    var oldCoreElevation = core.Elevation;
                    core.Elevation += elevation - oldCoreElevation;
                }
                foreach (Story story in Stories)
                {
                    story.Elevation += elevation - oldTowerElevation;
                }
            }
        }

        /// <summary>
        /// Room representing the Tower envelope.
        /// </summary>
        public Room Envelope
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                return
                    new Room()
                    {
                        Color = Color,
                        Height = Height,
                        Perimeter = Perimeter
                    };
            }
        }

        /// <summary>
        /// Polygon representation of the Story Perimeter.
        /// </summary>
        public Polygon EnvelopeAsPolygon
        {
            get { return Envelope.Perimeter; }
        }

        /// <summary>
        /// Space created from Story characteristics.
        /// </summary>
        public Space EnvelopeAsSpace
        {
            get
            {
                if (Perimeter == null)
                {
                    return null;
                }
                var space = new Space(Perimeter, Height, Elevation, new Material(Guid.NewGuid().ToString(), Color));
                space.AddProperty("Height", new NumericProperty(Height, UnitType.Text));
                space.AddProperty("Volume", new NumericProperty(Perimeter.Area * Height, UnitType.Volume));
                return space;
            }
        }

        /// <summary>
        /// Desired quantity of Stories in the Tower. 
        /// </summary>
        private int floors;
        public int Floors
        {
            get { return floors; }
            set
            {
                if (value > 0)
                {
                    floors = value;
                }
            }
        }

        /// <summary>
        /// Highest point of the highest tower story above the zero plane. 
        /// </summary>
        public double Height
        {
            get
            {
                var height = 0.0;
                if (Stories.Count > 0)
                {
                    height = Stories.Last().Elevation + Stories.Last().Height;
                }
                return height;
            }
        }

        /// <summary>
        /// Desired typical Story height in the Tower. 
        /// </summary>
        public double HeightLimit { get; set; }

        /// <summary>
        /// Arbitrary string identifier for this Tower instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Polygon perimeter of the Tower at ground level. 
        /// </summary>
        private Polygon perimeter;
        public Polygon Perimeter
        {
            get { return perimeter; }
            set
            {
                if (value != null)
                {
                    perimeter = value;
                }
            }
        }

        /// <summary>
        /// List of all Slabs from every Story in the Tower. 
        /// </summary>
        public List<Floor> Slabs
        {
            get
            {
                var slabs = new List<Floor>();
                foreach (Story story in Stories)
                {
                    slabs.Add(story.Slab);
                }
                return slabs;
            }
        }

        /// <summary>
        /// List of all Spaces from every Story in the Tower. 
        /// </summary>
        public List<Space> Spaces
        {
            get
            {
                List<Space> spaces = new List<Space>();
                foreach (Story story in Stories)
                {
                    spaces.Add(story.EnvelopeAsSpace);
                    spaces.AddRange(story.CorridorsAsSpaces);
                    spaces.AddRange(story.RoomsAsSpaces);
                }
                foreach (Room room in Cores)
                {
                    spaces.Add(room.AsSpace);
                }
                return spaces;
            }
        }

        /// <summary>
        /// List of all Stories in the Tower. 
        /// </summary>
        public List<Story> Stories = null;

        /// <summary>
        /// Desired typical Story height in the Tower. 
        /// </summary>
        private double storyHeight;
        public double StoryHeight
        {
            get { return storyHeight; }
            set
            {
                if (value > 0.0)
                {
                    storyHeight = value;
                }
            }
        }

        /// <summary>
        /// Target aggregate area for all Stories in the Tower. 
        /// </summary>
        private double targetArea;
        public double TargetArea
        {
            get { return targetArea; }
            set
            {
                if (value > 0.0)
                {
                    targetArea = value;
                }
            }
        }

        /// <summary>
        /// Arbitrary integer identifier of this instance..
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new service Core to the Tower.
        /// </summary>
        /// <param name="perimeter">Polygon perimeter defining the footprint of the service Core.</param> 
        /// <param name="baseStory">Index of the lowest Story whose elevation will serve as the lowest level of the Core.</param> 
        /// <param name="addHeight">Additional height of the Core above the highest Story.</param>
        /// <param name="color">Color of the Core when it it is accessed as a Space.</param>
        /// <returns>
        /// True if the Core is successfully added.
        /// </returns>
        public bool AddCore(Polygon perimeter,
                            int baseStory = 0,
                            double addHeight = 0.0,
                            Color color = null)
        {
            if (baseStory < 0 || baseStory > Stories.Count - 1)
            {
                return false;
            }
            var core = new Room()
            {
                Elevation = Stories[baseStory].Elevation,
                Height = Height + addHeight,
                Perimeter = perimeter
            };
            if (color != null)
            {
                core.Color = color;
            }
            if (Stories[baseStory].Elevation < 0.0)
            {
                core.Height += Stories[baseStory].Elevation * -1;
            }
            Cores.Add(core);
            foreach (Story story in Stories)
            {
                story.AddExclusion(core);
            }
            return true;
        }

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
            return area;
        }

        /// <summary>
        /// Moves all Cores and Stories in the Tower along a 3D vector calculated between the supplied Vector3 points.
        /// </summary>
        /// <param name="from">Vector3 base point of the move.</param>
        /// <param name="to">Vector3 target point of the move.</param>
        /// <returns>
        /// None.
        /// </returns>
        public void MoveFromTo(Vector3 from, Vector3 to)
        {
            foreach (Story story in Stories)
            {
                story.MoveFromTo(from, to);
            }
            foreach (Room core in Cores)
            {
                core.MoveFromTo(from, to);
            }
            if (Perimeter != null)
            {
                Perimeter = Perimeter.MoveFromTo(from, to);
            }
        }

        /// <summary>
        /// Returns a list of Rooms with a specific name.
        /// </summary>
        /// <param name="name">Name of the rooms to retrieve.</param>
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
        /// Rotates the Tower Perimeter and Stories in the horizontal plane around the supplied pivot point.
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
            foreach (Room core in Cores)
            {
                core.Rotate(pivot, angle);
            }
            if (Perimeter != null)
            {
                Perimeter = Perimeter.Rotate(pivot, angle);
            }
        }

        /// <summary>
        /// Creates the Tower by stacking a series of Story instances from the Tower Elevation.
        /// </summary>
        /// <param name="floors">Desired quantity of stacked Stories to form the Tower. If greater than zero, overrides and resets the current Floors property.</param>
        /// <param name="storyHeight">Desired typical Story height of the Tower. If greater than zero, overrides and resets the current StoryHeight property.</param>
        /// <param name="basement">Whether to consider the lowest floor a basement.</param>
        /// <returns>
        /// True if the Tower is successfully stacked.
        /// </returns>
        public bool Stack()
        {
            if (Perimeter == null || storyHeight <= 0.0 || (Floors <= 0.0 && TargetArea <= 0.0))
            {
                return false;
            }
            if (TargetArea > 0.0)
            {
                Floors = (int)Math.Ceiling(TargetArea / Perimeter.Area);
            }
            Stories.Clear();
            var elevation = Elevation;
            for (int i = 0; i < Floors; i++)
            {
                if (elevation + storyHeight > HeightLimit)
                {
                    break;
                }
                var story = new Story()
                {
                    Color = color,
                    Elevation = elevation,
                    Height = storyHeight,
                    Perimeter = perimeter
                };
                Stories.Add(story);
                elevation += storyHeight;
            }
            if (Stories.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sets the height of an index-specified Story and relocates Stories above to accommodate the Story's new height.
        /// </summary>
        /// <param name="story">Index of the Story to affect.</param>
        /// <param name="height">Desired new height of the specified Story.</param>
        /// <param name="interiors">If true also sets any Corridors and Rooms in the Story to the new Height.</param>
        /// <returns>
        /// True if the Tower is successfully stacked.
        /// </returns>
        public bool SetStoryHeight(int story, double height, bool interiors = true, bool upward = true)
        {
            if (story < 0 || story > Stories.Count - 1 || height <= 0.0)
            {
                return false;
            }
            var delta = height - Stories[story].Height;
            if (delta == 0.0)
            {
                return true;
            }
            Stories[story].Height = height;
            int index = 0;
            if (interiors)
            {
                Stories[story].HeightInteriors = height;
            }
            if (upward)
            {
                index = story + 1;
                while (index < Stories.Count)
                {
                    Stories[index].Elevation += delta;
                    index++;
                }
            }
            else 
            {
                Stories[story].Elevation -= delta;
                index = story - 1;
                while (index > -1)
                {
                    Stories[index].Elevation -= delta;
                    index--;
                }
            }
            return true;
        } 
        #endregion
    }
}
