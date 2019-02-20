# RoomKit
A library for managing architectural rooms and building area allocations.

## Hypar Inc.

## Version 0.2.0

## 02/20/2019

# Class Index

## Class List

**RoomKit.CoordGrid**

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.**

**RoomKit.Room**

A data structure recording room characteristics.**

**RoomKit.RoomGroup**

Creates and manages Rooms within a perimeter.**

**RoomKit.RoomRow**

Creates and manages Rooms placed along a line**

**RoomKit.Scope**

A data structure recording space program characteristics and the status of a**  **Room**  **placing process.**

**RoomKit.Story**

Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.**

**RoomKit.TopoBox**

Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. Other compass points define points along the relevant side between the cardinal and corner points. C corresponds to the center of the box.**

# Namespace Documentation

## RoomKit Namespace Reference

### Classes

- class **ArcEx**

_Extends Elements.Geometry.Arc with utility methods._

- class CoordGrid

_Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon._

- class **LineEx**

_Extends Elements.Geometry.Line with utility methods._

- class **Messages**

_Common exception messages._

- class **Palette**

_Commonly used Colors for Space rendering. These colors are translucent to allow viewing of representions several layers deep._

- class **Place**

_Places 2D Polygons in various spatial relationships to each other._

- class **PolygonEx**
- class Room

_A data structure recording room characteristics._

- class RoomGroup

_Creates and manages Rooms within a perimeter._

- class RoomRow

_Creates and manages Rooms placed along a line._

- class Scope

_A data structure recording space program characteristics and the status of a_ _Room_ _placing process._

- class **Shaper**

_Utilities for creating and editing Polygons._

- class Story

_Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services._

- class TopoBox

_Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. Other compass points define points along the relevant side between the cardinal and corner points. C corresponds to the center of the box._

- class **Vector3Ex**

_Extends Elements.Geometry.Vector3 with utility methods._

### Enumerations

- enum Corner { **NE** , **SE** , **SW** , **NW** }
- _A list of box corners as compass designations._ enum Orient { **C** , **N** , **NNE** , **NE** , **ENE** , **E** , **ESE** , **SE** , **SSE** , **S** , **SSW** , **SW** , **WSW** , **W** , **WNW** , **NW** , **NNW** }

_A list of compass orientations used to designate locations on a 2D box. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. Other compass points define points along the relevant side between the cardinal and corner points. C corresponds to the center of the box._

### Enumeration Type Documentation

#### enum RoomKit.Corner[strong]

A list of box corners as compass designations.

#### enum RoomKit.Orient[strong]

A list of compass orientations used to designate locations on a 2D box. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. Other compass points define points along the relevant side between the cardinal and corner points. C corresponds to the center of the box.

# Class Documentation

## RoomKit.CoordGrid Class Reference

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

### Public Member Functions

- CoordGrid (Polygon polygon, double xInterval=1, double yInterval=1)

_Creates an orthogonal 2D grid of Vector3 points from the supplied Polygon and axis intervals._

- void Allocate (Polygon polygon)

_Allocates the points in the grid falling within or on the supplied Polygon._

- void Allocate (IList\&lt; Polygon \&gt; polygons)

_Allocates points in the grid falling within the supplied Polygons._

- Vector3 AllocatedNearTo (Vector3 point)

_Returns the allocated grid point nearest to the supplied point._

- Vector3 AllocatedRandom ()

_Returns a random allocated point._

- Vector3 AvailableMax ()

_Returns the maximum available grid point._

- Vector3 AvailableMin ()

_Returns the minimum available grid point._

- Vector3 AvailableNearTo (Vector3 point)

_Returns the available grid point nearest to the supplied Vector3 point._

- Vector3 AvailableRandom ()

_Returns a random available grid point._

### Properties

- List\&lt; Vector3 \&gt; Allocated [get]

_The list of vector3 allocated points._

- List\&lt; Vector3 \&gt; Available [get]

_The list of Vector3 points available for allocation._

- Polygon Perimeter [get]

_The Polygon perimeter of the grid._

### Detailed Description

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

### Constructor &amp; Destructor Documentation

#### RoomKit.CoordGrid.CoordGrid (Polygon  _polygon_, double  _xInterval_ = 1, double  _yInterval_ = 1)

Creates an orthogonal 2D grid of Vector3 points from the supplied Polygon and axis intervals.

##### Parameters:

| _perimeter_ | The Polygon boundary of the point grid. |
| --- | --- |
| _xInterval_ | The spacing of the grid along the x-axis. |
| _yInterval_ | The spacing of the grid along the y-axis. |

##### Returns:

A new Coordgrid object.

### Member Function Documentation

#### void RoomKit.CoordGrid.Allocate (Polygon  _polygon_)

Allocates the points in the grid falling within or on the supplied Polygon.

##### Parameters:

| _polygon_ | The Polygon bounding the points to be allocated. |
| --- | --- |

##### Returns:

None.

#### void RoomKit.CoordGrid.Allocate (IList\&lt; Polygon \&gt;  _polygons_)

