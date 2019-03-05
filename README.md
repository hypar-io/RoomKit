## RoomKit Documentation
RoomKit is a C# library for defining architectural rooms, corridors, service areas, building stories, building service cores, and towers. It expands and depend on the Hypar Elements library at https://github.com/hypar-io/elements, and can be used on the Hypar platform at https://hypar.io.

See the RoomKitTest folder for examples of using the library.

# Hypar Inc.
# Version 0.3.0
03/05/2019

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

_Commonly used colors for Space rendering. These colors are translucent to allow viewing of representions several layers deep._

- class **Place**

_Rooms 2D Polygons in various spatial relationships to each other._

- class **PolygonEx**
- class Room

_A data structure recording room characteristics._

- class RoomGroup

_Creates and manages Rooms within a perimeter._

- class RoomRow

_Creates and manages Rooms placed along a line._

- class Scope

_Data structure recording space program characteristics and the status of a_ _Room_ _placing process._

- class **Shaper**

_Utilities for creating and editing Polygons._

- class Story

_Creates and manages the geometry of a slab and Rooms representing corridors, occupied rooms, and services._

- class TopoBox

_Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side._

- class Tower
- class **Vector3Ex**

_Extends Elements.Geometry.Vector3 with utility methods._

### Enumerations

- enum Corner { **NE** , **SE** , **SW** , **NW** }
- _A list of box corners as compass designations. NE = maximum X and Y corner. SE = maximum X and minimum Y corner. SW = minimum X and Y corner. NW = minimum X and maximum Y corner._ enum Orient { **C** , **N** , **NNE** , **NE** , **ENE** , **E** , **ESE** , **SE** , **SSE** , **S** , **SSW** , **SW** , **WSW** , **W** , **WNW** , **NW** , **NNW** }

_A list of compass orientations used to designate locations on a 2D box. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. C corresponds to the center of the box. Other compass points define locations along the relevant side between the cardinal and corner points. See documentation of corresponding properties of the TopoBox class for full documentation._

### Enumeration Type Documentation

#### enum RoomKit.Corner[strong]

A list of box corners as compass designations. NE = maximum X and Y corner. SE = maximum X and minimum Y corner. SW = minimum X and Y corner. NW = minimum X and maximum Y corner.

#### enum RoomKit.Orient[strong]

A list of compass orientations used to designate locations on a 2D box. N, S, E, and W define middle points on each orthogonal side of the box. NE, NW, SE, and SW correspond to the corners of the box. C corresponds to the center of the box. Other compass points define locations along the relevant side between the cardinal and corner points. See documentation of corresponding properties of the TopoBox class for full documentation.

# Class Documentation

## RoomKit.CoordGrid Class Reference

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

### Public Member Functions

- CoordGrid (Polygon polygon, double xInterval=1, double yInterval=1, int randomSeed=1)

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

- Polygon **Perimeter** [get, set]

### Detailed Description

Maintains a list of available and allocated points in a grid of the specified interval within the orthogonal bounding box of a Polygon.

### Constructor &amp; Destructor Documentation

#### RoomKit.CoordGrid.CoordGrid (Polygon  _polygon_, double  _xInterval_ = 1, double  _yInterval_ = 1, int  _randomSeed_ = 1)

Creates an orthogonal 2D grid of Vector3 points from the supplied Polygon and axis intervals.

##### Parameters:

| _perimeter_ | The Polygon boundary of the point grid. |
| --- | --- |
| _xInterval_ | The spacing of the grid along the x-axis. |
| _yInterval_ | The spacing of the grid along the y-axis. |

##### Returns:

A new CoordGrid.

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

#### The documentation for this class was generated from the following file:

- RoomKit/CoordGrid.cs

## RoomKit.Room Class Reference

A data structure recording room characteristics.

### Public Member Functions

- Room ()

_Constructor setting all internal variables to default values to create a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the zero plane with an empty string, null perimeter, and an integer TypeID of -1._

- Polygon MoveFromTo (Vector3 from, Vector3 to)

_Moves the_ _Room_ _along a 3D vector calculated between the supplied Vector3 points._

- bool Rotate (Vector3 pivot, double angle)

_Rotates the_ _Room_ _Perimeter in the horizontal plane around the supplied pivot point._

- bool SetDimensions (Vector3 xyz, Vector3 moveTo=null)

_Creates and sets a rectangular_ _Room_ _Perimeter, Height, and southwest corner location with a supplied vectors. Sets the DesignX and DesignY properties._

- bool SetPerimeter (Vector3 moveTo=null)

_Creates and sets a rectangular_ _Room_ _Perimeter with dimensions derived from_ _Room_ _characteristics with its southwest corner at the origin or at the 2D location implied by the supplied Vector3._

- bool SetPerimeter (double area, double ratio=1.5, Vector3 moveTo=null)

_Creates and sets a rectangular_ _Room_ _Perimeter with dimensions derived from_ _Room_ _characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin._

- bool SetPerimeter (Line axis, double width)

