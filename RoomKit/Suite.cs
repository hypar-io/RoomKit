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
            Name = "";
            Number = "";
            Rooms = new List<Room>();
            UniqueID = Guid.NewGuid().ToString();
        }

        public Suite(List<Room> rooms, string name = "", string number = "")
        {
            Name = name;
            Number = number;
            Rooms = new List<Room>(rooms);
            UniqueID = Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Aggregate area of all Room Perimeters.
        /// </summary>
        public double Area
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
        /// Elevation of the Suite relative to the zero plane. A change in this value will also change the elevations of all Rooms in the Suite.
        /// </summary>
        public double Elevation
        {
            get
            {
                if (Rooms.Count() == 0)
                {
                    return 0.0;
                }
                return Rooms.First().Elevation;
            }
            set
            {
                foreach (Room room in Rooms)
                {
                    room.Elevation = value;
                }
            }
        }

        /// <summary>
        /// Sets the height of all Rooms.
        /// </summary>
        public double Height
        {
            set
            {
                foreach (Room room in Rooms)
                {
                    room.Height = value;
                }
            }
        }

        /// <summary>
        /// Arbitrary identifier.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Arbitrary identifier.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// List of Rooms designated as occupiable rooms.
        /// </summary>
        public List<Room> Rooms { get; private set; }

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

        /// <summary>
        /// Returns a list of Rooms with a specific name.
        /// </summary>
        /// <param name="name">Name of the rooms to retrieve.</param>
        /// <returns>
        /// None.
        /// </returns>/// 
        public List<Room> RoomsByName (string name)
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