Allocates points in the grid falling within the supplied Polygons.

##### Parameters:

| _polygon_ | The Polygon bounding the points to be allocated. |
| --- | --- |

##### Returns:

None.

#### Vector3 RoomKit.CoordGrid.AllocatedNearTo (Vector3  _point_)

Returns the allocated grid point nearest to the supplied point.

##### Parameters:

| _point_ | The Vector3 point to compare. |
| --- | --- |

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AllocatedRandom ()

Returns a random allocated point.

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableMax ()

Returns the maximum available grid point.

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableMin ()

Returns the minimum available grid point.

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableNearTo (Vector3  _point_)

Returns the available grid point nearest to the supplied Vector3 point.

##### Parameters:

| _point_ | The Vector3 point to compare. |
| --- | --- |

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableRandom ()

Returns a random available grid point.

##### Returns:

A Vector3 point.

### Property Documentation

#### List\&lt;Vector3\&gt; RoomKit.CoordGrid.Allocated[get]

The list of vector3 allocated points.

#### List\&lt;Vector3\&gt; RoomKit.CoordGrid.Available[get]

The list of Vector3 points available for allocation.

#### Polygon RoomKit.CoordGrid.Perimeter[get]

The Polygon perimeter of the grid.

#### The documentation for this class was generated from the following file:

- RoomKit/CoordGrid.cs

## RoomKit.Room Class Reference

A data structure recording room characteristics.

### Public Member Functions

- Room ()

_Constructor setting all internal variables to default values, a 1.0 x 1.0 x 1.0 white cube placed on the zero plane with a blank name, no perimeter, and a numeric ID of -1._

- Polygon MakePerimeter (Vector3 moveTo=null)

_Places a Polygon east of another Polygon, attempting to align bounding box corners or the horizontal bounding box axis._

- Polygon MakePerimeter (Line axis, double width)

_Creates and sets a rectangular_ _Room_ _perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors._

- Polygon MakePerimeter (Vector3 start, Vector3 end, double width)

_Creates and sets a rectangular_ _Room_ _perimeter with dimensions derived from two points and a width. Intended for creating corridors._

### Properties

- int [] AdjacentTo [get, set]

_A list of Resource ID integers indicating the desired adjacencies of this_ _Room_ _type to other_ _Room_ _types._

- Color **Color** [get, set]
- double **DesignArea** [get, set]
- double **DesignX** [get, set]
- double **DesignY** [get, set]
- double Elevation [get, set]

_The vertical position of the_ _Room__&#39;s lowest plane, parallel to the ground plane._

- double **Height** [get, set]
- string Name [get, set]

_Arbitrary string identifier for this_ _Room_ _instance. Has no effect on_ _Room_ _operations._

- Polygon **Perimeter** [get, set]
- int ResourceID [get, set]

_Arbitrary integer identifier of this_ _Room_ _type. Can be used to identify desired adjacencies._

- string UniqueID [get]

_A UUID for this_ _Room_ _instance, set on initialization._

- double Area [get]

_The area of the room&#39;s perimeter Polygon. Returns -1.0 if the_ _Room__&#39;s Perimeter is null._

- double AreaVariance [get]

_The ratio between the intended area and the actual area of the_ _Room__. Returns a negative value if the_ _Room_ _has no Perimeter value._

- Space AsSpace [get]

_A Space created from_ _Room_ _characteristics._

### Detailed Description

A data structure recording room characteristics.

### Constructor &amp; Destructor Documentation

#### RoomKit.Room.Room ()

Constructor setting all internal variables to default values, a 1.0 x 1.0 x 1.0 white cube placed on the zero plane with a blank name, no perimeter, and a numeric ID of -1.

### Member Function Documentation

#### Polygon RoomKit.Room.MakePerimeter (Vector3  _moveTo_ = null)

Places a Polygon east of another Polygon, attempting to align bounding box corners or the horizontal bounding box axis.

##### Parameters:

| _polygon_ | The Polygon to be placed adjacent to another Polygon. |
| --- | --- |
| _adjTo_ | The Polygon adjacent to which the new Polygon will be located. |
| _perimeter_ | The Polygon that must cover the resulting Polygon. |
| _among_ | The collection of Polygons that must not intersect the resulting Polygon. |

##### Returns:

A new Polygon or null if the conditions of placement cannot be satisfied.

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin.

##### Parameters:

| _moveTo_ | The Vector3 indication the location of new Polygon&#39;s southwest corner. |
| --- | --- |

##### Returns:

A new rectilinear Polygon derived either from fixed DesignX and DesignY dimensions or as a rectilinear target area of a random ratio between 1 and 2 of the Room&#39;s X to Y dimensions.

#### Polygon RoomKit.Room.MakePerimeter (Line  _axis_, double  _width_)

Creates and sets a rectangular Room perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors.

##### Parameters:

| _axis_ | The Line defining the centerline of the perimeter. |
| --- | --- |
| _width_ | The width of the perimeter along the axis Line. |

##### Returns:

