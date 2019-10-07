using System;
using System.Collections.Generic;
using System.Linq;
using Elements.Geometry;

namespace RoomKit
{
    /// <summary>
    /// Data structure recording space program characteristics and the status of a Room placing process.
    /// </summary>
    public class Scope
    {
        #region Constructors
        /// <summary>
        /// Contructor creates empty Room lists for Circulation, Occupation, Service, and Tenant.
        /// </summary>
        /// <returns>
        /// A new Scope.
        /// </returns>

        public Scope()
        {
            Circulation = new List<Room>();
            Occupant = new List<Room>();
            Service = new List<Room>();
            Tenant = new List<Room>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of Rooms designated as circulation.
        /// </summary>
        public List<Room> Circulation { get; }

        /// <summary>
        /// List of Rooms designated for occupation, rather than circulation.
        /// </summary>
        public List<Room> Occupant { get; }

        /// <summary>
        /// List of Rooms designated for building services.
        /// </summary>
        public List<Room> Service { get; }

        /// <summary>
        /// List of Rooms intended as a series of tenant space containers of other Rooms.
        /// </summary>
        public List<Room> Tenant { get; }

        /// <summary>
        /// List of allocated Circulation, Occupant, and Service Room Perimeters as Polygons.
        /// </summary>
        public List<Polygon> AllocatedAsPolygons
        {
            get
            {
                List<Polygon> allocated = new List<Polygon>();
                foreach (Polygon polygon in CirculationAsPolygons)
                {
                    allocated.Add(polygon);
                }
                foreach (Polygon polygon in OccupantAsPolygons)
                {
                    allocated.Add(polygon);
                }
                foreach (Polygon polygon in ServiceAsPolygons)
                {
                    allocated.Add(polygon);
                }
                return allocated;
            }
        }

        /// <summary>
        /// Area available for horizontal circulation.
        /// </summary>
        public double AreaDesignAvailable
        {
            get
            {
                return AreaTenant - (AreaDesignCirculation + AreaDesignOccupant + AreaService);
            }
        }

        /// <summary>
        /// Intended aggregate area of all Occupant Rooms.
        /// </summary>
        public double AreaDesignCirculation
        {
            get
            {
                double area = 0.0;
                foreach (Room room in Circulation)
                {
                    if (room.DesignSet)
                    {
                        area += room.DesignLength * room.DesignWidth;
                    }
                    else
                    {
                        area += room.DesignArea;
                    }
                }
                return area;
            }
        }

        /// <summary>
        /// Intended aggregate area of all Occupant Rooms.
        /// </summary>
        public double AreaDesignOccupant
        {
            get
            {
                double area = 0.0;
                foreach (Room room in Occupant)
                {
                    if (room.DesignSet)
                    {
                        area += room.DesignLength * room.DesignWidth;
                    }
                    else
                    {
                        area += room.DesignArea;
                    }
                }
                return area;
            }
        }

        /// <summary>
        /// Allocated aggregate area of all placed Circulation Rooms.
        /// </summary>
        public double AreaCirculation
        {
            get
            {
                double area = 0.0;
                foreach (Room room in Circulation)
                {
                    if (room.Perimeter != null)
                    {
                        area += room.Perimeter.Area();
                    }
                }
                return area;
            }
        }

        /// <summary>
        /// Allocated aggregate area of all placed Occupant Rooms.
        /// </summary>
        public double AreaOccupant
        {
            get
            {
                double area = 0.0;
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter != null)
                    {
                        area += room.Perimeter.Area();
                    }
                }
                return area;
            }
        }

        /// <summary>
        /// Aggregate area of all Services Rooms.
        /// </summary>
        public double AreaService
        {
            get
            {
                double area = 0.0;
                foreach (Polygon polygon in ServiceAsPolygons)
                {
                    area += polygon.Area();
                }
                return area;
            }
        }

        /// <summary>
        /// Aggregate area of all occupiable Tenant Rooms.
        /// </summary>
        public double AreaTenant
        {
            get
            {
                double area = 0.0;
                foreach (Polygon polygon in TenantAsPolygons)
                {
                    area += polygon.Area();
                }
                return area;
            }
        }