_Creates and sets a rectangular_ _Room_ _perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors._

- bool SetPerimeter (Vector3 start, Vector3 end, double width)

_Creates and sets a rectangular_ _Room_ _perimeter with dimensions derived from two points and a width. Intended for creating corridors._

### Properties

- int [] AdjacentTo [get, set]

_A list of Resource ID integers indicating the desired adjacencies of this_ _Room_ _type to other_ _Room_ _types._

- double Area [get]

_The area of the room&#39;s perimeter Polygon. Returns -1.0 if the_ _Room__&#39;s Perimeter is null._

- double AreaVariance [get]

_The ratio between the intended area and the actual area of the_ _Room__. Returns a negative value if the_ _Room_ _has no Perimeter value._

- Space AsSpace [get]

_A Space created from_ _Room_ _characteristics. Adds properties to the Space recording Name TypeID as Type DesignArea as Design Area DesignX as Design Length DesignY as Design Width Perimeter.Area as Area Elevation Height_

- Color **Color** [get, set]
- double **DesignArea** [get, set]
- double DesignLength [get, set]

_Desired x-axis dimension of this_ _Room__._

- double DesignWidth [get, set]

_Desired y-axis dimension of this_ _Room__._

- double **DesignRatio** [get, set]
- bool DesignSet [get]

_Returns true if both DesignLength and DesignWidth are positive values._

- Vector3 **DesignXYZ** [get, set]
- double Elevation [get, set]

_The vertical position of the_ _Room__&#39;s lowest plane, parallel to the ground plane._

- double Height [get, set]

_Height of the_ _Room_ _prism. Set ignores non-positive values._

- string Name [get, set]

_Arbitrary string identifier for this_ _Room_ _instance._

- Polygon **Perimeter** [get, set]
- bool Placed [get, set]

_Manual flag to record if the_ _Room_ _has been placed in its final position._

- double SizeX [get]

_X dimensions of the_ _Room_ _Perimeter orthogonal bounding box._

- double SizeY [get]

_X dimensions of the_ _Room_ _Perimeter orthogonal bounding box._

- int TypeID [get, set]

_Arbitrary integer identifier of this_ _Room_ _type. Can be used to identify desired adjacencies._

- string UniqueID [get]

_UUID for this_ _Room_ _instance, set on initialization._

### Detailed Description

A data structure recording room characteristics.

### Constructor &amp; Destructor Documentation

#### RoomKit.Room.Room ()

Constructor setting all internal variables to default values to create a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the zero plane with an empty string, null perimeter, and an integer TypeID of -1.

### Member Function Documentation

#### Polygon RoomKit.Room.MoveFromTo (Vector3  _from_, Vector3  _to_)

Moves the Room along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| _from_ | Vector3 base point of the move. |
| --- | --- |
| _to_ | Vector3 target point of the move. |

##### Returns:

A Polygon represeting the Room&#39;s new Perimeter.

#### bool RoomKit.Room.Rotate (Vector3  _pivot_, double  _angle_)

Rotates the Room Perimeter in the horizontal plane around the supplied pivot point.

##### Parameters:

| _pivot_ | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| _angle_ | Angle in degrees to rotate the Perimeter. |

##### Returns:

True if the Perimeter is successfully rotated.

#### bool RoomKit.Room.SetDimensions (Vector3  _xyz_, Vector3  _moveTo_ = null)

Creates and sets a rectangular Room Perimeter, Height, and southwest corner location with a supplied vectors. Sets the DesignX and DesignY properties.

##### Parameters:

| _xyz_ | Vector3 dimensions of a new Polygon Perimeter. If xy.Z is \&gt; 0.0, sets the height of the Room. |
| --- | --- |
| _moveTo_ | Vector3 location of the new Polygon&#39;s southwest corner. |

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Vector3  _moveTo_ = null)

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the origin or at the 2D location implied by the supplied Vector3.

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (double  _area_, double  _ratio_ = 1.5, Vector3  _moveTo_ = null)

Creates and sets a rectangular Room Perimeter with dimensions derived from Room characteristics with its southwest corner at the supplied Vector3 point. If no point is supplied, the southwest corner is placed at the origin.

##### Parameters:

| _area_ | Area override for the new Room Perimeter. If zero, defaults to the value of DesignArea. |
| --- | --- |
| _ratio_ | Desired ratio of X to Y Room dimensions. |
| _moveTo_ | Vector3 location of the new Polygon&#39;s southwest corner. |

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Line  _axis_, double  _width_)

Creates and sets a rectangular Room perimeter with dimensions derived from a supplied Line and a width. Intended for creating corridors.

##### Parameters:

| _axis_ | The Line defining the centerline of the perimeter. |
| --- | --- |
| _width_ | The width of the perimeter along the axis Line. |

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Vector3  _start_, Vector3  _end_, double  _width_)

Creates and sets a rectangular Room perimeter with dimensions derived from two points and a width. Intended for creating corridors.

##### Parameters:

| _start_ | The start point of an axis defining centerline of the perimeter. |
| --- | --- |
| _end_ | The end point of an axis defining centerline of the perimeter. |
| _width_ | The width of the perimeter along the axis Line. |

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

Arbitrary integer identifier of this Room type. Can be used to identify desired adjacencies.

#### string RoomKit.Room.UniqueID[get]

UUID for this Room instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/Room.cs

## RoomKit.RoomGroup Class Reference

Creates and manages Rooms within a perimeter.

### Public Member Functions

- RoomGroup ()

_Creates an empty group of Rooms._

- void MoveFromTo (Vector3 from, Vector3 to)

_Moves all Rooms in the_ _RoomGroup_ _and the_ _RoomGroup_ _Perimeter along a 3D vector calculated between the supplied Vector3 points._

- void Rotate (Vector3 pivot, double angle)

_Rotates the_ _RoomGroup_ _Perimeter and Rooms in the horizontal plane around the supplied pivot point._

- void SetColor (Color color)

_Uniformly sets the color of all Rooms in the_ _RoomGroup__._

- void SetHeight (double height)

_Uniformly sets the height of all Rooms in the_ _RoomGroup__._

- bool RoomsByDivision (int xRooms=1, int yRooms=1, double height=3.0)

_Clears the current Rooms list and creates new Rooms defined by orthogonal x- and y-axis divisions of the_ _RoomGroup_ _Perimeter._

### Properties

- double AreaAvailable [get]

_Unallocated area of the_ _RoomGroup_ _perimeter._

- double AreaPlaced [get]

_Area allocated within the_ _RoomGroup__._

- double **Elevation** [get, set]
- string Name [get, set]

_Arbitrary string identifier for this_ _RoomGroup__._

- Polygon **Perimeter** [get, set]
- List\&lt; Room \&gt; Rooms [get]

_List of Rooms placed within the Perimeter._

- List\&lt; Polygon \&gt; RoomsAsPolygons [get]

_List of all_ _Room_ _perimeters as Polygons._

- List\&lt; Space \&gt; RoomsAsSpaces [get]

_List of all Rooms as Spaces._

- double SizeX [get]

_X dimension of the Perimeter orthogonal bounding box._

- double SizeY [get]

_Y dimension of the Perimeter orthogonal bounding box._

- string UniqueID [get]

_UUID for this_ _RoomGroup_ _instance, set on initialization._

### Detailed Description

Creates and manages Rooms within a perimeter.

### Constructor &amp; Destructor Documentation

#### RoomKit.RoomGroup.RoomGroup ()

Creates an empty group of Rooms.

##### Returns:

A new RoomGroup.

### Member Function Documentation

#### void RoomKit.RoomGroup.MoveFromTo (Vector3  _from_, Vector3  _to_)

Moves all Rooms in the RoomGroup and the RoomGroup Perimeter along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| _from_ | Vector3 base point of the move. |
| --- | --- |
| _to_ | Vector3 target point of the move. |

##### Returns:

None.

#### bool RoomKit.RoomGroup.RoomsByDivision (int  _xRooms_ = 1, int  _yRooms_ = 1, double  _height_ = 3.0)

Clears the current Rooms list and creates new Rooms defined by orthogonal x- and y-axis divisions of the RoomGroup Perimeter.

##### Parameters:

| _xRooms_ | The quantity of Rooms along orthogonal x-axis. Must be positive. |
| --- | --- |
| _yRooms_ | The quantity of Rooms along orthogonal y-axis. Must be positive. |

##### Returns:

True if the Rooms are created.

#### void RoomKit.RoomGroup.Rotate (Vector3  _pivot_, double  _angle_)

Rotates the RoomGroup Perimeter and Rooms in the horizontal plane around the supplied pivot point.

##### Parameters:

| _pivot_ | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| _angle_ | Angle in degrees to rotate the Perimeter. |

##### Returns:

None.

#### void RoomKit.RoomGroup.SetColor (Color  _color_)

Uniformly sets the color of all Rooms in the RoomGroup.

##### Parameters:

| _color_ | The new color of the Rooms. |
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

#### string RoomKit.RoomGroup.UniqueID[get]

UUID for this RoomGroup instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/RoomGroup.cs

## RoomKit.RoomRow Class Reference

Creates and manages Rooms placed along a line.

### Public Member Functions

- RoomRow (Line row)

_Constructor initializes the_ _RoomRow_ _with a new Line._

- RoomRow (Vector3 start, Vector3 end)

_Constructor initializes the_ _RoomRow_ _with line endpoints._

- bool AddRoom (Room room, Polygon within=null, IList\&lt; Polygon \&gt; among=null)

_Attempts to place a_ _Room_ _perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the_ _Room__&#39;s perimeter must fit and a list of Polygons with which it cannot intersect._

- void MoveFromTo (Vector3 from, Vector3 to)

_Moves all Rooms in the_ _RoomRow_ _and the_ _RoomRow_ _Row along a 3D vector calculated between the supplied Vector3 points._