A new rectilinear Polygon derived from the axis and the width.

#### Polygon RoomKit.Room.MakePerimeter (Vector3  _start_, Vector3  _end_, double  _width_)

Creates and sets a rectangular Room perimeter with dimensions derived from two points and a width. Intended for creating corridors.

##### Parameters:

| _start_ | The start point of an axis defining centerline of the perimeter. |
| --- | --- |
| _end_ | The end point of an axis defining centerline of the perimeter. |
| _width_ | The width of the perimeter along the axis Line. |

##### Returns:

A new rectilinear Polygon derived from the axis and the width.

### Property Documentation

#### int [] RoomKit.Room.AdjacentTo[get], [set]

A list of Resource ID integers indicating the desired adjacencies of this Room type to other Room types.

#### double RoomKit.Room.Area[get]

The area of the room&#39;s perimeter Polygon. Returns -1.0 if the Room&#39;s Perimeter is null.

#### double RoomKit.Room.AreaVariance[get]

The ratio between the intended area and the actual area of the Room. Returns a negative value if the Room has no Perimeter value.

#### Space RoomKit.Room.AsSpace[get]

A Space created from Room characteristics.

#### double RoomKit.Room.Elevation[get], [set]

The vertical position of the Room&#39;s lowest plane, parallel to the ground plane.

#### string RoomKit.Room.Name[get], [set]

Arbitrary string identifier for this Room instance. Has no effect on Room operations.

#### int RoomKit.Room.ResourceID[get], [set]

Arbitrary integer identifier of this Room type. Can be used to identify desired adjacencies.

#### string RoomKit.Room.UniqueID[get]

A UUID for this Room instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/Room.cs

## RoomKit.RoomGroup Class Reference

Creates and manages Rooms within a perimeter.

### Public Member Functions

- RoomGroup (Polygon perimeter, int xRooms=1, int yRooms=1, string name=&quot;&quot;)

_Creates a group of rooms by dividing the supplied Polygon perimeter by the quantity of supplied divisions along the orthogonal x and y axes._ _Room_ _perimeters conform to fit within the supplied Polygon._

- void SetElevation (double elevation)

_Uniformly sets the elevation of all Rooms in the_ _RoomGroup__._

- void SetHeight (double height)

_Uniformly sets the height of all Rooms in the_ _RoomGroup__._

### Properties

- string Name [get, set]

_An arbitrary string identifier for this_ _RoomGroup__._

- Polygon Perimeter [get]

_The Polygon within which all Rooms are placed._

- IList\&lt; Room \&gt; Rooms [get]

_The list of Rooms placed within the Perimeter._

- double AvailableArea [get]

_The unallocated area of the_ _RoomGroup_ _perimeter._

- double AreaPlaced [get]

_The area allocated within the_ _RoomGroup__._

- IList\&lt; Polygon \&gt; PerimetersRooms [get]

_A list of all placed_ _Room_ _perimeters._

### Detailed Description

Creates and manages Rooms within a perimeter.

### Constructor &amp; Destructor Documentation

#### RoomKit.RoomGroup.RoomGroup (Polygon  _perimeter_, int  _xRooms_ = 1, int  _yRooms_ = 1, string  _name_ = &quot;&quot;)

Creates a group of rooms by dividing the supplied Polygon perimeter by the quantity of supplied divisions along the orthogonal x and y axes. Room perimeters conform to fit within the supplied Polygon.

##### Parameters:

| _perimeter_ | The Polygon to divide with a number of Room perimeters. |
| --- | --- |
| _xRooms_ | The quantity of Rooms along the x axis. |
| _yRooms_ | The quantity of Rooms along the y axis. |
| _name_ | An arbitrary string identifier for this RoomGroup. |

##### Returns:

A new RoomGroup.

### Member Function Documentation

#### void RoomKit.RoomGroup.SetElevation (double  _elevation_)

Uniformly sets the elevation of all Rooms in the RoomGroup.

##### Parameters:

| _elevation_ | The new elevation of the Rooms. |
| --- | --- |

##### Returns:

None.

#### void RoomKit.RoomGroup.SetHeight (double  _height_)

Uniformly sets the height of all Rooms in the RoomGroup.

##### Parameters:

| _elevation_ | The new height of the Rooms. |
| --- | --- |

##### Returns:

None.

### Property Documentation

#### double RoomKit.RoomGroup.AreaPlaced[get]

The area allocated within the RoomGroup.

#### double RoomKit.RoomGroup.AvailableArea[get]

The unallocated area of the RoomGroup perimeter.

#### string RoomKit.RoomGroup.Name[get], [set]

An arbitrary string identifier for this RoomGroup.

#### Polygon RoomKit.RoomGroup.Perimeter[get]

The Polygon within which all Rooms are placed.

#### IList\&lt;Polygon\&gt; RoomKit.RoomGroup.PerimetersRooms[get]

A list of all placed Room perimeters.

#### IList\&lt;Room\&gt; RoomKit.RoomGroup.Rooms[get]