        /// <summary>
        /// List of all Circulation Room Perimeters as Polygons.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> CirculationAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Circulation)
                {
                    var perimeter = room.Perimeter;
                    if (perimeter != null)
                    {
                        polygons.Add(room.Perimeter);
                    }
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of all Occupant Room Perimeters as Polygons.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> OccupantAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter != null)
                    {
                        polygons.Add(room.Perimeter);
                    }
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of all Service Room Perimeters as Polygons.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> ServiceAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room Room in Service)
                {
                    var perimeter = Room.Perimeter;
                    if (perimeter != null)
                    {
                        polygons.Add(Room.Perimeter);
                    }
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of all Tenant Room Perimeter Polygons.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> TenantAsPolygons
        {
            get
            {
                var polygons = new List<Polygon>();
                foreach (Room Room in Tenant)
                {
                    var perimeter = Room.Perimeter;
                    if (perimeter != null)
                    {
                        polygons.Add(Room.Perimeter);
                    }
                }
                return polygons;
            }
        }

        /// <summary>
        /// List of all Rooms marked as Placed.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public List<Room> Placed
        {
            get
            {
                var placed = new List<Room>();
                foreach (Room room in Occupant)
                {
                    if (room.Placed)
                    {
                        placed.Add(room);
                    }
                }
                return placed;
            }
        }

        /// <summary>
        /// Returns whether all Occupant Rooms have been Placed.
        /// </summary>
        /// <returns>
        /// Returns true if each Room in Occupant has been marked with Room.Placed = true.
        /// </returns>
        public bool PlacedAll
        {
            get
            {
                foreach (Room room in Occupant)
                {
                    if (!room.Placed)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// The quantity of placed Rooms.
        /// </summary>
        public double PlacedQuantity
        {
            get
            {
                double rooms = 0;
                foreach (Room room in Occupant)
                {
                    if (room.Placed)
                    {
                        rooms++;
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// Returns the ratio of the aggregate area of all Occupant Rooms against the Circulation area.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public double RatioCirculation
        {
            get
            {
                return AreaCirculation / AreaOccupant;
            }
        }

        /// <summary>
        /// Returns the ratio of the aggregate area of all designed Occupant Rooms against the designed Circulation area.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public double RatioDesignCirculation
        {
            get
            {
                return AreaDesignCirculation / AreaDesignOccupant;
            }
        }

        /// <summary>
        /// Returns all Rooms with Placed = false.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public List<Room> Unplaced
        {
            get
            {
                var unPlaced = new List<Room>();
                foreach (Room room in Occupant)
                {
                    if (!room.Placed)
                    {
                        unPlaced.Add(room);
                    }
                }
                return unPlaced;
            }
        }

        /// <summary>
        /// The quantity of unplaced Rooms.
        /// </summary>
        public double UnplacedQuantity
        {
            get
            {
                double rooms = 0;
                foreach (Room room in Occupant)
                {
                    if (!room.Placed)
                    {
                        rooms++;
                    }
                }
                return rooms;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Finds the first Occupant Room with the DesignArea value closest to the supplied area.
        /// </summary>
        /// <param name="area">Area to match from the list of all Occupant Room definitions.</param>
        /// <param name="unplaced">Boolean indicating either placed or unplaced Room, unplaced by default.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindByDesignArea(double area, bool unplaced = true)
        {
            List<Room> rooms = null;
            if (unplaced)
            {
                rooms = Unplaced;
            }
            else
            {
                rooms = Occupant;
            }
            Room firstRoom = null;
            foreach (Room room in rooms)
            {
                if (room.DesignSet || room.DesignArea > 0.0)
                {
                    firstRoom = room;
                    break;
                }
            }
            if (firstRoom == null)
            {
                return null;
            }
            var delta = 0.0;
            if (firstRoom.DesignSet)
            {
                delta = Math.Abs(firstRoom.DesignLength * firstRoom.DesignWidth - area);
            }
            else if (firstRoom.DesignArea > 0.0)
            {
                delta = Math.Abs(firstRoom.DesignArea - area);
            }
            var closest = delta;
            Room match = firstRoom;
            foreach (Room room in rooms)
            {
                if (room.DesignSet)
                {
                    delta = Math.Abs(room.DesignLength * room.DesignWidth - area);
                }
                else if (room.DesignArea > 0.0)
                {
                    delta = Math.Abs(room.DesignArea - area);
                }
                if (delta < closest)
                {
                    closest = delta;
                    match = room;
                }
            }
            return match;
        }

        /// <summary>
        /// Finds the first Occupant Room with the designed x and y dimensions closest to the supplied values.
        /// </summary>
        /// <param name="designX">The x-axis dimension to match.</param>
        /// <param name="designY">The y-axis dimension to match.</param>
        /// <param name="unplaced">Boolean indicating either placed or unplaced Room, unplaced by default.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindByDesignXY(double designLength, double designWidth, bool unplaced = true)
        {
            List<Room> rooms = null;
            if (unplaced)
            {
                rooms = Unplaced;
            }
            else
            {
                rooms = Occupant;
            }
            Room firstRoom = null;
            foreach (Room room in Occupant)
            {
                if (room.DesignSet)
                {
                    firstRoom = room;
                    break;
                }
            }
            if (firstRoom == null)
            {
                return null;
            }
            var deltaL = Math.Abs(firstRoom.DesignLength - designLength);
            var deltaW = Math.Abs(firstRoom.DesignWidth - designWidth);
            var closestL = deltaL;
            var closestW = deltaW;
            Room match = firstRoom;
            foreach (Room room in Occupant)
            {
                if (room.DesignSet)
                {
                    deltaL = Math.Abs(firstRoom.DesignLength - designLength);
                    deltaW = Math.Abs(firstRoom.DesignWidth - designWidth);
                }
                if (deltaL < closestL && deltaW < closestW)
                {
                    closestL = deltaL;
                    closestL = deltaW;
                    match = room;
                }
            }
            return match;
        }

        /// <summary>
        /// Finds the first placed or unplaced Room with the specifed name.
        /// </summary>
        /// <param name="name">The name of a Room.</param>
        /// <param name="unplaced">Boolean indicating either placed or unplaced Room, unplaced by default.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindByName(string name, bool unplaced = true)
        {
            List<Room> rooms = null;
            if (unplaced)
            {
                rooms = Unplaced;
            }
            else
            {
                rooms = Occupant;
            }
            foreach (Room room in rooms)
            {
                if (room.Name == name)
                {
                    return room;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the first placed or unplaced Room with the specifed number.
        /// </summary>
        /// <param name="number">The number of a Room.</param>
        /// <param name="unplaced">Boolean indicating either placed or unplaced Room, unplaced by default.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindByNumber(string number, bool unplaced = true)
        {
            List<Room> rooms = null;
            if (unplaced)
            {
                rooms = Unplaced;
            }
            else
            {
                rooms = Occupant;
            }
            foreach (Room room in rooms)
            {
                if (room.Number == number)
                {
                    return room;
                }
            }
            return null;
        }

        #endregion
    }
}
