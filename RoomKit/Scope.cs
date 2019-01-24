using System;
using System.Collections.Generic;
using System.Linq;
using Hypar.Geometry;

namespace RoomKit
{
    /// <summary>
    /// A data structure recording space program characteristics and status of a Room placing process.
    /// </summary>
    public class Scope
    {
        public List<Room> Circulation { get; }
        public List<Room> Occupant { get; }
        public List<Room> Service { get; }
        public List<Room> Tenant { get; }

        public Scope()
        {
            Circulation = new List<Room>();
            Occupant = new List<Room>();
            Service = new List<Room>();
            Tenant = new List<Room>();
        }

        /// <summary>
        /// The area available for horizontal circulation.
        /// </summary>
        public double AreaDesignCirculation
        {
            get
            {
                return AreaTenant - (DesignAreaOccupant + AreaService);
            }
        }

        /// <summary>
        /// The allocated aggregate area of all placed occupant rooms.
        /// </summary>
        public double AreaRooms
        {
            get
            {
                double area = 0.0;
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter != null)
                    {
                        area += room.Perimeter.Area;
                    }
                }
                return area;
            }
        }

        /// <summary>
        /// The aggregate area of all services.
        /// </summary>
        public double AreaService
        {
            get
            {
                double area = 0.0;
                foreach (Polygon polygon in PerimetersService)
                {
                    area += polygon.Area;
                }
                return area;
            }
        }

        /// <summary>
        /// The aggregate of all occupiable tenant areas.
        /// </summary>
        public double AreaTenant
        {
            get
            {
                double area = 0.0;
                foreach (Polygon polygon in PerimetersTenant)
                {
                    area += polygon.Area;
                }
                foreach (Polygon polygon in PerimetersService)
                {
                    area -= polygon.Area;
                }
                return area;
            }
        }

        /// <summary>
        /// The intended aggregate area of all occupant rooms.
        /// </summary>
        public double DesignAreaOccupant
        {
            get
            {
                double area = 0.0;
                foreach (Room room in Occupant)
                {
                    area += room.DesignArea;
                }
                return area;
            }
        }

        /// <summary>
        /// The maximum fixed dimension of Occupant Rooms.
        /// </summary>
        public double MaxRoomDim
        {
            get
            {
                var sorter = new List<Room>(Occupant);
                sorter.Sort((a, b) => b.DesignY.CompareTo(a.DesignY));
                var maxDepth = sorter[0].DesignY;
                sorter.Sort((a, b) => b.DesignX.CompareTo(a.DesignX));
                var maxWidth = sorter[0].DesignX;
                if(maxDepth >= maxWidth)
                {
                    return maxDepth;
                }
                else
                {
                    return maxWidth;
                }
            }
        }

        /// <summary>
        /// The minimum fixed dimension of Occupant Rooms.
        /// </summary>
        public double MinRoomDim
        {
            get
            {
                var sorter = new List<Room>(Occupant);
                sorter.Sort((a, b) => a.DesignY.CompareTo(b.DesignY));
                var minDepth = sorter.Find(d => d.DesignY > 0).DesignY;
                sorter.Sort((a, b) => a.DesignX.CompareTo(b.DesignX));
                var minWidth = sorter.Find(d => d.DesignX > 0).DesignX;
                if (minDepth <= minWidth)
                {
                    return minDepth;
                }
                else
                {
                    return minWidth;
                }
            }
        }

        /// <summary>
        /// A list of allocated Circulation, Occupant, and Service Polygon areas.
        /// </summary>
        public List<Polygon> PerimetersAllocated
        {
            get
            {
                List<Polygon> allocated = new List<Polygon>();
                foreach (Polygon polygon in PerimetersCirculation)
                {
                    allocated.Add(polygon);
                }
                foreach (Polygon polygon in PerimetersOccupant)
                {
                    allocated.Add(polygon);
                }
                foreach (Polygon polygon in PerimetersService)
                {
                    allocated.Add(polygon);
                }
                return allocated;
            }
        }

        /// <summary>
        /// Returns a list of all Polygons in the Circulation category.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> PerimetersCirculation
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
        /// Returns a list of all Polygons in the Occupant category.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> PerimetersOccupant
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
        /// Returns a list of all Polygons in the Service category.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> PerimetersService
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
        /// Returns a list of all Polygons in the Tenant category.
        /// </summary>
        /// <returns>
        /// A list of Polygons.
        /// </returns>
        public List<Polygon> PerimetersTenant
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
        /// Returns all placed Rooms.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public IList<Room> Placed
        {
            get
            {
                var placed = new List<Room>();
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter != null)
                    {
                        placed.Add(room);
                    }
                }
                return placed;
            }
        }

        /// <summary>
        /// Returns whether all spaces in Spaces have been placed.
        /// </summary>
        /// <returns>
        /// Returns true if all spaces in Spaces have been marked as placed.
        /// </returns>
        public bool PlacedAll
        {
            get
            {
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter == null)
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
        public double QuantityPlaced
        {
            get
            {
                double rooms = 0;
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter != null)
                    {
                        rooms++;
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// The quantity of unplaced Rooms.
        /// </summary>
        public double QuantityUnplaced
        {
            get
            {
                double rooms = 0;
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter == null)
                    {
                        rooms++;
                    }
                }
                return rooms;
            }
        }

        /// <summary>
        /// Returns the ratio of the aggregate area of all rooms against the circulation area.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public double RatioCirculation
        {
            get
            {
                return AreaRooms / (AreaTenant - AreaRooms);
            }
        }

        /// <summary>
        /// Returns all unplaced Rooms.
        /// </summary>
        /// <returns>
        /// A list of Rooms.
        /// </returns>
        public IList<Room> Unplaced
        {
            get
            {
                var unPlaced = new List<Room>();
                foreach (Room room in Occupant)
                {
                    if (room.Perimeter == null)
                    {
                        unPlaced.Add(room);
                    }
                }
                return unPlaced;
            }
        }

        /// <summary>
        /// Finds the room with the design area closest to the supplied area.
        /// </summary>
        /// <param name="area">The area to match from the list of all Room definitions.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room Find(double area)
        {
            Room match = null;
            var closest = 0.0;
            if (Occupant.First().DesignArea > 0.0)
            {
                closest = Math.Abs(Occupant.First().DesignArea - area);
            }
            else
            {
                closest = Math.Abs(Occupant.First().DesignX * Occupant.First().DesignY);
            }
            foreach (Room room in Occupant)
            {
                var delta = 0.0;
                if (room.DesignArea > 0.0)
                {
                    delta = Math.Abs(room.DesignArea - area);
                }
                else
                {
                    delta = Math.Abs(room.DesignX * room.DesignY);
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
        /// Finds the room with the designed x and y dimensions closest to the supplied values.
        /// </summary>
        /// <param name="designX">The x-axis dimension to match.</param>
        /// <param name="designY">The y-axis dimension to match.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room Find(double designX, double designY)
        {
            Room match = null;
            var closestRatio = Math.Abs(designX / designY);
            foreach (Room room in Occupant)
            {
                var deltaProp = Math.Abs(room.DesignX / room.DesignY);
                if (deltaProp < closestRatio)
                {
                    closestRatio = deltaProp;
                    match = room;
                }
            }
            return match;
        }

        /// <summary>
        /// Finds the unplaced Room with the design area closest to the supplied area.
        /// </summary>
        /// <param name="area">The area to match from the list of all unplaced Room definitions.</param>
        /// <returns>
        /// An unplaced Room.
        /// </returns>
        public Room FindUnplaced(double area)
        {
            Room match = null;
            var closest = Math.Abs(Occupant[0].DesignArea - area);
            foreach (Room room in Occupant)
            {
                if (room.Perimeter == null)
                {
                    var delta = Math.Abs(room.DesignArea - area);
                    if (delta < closest)
                    {
                        closest = delta;
                        match = room;
                    }
                }
            }
            return match;
        }

        /// <summary>
        /// Finds the unplaced Room with the designed x and y dimensions closest to the supplied values.
        /// </summary>
        /// <param name="designX">The x-axis dimension to match.</param>
        /// <param name="designY">The y-axis dimension to match.</param>
        /// <returns>
        /// An unplaced Room.
        /// </returns> 
        public Room FindUnplaced(double designX, double designY)
        {
            Room match = null;
            var closestProp = Math.Abs(designX / designY);
            foreach (Room room in Occupant)
            {
                if (room.Perimeter == null)
                {
                    var deltaProp = Math.Abs(room.DesignX / room.DesignY);
                    if (deltaProp < closestProp)
                    {
                        closestProp = deltaProp;
                        match = room;
                    }
                }
            }
            return match;
        }

        /// <summary>
        /// Finds the first unplaced Room with the specifed ResourceID.
        /// </summary>
        /// <param name="resourceID">The integer ID of a Room type.</param>
        /// <returns>
        /// A Room.
        /// </returns>
        public Room FindUnplaced(int resourceID)
        {
            foreach (Room room in Occupant)
            {
                if (room.Perimeter == null && room.ResourceID == resourceID)
                {
                    return room;
                }
            }
            return null;
        }
    }
}
