<img src="https://github.com/hypar-io/sdk/blob/master/hypar_logo.svg" width="200px" style="display: block;margin-left: auto;margin-right: auto;width: 50%;">

### This toolkit is also available as a nuget package: https://www.nuget.org/packages/RoomKit

# RoomKit Reference

### Classes

- class CoordGrid

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

- class **Messages**

Common exception messages.

- class **Place**

Rooms 2D Polygons in various spatial relationships to each other.

- class Room

A data structure recording room characteristics.

- class RoomGroup

Creates and manages Rooms within a perimeter.

- class RoomRow

Creates and manages Rooms placed along a line.

- class Scope

Data structure recording space program characteristics and the status of a Room placing process.

- class Story

Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.

- class Tower

### Enumerations

- enum Corner { **NE** , **SE** , **SW** , **NW** }

A list of box corners as compass designations. NE = maximum X and Y corner. SE = maximum X and minimum Y corner. SW = minimum X and Y corner. NW = minimum X and maximum Y corner.

### Enumeration Type Documentation

#### enum RoomKit.Corner[strong]

A list of box corners as compass designations. NE = maximum X and Y corner. SE = maximum X and minimum Y corner. SW = minimum X and Y corner. NW = minimum X and maximum Y corner.

# Class Documentation

## RoomKit.CoordGrid Class Reference

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

### Public Member Functions

- CoordGrid (Polygon polygon, double xInterval=1, double yInterval=1, int randomSeed=1)

Creates an orthogonal 2D grid of Vector3 points from the supplied Polygon and axis intervals.

- void Allocate (Polygon polygon)

Allocates the points in the grid falling within or on the supplied Polygon.

- void Allocate (IList\&lt; Polygon \&gt; polygons)

Allocates points in the grid falling within the supplied Polygons.

- Vector3 AllocatedNearTo (Vector3 point)

Returns the allocated grid point nearest to the supplied point.

- Vector3 AllocatedRandom ()

Returns a random allocated point.

- Vector3 AvailableMax ()

Returns the maximum available grid point.

- Vector3 AvailableMin ()

Returns the minimum available grid point.

- Vector3 AvailableNearTo (Vector3 point)

Returns the available grid point nearest to the supplied Vector3 point.

- Vector3 AvailableRandom ()

Returns a random available grid point.

### Properties

- List\&lt; Vector3 \&gt; Allocated [get]

The list of vector3 allocated points.

- List\&lt; Vector3 \&gt; Available [get]

The list of Vector3 points available for allocation.

- Polygon **Perimeter** [get, set]

### Detailed Description

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

### Constructor &amp; Destructor Documentation

#### RoomKit.CoordGrid.CoordGrid (Polygon  polygon, double  xInterval = 1, double  yInterval = 1, int  randomSeed = 1)

Creates an orthogonal 2D grid of Vector3 points from the supplied Polygon and axis intervals.

##### Parameters:

| perimeter | The Polygon boundary of the point grid. |
| --- | --- |
| xInterval | The spacing of the grid along the x-axis. |
| yInterval | The spacing of the grid along the y-axis. |

##### Returns:

A new CoordGrid.

### Member Function Documentation

#### void RoomKit.CoordGrid.Allocate (Polygon  polygon)

Allocates the points in the grid falling within or on the supplied Polygon.

##### Parameters:

| polygon | The Polygon bounding the points to be allocated. |
| --- | --- |

##### Returns:

None.

#### void RoomKit.CoordGrid.Allocate (IList\&lt; Polygon \&gt;  polygons)

Allocates points in the grid falling within the supplied Polygons.

##### Parameters:

| polygon | The Polygon bounding the points to be allocated. |
| --- | --- |

##### Returns:

None.

#### Vector3 RoomKit.CoordGrid.AllocatedNearTo (Vector3  point)

Returns the allocated grid point nearest to the supplied point.

##### Parameters:

| point | The Vector3 point to compare. |
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

#### Vector3 RoomKit.CoordGrid.AvailableNearTo (Vector3  point)

Returns the available grid point nearest to the supplied Vector3 point.

##### Parameters:

| point | The Vector3 point to compare. |
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

#### The documentation for this class was generated from the following file:

- RoomKit/CoordGrid.cs

## RoomKit.Room Class Reference

A data structure recording room characteristics.

### Public Member Functions

- Room ()

Constructor setting all internal variables to default values to create a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the zero plane with an empty string, null perimeter, and an integer TypeID of -1.

- Polygon MoveFromTo (Vector3 from, Vector3 to)

Moves the Room along a 3D vector calculated between the supplied Vector3 points.

- bool Rotate (Vector3 pivot, double angle)

Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.

- bool SetDimensions (Vector3 xyz, Vector3 moveTo=null)

Creates and sets a rectangular Room Perimeter, Height, and southwest corner location with a supplied vectors. Sets the DesignX and DesignY properties.

- bool SetPerimeter (Vector3 moveTo=null)

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the origin or at the 2D location implied by the supplied Vector3.

- bool SetPerimeter (double area, double ratio=1.5, Vector3 moveTo=null)

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin.

- bool SetPerimeter (Line axis, double width)

Creates and sets a rectangular Room perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors.

- bool SetPerimeter (Vector3 start, Vector3 end, double width)

Creates and sets a rectangular Room perimeter with dimensions derived from two points and a width. Intended for creating corridors.

### Properties

- int [] AdjacentTo [get, set]

A list of Resource ID integers indicating the desired adjacencies of this Room type to other Room types.

- double Area [get]

The area of the room&#39;s perimeter Polygon. Returns -1.0 if the Room&#39;s Perimeter is null.

- double AreaVariance [get]

The ratio between the intended area and the actual area of the Room. Returns a negative value if the Room has no Perimeter value.

- Space AsSpace [get]