- void Rotate (Vector3 pivot, double angle)

_Rotates the_ _RoomRow_ _Row and Rooms in the horizontal plane around the supplied pivot point._

- void SetColor (Color color)

_Uniformly sets the color of all Rooms in the_ _RoomRow__._

- void SetHeight (double height)

_Uniformly sets the height of all Rooms in the_ _RoomRow__._

### Properties

- double AreaPlaced [get]

_Aggregate area of the Rooms placed on this Row._

- double AvailableLength [get]

_Unallocated length of the_ _RoomRow__._

- Polygon Circulation [get]

_Circulation envelope around the row._

- double **CirculationWidth** [get, set]
- double Depth = 0.0 [get]

_Depth of the deepest room along the Row._

- double **Elevation** [get, set]
- string Name [get, set]

_Arbitrary string identifier for this_ _RoomRow__._

- IList\&lt; Room \&gt; Rooms [get]

_List of Rooms placed along the Row._

- List\&lt; Polygon \&gt; RoomsAsPolygons [get]

_List of all_ _Room_ _perimeters as Polygons._

- List\&lt; Space \&gt; RoomsAsSpaces [get]

_List of all Rooms as Spaces._

- Line Row [get]

_Line along which Rooms can be placed._

- double SizeX [get]

_X dimension of the Circulation orthogonal bounding box._

- double SizeY [get]

_Y dimension of the Circulation orthogonal bounding box._

- string UniqueID [get]

_UUID for this_ _RoomRow_ _instance, set on initialization._

### Detailed Description

Creates and manages Rooms placed along a line.

### Constructor &amp; Destructor Documentation

#### RoomKit.RoomRow.RoomRow (Line  _row_)

Constructor initializes the RoomRow with a new Line.

#### RoomKit.RoomRow.RoomRow (Vector3  _start_, Vector3  _end_)

Constructor initializes the RoomRow with line endpoints.

### Member Function Documentation

#### bool RoomKit.RoomRow.AddRoom (Room  _room_, Polygon  _within_ = null, IList\&lt; Polygon \&gt;  _among_ = null)

Attempts to place a Room perimeter on the next open segment of the Row, with optional restrictions of a perimeter within which the Room&#39;s perimeter must fit and a list of Polygons with which it cannot intersect.

##### Parameters:

| _room_ | Room from which to derive the Polygon to place. |
| --- | --- |
| _within_ | Polygon perimeter within which a new Room must fit. |
| _among_ | List of Polygon perimeters the new Room cannot intersect. |

##### Returns:

True if the room was successfully placed.

#### void RoomKit.RoomRow.MoveFromTo (Vector3  _from_, Vector3  _to_)

Moves all Rooms in the RoomRow and the RoomRow Row along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| _from_ | Vector3 base point of the move. |
| --- | --- |
| _to_ | Vector3 target point of the move. |

##### Returns:

None.

#### void RoomKit.RoomRow.Rotate (Vector3  _pivot_, double  _angle_)

Rotates the RoomRow Row and Rooms in the horizontal plane around the supplied pivot point.

##### Parameters:

| _pivot_ | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| _angle_ | Angle in degrees to rotate the Perimeter. |

##### Returns:

None.

#### void RoomKit.RoomRow.SetColor (Color  _color_)

Uniformly sets the color of all Rooms in the RoomRow.

##### Parameters:

| _color_ | New color of the Rooms. |
| --- | --- |

##### Returns:

None.

#### void RoomKit.RoomRow.SetHeight (double  _height_)

Uniformly sets the height of all Rooms in the RoomRow.

##### Parameters:

| _elevation_ | New height of the Rooms. |
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

#### string RoomKit.RoomRow.UniqueID[get]

UUID for this RoomRow instance, set on initialization.

#### The documentation for this class was generated from the following file:

- RoomKit/RoomRow.cs

## RoomKit.Scope Class Reference

Data structure recording space program characteristics and the status of a Room placing process.

### Public Member Functions

- Scope ()

_Contructor creates empty_ _Room_ _lists for Circulation, Occupation, Service, and Tenant._

- RoomFindByDesignArea (double area, bool unplaced=true)

_Finds the first Occupant_ _Room_ _with the DesignArea value closest to the supplied area. C_

- RoomFindByDesignXY (double designLength, double designWidth, bool unplaced=true)

_Finds the first Occupant_ _Room_ _with the designed x and y dimensions closest to the supplied values._

- RoomFindByTypeID (int typeID, bool unplaced=true)

_Finds the first unplaced_ _Room_ _with the specifed TypeID._

### Properties

- List\&lt; Room \&gt; Circulation [get]

_List of Rooms designated as circulation._

- List\&lt; Room \&gt; Occupant [get]

_List of Rooms designated for occupation, rather than circulation._

- List\&lt; Room \&gt; Service [get]

_List of Rooms designated for building services._

- List\&lt; Room \&gt; Tenant [get]

_List of Rooms intended as a series of tenant space containers of other Rooms._

