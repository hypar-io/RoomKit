using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using Elements.Geometry;
using GeometryEx;

namespace RoomKit
{
    /// <summary>
    /// Creates and manages Rooms within a perimeter.
    /// </summary>
    public class Sector
    {

        #region Constructors

        /// <summary>
        /// Creates corridors and RoomRows from a perimeter.
        /// </summary>
        /// <param name="perimeter">Perimeter of the space to divide.</param>
        /// <param name="roomDepth"></param>
        /// <param name="corridorWidth"></param>
        /// <param name="rowLength"></param>
        /// <param name="axis"></param>
        /// /// <param name="rowCap"></param>
        public Sector(Polygon perimeter, double height,
                      double rowLength, double roomDepth,
                      double corridorWidth = 3.0, double axis = 0.0, 
                      GridPosition position = GridPosition.CenterXY)
        {
            Axis = axis;
            Corridors = new List<Room>();
            CorridorWidth = corridorWidth;
            Name = "";
            Perimeter = perimeter;
            perimeterJig = Perimeter.Rotate(Vector3.Origin, Axis * -1);
            RoomDepth = roomDepth;
            RoomRows = new List<RoomRow>();
            RowLength = rowLength;
            UniqueID = Guid.NewGuid().ToString();

            MakeCorridors(height, position);
            MakeRoomRows(position);
            foreach (var corridor in Corridors)
            {
                corridor.Rotate(Vector3.Origin, Axis);
            }

            // reorder lists by centers here
        }

        private readonly Polygon perimeterJig;

        private void MakeCorridors(double height, GridPosition position)
        {
            var grid = new Grid(perimeterJig, RowLength, RoomDepth * 2, 0.0, position);
            var pathsX = new List<Polygon>();
            foreach (var line in grid.LinesX)
            {
                pathsX.Add(line.Thicken(CorridorWidth));
            }
            var pathsY = new List<Polygon>();
            foreach (var line in grid.LinesY)
            {
                pathsY.Add(line.Thicken(CorridorWidth));
            }
            var polygons = new List<Polygon>(pathsX);
            foreach (var path in pathsY)
            {
                polygons.AddRange(path.Difference(pathsX));
            }
            foreach (var polygon in polygons)
            {
                var corridors = Shaper.FitTo(polygon, perimeterJig);
                if (corridors != null)
                {
                    Corridors.Add(new Room(corridors.First(), height));
                }
            }
        }


        private void MakeRoomRows(GridPosition position)
        {
            var grid = new Grid(perimeterJig, RowLength, RoomDepth, 0.0, position);
            var cells = new List<Polygon>();
            foreach (var cell in grid.Cells)
            {
                cells.Add(cell.Rotate(Vector3.Origin, Axis));
            }              
            foreach (var cell in cells)
            {
                if (perimeterJig.Intersects(cell))
                {
                    RoomRows.Add(new RoomRow(Shaper.FitTo(cell, Perimeter).First()));
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// List of corridors.
        /// </summary>
        public double Axis { get; }

        /// <summary>
        /// List of corridors.
        /// </summary>
        public List<Room> Corridors { get; private set; }

        /// <summary>
        /// List of corridors.
        /// </summary>
        public List<Polygon> CorridorsAsPolygons 
        { 
            get
            {
                var polygons = new List<Polygon>();
                foreach (var room in Corridors)
                {
                    polygons.Add(room.Perimeter);
                }
                return polygons;
            }
        }

        /// <summary>
        /// Width of all corridors.
        /// </summary>
        public double CorridorWidth { get; }

        /// <summary>
        /// Arbitrary string identifier for this RoomGroup.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Polygon within which all Rooms are placed.
        /// </summary>
        public Polygon Perimeter { get; }

        /// <summary>
        /// Target depth of all Rooms from facing corridor.
        /// </summary>
        public double RoomDepth { get; }

        /// <summary>
        /// RoomRows created by this Division.
        /// </summary>
        public List<RoomRow>RoomRows { get; }

        /// <summary>
        /// Target depth of all Rooms from facing corridor.
        /// </summary>
        public double RowLength { get; }

        /// <summary>
        /// UUID for this RoomGroup instance, set on initialization.
        /// </summary>
        public string UniqueID { get; }

        #endregion 
    }
}