A Space created from Room characteristics. Adds properties to the Space recording Name TypeID as Type DesignArea as Design Area DesignX as Design Length DesignY as Design Width Perimeter.Area as Area Elevation Height

- Color **Color** [get, set]
- double **DesignArea** [get, set]
- double DesignLength [get, set]

Desired x-axis dimension of this Room.

- double DesignWidth [get, set]

Desired y-axis dimension of this Room.

- double **DesignRatio** [get, set]
- bool DesignSet [get]

Returns true if both DesignLength and DesignWidth are positive values.

- Vector3 **DesignXYZ** [get, set]
- double Elevation [get, set]

The vertical position of the Room&#39;s lowest plane, parallel to the ground plane.

- double Height [get, set]

Height of the Room prism. Set ignores non-positive values.

- string Name [get, set]

Arbitrary string identifier for this Room instance.

- Polygon **Perimeter** [get, set]
- bool Placed [get, set]

Manual flag to record if the Room has been placed in its final position.

- double SizeX [get]

X dimensions of the Room Perimeter orthogonal bounding box.

- double SizeY [get]

X dimensions of the Room Perimeter orthogonal bounding box.

- int TypeID [get, set]

Arbitrary integer identifier of this instance..

- string UniqueID [get]

UUID for this instance, set on initialization.

### Detailed Description

A data structure recording room characteristics.

### Constructor &amp; Destructor Documentation

#### RoomKit.Room.Room ()

Constructor setting all internal variables to default values to create a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the zero plane with an empty string, null perimeter, and an integer TypeID of -1.

### Member Function Documentation

#### Polygon RoomKit.Room.MoveFromTo (Vector3  from, Vector3  to)

Moves the Room along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| from | Vector3 base point of the move. |
| --- | --- |
| to | Vector3 target point of the move. |

##### Returns:

A Polygon represeting the Room&#39;s new Perimeter.

#### bool RoomKit.Room.Rotate (Vector3  pivot, double  angle)

Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.

##### Parameters:

| pivot | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| angle | Angle in degrees to rotate the Perimeter. |

##### Returns:

True if the Perimeter is successfully rotated.

#### bool RoomKit.Room.SetDimensions (Vector3  xyz, Vector3  moveTo = null)

Creates and sets a rectangular Room Perimeter, Height, and southwest corner location with a supplied vectors. Sets the DesignX and DesignY properties.

##### Parameters:

| xyz | Vector3 dimensions of a new Polygon Perimeter. If xy.Z is \&gt; 0.0, sets the height of the Room. |
| --- | --- |
| moveTo | Vector3 location of the new Polygon&#39;s southwest corner. |

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Vector3  moveTo = null)

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the origin or at the 2D location implied by the supplied Vector3.

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (double  area, double  ratio = 1.5, Vector3  moveTo = null)

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin.

##### Parameters:

| area | Area override for the new Room Perimeter. If zero, defaults to the value of DesignArea. |
| --- | --- |
| ratio | Desired ratio of X to Y Room dimensions. |
| moveTo | Vector3 location of the new Polygon&#39;s southwest corner. |

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Line  axis, double  width)

Creates and sets a rectangular Room perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors.

##### Parameters:

| axis | The Line defining the centerline of the perimeter. |
| --- | --- |
| width | The width of the perimeter along the axis Line. |

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Vector3  start, Vector3  end, double  width)

Creates and sets a rectangular Room perimeter with dimensions derived from two points and a width. Intended for creating corridors.

##### Parameters:

| start | The start point of an axis defining centerline of the perimeter. |
| --- | --- |
| end | The end point of an axis defining centerline of the perimeter. |
| width | The width of the perimeter along the axis Line. |

##### Returns:

True if the Perimeter is successfully set.

### Property Documentation

#### int [] RoomKit.Room.AdjacentTo[get], [set]

A list of Resource ID integers indicating the desired adjacencies of this Room type to other Room types.

#### double RoomKit.Room.Area[get]

The area of the room&#39;s perimeter Polygon. Returns -1.0 if the Room&#39;s Perimeter is null.

#### double RoomKit.Room.AreaVariance[get]

The ratio between the intended area and the actual area of the Room. Returns a negative value if the Room has no Perimeter value.

#### Space RoomKit.Room.AsSpace[get]

A Space created from Room characteristics. Adds properties to the Space recording Name TypeID as Type DesignArea as Design Area DesignX as Design Length DesignY as Design Width Perimeter.Area as Area Elevation Height

#### double RoomKit.Room.DesignLength[get], [set]

Desired x-axis dimension of this Room.

#### bool RoomKit.Room.DesignSet[get]

Returns true if both DesignLength and DesignWidth are positive values.

#### double RoomKit.Room.DesignWidth[get], [set]

Desired y-axis dimension of this Room.

#### double RoomKit.Room.Elevation[get], [set]

The vertical position of the Room&#39;s lowest plane, parallel to the ground plane.

#### double RoomKit.Room.Height[get], [set]

Height of the Room prism. Set ignores non-positive values.

#### string RoomKit.Room.Name[get], [set]

Arbitrary string identifier for this Room instance.

#### bool RoomKit.Room.Placed[get], [set]

Manual flag to record if the Room has been placed in its final position.

#### double RoomKit.Room.SizeX[get]

X dimensions of the Room Perimeter orthogonal bounding box.

#### double RoomKit.Room.SizeY[get]

X dimensions of the Room Perimeter orthogonal bounding box.

#### int RoomKit.Room.TypeID[get], [set]

Arbitrary integer identifier of this instance..

#### string RoomKit.Room.UniqueID[get]

UUID for this instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/Room.cs

## RoomKit.RoomGroup Class Reference

Creates and manages Rooms within a perimeter.

### Public Member Functions

- RoomGroup ()

Creates an empty group of Rooms.

- void MoveFromTo (Vector3 from, Vector3 to)