- List\&lt; Polygon \&gt; AllocatedAsPolygons [get]

_List of allocated Circulation, Occupant, and Service_ _Room_ _Perimeters as Polygons._

- double AreaDesignAvailable [get]

_Area available for horizontal circulation._

- double AreaDesignCirculation [get]

_Intended aggregate area of all Occupant Rooms._

- double AreaDesignOccupant [get]

_Intended aggregate area of all Occupant Rooms._

- double AreaCirculation [get]

_Allocated aggregate area of all placed Circulation Rooms._

- double AreaOccupant [get]

_Allocated aggregate area of all placed Occupant Rooms._

- double AreaService [get]

_Aggregate area of all Services Rooms._

- double AreaTenant [get]

_Aggregate area of all occupiable Tenant Rooms._

- List\&lt; Polygon \&gt; CirculationAsPolygons [get]

_List of all Circulation_ _Room_ _Perimeters as Polygons._

- List\&lt; Polygon \&gt; OccupantAsPolygons [get]

_List of all Occupant_ _Room_ _Perimeters as Polygons._

- List\&lt; Polygon \&gt; ServiceAsPolygons [get]

_List of all Service_ _Room_ _Perimeters as Polygons._

- List\&lt; Polygon \&gt; TenantAsPolygons [get]

_List of all Tenant_ _Room_ _Perimeter Polygons._

- List\&lt; Room \&gt; Placed [get]

_List of all Rooms marked as Placed._

- bool PlacedAll [get]

_Returns whether all Occupant Rooms have been Placed._

- double PlacedQuantity [get]

_The quantity of placed Rooms._

- double RatioCirculation [get]

_Returns the ratio of the aggregate area of all Occupant Rooms against the Circulation area._

- double RatioDesignCirculation [get]

_Returns the ratio of the aggregate area of all designed Occupant Rooms against the designed Circulation area._

- List\&lt; Room \&gt; Unplaced [get]

_Returns all Rooms with Placed = false._

- double UnplacedQuantity [get]

_The quantity of unplaced Rooms._

### Detailed Description

Data structure recording space program characteristics and the status of a Room placing process.

### Constructor &amp; Destructor Documentation

#### RoomKit.Scope.Scope ()

Contructor creates empty Room lists for Circulation, Occupation, Service, and Tenant.

##### Returns:

A new Scope.

### Member Function Documentation

#### Room RoomKit.Scope.FindByDesignArea (double  _area_, bool  _unplaced_ = true)

Finds the first Occupant Room with the DesignArea value closest to the supplied area. C

##### Parameters:

| _area_ | Area to match from the list of all Occupant Room definitions. |
| --- | --- |

##### Returns:

A Room.

#### Room RoomKit.Scope.FindByDesignXY (double  _designLength_, double  _designWidth_, bool  _unplaced_ = true)

Finds the first Occupant Room with the designed x and y dimensions closest to the supplied values.

##### Parameters:

| _designX_ | The x-axis dimension to match. |
| --- | --- |
| _designY_ | The y-axis dimension to match. |

##### Returns:

A Room.

#### Room RoomKit.Scope.FindByTypeID (int  _typeID_, bool  _unplaced_ = true)

Finds the first unplaced Room with the specifed TypeID.

##### Parameters:

| _typeID_ | The integer ID of a Room type. |
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

_Creates a_ _Story_ _at a 1.0 Height on the zero plane with new lists for Corridors, Rooms, and Services. Perimeter is set to null, Name is blank, and SlabThickness is s0.1._

- bool AddCorridor (Room room, bool fit=true)

_Adds a_ _Room_ _to the Corridors list._

- bool AddExclusion (Room room, bool fit=true)

_Adds a_ _Room_ _to the Exclusions list._

- bool AddRoom (Room room, bool fit=true)

_Adds a_ _Room_ _to the Rooms list._

- bool AddService (Room room, bool fit=true)

_Adds a_ _Room_ _to the Services list._

- void MoveFromTo (Vector3 from, Vector3 to)

_Moves all Rooms in the_ _Story_ _and the_ _Story_ _Envelope along a 3D vector calculated between the supplied Vector3 points._

- bool RoomsByDivision (int xRooms=1, int yRooms=1, double height=3.0, double setback=0.0, string name=&quot;&quot;, Color color=null, bool fit=true)

_Creates Rooms by orthogonally dividing the interior of the_ _Story_ _perimeter by a quantity of x-axis and y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms conform to Corridor and Service perimeters._

- void Rotate (Vector3 pivot, double angle)

_Rotates the_ _Story_ _Perimeter and Rooms in the horizontal plane around the supplied pivot point._

### Properties

- double Area [get]

_Area of the perimeter._

- double AreaAvailable [get]

_Unallocated area within the_ _Story__._

- double AreaPlaced [get]

_Area allocated to Corridors, Rooms, and Services._

- Color **Color** [get, set]
- List\&lt; Room \&gt; Corridors [get]

_List of Rooms designated as cooridors._

