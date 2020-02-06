using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages the geometry of a floor with openings and Rooms representing excluded areas, corridors, occupied rooms, and services.
    /// </summary>
    public class Suite
    {
        #region Constructors

        /// <summary>
        /// Default constructor creates an empty Suite.
        /// </summary>
        /// <returns>
        /// A new Suite.
        /// </returns>

        public Suite()
        {
            Rooms = new List<Room>();
            Name = "";
            Number = "";
            UniqueID = Guid.NewGuid().ToString();
        }

        public Suite(string name = "", string number = "", List<Room> rooms = null)
        {
            Rooms = rooms == null ? new List<Room>() : new List<Room>(rooms);
            Name = name == null ? "": name;
            Number = number == null ? "" : number; ;
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Aggregate area of all Room Perimeters.
        /// </summary>
        public double AreaofRooms
        {
            get 
            {
                var area = 0.0;
                foreach (var room in Rooms)
                {
                    area += room.Area;
                }
                return area;
            }
        }

        /// <summary>
        /// Count of all Rooms.
        /// </summary>
        public double Count
        {
            get { return Rooms.Count(); }
        }

        /// <summary>
        /// Arbitrary identifier.
        /// </summary>
        private string name;
        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                name = value;
                foreach (Room room in Rooms)
                {
                    room.Suite = value;
                }
            }
        }

        /// <summary>
        /// Arbitrary identifier.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// List of Rooms designated as occupiable rooms.
        /// </summary>
        public List<Room> Rooms { get; }

        /// <summary>
        /// Placed Rooms.
        /// </summary>
        public List<Room> RoomsPlaced
        {
            get
            {
                var rooms = new List<Room>();
                foreach (var room in Rooms)
                {
                    if (room.Placed)
                    {
                        rooms.Add(room);
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// Unplaced Rooms.
        /// </summary>
        public List<Room> RoomsUnplaced
        {
            get
            {
                var rooms = new List<Room>();
                foreach (var room in Rooms)
                {
                    if (!room.Placed)
                    {
                        rooms.Add(room);
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// UUID for this instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a Room to the Rooms list.
        /// </summary>
        /// <param name="room">Room to add.</param>
        /// <param name="fit">Indicates whether the new Room should mutually fit to other Story features. Default is true.</param>
        /// <returns>
        /// True if one or more Rooms were added to the Story.
        /// </returns>
        public void AddRoom(Room room, int position = int.MaxValue)
        {
            room.Suite = Name;
            room.SuiteID = UniqueID;
            if (position == int.MaxValue)
            {
                Rooms.Add(room);
                return;
            }
            if (position <= 0)
            {
                Rooms.Insert(0, room);
                return;
            }
            try
            {
                Rooms.Insert(position, room);
            }
            catch
            {
                Rooms.Add(room);
            }
        }

        /// <summary>
        /// Returns the aggregate area of all Rooms with a supplied name.
        /// </summary>
        /// <param name="name">Name of the Rooms to retrieve.</param>
        /// <returns>
        /// Aggregate area of all Rooms with the specified name.
        /// </returns>/// 
        public double AreaByName(string name)
        {
            var area = 0.0;
            foreach (var room in Rooms)
            {
                if (room.Name == name)
                {
                    area += room.Area;
                }
            }
            return area;
        }

        /// Returns a Rooms with a specific UniqueID.
        /// </summary>
        /// <param name="name">Name of the rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public Room RoomByID(string uniqueID)
        {
            return Rooms.Find(r => r.UniqueID == uniqueID);
        }

        /// <summary>
        /// Returns a list of Rooms with an area within a tolerance.
        /// </summary>
        /// <param name="area">Area of the rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public List<Room> RoomsByArea (double area, double tolerance = 0.01)
        {
            var rooms = new List<Room>();
            foreach (var room in Rooms)
            {
                if (Math.Abs(room.Area - area) <= tolerance)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
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
            foreach (var room in Rooms)
            {
                if (room.Name == name)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }

        #endregion
    }
}