Moves all Rooms in the RoomGroup and the RoomGroup Perimeter along a 3D vector calculated between the supplied Vector3 points.

- void Rotate (Vector3 pivot, double angle)

Rotates the RoomGroup Perimeter and Rooms in the horizontal plane around the supplied pivot point.

- void SetColor (Color color)

Uniformly sets the color of all Rooms in the RoomGroup.

- void SetHeight (double height)

Uniformly sets the height of all Rooms in the RoomGroup.

- bool RoomsByDivision (int xRooms=1, int yRooms=1, double height=3.0, string name=&quot;&quot;)

Clears the current Rooms list and creates new Rooms defined by orthogonal x- and y-axis divisions of the RoomGroup Perimeter.

### Properties

- double AreaAvailable [get]

Unallocated area of the RoomGroup perimeter.

- double AreaPlaced [get]

Area allocated within the RoomGroup.

- double **Elevation** [get, set]
- string Name [get, set]

Arbitrary string identifier for this RoomGroup.

- Polygon **Perimeter** [get, set]
- List\&lt; Room \&gt; Rooms [get]

List of Rooms placed within the Perimeter.

- List\&lt; Polygon \&gt; RoomsAsPolygons [get]

List of all Room perimeters as Polygons.

- List\&lt; Space \&gt; RoomsAsSpaces [get]

List of all Rooms as Spaces.

- double SizeX [get]

X dimension of the Perimeter orthogonal bounding box.

- double SizeY [get]

Y dimension of the Perimeter orthogonal bounding box.

- int TypeID [get, set]

Arbitrary integer identifier of this instance..

- string UniqueID [get]

UUID for this RoomGroup instance, set on initialization.

### Detailed Description

Creates and manages Rooms within a perimeter.

### Constructor &amp; Destructor Documentation

#### RoomKit.RoomGroup.RoomGroup ()

Creates an empty group of Rooms.

##### Returns:

A new RoomGroup.

### Member Function Documentation

#### void RoomKit.RoomGroup.MoveFromTo (Vector3  from, Vector3  to)

Moves all Rooms in the RoomGroup and the RoomGroup Perimeter along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| from | Vector3 base point of the move. |
| --- | --- |
| to | Vector3 target point of the move. |

##### Returns:

None.

#### bool RoomKit.RoomGroup.RoomsByDivision (int  xRooms = 1, int  yRooms = 1, double  height = 3.0, string  name = &quot;&quot;)

Clears the current Rooms list and creates new Rooms defined by orthogonal x- and y-axis divisions of the RoomGroup Perimeter.

##### Parameters:

| xRooms | The quantity of Rooms along orthogonal x-axis. Must be positive. |
| --- | --- |
| yRooms | The quantity of Rooms along orthogonal y-axis. Must be positive. |

##### Returns:

True if the Rooms are created.

#### void RoomKit.RoomGroup.Rotate (Vector3  pivot, double  angle)

Rotates the RoomGroup Perimeter and Rooms in the horizontal plane around the supplied pivot point.

##### Parameters:

| pivot | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| angle | Angle in degrees to rotate the Perimeter. |

##### Returns:

None.

#### void RoomKit.RoomGroup.SetColor (Color  color)

Uniformly sets the color of all Rooms in the RoomGroup.

##### Parameters:

| color | The new color of the Rooms. |
| --- | --- |

##### Returns:

None.

#### void RoomKit.RoomGroup.SetHeight (double  height)

Uniformly sets the height of all Rooms in the RoomGroup.

##### Parameters:

| elevation | The new height of the Rooms. |
| --- | --- |

##### Returns:

None.

### Property Documentation

#### double RoomKit.RoomGroup.AreaAvailable[get]

Unallocated area of the RoomGroup perimeter.

#### double RoomKit.RoomGroup.AreaPlaced[get]

Area allocated within the RoomGroup.

#### string RoomKit.RoomGroup.Name[get], [set]

Arbitrary string identifier for this RoomGroup.

#### List\&lt;Room\&gt; RoomKit.RoomGroup.Rooms[get]

List of Rooms placed within the Perimeter.

#### List\&lt;Polygon\&gt; RoomKit.RoomGroup.RoomsAsPolygons[get]

List of all Room perimeters as Polygons.

#### List\&lt;Space\&gt; RoomKit.RoomGroup.RoomsAsSpaces[get]

List of all Rooms as Spaces.

#### double RoomKit.RoomGroup.SizeX[get]

X dimension of the Perimeter orthogonal bounding box.

#### double RoomKit.RoomGroup.SizeY[get]

Y dimension of the Perimeter orthogonal bounding box.

#### int RoomKit.RoomGroup.TypeID[get], [set]

Arbitrary integer identifier of this instance..

#### string RoomKit.RoomGroup.UniqueID[get]

UUID for this RoomGroup instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/RoomGroup.cs

## RoomKit.RoomRow Class Reference

Creates and manages Rooms placed along a line.

### Public Member Functions

- RoomRow (Line row)

Constructor initializes the RoomRow with a new Line.

- RoomRow (Vector3 start, Vector3 end)

Constructor initializes the RoomRow with line endpoints.

- bool AddRoom (Room room, Polygon within=null, IList\&lt; Polygon \&gt; among=null)

Attempts to place a Room perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the Room&#39;s perimeter must fit and a list of Polygons with which it cannot intersect.

- void MoveFromTo (Vector3 from, Vector3 to)

Moves all Rooms in the RoomRow and the RoomRow Row along a 3D vector calculated between the supplied Vector3 points.

- void Rotate (Vector3 pivot, double angle)

Rotates the RoomRow Row and Rooms in the horizontal plane around the supplied pivot point.

- void SetColor (Color color)

Uniformly sets the color of all Rooms in the RoomRow.

- void SetHeight (double height)

Uniformly sets the height of all Rooms in the RoomRow.

### Properties

- double AreaPlaced [get]

