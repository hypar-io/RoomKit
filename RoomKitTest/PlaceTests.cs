using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using RoomKit;
using GeometryEx;

namespace RoomKitTest
{
    public class PlaceTests
    {
        [Fact]
        public void Adjacent()
        {
            var perimeter =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(20.0, 0.0),
                        new Vector3(20.0, 20.0),
                        new Vector3(14.0, 20.0),
                        new Vector3(14.0, 11.0),
                        new Vector3(11.0, 11.0),
                        new Vector3(11.0, 20.0),
                        new Vector3(0.0, 20.0)
                    }
                );
            var Rooms = new List<Polygon>();
            var among = new List<Polygon>();
            for (int i = 0; i < 3; i++)
            {
                Rooms.Add(new Polygon(new[] { Vector3.Origin,
                                               new Vector3(10.0, 0.0),
                                               new Vector3(10.0, 5.0),
                                               new Vector3(0.0, 5.0) }));
            }
            for (int i = 0; i < 8; i++)
            {
                Rooms.Add(new Polygon(new[] { Vector3.Origin,
                                               new Vector3(5.0, 0.0),
                                               new Vector3(5.0, 5.0),
                                               new Vector3(0.0, 5.0) }));
            }
            var boxPlace = new TopoBox(Rooms.First());
            var boxPerim = new TopoBox(perimeter);
            among.Add(Rooms.First().MoveFromTo(boxPlace.SW, boxPerim.SW));
            foreach (Polygon place in Rooms)
            {
                Polygon polygon = null;
                foreach (Polygon inPlace in among)
                {
                    polygon = Place.Adjacent(place, inPlace, perimeter, among);
                    if (polygon != null)
                    {
                        break;
                    }
                }
                if (polygon != null)
                {
                    among.Add(polygon);
                }
            }
            var model = new Model();
            model.AddElement(new Space(perimeter, 
                                       0.1, 
                                       BuiltInMaterials.Concrete, 
                                       new Transform(0.0, 0.0, -0.02), 
                                       null, 
                                       Guid.NewGuid(), 
                                       ""));
            foreach (Polygon polygon in among)
            {
                var color = new Color((float)Shaper.RandomDouble(0, 1),
                                      (float)Shaper.RandomDouble(0, 1),
                                      (float)Shaper.RandomDouble(0, 1), 0.7f);
                model.AddElement(new Space(polygon, height: 4.0, material: new Material(Guid.NewGuid().ToString(), color)));
            }
            model.ToGlTF("../../../../adjacent.glb");
        }

        [Fact]
        public void ByOrient()
        {
            var place =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(4.0, 0.0),
                        new Vector3(4.0, 4.0),
                        new Vector3(0.0, 4.0)
                    }
                );
            var placeL =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(10.0, 0.0),
                        new Vector3(10.0, 10.0),
                        new Vector3(0.0, 10.0)
                    }
                );
            var adjacentTo =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(5.0, 5.0),
                        new Vector3(9.0, 5.0),
                        new Vector3(9.0, 9.0),
                        new Vector3(5.0, 9.0)
                    }
                );
            var within =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(14.0, 0.0),
                        new Vector3(14.0, 14.0),
                        new Vector3(0.0, 14.0)
                    }
                );
            var among =
                new List<Polygon>
                {
                    new Polygon(
                        new []
                        {
                            new Vector3(1.0, 10.0),
                            new Vector3(4.0, 10.0),
                            new Vector3(4.0, 13.0),
                            new Vector3(1.0, 13.0)
                        })
                };
            var placed = Place.ByOrient(place, Orient.SSW, adjacentTo, Orient.N, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(6.0, 9.0), placed.Vertices);
            placed = Place.ByOrient(place, Orient.W, adjacentTo, Orient.ESE, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(9.0, 4.0), placed.Vertices);
            placed = Place.ByOrient(place, Orient.N, adjacentTo, Orient.S, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(5.0, 1.0), placed.Vertices);
            placed = Place.ByOrient(place, Orient.NE, adjacentTo, Orient.WSW, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(1.0, 6.0), placed.Vertices);
            placed = Place.ByOrient(place, Orient.SE, adjacentTo, Orient.NW, within, among);
            Assert.Null(placed);
            placed = Place.ByOrient(place, Orient.SE, adjacentTo, Orient.NW, within);
            Assert.NotNull(placed);
            placed = Place.ByOrient(placeL, Orient.SW, adjacentTo, Orient.N, within);
            Assert.Null(placed);
        }

        [Fact]
        public void NSWE()
        {
            var place =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(3.0, 0.0),
                        new Vector3(3.0, 3.0),
                        new Vector3(0.0, 3.0)
                    }
                );
            var placeL =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(4.0, 0.0),
                        new Vector3(4.0, 4.0),
                        new Vector3(0.0, 4.0)
                    }
                );
            var adjacentTo =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(5.0, 5.0),
                        new Vector3(9.0, 5.0),
                        new Vector3(9.0, 9.0),
                        new Vector3(5.0, 9.0)
                    }
                );
            var within =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(14.0, 0.0),
                        new Vector3(14.0, 14.0),
                        new Vector3(0.0, 14.0)
                    }
                );
            var among =
                new List<Polygon>
                {
                    new Polygon(
                        new []
                        {
                            new Vector3(2.0, 10.0),
                            new Vector3(5.7, 10.0),
                            new Vector3(5.7, 13.0),
                            new Vector3(2.0, 13.0)
                        }),
                    new Polygon(
                        new []
                        {
                            new Vector3(10.0, 8.3),
                            new Vector3(13.0, 8.3),
                            new Vector3(13.0, 12.0),
                            new Vector3(10.0, 12.0)
                        }),
                    new Polygon(
                        new []
                        {
                            new Vector3(8.3, 1.0),
                            new Vector3(12.0, 1.0),
                            new Vector3(12.0, 4.0),
                            new Vector3(8.3, 4.0)
                        }),
                    new Polygon(
                        new []
                        {
                            new Vector3(1.0, 2.0),
                            new Vector3(4.0, 2.0),
                            new Vector3(4.0, 5.7),
                            new Vector3(1.0, 5.7)
                        })
                };
            var placed = Place.N(place, adjacentTo, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(9.0, 9.0), placed.Vertices);
            Assert.Contains(new Vector3(9.0, 12.0), placed.Vertices);
            placed = Place.S(place, adjacentTo, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(5.0, 5.0), placed.Vertices);
            Assert.Contains(new Vector3(5.0, 2.0), placed.Vertices);
            placed = Place.W(place, adjacentTo, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(5.0, 6.0), placed.Vertices);
            Assert.Contains(new Vector3(5.0, 9.0), placed.Vertices);
            placed = Place.E(place, adjacentTo, within, among);
            Assert.NotNull(placed);
            Assert.Contains(new Vector3(9.0, 5.0), placed.Vertices);
            Assert.Contains(new Vector3(12.0, 5.0), placed.Vertices);

            placed = Place.N(placeL, adjacentTo, within, among);
            Assert.Null(placed);
            placed = Place.S(placeL, adjacentTo, within, among);
            Assert.Null(placed);
            placed = Place.W(placeL, adjacentTo, within, among);
            Assert.Null(placed);
            placed = Place.E(placeL, adjacentTo, within, among);
            Assert.Null(placed);
        }

    }
 }