- List\&lt; Polygon \&gt; CorridorsAsPolygons [get]

_Polygons representing Corridors. Rooms Perimeters in the_ _Story_ _conform to Corridor Perimeters._

- List\&lt; Space \&gt; CorridorsAsSpaces [get]

_List of Spaces created from_ _Room_ _characteristics within the Corridors list._

- Color CorridorsColor [set]

_Sets the Corridors color._

- double **Elevation** [get, set]
- RoomEnvelope [get]

_Room_ _representing the_ _Story_ _envelope._

- Polygon EnvelopeAsPolygon [get]

_Polygon representation of the_ _Story_ _Perimeter._

- Space EnvelopeAsSpace [get]

_Space created from_ _Story_ _characteristics._

- List\&lt; Room \&gt; Exclusions [get]

_Rooms representing areas that must not be intersected, but which will not be available as Spaces. All other_ _Room_ _Perimeters in the_ _Story_ _conform to Exclusion_ _Room_ _Perimeters._

- List\&lt; Polygon \&gt; ExclusionsAsPolygons [get]

_Polygons representing areas that must not be intersected. All other_ _Room_ _Perimeters in the_ _Story_ _conform to Exclusion_ _Room_ _Perimeters._

- double **Height** [get, set]
- double HeightInteriors [set]

_Sets the height of all Corridors, Rooms, and Services._

- IList\&lt; Polygon \&gt; InteriorsAsPolygons [get]

_Returns all Corridors, Exclusions, Rooms, and Services as Polygons._

- IList\&lt; Space \&gt; InteriorsAsSpaces [get]

_Returns all Corridors, Rooms, and Services as Spaces._

- string Name [get, set]

_Arbitrary string identifier._

- Polygon **Perimeter** [get, set]
- List\&lt; Room \&gt; Rooms [get]

_List of Rooms designated as occupiable rooms._

- List\&lt; Polygon \&gt; RoomsAsPolygons [get]

_Polygons representing Services. Corridors and Rooms Perimeters in the_ _Story_ _conform to Service_ _Room_ _Perimeters._

- List\&lt; Space \&gt; RoomsAsSpaces [get]

_List of Spaces created from_ _Room_ _characteristics within the Rooms list._

- Color RoomsColor [set]

_Sets the Rooms rendering color._

- List\&lt; Room \&gt; Services [get]

_A list of Rooms designated as building services._

- List\&lt; Polygon \&gt; ServicesAsPolygons [get]

_Polygons representing Services. Corridors and Rooms Perimeters in the_ _Story_ _conform to Service_ _Room_ _Perimeters._

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

##### Returns:

A new Story.

### Member Function Documentation

#### bool RoomKit.Story.AddCorridor (Room  _room_, bool  _fit_ = true)

Adds a Room to the Corridors list.

##### Parameters:

| _room_ | Room to add. |
| --- | --- |
| _fit_ | Indicates whether the new room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more rooms were added to the Story.

#### bool RoomKit.Story.AddExclusion (Room  _room_, bool  _fit_ = true)

Adds a Room to the Exclusions list.

##### Parameters:

| _room_ | Room to add. |
| --- | --- |
| _fit_ | Indicates whether the new room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more rooms were added to the Story.

#### bool RoomKit.Story.AddRoom (Room  _room_, bool  _fit_ = true)

Adds a Room to the Rooms list.

##### Parameters:

| _room_ | Room to add. |
| --- | --- |
| _fit_ | Indicates whether the new room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more rooms were added to the Story.

#### bool RoomKit.Story.AddService (Room  _room_, bool  _fit_ = true)

Adds a Room to the Services list.

##### Parameters:

| _room_ | Room to add. |
| --- | --- |
| _fit_ | Indicates whether the new room should mutually fit to other Story features. Default is true. |

##### Returns:

True if one or more rooms were added to the Story.

#### void RoomKit.Story.MoveFromTo (Vector3  _from_, Vector3  _to_)

Moves all Rooms in the Story and the Story Envelope along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| _from_ | Vector3 base point of the move. |
| --- | --- |
| _to_ | Vector3 target point of the move. |

##### Returns:

None.

#### bool RoomKit.Story.RoomsByDivision (int  _xRooms_ = 1, int  _yRooms_ = 1, double  _height_ = 3.0, double  _setback_ = 0.0, string  _name_ = &quot;&quot;, Color  _color_ = null, bool  _fit_ = true)

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

#### void RoomKit.Story.Rotate (Vector3  _pivot_, double  _angle_)

Rotates the Story Perimeter and Rooms in the horizontal plane around the supplied pivot point.

##### Parameters:

| _pivot_ | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| _angle_ | Angle in degrees to rotate the Perimeter. |

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

#### The documentation for this class was generated from the following file:

- RoomKit/Story.cs

## RoomKit.TopoBox Class Reference

Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side.

### Public Member Functions

- TopoBox (Polygon polygon)

_Constructor creates a new mathematical bounding box from the supplied Polygon and populates all orientation points._

- Vector3 PointBy (Orient orient)