Aggregate area of the Rooms placed on this Row.

- double AvailableLength [get]

Unallocated length of the RoomRow.

- Polygon Circulation [get]

Circulation envelope around the row.

- double **CirculationWidth** [get, set]
- double Depth = 0.0 [get]

Depth of the deepest room along the Row.

- double **Elevation** [get, set]
- string Name [get, set]

Arbitrary string identifier for this RoomRow.

- IList\&lt; Room \&gt; Rooms [get]

List of Rooms placed along the Row.

- List\&lt; Polygon \&gt; RoomsAsPolygons [get]

List of all Room perimeters as Polygons.

- List\&lt; Space \&gt; RoomsAsSpaces [get]

List of all Rooms as Spaces.

- Line Row [get]

Line along which Rooms can be placed.

- double SizeX [get]

X dimension of the Circulation orthogonal bounding box.

- double SizeY [get]

Y dimension of the Circulation orthogonal bounding box.

- int TypeID [get, set]

Arbitrary integer identifier of this instance..

- string UniqueID [get]

UUID for this RoomRow instance, set on initialization.

### Detailed Description

Creates and manages Rooms placed along a line.

### Constructor &amp; Destructor Documentation

#### RoomKit.RoomRow.RoomRow (Line  row)

Constructor initializes the RoomRow with a new Line.

#### RoomKit.RoomRow.RoomRow (Vector3  start, Vector3  end)

Constructor initializes the RoomRow with line endpoints.

### Member Function Documentation

#### bool RoomKit.RoomRow.AddRoom (Room  room, Polygon  within = null, IList\&lt; Polygon \&gt;  among = null)

Attempts to place a Room perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the Room&#39;s perimeter must fit and a list of Polygons with which it cannot intersect.

##### Parameters:

| room | Room from which to derive the Polygon to place. |
| --- | --- |
| within | Polygon perimeter within which a new Room must fit. |
| among | List of Polygon perimeters the new Room cannot intersect. |

##### Returns:

True if the room was successfully placed.

#### void RoomKit.RoomRow.MoveFromTo (Vector3  from, Vector3  to)

Moves all Rooms in the RoomRow and the RoomRow Row along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| from | Vector3 base point of the move. |
| --- | --- |
| to | Vector3 target point of the move. |

##### Returns:

None.

#### void RoomKit.RoomRow.Rotate (Vector3  pivot, double  angle)

Rotates the RoomRow Row and Rooms in the horizontal plane around the supplied pivot point.

##### Parameters:

| pivot | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| angle | Angle in degrees to rotate the Perimeter. |

##### Returns:

None.

#### void RoomKit.RoomRow.SetColor (Color  color)

Uniformly sets the color of all Rooms in the RoomRow.

##### Parameters:

| color | New color of the Rooms. |
| --- | --- |

##### Returns:

None.

#### void RoomKit.RoomRow.SetHeight (double  height)

Uniformly sets the height of all Rooms in the RoomRow.

##### Parameters:

| elevation | New height of the Rooms. |
| --- | --- |

##### Returns:

None.

### Property Documentation

#### double RoomKit.RoomRow.AreaPlaced[get]

Aggregate area of the Rooms placed on this Row.

#### double RoomKit.RoomRow.AvailableLength[get]

Unallocated length of the RoomRow.

#### Polygon RoomKit.RoomRow.Circulation[get]

Circulation envelope around the row.

#### double RoomKit.RoomRow.Depth = 0.0[get]

Depth of the deepest room along the Row.

#### string RoomKit.RoomRow.Name[get], [set]

Arbitrary string identifier for this RoomRow.

#### IList\&lt;Room\&gt; RoomKit.RoomRow.Rooms[get]

List of Rooms placed along the Row.

#### List\&lt;Polygon\&gt; RoomKit.RoomRow.RoomsAsPolygons[get]

List of all Room perimeters as Polygons.

#### List\&lt;Space\&gt; RoomKit.RoomRow.RoomsAsSpaces[get]

List of all Rooms as Spaces.

#### Line RoomKit.RoomRow.Row[get]

Line along which Rooms can be placed.

#### double RoomKit.RoomRow.SizeX[get]

X dimension of the Circulation orthogonal bounding box.

#### double RoomKit.RoomRow.SizeY[get]

Y dimension of the Circulation orthogonal bounding box.

#### int RoomKit.RoomRow.TypeID[get], [set]

Arbitrary integer identifier of this instance..

#### string RoomKit.RoomRow.UniqueID[get]

UUID for this RoomRow instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/RoomRow.cs

## RoomKit.Scope Class Reference

Data structure recording space program characteristics and the status of a Room placing process.

### Public Member Functions

- Scope ()

Contructor creates empty Room lists for Circulation, Occupation, Service, and Tenant.

- RoomFindByDesignArea (double area, bool unplaced=true)

Finds the first Occupant Room with the DesignArea value closest to the supplied area. C

- RoomFindByDesignXY (double designLength, double designWidth, bool unplaced=true)

Finds the first Occupant Room with the designed x and y dimensions closest to the supplied values.

- RoomFindByTypeID (int typeID, bool unplaced=true)

Finds the first unplaced Room with the specifed TypeID.

### Properties

- List\&lt; Room \&gt; Circulation [get]

List of Rooms designated as circulation.

- List\&lt; Room \&gt; Occupant [get]

List of Rooms designated for occupation, rather than circulation.

- List\&lt; Room \&gt; Service [get]

List of Rooms designated for building services.

- List\&lt; Room \&gt; Tenant [get]

List of Rooms intended as a series of tenant space containers of other Rooms.

- List\&lt; Polygon \&gt; AllocatedAsPolygons [get]

List of allocated Circulation, Occupant, and Service Room Perimeters as Polygons.

- double AreaDesignAvailable [get]

Area available for horizontal circulation.