The list of Rooms placed within the Perimeter.

#### The documentation for this class was generated from the following file:

- RoomKit/RoomGroup.cs

## RoomKit.RoomRow Class Reference

Creates and manages Rooms placed along a line.

### Public Member Functions

- RoomRow ()

_Constructor initializes the_ _RoomRow_ _with default values._

- RoomRow (Line row, string name=&quot;&quot;)

_Constructor initializes the_ _RoomRow_ _with a new Line and an optional name._

- RoomRow (Vector3 start, Vector3 end, string name=&quot;&quot;)

_Constructor initializes the_ _RoomRow_ _with line endpoints and an optional name._

- bool AddRoom (Room room, Polygon within=null, IList\&lt; Polygon \&gt; among=null, double circ=2.0)

_Attempts to place a_ _Room_ _perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the_ _Room__&#39;s perimeter must fit and a list of Polygons which it cannot intersect._

### Properties

- Polygon Circulation [get]

_The circulation envelope around the row._

- double Depth = 0.0 [get]

_The depth of the deepest room along the Row._

- string Name [get, set]

_Arbitrary string identifier for this_ _RoomRow__._

- IList\&lt; Room \&gt; Rooms [get]

_The list of Rooms placed along the Row._

- Line Row [get]

_The Line along which Rooms can be placed._

- double AvailableLength [get]

_The unallocated length of the_ _RoomRow__._

- double AreaPlaced [get]

_The aggregate area of the Rooms placed on this Row._

- IList\&lt; Polygon \&gt; PerimetersRooms [get]

_A list of all placed_ _Room_ _perimeters._

### Detailed Description

Creates and manages Rooms placed along a line.

### Constructor &amp; Destructor Documentation

#### RoomKit.RoomRow.RoomRow ()

Constructor initializes the RoomRow with default values.

#### RoomKit.RoomRow.RoomRow (Line  _row_, string  _name_ = &quot;&quot;)

Constructor initializes the RoomRow with a new Line and an optional name.

#### RoomKit.RoomRow.RoomRow (Vector3  _start_, Vector3  _end_, string  _name_ = &quot;&quot;)

Constructor initializes the RoomRow with line endpoints and an optional name.

### Member Function Documentation

#### bool RoomKit.RoomRow.AddRoom (Room  _room_, Polygon  _within_ = null, IList\&lt; Polygon \&gt;  _among_ = null, double  _circ_ = 2.0)

Attempts to place a Room perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the Room&#39;s perimeter must fit and a list of Polygons which it cannot intersect.

##### Parameters:

| _room_ | The Room from which to derive the Polygon to place. |
| --- | --- |
| _within_ | The optional Polygon perimeter within which a new Room must fit. |
| _among_ | The optional list of Polygon perimeters the new Room cannot intersect. |
| _circ_ | The optional additional allowance opposite the Row to allow for circulation to the Rooms. |

##### Returns:

True if the room was successfully placed.

### Property Documentation

#### double RoomKit.RoomRow.AreaPlaced[get]

The aggregate area of the Rooms placed on this Row.

#### double RoomKit.RoomRow.AvailableLength[get]

The unallocated length of the RoomRow.

#### Polygon RoomKit.RoomRow.Circulation[get]

The circulation envelope around the row.

#### double RoomKit.RoomRow.Depth = 0.0[get]

The depth of the deepest room along the Row.

#### string RoomKit.RoomRow.Name[get], [set]

Arbitrary string identifier for this RoomRow.

#### IList\&lt;Polygon\&gt; RoomKit.RoomRow.PerimetersRooms[get]

A list of all placed Room perimeters.

#### IList\&lt;Room\&gt; RoomKit.RoomRow.Rooms[get]

The list of Rooms placed along the Row.

#### Line RoomKit.RoomRow.Row[get]

The Line along which Rooms can be placed.

#### The documentation for this class was generated from the following file:

- RoomKit/RoomRow.cs

## RoomKit.Scope Class Reference

A data structure recording space program characteristics and the status of a Room placing process.

### Public Member Functions

- Scope ()

_Contructor creates empty_ _Room_ _lists for Circulation, Occupation, Service, and Tenant._

- RoomFind (double area)

_Finds the room with the design area closest to the supplied area._

- RoomFind (double designX, double designY)

_Finds the room with the designed x and y dimensions closest to the supplied values._

- RoomFindUnplaced (double area)

_Finds the unplaced_ _Room_ _with the design area closest to the supplied area._

- RoomFindUnplaced (double designX, double designY)

_Finds the unplaced_ _Room_ _with the designed x and y dimensions closest to the supplied values._

- RoomFindUnplaced (int resourceID)

_Finds the first unplaced_ _Room_ _with the specifed ResourceID._

### Properties

- List\&lt; Room \&gt; Circulation [get]

_A list of Rooms designated as circulation._

- List\&lt; Room \&gt; Occupant [get]

_A list of Rooms designated for occupation, rather than circulation._

- List\&lt; Room \&gt; Service [get]

_A list of Rooms designated for building services._

