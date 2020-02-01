using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Elements;
using Elements.Geometry;
using RoomKit;
using GeometryEx;

namespace RoomKitTest
{
    public class ScopeTests
    {
        //private List<Room> MakeCirculation()
        //{
        //    var rooms = new List<Room>();
        //    var room = new Room()
        //    {
        //        Color = Palette.Yellow,
        //        DesignXYZ = new Vector3(20.0, 2.0, 3.0),
        //        Name = "Corridor West-East"
        //    };
        //    room.SetPerimeter();
        //    rooms.Add(room);
        //    room = new Room()
        //    {
        //        Color = Palette.Yellow,
        //        DesignXYZ = new Vector3(2.0, 20.0, 3.0),
        //        Name = "Corridor North-South",
        //    };
        //    room.SetPerimeter();
        //    rooms.Add(room);
        //    //80 sm
        //    return rooms;
        //}

        //private List<Room> MakeOccupant()
        //{
        //    var rooms = new List<Room>();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var room = new Room()
        //        {
        //            Color = Palette.Green,
        //            DesignXYZ = new Vector3(5.0, 4.0, 3.0),
        //            Name = "Office Large",
        //            Number = "0"
        //        };
        //        room.SetPerimeter();
        //        rooms.Add(room);
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var room = new Room()
        //        {
        //            Color = Palette.Lime,
        //            DesignArea = 12.0,
        //            Height = 3.0,
        //            Name = "Office Medium",
        //            Number = "1"
        //        };
        //        room.SetPerimeter();
        //        rooms.Add(room);
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var room = new Room()
        //        {
        //            Color = Palette.Mint,
        //            DesignXYZ = new Vector3(3.0, 3.0, 3.0),
        //            Name = "Office Small",
        //            Number = "2"
        //        };
        //        room.SetPerimeter();
        //        rooms.Add(room);
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var room = new Room()
        //        {
        //            Color = Palette.Purple,
        //            DesignArea = 16.0,
        //            Height = 5.0,
        //            Name = "Conference Large",
        //            Number = "3"
        //        };
        //        room.SetPerimeter();
        //        rooms.Add(room);
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var room = new Room()
        //        {
        //            Color = Palette.Magenta,
        //            DesignXYZ = new Vector3(6.0, 4.0, 5.0),
        //            Name = "Conference Medium",
        //            Number = "4"
        //        };
        //        room.SetPerimeter();
        //        rooms.Add(room);
        //    }
        //    for (int i = 0; i < 3; i++)
        //    {
        //        var room = new Room()
        //        {
        //            Color = Palette.Lavender,
        //            DesignXYZ = new Vector3(4.0, 3.0, 5.0),
        //            Name = "Conference Small",
        //            Number = "5"
        //        };
        //        room.SetPerimeter();
        //        rooms.Add(room);
        //    }
        //    // 279 sm
        //    return rooms;
        //}

        //private List<Room> MakeService()
        //{
        //    var rooms = new List<Room>();
        //    var room = new Room()
        //    {
        //        Color = Palette.Red,
        //        DesignXYZ = new Vector3(6.0, 8.0, 3.0),
        //        Name = "Service Core"
        //    };
        //    room.SetPerimeter();
        //    rooms.Add(room);
        //    //48 sm
        //    return rooms;
        //}

        //private List<Room> MakeTenant()
        //{
        //    var rooms = new List<Room>();
        //    var room = new Room()
        //    {
        //        Color = Palette.Red,
        //        DesignXYZ = new Vector3(60.0, 40.0, 3.0),
        //        Name = "tenant",
        //    };
        //    room.SetPerimeter();
        //    rooms.Add(room);
        //    return rooms;
        //    //2400 sm
        //}

        //[Fact]
        //public void AllocatedAsPolygons()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    scope.Occupant.AddRange(MakeOccupant());
        //    scope.Service.AddRange(MakeService());
        //    scope.Tenant.AddRange(MakeTenant());
        //    Assert.Equal(21.0, scope.AllocatedAsPolygons.Count);
        //}

        //[Fact]
        //public void AreaDesignAvailable()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    scope.Occupant.AddRange(MakeOccupant());
        //    scope.Service.AddRange(MakeService());
        //    scope.Tenant.AddRange(MakeTenant());
        //    Assert.Equal(1993.0, scope.AreaDesignAvailable);
        //}

        //[Fact]
        //public void AreaDesignCirculation()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    Assert.Equal(80.0, scope.AreaDesignCirculation);
        //}