- double AreaDesignCirculation [get]

Intended aggregate area of all Occupant Rooms.

- double AreaDesignOccupant [get]

Intended aggregate area of all Occupant Rooms.

- double AreaCirculation [get]

Allocated aggregate area of all placed Circulation Rooms.

- double AreaOccupant [get]

Allocated aggregate area of all placed Occupant Rooms.

- double AreaService [get]

Aggregate area of all Services Rooms.

- double AreaTenant [get]

Aggregate area of all occupiable Tenant Rooms.

- List\&lt; Polygon \&gt; CirculationAsPolygons [get]

List of all Circulation Room Perimeters as Polygons.

- List\&lt; Polygon \&gt; OccupantAsPolygons [get]

List of all Occupant Room Perimeters as Polygons.

- List\&lt; Polygon \&gt; ServiceAsPolygons [get]

List of all Service Room Perimeters as Polygons.

- List\&lt; Polygon \&gt; TenantAsPolygons [get]

List of all Tenant Room Perimeter Polygons.

- List\&lt; Room \&gt; Placed [get]

List of all Rooms marked as Placed.

- bool PlacedAll [get]

Returns whether all Occupant Rooms have been Placed.

- double PlacedQuantity [get]

The quantity of placed Rooms.

- double RatioCirculation [get]

Returns the ratio of the aggregate area of all Occupant Rooms against the Circulation area.

- double RatioDesignCirculation [get]

Returns the ratio of the aggregate area of all designed Occupant Rooms against the designed Circulation area.

- List\&lt; Room \&gt; Unplaced [get]

Returns all Rooms with Placed = false.

- double UnplacedQuantity [get]

The quantity of unplaced Rooms.

### Detailed Description

Data structure recording space program characteristics and the status of a Room placing process.

### Constructor &amp; Destructor Documentation

#### RoomKit.Scope.Scope ()

Contructor creates empty Room lists for Circulation, Occupation, Service, and Tenant.

##### Returns:

A new Scope.

### Member Function Documentation

#### Room RoomKit.Scope.FindByDesignArea (double  area, bool  unplaced = true)

Finds the first Occupant Room with the DesignArea value closest to the supplied area. C

##### Parameters:

| area | Area to match from the list of all Occupant Room definitions. |
| --- | --- |

##### Returns:

A Room.

#### Room RoomKit.Scope.FindByDesignXY (double  designLength, double  designWidth, bool  unplaced = true)

Finds the first Occupant Room with the designed x and y dimensions closest to the supplied values.

##### Parameters:

| designX | The x-axis dimension to match. |
| --- | --- |
| designY | The y-axis dimension to match. |

##### Returns:

A Room.

#### Room RoomKit.Scope.FindByTypeID (int  typeID, bool  unplaced = true)

Finds the first unplaced Room with the specifed TypeID.

##### Parameters:

| typeID | The integer ID of a Room type. |
| --- | --- |

##### Returns:

A Room.

### Property Documentation

#### List\&lt;Polygon\&gt; RoomKit.Scope.AllocatedAsPolygons[get]

List of allocated Circulation, Occupant, and Service Room Perimeters as Polygons.

#### double RoomKit.Scope.AreaCirculation[get]

Allocated aggregate area of all placed Circulation Rooms.

#### double RoomKit.Scope.AreaDesignAvailable[get]

Area available for horizontal circulation.

#### double RoomKit.Scope.AreaDesignCirculation[get]

Intended aggregate area of all Occupant Rooms.

#### double RoomKit.Scope.AreaDesignOccupant[get]

Intended aggregate area of all Occupant Rooms.

#### double RoomKit.Scope.AreaOccupant[get]

Allocated aggregate area of all placed Occupant Rooms.

#### double RoomKit.Scope.AreaService[get]

Aggregate area of all Services Rooms.

#### double RoomKit.Scope.AreaTenant[get]

Aggregate area of all occupiable Tenant Rooms.

#### List\&lt;Room\&gt; RoomKit.Scope.Circulation[get]

List of Rooms designated as circulation.

#### List\&lt;Polygon\&gt; RoomKit.Scope.CirculationAsPolygons[get]

List of all Circulation Room Perimeters as Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Room\&gt; RoomKit.Scope.Occupant[get]

List of Rooms designated for occupation, rather than circulation.

#### List\&lt;Polygon\&gt; RoomKit.Scope.OccupantAsPolygons[get]

List of all Occupant Room Perimeters as Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Room\&gt; RoomKit.Scope.Placed[get]

List of all Rooms marked as Placed.

##### Returns:

A list of Rooms.

#### bool RoomKit.Scope.PlacedAll[get]

Returns whether all Occupant Rooms have been Placed.

##### Returns:

Returns true if each Room in Occupant has been marked with Room.Placed = true.

#### double RoomKit.Scope.PlacedQuantity[get]

The quantity of placed Rooms.

#### double RoomKit.Scope.RatioCirculation[get]

Returns the ratio of the aggregate area of all Occupant Rooms against the Circulation area.

##### Returns:

A list of Rooms.

#### double RoomKit.Scope.RatioDesignCirculation[get]

Returns the ratio of the aggregate area of all designed Occupant Rooms against the designed Circulation area.

##### Returns:

A list of Rooms.

#### List\&lt;Room\&gt; RoomKit.Scope.Service[get]

List of Rooms designated for building services.

#### List\&lt;Polygon\&gt; RoomKit.Scope.ServiceAsPolygons[get]

List of all Service Room Perimeters as Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Room\&gt; RoomKit.Scope.Tenant[get]

List of Rooms intended as a series of tenant space containers of other Rooms.

#### List\&lt;Polygon\&gt; RoomKit.Scope.TenantAsPolygons[get]

List of all Tenant Room Perimeter Polygons.

##### Returns:

A list of Polygons.

#### List\&lt;Room\&gt; RoomKit.Scope.Unplaced[get]