_Returns the requested bounding box location by orientation._

- Vector3 PointOpposite (Orient orient)

_Returns the reciprocal bounding box location by orientation._

### Properties

- Vector3 C [get]

_Vector3 location identifier corresponding to the center of the box perimeter._

- Vector3 N [get]

_Vector3 location identifier corresponding to the midpoint of the maximum Y side of the box perimeter._

- Vector3 NNW [get]

_Vector3 location identifier corresponding to the midpoint between the NW and N points of the box perimeter._

- Vector3 NW [get]

_Vector3 location identifier corresponding to the mimimum X and maximum Y corner of the box perimeter._

- Vector3 WNW [get]

_Vector3 location identifier corresponding to the midpoint between the NW and W points of the box perimeter._

- Vector3 W [get]

_Vector3 location identifier corresponding to the midpoint of the minimum X side of the box perimeter._

- Vector3 WSW [get]

_Vector3 location identifier corresponding to the midpoint between the SW and W points of the box perimeter._

- Vector3 SW [get]

_Vector3 location identifier corresponding to the mimimum X and Y corner of the box perimeter._

- Vector3 SSW [get]

_Vector3 location identifier corresponding to the midpoint between the SW and S points of the box perimeter._

- Vector3 S [get]

_Vector3 location identifier corresponding to the midpoint of the minimum Y side of the box perimeter._

- Vector3 SSE [get]

_Vector3 location identifier corresponding to the midpoint between the SE and S points of the box perimeter._

- Vector3 SE [get]

_Vector3 location identifier corresponding to the maximum X and minimum Y corner of the box perimeter._

- Vector3 ESE [get]

_Vector3 location identifier corresponding to the midpoint between the SE and E points of the box perimeter._

- Vector3 E [get]

_Vector3 location identifier corresponding to the midpoint of the maximum X side of the box perimeter._

- Vector3 ENE [get]

_Vector3 location identifier corresponding to the midpoint between the NE and E points of the box perimeter._

- Vector3 NE [get]

_Vector3 location identifier corresponding to the maximum X and Y corner of the box perimeter._

- Vector3 NNE [get]

_Vector3 location identifier corresponding to the midpoint between the NE and N points of the box perimeter._

- double SizeX [get]

_X and Y dimensions of the_ _TopoBox_ _perimeter._

- double **SizeY** [get]

### Detailed Description

Maintains a set of points on the orthogonal bounding box of a supplied Polygon corresponding to four divisions of each side.

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

Vector3 location identifier corresponding to the center of the box perimeter.

#### Vector3 RoomKit.TopoBox.E[get]

Vector3 location identifier corresponding to the midpoint of the maximum X side of the box perimeter.

#### Vector3 RoomKit.TopoBox.ENE[get]

Vector3 location identifier corresponding to the midpoint between the NE and E points of the box perimeter.

#### Vector3 RoomKit.TopoBox.ESE[get]

Vector3 location identifier corresponding to the midpoint between the SE and E points of the box perimeter.

#### Vector3 RoomKit.TopoBox.N[get]

Vector3 location identifier corresponding to the midpoint of the maximum Y side of the box perimeter.

#### Vector3 RoomKit.TopoBox.NE[get]

Vector3 location identifier corresponding to the maximum X and Y corner of the box perimeter.

#### Vector3 RoomKit.TopoBox.NNE[get]

Vector3 location identifier corresponding to the midpoint between the NE and N points of the box perimeter.

#### Vector3 RoomKit.TopoBox.NNW[get]

Vector3 location identifier corresponding to the midpoint between the NW and N points of the box perimeter.

#### Vector3 RoomKit.TopoBox.NW[get]

Vector3 location identifier corresponding to the mimimum X and maximum Y corner of the box perimeter.

#### Vector3 RoomKit.TopoBox.S[get]

Vector3 location identifier corresponding to the midpoint of the minimum Y side of the box perimeter.

#### Vector3 RoomKit.TopoBox.SE[get]

Vector3 location identifier corresponding to the maximum X and minimum Y corner of the box perimeter.

#### double RoomKit.TopoBox.SizeX[get]

X and Y dimensions of the TopoBox perimeter.

#### Vector3 RoomKit.TopoBox.SSE[get]

Vector3 location identifier corresponding to the midpoint between the SE and S points of the box perimeter.

#### Vector3 RoomKit.TopoBox.SSW[get]

Vector3 location identifier corresponding to the midpoint between the SW and S points of the box perimeter.

#### Vector3 RoomKit.TopoBox.SW[get]

Vector3 location identifier corresponding to the mimimum X and Y corner of the box perimeter.

#### Vector3 RoomKit.TopoBox.W[get]

Vector3 location identifier corresponding to the midpoint of the minimum X side of the box perimeter.

#### Vector3 RoomKit.TopoBox.WNW[get]

Vector3 location identifier corresponding to the midpoint between the NW and W points of the box perimeter.

#### Vector3 RoomKit.TopoBox.WSW[get]