- List\&lt; Room \&gt; Tenant [get]

_A list of Rooms intended as a series of tenant space containers of other Rooms._

- double AreaDesignCirculation [get]

_The area available for horizontal circulation._

- double AreaCirculation [get]

_The allocated aggregate area of all placed circulation rooms._

- double AreaRooms [get]

_The allocated aggregate area of all placed occupant rooms._

- double AreaService [get]

_The aggregate area of all services._

- double AreaTenant [get]

_The aggregate of all occupiable tenant areas._

- double DesignAreaOccupant [get]

_The intended aggregate area of all occupant rooms._

- double MaxRoomDim [get]

_The maximum fixed dimension of Occupant Rooms._

- double MinRoomDim [get]

_The minimum fixed dimension of Occupant Rooms._

- List\&lt; Polygon \&gt; PerimetersAllocated [get]

_A list of allocated Circulation, Occupant, and Service Polygon perimeters._

- List\&lt; Polygon \&gt; PerimetersCirculation [get]

_A list of all Circulation perimeter Polygons._

- List\&lt; Polygon \&gt; PerimetersOccupant [get]

_A list of all Occupant perimeter Polygons._

- List\&lt; Polygon \&gt; PerimetersService [get]

_A list of all Service perimeter Polygons._

- List\&lt; Polygon \&gt; PerimetersTenant [get]

_A list of all Tenant perimeter Polygons._

- IList\&lt; Room \&gt; Placed [get]

_Returns all placed Rooms._

- bool PlacedAll [get]

_Returns whether Occupant Rooms have been placed._

- double QuantityPlaced [get]

_The quantity of placed Rooms._

- double QuantityUnplaced [get]

_The quantity of unplaced Rooms._

- double RatioCirculation [get]

_Returns the ratio of the aggregate area of all rooms against the available circulation area._

- IList\&lt; Room \&gt; Unplaced [get]

_Returns all unplaced Rooms._

### Detailed Description

A data structure recording space program characteristics and the status of a Room placing process.

### Constructor &amp; Destructor Documentation

#### RoomKit.Scope.Scope ()

Contructor creates empty Room lists for Circulation, Occupation, Service, and Tenant.

##### Returns:

A new Scope.

### Member Function Documentation

#### Room RoomKit.Scope.Find (double  _area_)

Finds the room with the design area closest to the supplied area.

##### Parameters:

| _area_ | The area to match from the list of all Room definitions. |
| --- | --- |

##### Returns:

A Room.

#### Room RoomKit.Scope.Find (double  _designX_, double  _designY_)

Finds the room with the designed x and y dimensions closest to the supplied values.

##### Parameters:

| _designX_ | The x-axis dimension to match. |
| --- | --- |
| _designY_ | The y-axis dimension to match. |

##### Returns:

A Room.

#### Room RoomKit.Scope.FindUnplaced (double  _area_)

Finds the unplaced Room with the design area closest to the supplied area.

##### Parameters:

| _area_ | The area to match from the list of all unplaced Room definitions. |
| --- | --- |

##### Returns:

An unplaced Room.

#### Room RoomKit.Scope.FindUnplaced (double  _designX_, double  _designY_)

Finds the unplaced Room with the designed x and y dimensions closest to the supplied values.

##### Parameters:

| _designX_ | The x-axis dimension to match. |
| --- | --- |
| _designY_ | The y-axis dimension to match. |

##### Returns:

An unplaced Room.

#### Room RoomKit.Scope.FindUnplaced (int  _resourceID_)

Finds the first unplaced Room with the specifed ResourceID.

##### Parameters:

| _resourceID_ | The integer ID of a Room type. |
| --- | --- |

##### Returns:

A Room.

### Property Documentation

#### double RoomKit.Scope.AreaCirculation[get]

The allocated aggregate area of all placed circulation rooms.

#### double RoomKit.Scope.AreaDesignCirculation[get]

The area available for horizontal circulation.

#### double RoomKit.Scope.AreaRooms[get]

The allocated aggregate area of all placed occupant rooms.

#### double RoomKit.Scope.AreaService[get]

The aggregate area of all services.

#### double RoomKit.Scope.AreaTenant[get]

The aggregate of all occupiable tenant areas.

#### List\&lt;Room\&gt; RoomKit.Scope.Circulation[get]

A list of Rooms designated as circulation.

#### double RoomKit.Scope.DesignAreaOccupant[get]

The intended aggregate area of all occupant rooms.

#### double RoomKit.Scope.MaxRoomDim[get]

The maximum fixed dimension of Occupant Rooms.

#### double RoomKit.Scope.MinRoomDim[get]

The minimum fixed dimension of Occupant Rooms.

#### List\&lt;Room\&gt; RoomKit.Scope.Occupant[get]

A list of Rooms designated for occupation, rather than circulation.

#### List\&lt;Polygon\&gt; RoomKit.Scope.PerimetersAllocated[get]

A list of allocated Circulation, Occupant, and Service Polygon perimeters.