Returns all Rooms with Placed = false.

##### Returns:

A list of Rooms.

#### double RoomKit.Scope.UnplacedQuantity[get]

The quantity of unplaced Rooms.

#### The documentation for this class was generated from the following file:

- RoomKit/Scope.cs

## RoomKit.Story Class Reference

Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.

### Public Member Functions

- Story ()

Creates a Story at a 1.0 Height on the zero plane with new lists for Corridors, Rooms, and Services. Perimeter is set to null, Name is blank, and SlabThickness is s0.1.

- bool AddCorridor (Room room, bool fit=true)

Adds a Room to the Corridors list.

- bool AddExclusion (Room room, bool fit=true)

Adds a Room to the Exclusions list.

- bool AddRoom (Room room, bool fit=true)

Adds a Room to the Rooms list.

- bool AddService (Room room, bool fit=true)

Adds a Room to the Services list.

- double AreaByName (string name)

Returns the aggregate area of all Rooms with a supplied name.

- void MoveFromTo (Vector3 from, Vector3 to)

Moves all Rooms in the Story and the Story Envelope along a 3D vector calculated between the supplied Vector3 points.

- bool RoomsByDivision (int xRooms=1, int yRooms=1, double height=3.0, double setback=0.0, string name=&quot;&quot;, Color color=null, bool fit=true)

Creates Rooms by orthogonally dividing the interior of the Story perimeter by a quantity of x-axis and y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms conform to Corridor and Service perimeters.

- List\&lt; Room \&gt; RoomsByName (string name)

Returns a list of Rooms with a specific name.

- void Rotate (Vector3 pivot, double angle)

Rotates the Story Perimeter and Rooms in the horizontal plane around the supplied pivot point.

### Properties

- double Area [get]

Area of the perimeter.

- double AreaAvailable [get]

Unallocated area within the Story.

- double AreaPlaced [get]

Area allocated to Corridors, Rooms, and Services.

- Color **Color** [get, set]
- List\&lt; Room \&gt; Corridors [get]

List of Rooms designated as cooridors.

- List\&lt; Polygon \&gt; CorridorsAsPolygons [get]

Polygons representing Corridors. Rooms Perimeters in the Story conform to Corridor Perimeters.

- List\&lt; Space \&gt; CorridorsAsSpaces [get]

List of Spaces created from Room characteristics within the Corridors list.

- Color CorridorsColor [set]

Sets the Corridors color.

- double **Elevation** [get, set]
- RoomEnvelope [get]

Room representing the Story envelope.

- Polygon EnvelopeAsPolygon [get]

Polygon representation of the Story Perimeter.

- Space EnvelopeAsSpace [get]

Space created from Story characteristics.

- List\&lt; Room \&gt; Exclusions [get]

Rooms representing areas that must not be intersected, but which will not be available as Spaces. All other Room Perimeters in the Story conform to Exclusion Room Perimeters.

- List\&lt; Polygon \&gt; ExclusionsAsPolygons [get]

Polygons representing areas that must not be intersected. All other Room Perimeters in the Story conform to Exclusion Room Perimeters.

- double **Height** [get, set]
- double HeightInteriors [set]

Sets the height of all Corridors, Rooms, and Services.

- IList\&lt; Polygon \&gt; InteriorsAsPolygons [get]

Returns all Corridors, Exclusions, Rooms, and Services as Polygons.

- IList\&lt; Space \&gt; InteriorsAsSpaces [get]

Returns all Corridors, Rooms, and Services as Spaces.

- bool IsBasement [get, set]

Identifies whether this story represents a base ment level.

- string Name [get, set]

Arbitrary string identifier.

- Polygon **Perimeter** [get, set]
- List\&lt; Room \&gt; Rooms [get]

List of Rooms designated as occupiable rooms.

- List\&lt; Polygon \&gt; RoomsAsPolygons [get]

Polygons representing Services. Corridors and Rooms Perimeters in the Story conform to Service Room Perimeters.

- List\&lt; Space \&gt; RoomsAsSpaces [get]

List of Spaces created from Room characteristics within the Rooms list.

- Color RoomsColor [set]

Sets the Rooms rendering color.

- List\&lt; Room \&gt; Services [get]

A list of Rooms designated as building services.

- List\&lt; Polygon \&gt; ServicesAsPolygons [get]

Polygons representing Services. Corridors and Rooms Perimeters in the Story conform to Service Room Perimeters.

- List\&lt; Space \&gt; ServicesAsSpaces [get]

List of Spaces created from Room characteristics within the Services list.

- Color ServicesColor [set]

Sets the Services Space rendering color.

- Floor Slab [get]

Concrete Floor created from Story and Slab characteristics.

- double **SlabThickness** [get, set]
- int TypeID [get, set]

Arbitrary integer identifier of this instance..

- string UniqueID [get]

UUID for this instance, set on initialization.

### Detailed Description

Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services.

### Constructor &amp; Destructor Documentation

#### RoomKit.Story.Story ()

Creates a Story at a 1.0 Height on the zero plane with new lists for Corridors, Rooms, and Services. Perimeter is set to null, Name is blank, and SlabThickness is s0.1.

##### Returns:

A new Story.

### Member Function Documentation

#### bool RoomKit.Story.AddCorridor (Room  room, bool  fit = true)

Adds a Room to the Corridors list.

##### Parameters:

| room | Room to add. |
| --- | --- |
| fit | Indicates whether the new room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more rooms were added to the Story.

#### bool RoomKit.Story.AddExclusion (Room  room, bool  fit = true)

Adds a Room to the Exclusions list.

##### Parameters:

| room | Room to add. |
| --- | --- |
| fit | Indicates whether the new room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more rooms were added to the Story.

#### bool RoomKit.Story.AddRoom (Room  room, bool  fit = true)

Adds a Room to the Rooms list.

##### Parameters:

| room | Room to add. |
| --- | --- |
| fit | Indicates whether the new Room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more Rooms were added to the Story.

#### bool RoomKit.Story.AddService (Room  room, bool  fit = true)

Adds a Room to the Services list.

##### Parameters:

| room | Room to add. |
| --- | --- |
| fit | Indicates whether the new Room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more Rooms were added to the Story.

#### double RoomKit.Story.AreaByName (string  name)

Returns the aggregate area of all Rooms with a supplied name.

##### Parameters:

| name | Name of the Rooms to retrieve. |
| --- | --- |

##### Returns:

None.

///

#### void RoomKit.Story.MoveFromTo (Vector3  from, Vector3  to)

Moves all Rooms in the Story and the Story Envelope along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| from | Vector3 base point of the move. |
| --- | --- |
| to | Vector3 target point of the move. |

##### Returns:

None.

#### bool RoomKit.Story.RoomsByDivision (int  xRooms = 1, int  yRooms = 1, double  height = 3.0, double  setback = 0.0, string  name = &quot;&quot;, Color  color = null, bool  fit = true)

Creates Rooms by orthogonally dividing the interior of the Story perimeter by a quantity of x-axis and y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms conform to Corridor and Service perimeters.

##### Parameters:

| xRooms | Quantity Rooms along the orthogonal x-axis. |
| --- | --- |
| yRooms | Quantity Rooms along the orthogonal y-axis. |
| height | Height of the new Rooms. |
| setback | Offset from the Story perimeter. |
| name | String identifier applied to every new Room. |
| color | Rendering color of the Room as a Space. |

##### Returns:

None.

#### List\&lt;Room\&gt; RoomKit.Story.RoomsByName (string  name)

Returns a list of Rooms with a specific name.

##### Parameters:

| name | Name of the rooms to retrieve. |
| --- | --- |

##### Returns:

None.

///

#### void RoomKit.Story.Rotate (Vector3  pivot, double  angle)

Rotates the Story Perimeter and Rooms in the horizontal plane around the supplied pivot point.

##### Parameters:

| pivot | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| angle | Angle in degrees to rotate the Perimeter. |

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

#### List\&lt;Polygon\&gt; RoomKit.Story.CorridorsAsPolygons[get]

Polygons representing Corridors. Rooms Perimeters in the Story conform to Corridor Perimeters.

#### List\&lt;Space\&gt; RoomKit.Story.CorridorsAsSpaces[get]

List of Spaces created from Room characteristics within the Corridors list.

#### Color RoomKit.Story.CorridorsColor[set]

Sets the Corridors color.

#### Room RoomKit.Story.Envelope[get]

Room representing the Story envelope.

#### Polygon RoomKit.Story.EnvelopeAsPolygon[get]

Polygon representation of the Story Perimeter.

#### Space RoomKit.Story.EnvelopeAsSpace[get]

Space created from Story characteristics.

#### List\&lt;Room\&gt; RoomKit.Story.Exclusions[get]

Rooms representing areas that must not be intersected, but which will not be available as Spaces. All other Room Perimeters in the Story conform to Exclusion Room Perimeters.

#### List\&lt;Polygon\&gt; RoomKit.Story.ExclusionsAsPolygons[get]

Polygons representing areas that must not be intersected. All other Room Perimeters in the Story conform to Exclusion Room Perimeters.

#### double RoomKit.Story.HeightInteriors[set]

Sets the height of all Corridors, Rooms, and Services.

#### IList\&lt;Polygon\&gt; RoomKit.Story.InteriorsAsPolygons[get]

Returns all Corridors, Exclusions, Rooms, and Services as Polygons.

#### IList\&lt;Space\&gt; RoomKit.Story.InteriorsAsSpaces[get]

Returns all Corridors, Rooms, and Services as Spaces.

#### bool RoomKit.Story.IsBasement[get], [set]

Identifies whether this story represents a base ment level.

#### string RoomKit.Story.Name[get], [set]

Arbitrary string identifier.

#### List\&lt;Room\&gt; RoomKit.Story.Rooms[get]

List of Rooms designated as occupiable rooms.

#### List\&lt;Polygon\&gt; RoomKit.Story.RoomsAsPolygons[get]

Polygons representing Services. Corridors and Rooms Perimeters in the Story conform to Service Room Perimeters.

#### List\&lt;Space\&gt; RoomKit.Story.RoomsAsSpaces[get]

List of Spaces created from Room characteristics within the Rooms list.

#### Color RoomKit.Story.RoomsColor[set]

Sets the Rooms rendering color.

#### List\&lt;Room\&gt; RoomKit.Story.Services[get]

A list of Rooms designated as building services.

#### List\&lt;Polygon\&gt; RoomKit.Story.ServicesAsPolygons[get]

Polygons representing Services. Corridors and Rooms Perimeters in the Story conform to Service Room Perimeters.

#### List\&lt;Space\&gt; RoomKit.Story.ServicesAsSpaces[get]

List of Spaces created from Room characteristics within the Services list.

#### Color RoomKit.Story.ServicesColor[set]

Sets the Services Space rendering color.

#### Floor RoomKit.Story.Slab[get]

Concrete Floor created from Story and Slab characteristics.

#### int RoomKit.Story.TypeID[get], [set]

Arbitrary integer identifier of this instance..

#### string RoomKit.Story.UniqueID[get]

UUID for this instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/Story.cs

## RoomKit.Tower Class Reference

### Public Member Functions

- bool AddCore (Polygon perimeter, int baseStory=0, double addHeight=0.0, Color color=null)

Adds a new service Core to the Tower.

- double AreaByName (string name)

Returns the aggregate area of all Rooms with a supplied name.

- void MoveFromTo (Vector3 from, Vector3 to)

Moves all Cores and Stories in the Tower along a 3D vector calculated between the supplied Vector3 points.

