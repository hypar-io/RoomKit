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
        public double AreaAvailable
        {
            get
            {
                return AreaTenant - (AreaCirculation + AreaOccupant + AreaService);
            }
        }

        /// <summary>
        /// Intended aggregate area of all Occupant Rooms.
        /// </summary>
        public double AreaProgram
        {
            get
            {
                double area = 0.0;
                foreach (var room in Occupant)
                {
                    area += room.Area;
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
                var area = 0.0;
                foreach (var room in Circulation)
                {
                    if (room.Perimeter != null)
                    {
                        area += room.Area;
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
                var area = 0.0;
                foreach (var room in Occupant)
                {
                    if (room.Placed)
                    {
                        area += room.Area;
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
                var area = 0.0;
                foreach (var room in Service)
                {
                    area += room.Area;
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
                var area = 0.0;
                foreach (var room in Tenant)
                {
                    area += room.Area;
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
                foreach (var room in Circulation)
                {
                    if (room.Placed)
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
                    if (room.Placed)
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
                foreach (var room in Service)
                {
                    if (room.Placed)
                    {
                        polygons.Add(room.Perimeter);
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
                foreach (var room in Tenant)
                {
                    if (room.Placed)
                    {
                        polygons.Add(room.Perimeter);
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
        //public double RatioDesignCirculation
        //{
        //    get
        //    {
        //        return AreaDesignCirculation / AreaDesignOccupant;
        //    }
        //}

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
        /// Finds the first Occupant Room with the intended value closest to the supplied area.
        /// </summary>
        /// <param name="area">Area to match from the list of all Occupant Room definitions.</param>
        /// <param name="unplaced">Boolean indicating either placed or unplaced Room, unplaced by default.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindByArea(double area, bool unplaced = true)
        {
            var rooms = new List<Room>();
            rooms = unplaced ? Unplaced : Placed;
            if (rooms.Count == 0)
            {
                return null;
            }
            return rooms.OrderBy(r => Math.Abs(r.Area - area)).First();
        }

        /// <summary>
        /// Finds the first Occupant Room with the designed x and y dimension ratio closest to the supplied value.
        /// </summary>
        /// <param name="ratio">X to Y dimension ratio.</param>
        /// <param name="unplaced">Boolean indicating either placed or unplaced Room, unplaced by default.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindByRatio(double ratio, bool unplaced = true)
        {
            var rooms = unplaced ? Unplaced : Placed;
            if (rooms.Count == 0)
            {
                return null;
            }
            if (unplaced)
            {
                return rooms.OrderBy(r => Math.Abs(r.DesignRatio - ratio)).First();
            }
            else
            {
                return rooms.OrderBy(r => Math.Abs(r.Ratio - ratio)).First();
            }
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
            var rooms = unplaced ? Unplaced : Placed;
            if (rooms.Count == 0)
            {
                return null;
            }
            return rooms.Find(r => r.Name == name);
        }

        #endregion
    }
}