#### List\&lt;Polygon\&gt; RoomKit.Scope.PerimetersCirculation[get]

A list of all Circulation perimeter Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Polygon\&gt; RoomKit.Scope.PerimetersOccupant[get]

A list of all Occupant perimeter Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Polygon\&gt; RoomKit.Scope.PerimetersService[get]

A list of all Service perimeter Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Polygon\&gt; RoomKit.Scope.PerimetersTenant[get]

A list of all Tenant perimeter Polygons.

##### Returns:

A list of Polygons.

#### IList\&lt;Room\&gt; RoomKit.Scope.Placed[get]

Returns all placed Rooms.

##### Returns:

A list of Rooms.

#### bool RoomKit.Scope.PlacedAll[get]

Returns whether Occupant Rooms have been placed.

##### Returns:

Returns true each Room in Occupant has a perimeter.

#### double RoomKit.Scope.QuantityPlaced[get]

The quantity of placed Rooms.

#### double RoomKit.Scope.QuantityUnplaced[get]

The quantity of unplaced Rooms.

#### double RoomKit.Scope.RatioCirculation[get]

Returns the ratio of the aggregate area of all rooms against the available circulation area.

##### Returns:

A list of Rooms.

#### List\&lt;Room\&gt; RoomKit.Scope.Service[get]

A list of Rooms designated for building services.

#### List\&lt;Room\&gt; RoomKit.Scope.Tenant[get]

A list of Rooms intended as a series of tenant space containers of other Rooms.

#### IList\&lt;Room\&gt; RoomKit.Scope.Unplaced[get]

Returns all unplaced Rooms.

##### Returns:

A list of Rooms.

#### The documentation for this class was generated from the following file:

- RoomKit/Scope.cs

## RoomKit.Story Class Reference

Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.

### Public Member Functions

- Story ()

_Creates a_ _Story_ _at a 1.0 Height on the zero plane with new lists for Corridors, Rooms, and Services. Perimeter is set to null, Name is blank, and SlabThickness is s0.1._

- void AddCorridor (Line axis, double width=2.0, double height=3.0, string name=&quot;&quot;, Color color=null)

_Creates a rectangular corridor_ _Room_ _from a centerline axis, width, and height, at the_ _Story_ _elevation. Adds the new_ _Room_ _to the Corrdors list. Corridors conform to Service perimeters. Corridors change intersecting_ _Room_ _perimeters to conform to the corridor&#39;s perimeter._

- void AddCorridor (Vector3 start, Vector3 end, double width=2.0, double height=3.0, string name=&quot;&quot;, Color color=null)

_Creates a rectangular corridor_ _Room_ _from a centerline axis, width, and height, at the_ _Story_ _elevation. Adds the new_ _Room_ _to the Corrdors list. Corridors conform to Service perimeters. Corridors change intersecting_ _Room_ _perimeters to conform to the corridor&#39;s perimeter._

- void AddCorridor (Polygon perimeter, double height=3.0, string name=&quot;&quot;, Color color=null)

_Creates a corridor_ _Room_ _from a perimeter and height, at the_ _Story_ _elevation. Adds the new_ _Room_ _to the Corrdors list. Corridors conform to Service perimeters. Corridors change intersecting_ _Room_ _perimeters to conform to the corridor&#39;s perimeter._

- void AddRoom (Polygon perimeter, double height=3.0, string name=&quot;&quot;, Color color=null)

_Creates an occupied_ _Room_ _from a perimeter and height, at the_ _Story_ _elevation. Adds the new_ _Room_ _to the Rooms list. Rooms conform to corridor and service perimeters._

- void AddService (Polygon perimeter, string name=&quot;&quot;, Color color=null)

_Creates a Service from a perimeter at the_ _Story__&#39;s height and elevation. Adds the new_ _Room_ _to the Services list. Corridors and Rooms conform to Service perimeters._

- void RoomsByDivision (int xRooms=1, int yRooms=1, double height=3.0, double setback=0.0, string name=&quot;&quot;, Color color=null)

_Creates Rooms by orthogonally dividing the interior of the_ _Story_ _perimeter by a quantity of x-axis and y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms conform to Corridor and Service perimeters._

### Properties

- double Area [get]

_Area of the perimeter._

- double AreaPlaced [get]

_Area allocated to Corridors, Rooms, and Services._

- double AreaAvailable [get]

_Unallocated area within the_ _Story__._

- Color **Color** [get, set]
- List\&lt; Room \&gt; Corridors [get]

_List of Rooms designated as cooridors._

- List\&lt; Space \&gt; CorridorsAsSpaces [get]

_List of Spaces created from_ _Room_ _characteristics within the Corridors list._

- Color CorridorsColor [set]

_Sets the Corridors color._

- double **Elevation** [get, set]
- Space Envelope [get]

_Space created from_ _Story_ _characteristics._

- double **Height** [get, set]
- double HeightInteriors [set]

_Sets the height of all Corridors, Rooms, and Services._

- IList\&lt; Space \&gt; InteriorsAsSpaces [get]