- List\&lt; Room \&gt; RoomsByName (string name)

Returns a list of Rooms with a specific name.

- void Rotate (Vector3 pivot, double angle)

Rotates the Tower Perimeter and Stories in the horizontal plane around the supplied pivot point.

- bool Stack ()

Creates the Tower by stacking a series of Story instances from the Tower Elevation.

- bool SetStoryHeight (int story, double height, bool interiors=true, bool upward=true)

Sets the height of an index-specified Story and relocates Stories above to accommodate the Story&#39;s new height.

### Public Attributes

- List\&lt; Story \&gt; Stories = null

List of all Stories in the Tower.

### Properties

- double Area [get]

Returns the aggregate area of all Stories in the Tower.

- Color **Color** [get, set]
- List\&lt; Room \&gt; Cores [get]

List of all service Cores in the Tower.

- double **Elevation** [get, set]
- RoomEnvelope [get]

Room representing the Tower envelope.

- Polygon EnvelopeAsPolygon [get]

Polygon representation of the Story Perimeter.

- Space EnvelopeAsSpace [get]

Space created from Story characteristics.

- int **Floors** [get, set]
- double Height [get]

Highest point of the highest tower story above the zero plane.

- double HeightLimit [get, set]

Desired typical Story height in the Tower.

- string Name [get, set]

Arbitrary string identifier for this Tower instance.

- Polygon **Perimeter** [get, set]
- List\&lt; Floor \&gt; Slabs [get]

List of all Slabs from every Story in the Tower.

- List\&lt; Space \&gt; Spaces [get]

List of all Spaces from every Story in the Tower.

- double **StoryHeight** [get, set]
- double **TargetArea** [get, set]
- int TypeID [get, set]

Arbitrary integer identifier of this instance..

- string UniqueID [get]

UUID for this instance, set on initialization.

### Member Function Documentation

#### bool RoomKit.Tower.AddCore (Polygon  perimeter, int  baseStory = 0, double  addHeight = 0.0, Color  color = null)

Adds a new service Core to the Tower.

##### Parameters:

| perimeter | Polygon perimeter defining the footprint of the service Core. |
| --- | --- |
| baseStory | Index of the lowest Story whose elevation will serve as the lowest level of the Core. |
| addHeight | Additional height of the Core above the highest Story. |
| color | Color of the Core when it it is accessed as a Space. |

##### Returns:

True if the Core is successfully added.

#### double RoomKit.Tower.AreaByName (string  name)

Returns the aggregate area of all Rooms with a supplied name.

##### Parameters:

| name | Name of the Rooms to retrieve. |
| --- | --- |

##### Returns:

None.

///

#### void RoomKit.Tower.MoveFromTo (Vector3  from, Vector3  to)

Moves all Cores and Stories in the Tower along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| from | Vector3 base point of the move. |
| --- | --- |
| to | Vector3 target point of the move. |

##### Returns:

None.

#### List\&lt;Room\&gt; RoomKit.Tower.RoomsByName (string  name)

Returns a list of Rooms with a specific name.

##### Parameters:

| name | Name of the rooms to retrieve. |
| --- | --- |

##### Returns:

None.

///

#### void RoomKit.Tower.Rotate (Vector3  pivot, double  angle)

Rotates the Tower Perimeter and Stories in the horizontal plane around the supplied pivot point.

##### Parameters:

| pivot | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| angle | Angle in degrees to rotate the Perimeter. |

##### Returns:

None.

#### bool RoomKit.Tower.SetStoryHeight (int  story, double  height, bool  interiors = true, bool  upward = true)

Sets the height of an index-specified Story and relocates Stories above to accommodate the Story&#39;s new height.

##### Parameters:

| story | Index of the Story to affect. |
| --- | --- |
| height | Desired new height of the specified Story. |
| interiors | If true also sets any Corridors and Rooms in the Story to the new Height. |

##### Returns:

True if the Tower is successfully stacked.

#### bool RoomKit.Tower.Stack ()

Creates the Tower by stacking a series of Story instances from the Tower Elevation.

##### Parameters:

| floors | Desired quantity of stacked Stories to form the Tower. If greater than zero, overrides and resets the current Floors property. |
| --- | --- |
| storyHeight | Desired typical Story height of the Tower. If greater than zero, overrides and resets the current StoryHeight property. |
| basement | Whether to consider the lowest floor a basement. |

##### Returns:

True if the Tower is successfully stacked.

### Member Data Documentation

#### List\&lt;Story\&gt; RoomKit.Tower.Stories = null

List of all Stories in the Tower.

### Property Documentation

#### double RoomKit.Tower.Area[get]

Returns the aggregate area of all Stories in the Tower.

#### List\&lt;Room\&gt; RoomKit.Tower.Cores[get]

List of all service Cores in the Tower.

#### Room RoomKit.Tower.Envelope[get]

Room representing the Tower envelope.

#### Polygon RoomKit.Tower.EnvelopeAsPolygon[get]

Polygon representation of the Story Perimeter.

#### Space RoomKit.Tower.EnvelopeAsSpace[get]

Space created from Story characteristics.

#### double RoomKit.Tower.Height[get]

Highest point of the highest tower story above the zero plane.

#### double RoomKit.Tower.HeightLimit[get], [set]

Desired typical Story height in the Tower.

#### string RoomKit.Tower.Name[get], [set]

Arbitrary string identifier for this Tower instance.

#### List\&lt;Floor\&gt; RoomKit.Tower.Slabs[get]

List of all Slabs from every Story in the Tower.

#### List\&lt;Space\&gt; RoomKit.Tower.Spaces[get]

List of all Spaces from every Story in the Tower.

#### int RoomKit.Tower.TypeID[get], [set]

Arbitrary integer identifier of this instance..

#### string RoomKit.Tower.UniqueID[get]

UUID for this instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/Tower.cs