        //[Fact]
        //public void AreaDesignOccupant()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    Assert.Equal(279.0, scope.AreaDesignOccupant);
        //}

        //[Fact]
        //public void AreaCirculation()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    Assert.Equal(80.0, scope.AreaCirculation);
        //}

        //[Fact]
        //public void AreaOccupant()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    Assert.Equal(279.0, scope.AreaOccupant);
        //}

        //[Fact]
        //public void AreaService()
        //{
        //    var scope = new Scope();
        //    scope.Service.AddRange(MakeService());
        //    Assert.Equal(48.0, scope.AreaService);
        //}

        //[Fact]
        //public void AreaTenant()
        //{
        //    var scope = new Scope();
        //    scope.Tenant.AddRange(MakeTenant());
        //    Assert.Equal(2400.0, scope.AreaTenant);
        //}

        //[Fact]
        //public void CirculationAsPolygons()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    Assert.Equal(2.0, scope.CirculationAsPolygons.Count);
        //}

        //[Fact]
        //public void FindByDesignArea()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var findRoom = scope.FindByDesignArea(20.0);
        //    Assert.Equal("Office Large", findRoom.Name);
        //}

        //[Fact]
        //public void FindByDesignXY()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var findRoom = scope.FindByDesignXY(5.0, 4.0);
        //    Assert.Equal("Office Large", findRoom.Name);
        //}

        //[Fact]
        //public void FindByName()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var findRoom = scope.FindByName("Conference Medium");
        //    Assert.Equal("Conference Medium", findRoom.Name);
        //    findRoom = scope.FindByName("Conference Large");
        //    Assert.Equal("Conference Large", findRoom.Name);
        //}

        //[Fact]
        //public void FindByNumber()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var findRoom = scope.FindByNumber("4");
        //    Assert.Equal("Conference Medium", findRoom.Name);
        //    Assert.Equal(Palette.Magenta, findRoom.Color);
        //    Assert.Equal(6.0, findRoom.DesignLength);
        //    Assert.Equal(4.0, findRoom.DesignWidth);
        //    Assert.Equal(5.0, findRoom.Height);
        //}

        //[Fact]
        //public void OccupantAsPolygons()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    Assert.Equal(18.0, scope.OccupantAsPolygons.Count);
        //}

        //[Fact]
        //public void Placed()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var rooms = scope.Occupant;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        rooms[i].Placed = true;
        //    }
        //    Assert.Equal(10.0, scope.Placed.Count);
        //}

        //[Fact]
        //public void PlacedAll()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var rooms = scope.Occupant;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        rooms[i].Placed = true;
        //    }
        //    Assert.False(scope.PlacedAll);
        //}

        //[Fact]
        //public void PlacedQuantity()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var rooms = scope.Occupant;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        rooms[i].Placed = true;
        //    }
        //    Assert.Equal(10.0, scope.PlacedQuantity);
        //}

        //[Fact]
        //public void RatioCirculation()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    scope.Occupant.AddRange(MakeOccupant());
        //    Assert.Equal(0.28673835125448, scope.RatioCirculation, 10);
        //}

        //[Fact]
        //public void RatioDesignCirculation()
        //{
        //    var scope = new Scope();
        //    scope.Circulation.AddRange(MakeCirculation());
        //    scope.Occupant.AddRange(MakeOccupant());
        //    Assert.Equal(0.28673835125448, scope.RatioDesignCirculation, 10);
        //}

        //[Fact]
        //public void ServiceAsPolygons()
        //{
        //    var scope = new Scope();
        //    scope.Service.AddRange(MakeService());
        //    Assert.Equal(1.0, scope.ServiceAsPolygons.Count);
        //}

        //[Fact]
        //public void TenantAsPolygons()
        //{
        //    var scope = new Scope();
        //    scope.Tenant.AddRange(MakeTenant());
        //    Assert.Equal(1.0, scope.TenantAsPolygons.Count);
        //}

        //[Fact]
        //public void Unplaced()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var rooms = scope.Occupant;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        rooms[i].Placed = true;
        //    }
        //    Assert.Equal(8.0, scope.Unplaced.Count);
        //}

        //[Fact]
        //public void UnplacedQuantity()
        //{
        //    var scope = new Scope();
        //    scope.Occupant.AddRange(MakeOccupant());
        //    var rooms = scope.Occupant;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        rooms[i].Placed = true;
        //    }
        //    Assert.Equal(8.0, scope.UnplacedQuantity);
        //}
    }
}

 