Vector3 location identifier corresponding to the midpoint between the SW and W points of the box perimeter.

#### The documentation for this class was generated from the following file:

- RoomKit/TopoBox.cs

## RoomKit.Tower Class Reference

### Public Member Functions

- bool AddServiceCore (Polygon perimeter, int baseStory=0, double addHeight=0.0, Color color=null)

_Adds a new service Core to the_ _Tower__._

- void MoveFromTo (Vector3 from, Vector3 to)

_Moves all Cores and Stories in the_ _Tower_ _along a 3D vector calculated between the supplied Vector3 points._

- void Rotate (Vector3 pivot, double angle)

_Rotates the_ _Tower_ _Perimeter and Stories in the horizontal plane around the supplied pivot point._

- bool Stack (int floors=0, double storyHeight=0.0)

_Creates the_ _Tower_ _by stacking a series of_ _Story_ _instances from the_ _Tower_ _Elevation._

- bool SetStoryHeight (int story, double height, bool interiors=true)

_Sets the height of an index-specified_ _Story_ _and relocates Stories above to accommodate the_ _Story__&#39;s new height._

### Public Attributes

- List\&lt; Story \&gt; Stories = null

_List of all Stories in the_ _Tower__._

### Properties

- Color **Color** [get, set]
- List\&lt; Room \&gt; Cores [get]

_List of all service Cores in the_ _Tower__._

- double **Elevation** [get, set]
- int **Floors** [get, set]
- double Height [get]

_Total height of all Stories in the_ _Tower__._

- Polygon **Perimeter** [get, set]
- List\&lt; Floor \&gt; Slabs [get]

_List of all Slabs from every_ _Story_ _in the_ _Tower__._

- List\&lt; Space \&gt; Spaces [get]

_List of all Spaces from every_ _Story_ _in the_ _Tower__._

- double **StoryHeight** [get, set]

### Member Function Documentation

#### bool RoomKit.Tower.AddServiceCore (Polygon  _perimeter_, int  _baseStory_ = 0, double  _addHeight_ = 0.0, Color  _color_ = null)

Adds a new service Core to the Tower.

##### Parameters:

| _perimeter_ | Polygon perimeter defining the footprint of the service Core. |
| --- | --- |
| _baseStory_ | Index of the lowest Story whose elevation will serve as the lowest level of the Core. |
| _addHeight_ | Additional height of the Core above the highest Story. |
| _color_ | Color of the Core when it it is accessed as a Space. |

##### Returns:

True if the Core is successfully added.

#### void RoomKit.Tower.MoveFromTo (Vector3  _from_, Vector3  _to_)

Moves all Cores and Stories in the Tower along a 3D vector calculated between the supplied Vector3 points.

##### Parameters:

| _from_ | Vector3 base point of the move. |
| --- | --- |
| _to_ | Vector3 target point of the move. |

##### Returns:

None.

#### void RoomKit.Tower.Rotate (Vector3  _pivot_, double  _angle_)

Rotates the Tower Perimeter and Stories in the horizontal plane around the supplied pivot point.

##### Parameters:

| _pivot_ | Vector3 point around which the Room Perimeter will be rotated. |
| --- | --- |
| _angle_ | Angle in degrees to rotate the Perimeter. |

##### Returns:

None.

#### bool RoomKit.Tower.SetStoryHeight (int  _story_, double  _height_, bool  _interiors_ = true)

Sets the height of an index-specified Story and relocates Stories above to accommodate the Story&#39;s new height.

##### Parameters:

| _story_ | Index of the Story to affect. |
| --- | --- |
| _height_ | Desired new height of the specified Story. |
| _interiors_ | If true also sets any Corridors and Rooms in the Story to the new Height. |

##### Returns:

True if the Tower is successfully stacked.

#### bool RoomKit.Tower.Stack (int  _floors_ = 0, double  _storyHeight_ = 0.0)

Creates the Tower by stacking a series of Story instances from the Tower Elevation.

##### Parameters:

| _floors_ | Desired quantity of stacked Stories to form the Tower. If greater than zero, overrides and resets the current Floors property. |
| --- | --- |
| _storyHeight_ | Desired typical Story height of the Tower. If greater than zero, overrides and resets the current StoryHeight property. |

##### Returns:

True if the Tower is successfully stacked.

### Member Data Documentation

#### List\&lt;Story\&gt; RoomKit.Tower.Stories = null

List of all Stories in the Tower.

### Property Documentation

#### List\&lt;Room\&gt; RoomKit.Tower.Cores[get]

List of all service Cores in the Tower.

#### double RoomKit.Tower.Height[get]

Total height of all Stories in the Tower.

#### List\&lt;Floor\&gt; RoomKit.Tower.Slabs[get]

List of all Slabs from every Story in the Tower.

#### List\&lt;Space\&gt; RoomKit.Tower.Spaces[get]

List of all Spaces from every Story in the Tower.

#### The documentation for this class was generated from the following file:

- RoomKit/Tower.cs

# Index

INDE