_Returns all Corridors, Rooms, and Services as Spaces._

- string Name [get, set]

_Arbitrary string identifier._

- Polygon **Perimeter** [get, set]
- List\&lt; Room \&gt; Rooms [get]

_List of Rooms designated as occupiable rooms._

- List\&lt; Space \&gt; RoomsAsSpaces [get]

_List of Spaces created from_ _Room_ _characteristics within the Rooms list._

- Color RoomsColor [set]

_Sets the Rooms Space rendering color._

- List\&lt; Room \&gt; Services [get]

_A list of Rooms designated as building services._

- List\&lt; Space \&gt; ServicesAsSpaces [get]

_List of Spaces created from_ _Room_ _characteristics within the Services list._

- Color ServicesColor [set]

_Sets the Services Space rendering color._

- Floor Slab [get]

_Concrete Floor created from_ _Story_ _and Slab characteristics._

- double **SlabThickness** [get, set]

### Detailed Description

Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.

### Constructor &amp; Destructor Documentation

#### RoomKit.Story.Story ()

Creates a Story at a 1.0 Height on the zero plane with new lists for Corridors, Rooms, and Services. Perimeter is set to null, Name is blank, and SlabThickness is s0.1.

##### Parameters:

| _ratio_ | The ratio of width to depth |
| --- | --- |
| _area_ | The required area of the Polygon. |

##### Returns:

A new Story.

### Member Function Documentation

#### void RoomKit.Story.AddCorridor (Line  _axis_, double  _width_ = 2.0, double  _height_ = 3.0, string  _name_ = &quot;&quot;, Color  _color_ = null)

Creates a rectangular corridor Room from a centerline axis, width, and height, at the Story elevation. Adds the new Room to the Corrdors list. Corridors conform to Service perimeters. Corridors change intersecting Room perimeters to conform to the corridor&#39;s perimeter.

##### Parameters:

| _axis_ | Center Line of the corridor. |
| --- | --- |
| _width_ | Width of the corridor. |
| _height_ | Height of the corridor. |
| _name_ | String identifier. |
| _color_ | Rendering color of the Room as a Space. |

##### Returns:

None.

#### void RoomKit.Story.AddCorridor (Vector3  _start_, Vector3  _end_, double  _width_ = 2.0, double  _height_ = 3.0, string  _name_ = &quot;&quot;, Color  _color_ = null)

Creates a rectangular corridor Room from a centerline axis, width, and height, at the Story elevation. Adds the new Room to the Corrdors list. Corridors conform to Service perimeters. Corridors change intersecting Room perimeters to conform to the corridor&#39;s perimeter.

##### Parameters:

| _start_ | First endpoint of the centerline of the corridor. |
| --- | --- |
| _end_ | Second endpoint of the centerline of the corridor. |
| _width_ | Width of the corridor. |
| _height_ | Height of the corridor. |
| _name_ | String identifier. |
| _color_ | Rendering color of the Room as a Space. |

##### Returns:

None.

#### void RoomKit.Story.AddCorridor (Polygon  _perimeter_, double  _height_ = 3.0, string  _name_ = &quot;&quot;, Color  _color_ = null)

Creates a corridor Room from a perimeter and height, at the Story elevation. Adds the new Room to the Corrdors list. Corridors conform to Service perimeters. Corridors change intersecting Room perimeters to conform to the corridor&#39;s perimeter.

##### Parameters:

| _perimeter_ | Polygon perimeter of the corridor. |
| --- | --- |
| _height_ | Height of the corridor. |
| _name_ | String identifier. |
| _color_ | Rendering color of the Room as a Space. |

##### Returns:

None.

#### void RoomKit.Story.AddRoom (Polygon  _perimeter_, double  _height_ = 3.0, string  _name_ = &quot;&quot;, Color  _color_ = null)

Creates an occupied Room from a perimeter and height, at the Story elevation. Adds the new Room to the Rooms list. Rooms conform to corridor and service perimeters.

##### Parameters:

| _perimeter_ | Polygon perimeter of the corridor. |
| --- | --- |
| _height_ | Height of the corridor. |
| _name_ | String identifier. |
| _color_ | Rendering color of the Room as a Space. |

##### Returns:

None.

#### void RoomKit.Story.AddService (Polygon  _perimeter_, string  _name_ = &quot;&quot;, Color  _color_ = null)

Creates a Service from a perimeter at the Story&#39;s height and elevation. Adds the new Room to the Services list. Corridors and Rooms conform to Service perimeters.

##### Parameters:

| _perimeter_ | Polygon perimeter of the corridor. |
| --- | --- |
| _name_ | String identifier. |
| _color_ | Rendering color of the Room as a Space. |

##### Returns:

None.

#### void RoomKit.Story.RoomsByDivision (int  _xRooms_ = 1, int  _yRooms_ = 1, double  _height_ = 3.0, double  _setback_ = 0.0, string  _name_ = &quot;&quot;, Color  _color_ = null)

Creates Rooms by orthogonally dividing the interior of the Story perimeter by a quantity of x-axis and y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms conform to Corridor and Service perimeters.

