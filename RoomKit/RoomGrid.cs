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
    public class RoomGrid
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
        public RoomGrid(Polygon perimeter, double height,
                        double rowLength, double roomDepth,
                        double corridorWidth = 3.0, double axis = 0.0, 
                        GridPosition position = GridPosition.CenterXY)
        {
            Axis = axis;
            Corridors = new List<Room>();
            CorridorWidth = corridorWidth;
            Name = "";
            Perimeter = perimeter;
            perimeterJig = perimeter;
            RoomDepth = roomDepth + (corridorWidth * 0.5);
            RoomRows = new List<RoomRow>();
            RowLength = rowLength;
            UniqueID = Guid.NewGuid().ToString();

            MakeCorridors(height, position);
            MakeRoomRows(position);
        }

        private readonly Polygon perimeterJig;
        private Grid grid;

        /// <summary>
        /// Adds a Room to the first RoomRow with sufficient available area.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool AddRoom(Room room)
        {
            foreach (var roomRow in RoomRows)
            {
                if (roomRow.AddRoom(room))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates Corridors along grid lines.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="position"></param>
        private void MakeCorridors(double height, GridPosition position)
        {
            grid = new Grid(perimeterJig, RowLength, RoomDepth * 2, Axis, CorridorWidth * 0.5, true, true, position);
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
            var corridors = Shaper.Merge(polygons);
            foreach (var polygon in corridors)
            {
                var corridor = polygon.FitMost(perimeterJig);
                if (corridor != null)
                {
                    Corridors.Add(new Room(corridor, height));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        private void MakeRoomRows(GridPosition position)
        {
            //var grid = new Grid(perimeterJig, RowLength, RoomDepth, Axis, position);
            foreach (var cell in grid.Cells)
            {
                var row = cell.Segments().First();
                var fit = cell.FitMost(perimeterJig);
                if (fit == null)
                {
                    continue;
                }
                fit = fit.FitAmong(CorridorsAsPolygons);
                if (fit == null)
                {
                    continue;
                }
                if (fit.Vertices.Contains(row.Start))
                {
                    fit = fit.RewindFrom(row.Start);
                }
                else
                {
                    var start = fit.Segments().OrderByDescending(s => s.Length()).First().Start;
                    fit = fit.RewindFrom(start);
                }
                RoomRows.Add(new RoomRow(fit));
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
