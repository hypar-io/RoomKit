using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Hypar.Elements;
using Hypar.Geometry;

namespace HyparSpaces.Tests
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
            var places = new List<Polygon>();
            var among = new List<Polygon>();
            for (int i = 0; i < 3; i++)
            {
                places.Add(new Polygon(new[] { new Vector3(),
                                               new Vector3(10.0, 0.0),
                                               new Vector3(10.0, 5.0),
                                               new Vector3(0.0, 5.0) }));
            }
            for (int i = 0; i < 8; i++)
            {
                places.Add(new Polygon(new[] { new Vector3(),
                                               new Vector3(5.0, 0.0),
                                               new Vector3(5.0, 5.0),
                                               new Vector3(0.0, 5.0) }));
            }
            var boxPlace = new TopoBox(places.First());
            var boxPerim = new TopoBox(perimeter);
            among.Add(places.First().MoveFromTo(boxPlace.SW, boxPerim.SW));
            foreach (Polygon place in places)
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
            model.AddElement(new Space(perimeter, elevation: -0.01, height: 0.1, material: BuiltInMaterials.Concrete));
            foreach (Polygon polygon in among)
            {
                var color = new Color((float)Shaper.RandomDouble(0, 1),
                                      (float)Shaper.RandomDouble(0, 1),
                                      (float)Shaper.RandomDouble(0, 1), 0.7f);
                model.AddElement(new Space(polygon, height: 4.0, material: new Material(Guid.NewGuid().ToString(), color)));
            }
            model.SaveGlb("adjacent.glb");
        }

        [Fact]
        public void N()
        {
            var place =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(3.0, 0.0),
                        new Vector3(3.0, 5.0),
                        new Vector3(0.0, 5.0)
                    }
                );
            var northOf =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(5.0, 0.0),
                        new Vector3(5.0, 5.0),
                        new Vector3(0.0, 5.0)
                    }
                );
            var perimeter =
                new Polygon
                (
                    new[]
                    {
                        new Vector3(0.0, 0.0),
                        new Vector3(20.0, 0.0),
                        new Vector3(20.0, 20.0),
                        new Vector3(0.0, 20.0)
                    }
                );
            var among =
                new List<Polygon>
                {
                    new Polygon(
                        new []
                        {
                            new Vector3(2.0, 8.0),
                            new Vector3(12.0, 8.0),
                            new Vector3(12.0, 12.0),
                            new Vector3(2.0, 12.0)
                        })
                };
            //Assert.NotNull(northOf.PlaceNorth(place, perimeter));
            //Assert.Null(northOf.PlaceNorth(place, perimeter, among));
            //var t = new Transform();
            //t.Rotate(Vector3.ZAxis, 90);
            //place = place.Transform(t);
            //Assert.NotNull(northOf.PlaceNorth(place, perimeter, among));
        }
    }
}