##### Parameters:

| _xRooms_ | Quantity Rooms along the orthogonal x-axis. |
| --- | --- |
| _yRooms_ | Quantity Rooms along the orthogonal y-axis. |
| _height_ | Height of the new Rooms. |
| _setback_ | Offset from the Story perimeter. |
| _name_ | String identifier applied to every new Room. |
| _color_ | Rendering color of the Room as a Space. |

##### Returns:

None.

### Property Documentation

#### double RoomKit.Story.Area[get]

Area of the perimeter.

#### double RoomKit.Story.AreaAvailable[get]

Unallocated area within the Story.

#### double RoomKit.Story.AreaPlaced[get]

Area allocated to Corridors, Rooms, and Services.

#### List\&lt;Room\&gt; RoomKit.Story.Corridors[get]

List of Rooms designated as cooridors.

#### List\&lt;Space\&gt; RoomKit.Story.CorridorsAsSpaces[get]

List of Spaces created from Room characteristics within the Corridors list.

#### Color RoomKit.Story.CorridorsColor[set]

Sets the Corridors color.

#### Space RoomKit.Story.Envelope[get]

Space created from Story characteristics.

#### double RoomKit.Story.HeightInteriors[set]

Sets the height of all Corridors, Rooms, and Services.

#### IList\&lt;Space\&gt; RoomKit.Story.InteriorsAsSpaces[get]

Returns all Corridors, Rooms, and Services as Spaces.

#### string RoomKit.Story.Name[get], [set]

Arbitrary string identifier.

#### List\&lt;Room\&gt; RoomKit.Story.Rooms[get]

List of Rooms designated as occupiable rooms.

#### List\&lt;Space\&gt; RoomKit.Story.RoomsAsSpaces[get]

List of Spaces created from Room characteristics within the Rooms list.

#### Color RoomKit.Story.RoomsColor[set]

Sets the Rooms Space rendering color.

#### List\&lt;Room\&gt; RoomKit.Story.Services[get]

A list of Rooms designated as building services.

#### List\&lt;Space\&gt; RoomKit.Story.ServicesAsSpaces[get]

List of Spaces created from Room characteristics within the Services list.

#### Color RoomKit.Story.ServicesColor[set]

Sets the Services Space rendering color.

#### Floor RoomKit.Story.Slab[get]

Concrete Floor created from Story and Slab characteristics.

#### The documentation for this class was generated from the following file:

- RoomKit/Story.cs

## RoomKit.TopoBox Class Reference

Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. Other compass points define points along the relevant side between the cardinal and corner points. C corresponds to the center of the box.

### Public Member Functions

- TopoBox (Polygon polygon)

_Constructor creates a new mathematical bounding box from the supplied Polygon and populates all orientation points._

- Vector3 PointBy (Orient orient)

_Returns the requested bounding box location by orientation._

- Vector3 PointOpposite (Orient orient)

_Returns the reciprocal bounding box location by orientation._

### Properties

- Vector3 C [get]

_Vector3 location identifiers corresponding to points on the box perimeter._

- Vector3 **N** [get]
- Vector3 **NNW** [get]
- Vector3 **NW** [get]
- Vector3 **WNW** [get]
- Vector3 **W** [get]
- Vector3 **WSW** [get]
- Vector3 **SW** [get]
- Vector3 **SSW** [get]
- Vector3 **S** [get]
- Vector3 **SSE** [get]
- Vector3 **SE** [get]
- Vector3 **ESE** [get]
- Vector3 **E** [get]
- Vector3 **ENE** [get]
- Vector3 **NE** [get]
- Vector3 **NNE** [get]
- double SizeX [get]

_X and Y dimensions of the_ _TopoBox_ _perimeter._

- double **SizeY** [get]

### Detailed Description

Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. Other compass points define points along the relevant side between the cardinal and corner points. C corresponds to the center of the box.

### Constructor &amp; Destructor Documentation

#### RoomKit.TopoBox.TopoBox (Polygon  _polygon_)

Constructor creates a new mathematical bounding box from the supplied Polygon and populates all orientation points.

##### Returns:

A new TopoBox.

### Member Function Documentation

#### Vector3 RoomKit.TopoBox.PointBy (Orient  _orient_)

Returns the requested bounding box location by orientation.

##### Parameters:

| _orient_ | The Orient value to index point. |
| --- | --- |

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.TopoBox.PointOpposite (Orient  _orient_)

Returns the reciprocal bounding box location by orientation.

##### Parameters:

| _orient_ | The Orient value to find the reciprocal point. |
| --- | --- |

##### Returns:

A Vector3 point.

### Property Documentation

#### Vector3 RoomKit.TopoBox.C[get]

Vector3 location identifiers corresponding to points on the box perimeter.

#### double RoomKit.TopoBox.SizeX[get]

X and Y dimensions of the TopoBox perimeter.

#### The documentation for this class was generated from the following file:

- RoomKit/TopoBox.cs

# Index

INDE
