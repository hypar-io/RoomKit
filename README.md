RoomKit Documentation
=======================
RoomKit is a C# library for defining architectural rooms, corridors, service areas, building stories, building service cores, and towers. It expands and depend on the Hypar Elements library, and can be used on the Hypar platform at https://hypar.io.

See the RoomKitTest folder above for example code.

RoomKit Namespace Reference
---------------------------

### Classes

-   class **ArcEx**

*Extends Elements.Geometry.Arc with utility methods.*

-   class [[CoordGrid]{.underline}](#AAAAAAAAAB)

*Maintains a list of available and allocated points in a grid of the
specified interval within the orthogonal bounding box of a Polygon.*

-   class **LineEx**

*Extends Elements.Geometry.Line with utility methods.*

-   class **Messages**

*Common exception messages.*

-   class **Palette**

*Commonly used colors for Space rendering. These colors are translucent
to allow viewing of representions several layers deep.*

-   class **Place**

*Rooms 2D Polygons in various spatial relationships to each other.*

-   class **PolygonEx**

-   class [[Room]{.underline}](#AAAAAAAAAC)

*A data structure recording room characteristics.*

-   class [[RoomGroup]{.underline}](#AAAAAAAAAD)

*Creates and manages Rooms within a perimeter.*

-   class [[RoomRow]{.underline}](#AAAAAAAAAE)

*Creates and manages Rooms placed along a line.*

-   class [[Scope]{.underline}](#AAAAAAAAAF)

*Data structure recording space program characteristics and the status
of a [[Room]{.underline}](#AAAAAAAAAC) placing process.*

-   class **Shaper**

*Utilities for creating and editing Polygons.*

-   class [[Story]{.underline}](#AAAAAAAAAG)

*Creates and manages the geometry of a slab and Rooms representing
corridors, occupied rooms, and services.*

-   class [[TopoBox]{.underline}](#AAAAAAAAAH)

*Maintains a set of points on the orthogonal bounding box of a supplied
Polygon corresponding to four divisions of each side.*

-   class [[Tower]{.underline}](#public-member-functions-7)

-   class **Vector3Ex**

*Extends Elements.Geometry.Vector3 with utility methods.*

### Enumerations

-   enum [[Corner]{.underline}](#AAAAAAAAAJ) { **NE**, **SE**, **SW**,
    **NW** }

-   *A list of box corners as compass designations. NE = maximum X and Y
    corner. SE = maximum X and minimum Y corner. SW = minimum X and Y
    corner. NW = minimum X and maximum Y corner.* enum
    [[Orient]{.underline}](#AAAAAAAAAK) { **C**, **N**, **NNE**, **NE**,
    **ENE**, **E**, **ESE**, **SE**, **SSE**, **S**, **SSW**, **SW**,
    **WSW**, **W**, **WNW**, **NW**, **NNW** }

*A list of compass orientations used to designate locations on a 2D box.
N, S, E, and W define middle points on each orthogonal side of the box.
NE, NW, SE, and SW correspond to the corners of the box. C corresponds
to the center of the box. Other compass points define locations along
the relevant side between the cardinal and corner points. See
documentation of corresponding properties of the TopoBox class for full
documentation.*

### Enumeration Type Documentation

#### enum [[RoomKit.Corner]{.underline}](#AAAAAAAAAJ)\[strong\]

A list of box corners as compass designations. NE = maximum X and Y
corner. SE = maximum X and minimum Y corner. SW = minimum X and Y
corner. NW = minimum X and maximum Y corner.

#### enum [[RoomKit.Orient]{.underline}](#AAAAAAAAAK)\[strong\]

A list of compass orientations used to designate locations on a 2D box.
N, S, E, and W define middle points on each orthogonal side of the box.
NE, NW, SE, and SW correspond to the corners of the box. C corresponds
to the center of the box. Other compass points define locations along
the relevant side between the cardinal and corner points. See
documentation of corresponding properties of the
[[TopoBox]{.underline}](#AAAAAAAAAH) class for full documentation.

Class Documentation
===================

RoomKit.CoordGrid Class Reference
---------------------------------

Maintains a list of available and allocated points in a grid of the
specified interval within the orthogonal bounding box of a Polygon.

### Public Member Functions

-   [[CoordGrid]{.underline}](#AAAAAAAAAL) (Polygon polygon, double
    xInterval=1, double yInterval=1, int randomSeed=1)

*Creates an orthogonal 2D grid of Vector3 points from the supplied
Polygon and axis intervals.*

-   void [[Allocate]{.underline}](#AAAAAAAAAM) (Polygon polygon)

*Allocates the points in the grid falling within or on the supplied
Polygon.*

-   void [[Allocate]{.underline}](#AAAAAAAAAN) (IList\< Polygon \>
    polygons)

*Allocates points in the grid falling within the supplied Polygons.*

-   Vector3 [[AllocatedNearTo]{.underline}](#AAAAAAAAAO) (Vector3 point)

*Returns the allocated grid point nearest to the supplied point.*

-   Vector3 [[AllocatedRandom]{.underline}](#AAAAAAAAAP) ()

*Returns a random allocated point.*

-   Vector3 [[AvailableMax]{.underline}](#AAAAAAAAAQ) ()

*Returns the maximum available grid point.*

-   Vector3 [[AvailableMin]{.underline}](#AAAAAAAAAR) ()

*Returns the minimum available grid point.*

-   Vector3 [[AvailableNearTo]{.underline}](#AAAAAAAAAS) (Vector3 point)

*Returns the available grid point nearest to the supplied Vector3
point.*

-   Vector3 [[AvailableRandom]{.underline}](#AAAAAAAAAT) ()

*Returns a random available grid point.*

### Properties

-   List\< Vector3 \> [[Allocated]{.underline}](#AAAAAAAAAU) \[get\]

*The list of vector3 allocated points.*

-   List\< Vector3 \> [[Available]{.underline}](#AAAAAAAAAV) \[get\]

*The list of Vector3 points available for allocation.*

-   Polygon **Perimeter** \[get, set\]

### Detailed Description

Maintains a list of available and allocated points in a grid of the
specified interval within the orthogonal bounding box of a Polygon.

### Constructor & Destructor Documentation

#### RoomKit.CoordGrid.CoordGrid (Polygon *polygon*, double *xInterval* = 1, double *yInterval* = 1, int *randomSeed* = 1)

Creates an orthogonal 2D grid of Vector3 points from the supplied
Polygon and axis intervals.

##### Parameters:

  ------------- -------------------------------------------
  *perimeter*   The Polygon boundary of the point grid.
  *xInterval*   The spacing of the grid along the x-axis.
  *yInterval*   The spacing of the grid along the y-axis.
  ------------- -------------------------------------------

##### Returns:

A new [[CoordGrid]{.underline}](#AAAAAAAAAB).

### Member Function Documentation

#### void RoomKit.CoordGrid.Allocate (Polygon *polygon*)

Allocates the points in the grid falling within or on the supplied
Polygon.

##### Parameters:

  ----------- --------------------------------------------------
  *polygon*   The Polygon bounding the points to be allocated.
  ----------- --------------------------------------------------

##### Returns:

None.

#### void RoomKit.CoordGrid.Allocate (IList\< Polygon \> *polygons*)

Allocates points in the grid falling within the supplied Polygons.

##### Parameters:

  ----------- --------------------------------------------------
  *polygon*   The Polygon bounding the points to be allocated.
  ----------- --------------------------------------------------

##### Returns:

None.

#### Vector3 RoomKit.CoordGrid.AllocatedNearTo (Vector3 *point*)

Returns the allocated grid point nearest to the supplied point.

##### Parameters:

  --------- -------------------------------
  *point*   The Vector3 point to compare.
  --------- -------------------------------

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AllocatedRandom ()

[]{#AAAAAAAAAP .anchor}

Returns a random allocated point.

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableMax ()

[]{#AAAAAAAAAQ .anchor}

Returns the maximum available grid point.

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableMin ()

[]{#AAAAAAAAAR .anchor}

Returns the minimum available grid point.

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableNearTo (Vector3 *point*)

[]{#AAAAAAAAAS .anchor}

Returns the available grid point nearest to the supplied Vector3 point.

##### Parameters:

  --------- -------------------------------
  *point*   The Vector3 point to compare.
  --------- -------------------------------

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.CoordGrid.AvailableRandom ()

[]{#AAAAAAAAAT .anchor}

Returns a random available grid point.

##### Returns:

A Vector3 point.

### Property Documentation

#### List\<Vector3\> RoomKit.CoordGrid.Allocated\[get\]

[]{#AAAAAAAAAU .anchor}

The list of vector3 allocated points.

#### List\<Vector3\> RoomKit.CoordGrid.Available\[get\]

[]{#AAAAAAAAAV .anchor}

The list of Vector3 points available for allocation.

#### The documentation for this class was generated from the following file:

-   RoomKit/CoordGrid.cs

#### 

RoomKit.Room Class Reference
----------------------------

[]{#AAAAAAAAAC .anchor}

A data structure recording room characteristics.

### Public Member Functions

-   [[Room]{.underline}](#AAAAAAAAAX) ()

*Constructor setting all internal variables to default values to create
a 1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the
zero plane with an empty string, null perimeter, and an integer TypeID
of -1.*

-   Polygon [[MoveFromTo]{.underline}](#AAAAAAAAAY) (Vector3 from,
    Vector3 to)

*Moves the [[Room]{.underline}](#AAAAAAAAAC) along a 3D vector
calculated between the supplied Vector3 points.*

-   bool [[Rotate]{.underline}](#AAAAAAAAAZ) (Vector3 pivot, double
    angle)

*Rotates the [[Room]{.underline}](#AAAAAAAAAC) Perimeter in the
horizontal plane around the supplied pivot point.*

-   bool [[SetDimensions]{.underline}](#AAAAAAAABA) (Vector3 xyz,
    Vector3 moveTo=null)

*Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
Perimeter, Height, and southwest corner location with a supplied
vectors. Sets the DesignX and DesignY properties.*

-   bool [[SetPerimeter]{.underline}](#AAAAAAAABB) (Vector3 moveTo=null)

*Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
Perimeter with dimensions derived from [[Room]{.underline}](#AAAAAAAAAC)
characteristics with its southwest corner at the origin or at the 2D
location implied by the supplied Vector3.*

-   bool [[SetPerimeter]{.underline}](#AAAAAAAABC) (double area, double
    ratio=1.5, Vector3 moveTo=null)

*Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
Perimeter with dimensions derived from [[Room]{.underline}](#AAAAAAAAAC)
characteristics with its southwest corner at the supplied Vector3 point.
If no point is supplied, the southwest corner is placed at the origin.*

-   bool [[SetPerimeter]{.underline}](#AAAAAAAABD) (Line axis, double
    width)

*Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
perimeter with dimensions derived from a supplied Line and a width.
Intended for creating corridors.*

-   bool [[SetPerimeter]{.underline}](#AAAAAAAABE) (Vector3 start,
    Vector3 end, double width)

*Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
perimeter with dimensions derived from two points and a width. Intended
for creating corridors.*

### Properties

-   int \[\] [[AdjacentTo]{.underline}](#AAAAAAAABF) \[get, set\]

*A list of Resource ID integers indicating the desired adjacencies of
this [[Room]{.underline}](#AAAAAAAAAC) type to other
[[Room]{.underline}](#AAAAAAAAAC) types.*

-   double [[Area]{.underline}](#AAAAAAAABG) \[get\]

*The area of the room\'s perimeter Polygon. Returns -1.0 if the
[[Room]{.underline}](#AAAAAAAAAC)\'s Perimeter is null.*

-   double [[AreaVariance]{.underline}](#AAAAAAAABH) \[get\]

*The ratio between the intended area and the actual area of the
[[Room]{.underline}](#AAAAAAAAAC). Returns a negative value if the
[[Room]{.underline}](#AAAAAAAAAC) has no Perimeter value.*

-   Space [[AsSpace]{.underline}](#AAAAAAAABI) \[get\]

*A Space created from [[Room]{.underline}](#AAAAAAAAAC) characteristics.
Adds properties to the Space recording Name TypeID as Type DesignArea as
Design Area DesignX as Design Length DesignY as Design Width
Perimeter.Area as Area Elevation Height*

-   Color **Color** \[get, set\]

-   double **DesignArea** \[get, set\]

-   double [[DesignLength]{.underline}](#AAAAAAAABL) \[get, set\]

*Desired x-axis dimension of this [[Room]{.underline}](#AAAAAAAAAC).*

-   double [[DesignWidth]{.underline}](#AAAAAAAABM) \[get, set\]

*Desired y-axis dimension of this [[Room]{.underline}](#AAAAAAAAAC).*

-   double **DesignRatio** \[get, set\]

-   bool [[DesignSet]{.underline}](#AAAAAAAABO) \[get\]

*Returns true if both DesignLength and DesignWidth are positive values.*

-   Vector3 **DesignXYZ** \[get, set\]

-   double [[Elevation]{.underline}](#AAAAAAAABQ) \[get, set\]

*The vertical position of the [[Room]{.underline}](#AAAAAAAAAC)\'s
lowest plane, parallel to the ground plane.*

-   double [[Height]{.underline}](#AAAAAAAABR) \[get, set\]

*Height of the [[Room]{.underline}](#AAAAAAAAAC) prism. Set ignores
non-positive values.*

-   string [[Name]{.underline}](#AAAAAAAABS) \[get, set\]

*Arbitrary string identifier for this [[Room]{.underline}](#AAAAAAAAAC)
instance.*

-   Polygon **Perimeter** \[get, set\]

-   bool [[Placed]{.underline}](#AAAAAAAABU) \[get, set\]

*Manual flag to record if the [[Room]{.underline}](#AAAAAAAAAC) has been
placed in its final position.*

-   double [[SizeX]{.underline}](#AAAAAAAABV) \[get\]

*X dimensions of the [[Room]{.underline}](#AAAAAAAAAC) Perimeter
orthogonal bounding box.*

-   double [[SizeY]{.underline}](#AAAAAAAABW) \[get\]

*X dimensions of the [[Room]{.underline}](#AAAAAAAAAC) Perimeter
orthogonal bounding box.*

-   int [[TypeID]{.underline}](#AAAAAAAABX) \[get, set\]

*Arbitrary integer identifier of this [[Room]{.underline}](#AAAAAAAAAC)
type. Can be used to identify desired adjacencies.*

-   string [[UniqueID]{.underline}](#AAAAAAAABY) \[get\]

*UUID for this [[Room]{.underline}](#AAAAAAAAAC) instance, set on
initialization.*

### Detailed Description

A data structure recording room characteristics.

### Constructor & Destructor Documentation

#### RoomKit.Room.Room ()

[]{#AAAAAAAAAX .anchor}

Constructor setting all internal variables to default values to create a
1.0 x 1.0 x 1.0 white cube with no required adjacencies placed on the
zero plane with an empty string, null perimeter, and an integer TypeID
of -1.

### Member Function Documentation

#### Polygon RoomKit.Room.MoveFromTo (Vector3 *from*, Vector3 *to*)

[]{#AAAAAAAAAY .anchor}

Moves the [[Room]{.underline}](#AAAAAAAAAC) along a 3D vector calculated
between the supplied Vector3 points.

##### Parameters:

  -------- -----------------------------------
  *from*   Vector3 base point of the move.
  *to*     Vector3 target point of the move.
  -------- -----------------------------------

##### Returns:

A Polygon represeting the [[Room]{.underline}](#AAAAAAAAAC)\'s new
Perimeter.

#### bool RoomKit.Room.Rotate (Vector3 *pivot*, double *angle*)

[]{#AAAAAAAAAZ .anchor}

Rotates the [[Room]{.underline}](#AAAAAAAAAC) Perimeter in the
horizontal plane around the supplied pivot point.

##### Parameters:

  --------- ---------------------------------------------------------------------------------------------
  *pivot*   Vector3 point around which the [[Room]{.underline}](#AAAAAAAAAC) Perimeter will be rotated.
  *angle*   Angle in degrees to rotate the Perimeter.
  --------- ---------------------------------------------------------------------------------------------

##### Returns:

True if the Perimeter is successfully rotated.

#### bool RoomKit.Room.SetDimensions (Vector3 *xyz*, Vector3 *moveTo* = null)

[]{#AAAAAAAABA .anchor}

Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
Perimeter, Height, and southwest corner location with a supplied
vectors. Sets the DesignX and DesignY properties.

##### Parameters:

  ---------- -----------------------------------------------------------------------------------------------------------------------------
  *xyz*      Vector3 dimensions of a new Polygon Perimeter. If xy.Z is \> 0.0, sets the height of the [[Room]{.underline}](#AAAAAAAAAC).
  *moveTo*   Vector3 location of the new Polygon\'s southwest corner.
  ---------- -----------------------------------------------------------------------------------------------------------------------------

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Vector3 *moveTo* = null)

[]{#AAAAAAAABB .anchor}

Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
Perimeter with dimensions derived from [[Room]{.underline}](#AAAAAAAAAC)
characteristics with its southwest corner at the origin or at the 2D
location implied by the supplied Vector3.

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (double *area*, double *ratio* = 1.5, Vector3 *moveTo* = null)

[]{#AAAAAAAABC .anchor}

Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
Perimeter with dimensions derived from [[Room]{.underline}](#AAAAAAAAAC)
characteristics with its southwest corner at the supplied Vector3 point.
If no point is supplied, the southwest corner is placed at the origin.

##### Parameters:

  ---------- ----------------------------------------------------------------------------------------------------------------------
  *area*     Area override for the new [[Room]{.underline}](#AAAAAAAAAC) Perimeter. If zero, defaults to the value of DesignArea.
  *ratio*    Desired ratio of X to Y [[Room]{.underline}](#AAAAAAAAAC) dimensions.
  *moveTo*   Vector3 location of the new Polygon\'s southwest corner.
  ---------- ----------------------------------------------------------------------------------------------------------------------

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Line *axis*, double *width*)

[]{#AAAAAAAABD .anchor}

Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
perimeter with dimensions derived from a supplied Line and a width.
Intended for creating corridors.

##### Parameters:

  --------- ----------------------------------------------------
  *axis*    The Line defining the centerline of the perimeter.
  *width*   The width of the perimeter along the axis Line.
  --------- ----------------------------------------------------

##### Returns:

True if the Perimeter is successfully set.

#### bool RoomKit.Room.SetPerimeter (Vector3 *start*, Vector3 *end*, double *width*)

[]{#AAAAAAAABE .anchor}

Creates and sets a rectangular [[Room]{.underline}](#AAAAAAAAAC)
perimeter with dimensions derived from two points and a width. Intended
for creating corridors.

##### Parameters:

  --------- ------------------------------------------------------------------
  *start*   The start point of an axis defining centerline of the perimeter.
  *end*     The end point of an axis defining centerline of the perimeter.
  *width*   The width of the perimeter along the axis Line.
  --------- ------------------------------------------------------------------

##### Returns:

True if the Perimeter is successfully set.

### Property Documentation

#### int \[\] RoomKit.Room.AdjacentTo\[get\], \[set\]

[]{#AAAAAAAABF .anchor}

A list of Resource ID integers indicating the desired adjacencies of
this [[Room]{.underline}](#AAAAAAAAAC) type to other
[[Room]{.underline}](#AAAAAAAAAC) types.

#### double RoomKit.Room.Area\[get\]

[]{#AAAAAAAABG .anchor}

The area of the room\'s perimeter Polygon. Returns -1.0 if the
[[Room]{.underline}](#AAAAAAAAAC)\'s Perimeter is null.

#### double RoomKit.Room.AreaVariance\[get\]

[]{#AAAAAAAABH .anchor}

The ratio between the intended area and the actual area of the
[[Room]{.underline}](#AAAAAAAAAC). Returns a negative value if the
[[Room]{.underline}](#AAAAAAAAAC) has no Perimeter value.

#### Space RoomKit.Room.AsSpace\[get\]

[]{#AAAAAAAABI .anchor}

A Space created from [[Room]{.underline}](#AAAAAAAAAC) characteristics.
Adds properties to the Space recording Name TypeID as Type DesignArea as
Design Area DesignX as Design Length DesignY as Design Width
Perimeter.Area as Area Elevation Height

#### double RoomKit.Room.DesignLength\[get\], \[set\]

[]{#AAAAAAAABL .anchor}

Desired x-axis dimension of this [[Room]{.underline}](#AAAAAAAAAC).

#### bool RoomKit.Room.DesignSet\[get\]

[]{#AAAAAAAABO .anchor}

Returns true if both DesignLength and DesignWidth are positive values.

#### double RoomKit.Room.DesignWidth\[get\], \[set\]

[]{#AAAAAAAABM .anchor}

Desired y-axis dimension of this [[Room]{.underline}](#AAAAAAAAAC).

#### double RoomKit.Room.Elevation\[get\], \[set\]

[]{#AAAAAAAABQ .anchor}

The vertical position of the [[Room]{.underline}](#AAAAAAAAAC)\'s lowest
plane, parallel to the ground plane.

#### double RoomKit.Room.Height\[get\], \[set\]

[]{#AAAAAAAABR .anchor}

Height of the [[Room]{.underline}](#AAAAAAAAAC) prism. Set ignores
non-positive values.

#### string RoomKit.Room.Name\[get\], \[set\]

[]{#AAAAAAAABS .anchor}

Arbitrary string identifier for this [[Room]{.underline}](#AAAAAAAAAC)
instance.

#### bool RoomKit.Room.Placed\[get\], \[set\]

[]{#AAAAAAAABU .anchor}

Manual flag to record if the [[Room]{.underline}](#AAAAAAAAAC) has been
placed in its final position.

#### double RoomKit.Room.SizeX\[get\]

[]{#AAAAAAAABV .anchor}

X dimensions of the [[Room]{.underline}](#AAAAAAAAAC) Perimeter
orthogonal bounding box.

#### double RoomKit.Room.SizeY\[get\]

[]{#AAAAAAAABW .anchor}

X dimensions of the [[Room]{.underline}](#AAAAAAAAAC) Perimeter
orthogonal bounding box.

#### int RoomKit.Room.TypeID\[get\], \[set\]

[]{#AAAAAAAABX .anchor}

Arbitrary integer identifier of this [[Room]{.underline}](#AAAAAAAAAC)
type. Can be used to identify desired adjacencies.

#### string RoomKit.Room.UniqueID\[get\]

[]{#AAAAAAAABY .anchor}

UUID for this [[Room]{.underline}](#AAAAAAAAAC) instance, set on
initialization.

#### The documentation for this class was generated from the following file:

-   RoomKit/Room.cs

#### 

RoomKit.RoomGroup Class Reference
---------------------------------

[]{#AAAAAAAAAD .anchor}

Creates and manages Rooms within a perimeter.

### Public Member Functions

-   [[RoomGroup]{.underline}](#AAAAAAAABZ) ()

*Creates an empty group of Rooms.*

-   void [[MoveFromTo]{.underline}](#AAAAAAAACA) (Vector3 from, Vector3
    to)

*Moves all Rooms in the [[RoomGroup]{.underline}](#AAAAAAAAAD) and the
[[RoomGroup]{.underline}](#AAAAAAAAAD) Perimeter along a 3D vector
calculated between the supplied Vector3 points.*

-   void [[Rotate]{.underline}](#AAAAAAAACB) (Vector3 pivot, double
    angle)

*Rotates the [[RoomGroup]{.underline}](#AAAAAAAAAD) Perimeter and Rooms
in the horizontal plane around the supplied pivot point.*

-   void [[SetColor]{.underline}](#AAAAAAAACC) (Color color)

*Uniformly sets the color of all Rooms in the
[[RoomGroup]{.underline}](#AAAAAAAAAD).*

-   void [[SetHeight]{.underline}](#AAAAAAAACD) (double height)

*Uniformly sets the height of all Rooms in the
[[RoomGroup]{.underline}](#AAAAAAAAAD).*

-   bool [[RoomsByDivision]{.underline}](#AAAAAAAACE) (int xRooms=1, int
    yRooms=1, double height=3.0)

*Clears the current Rooms list and creates new Rooms defined by
orthogonal x- and y-axis divisions of the
[[RoomGroup]{.underline}](#AAAAAAAAAD) Perimeter.*

### Properties

-   double [[AreaAvailable]{.underline}](#AAAAAAAACF) \[get\]

*Unallocated area of the [[RoomGroup]{.underline}](#AAAAAAAAAD)
perimeter.*

-   double [[AreaPlaced]{.underline}](#AAAAAAAACG) \[get\]

*Area allocated within the [[RoomGroup]{.underline}](#AAAAAAAAAD).*

-   double **Elevation** \[get, set\]

-   string [[Name]{.underline}](#AAAAAAAACI) \[get, set\]

*Arbitrary string identifier for this
[[RoomGroup]{.underline}](#AAAAAAAAAD).*

-   Polygon **Perimeter** \[get, set\]

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Rooms]{.underline}](#AAAAAAAACK) \[get\]

*List of Rooms placed within the Perimeter.*

-   List\< Polygon \> [[RoomsAsPolygons]{.underline}](#AAAAAAAACL)
    \[get\]

*List of all [[Room]{.underline}](#AAAAAAAAAC) perimeters as Polygons.*

-   List\< Space \> [[RoomsAsSpaces]{.underline}](#AAAAAAAACM) \[get\]

*List of all Rooms as Spaces.*

-   double [[SizeX]{.underline}](#AAAAAAAACN) \[get\]

*X dimension of the Perimeter orthogonal bounding box.*

-   double [[SizeY]{.underline}](#AAAAAAAACO) \[get\]

*Y dimension of the Perimeter orthogonal bounding box.*

-   string [[UniqueID]{.underline}](#AAAAAAAACP) \[get\]

*UUID for this [[RoomGroup]{.underline}](#AAAAAAAAAD) instance, set on
initialization.*

### Detailed Description

Creates and manages Rooms within a perimeter.

### Constructor & Destructor Documentation

#### RoomKit.RoomGroup.RoomGroup ()

[]{#AAAAAAAABZ .anchor}

Creates an empty group of Rooms.

##### Returns:

A new [[RoomGroup]{.underline}](#AAAAAAAAAD).

### Member Function Documentation

#### void RoomKit.RoomGroup.MoveFromTo (Vector3 *from*, Vector3 *to*)

[]{#AAAAAAAACA .anchor}

Moves all Rooms in the [[RoomGroup]{.underline}](#AAAAAAAAAD) and the
[[RoomGroup]{.underline}](#AAAAAAAAAD) Perimeter along a 3D vector
calculated between the supplied Vector3 points.

##### Parameters:

  -------- -----------------------------------
  *from*   Vector3 base point of the move.
  *to*     Vector3 target point of the move.
  -------- -----------------------------------

##### Returns:

None.

#### bool RoomKit.RoomGroup.RoomsByDivision (int *xRooms* = 1, int *yRooms* = 1, double *height* = 3.0)

[]{#AAAAAAAACE .anchor}

Clears the current Rooms list and creates new Rooms defined by
orthogonal x- and y-axis divisions of the
[[RoomGroup]{.underline}](#AAAAAAAAAD) Perimeter.

##### Parameters:

  ---------- ------------------------------------------------------------------
  *xRooms*   The quantity of Rooms along orthogonal x-axis. Must be positive.
  *yRooms*   The quantity of Rooms along orthogonal y-axis. Must be positive.
  ---------- ------------------------------------------------------------------

##### Returns:

True if the Rooms are created.

#### void RoomKit.RoomGroup.Rotate (Vector3 *pivot*, double *angle*)

[]{#AAAAAAAACB .anchor}

Rotates the [[RoomGroup]{.underline}](#AAAAAAAAAD) Perimeter and Rooms
in the horizontal plane around the supplied pivot point.

##### Parameters:

  --------- ---------------------------------------------------------------------------------------------
  *pivot*   Vector3 point around which the [[Room]{.underline}](#AAAAAAAAAC) Perimeter will be rotated.
  *angle*   Angle in degrees to rotate the Perimeter.
  --------- ---------------------------------------------------------------------------------------------

##### Returns:

None.

#### void RoomKit.RoomGroup.SetColor (Color *color*)

[]{#AAAAAAAACC .anchor}

Uniformly sets the color of all Rooms in the
[[RoomGroup]{.underline}](#AAAAAAAAAD).

##### Parameters:

  --------- -----------------------------
  *color*   The new color of the Rooms.
  --------- -----------------------------

##### Returns:

None.

#### void RoomKit.RoomGroup.SetHeight (double *height*)

[]{#AAAAAAAACD .anchor}

Uniformly sets the height of all Rooms in the
[[RoomGroup]{.underline}](#AAAAAAAAAD).

##### Parameters:

  ------------- ------------------------------
  *elevation*   The new height of the Rooms.
  ------------- ------------------------------

##### Returns:

None.

### Property Documentation

#### double RoomKit.RoomGroup.AreaAvailable\[get\]

[]{#AAAAAAAACF .anchor}

Unallocated area of the [[RoomGroup]{.underline}](#AAAAAAAAAD)
perimeter.

#### double RoomKit.RoomGroup.AreaPlaced\[get\]

[]{#AAAAAAAACG .anchor}

Area allocated within the [[RoomGroup]{.underline}](#AAAAAAAAAD).

#### string RoomKit.RoomGroup.Name\[get\], \[set\]

[]{#AAAAAAAACI .anchor}

Arbitrary string identifier for this
[[RoomGroup]{.underline}](#AAAAAAAAAD).

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.RoomGroup.Rooms\[get\]

[]{#AAAAAAAACK .anchor}

List of Rooms placed within the Perimeter.

#### List\<Polygon\> RoomKit.RoomGroup.RoomsAsPolygons\[get\]

[]{#AAAAAAAACL .anchor}

List of all [[Room]{.underline}](#AAAAAAAAAC) perimeters as Polygons.

#### List\<Space\> RoomKit.RoomGroup.RoomsAsSpaces\[get\]

[]{#AAAAAAAACM .anchor}

List of all Rooms as Spaces.

#### double RoomKit.RoomGroup.SizeX\[get\]

[]{#AAAAAAAACN .anchor}

X dimension of the Perimeter orthogonal bounding box.

#### double RoomKit.RoomGroup.SizeY\[get\]

[]{#AAAAAAAACO .anchor}

Y dimension of the Perimeter orthogonal bounding box.

#### string RoomKit.RoomGroup.UniqueID\[get\]

[]{#AAAAAAAACP .anchor}

UUID for this [[RoomGroup]{.underline}](#AAAAAAAAAD) instance, set on
initialization.

#### The documentation for this class was generated from the following file:

-   RoomKit/RoomGroup.cs

#### 

RoomKit.RoomRow Class Reference
-------------------------------

[]{#AAAAAAAAAE .anchor}

Creates and manages Rooms placed along a line.

### Public Member Functions

-   [[RoomRow]{.underline}](#AAAAAAAACQ) (Line row)

*Constructor initializes the [[RoomRow]{.underline}](#AAAAAAAAAE) with a
new Line.*

-   [[RoomRow]{.underline}](#AAAAAAAACR) (Vector3 start, Vector3 end)

*Constructor initializes the [[RoomRow]{.underline}](#AAAAAAAAAE) with
line endpoints.*

-   bool [[AddRoom]{.underline}](#AAAAAAAACS)
    ([[Room]{.underline}](#AAAAAAAAAC) room, Polygon within=null,
    IList\< Polygon \> among=null)

*Attempts to place a [[Room]{.underline}](#AAAAAAAAAC) perimeter on the
next open segment of the Row, with optional restrictions of a perimeter
within which the [[Room]{.underline}](#AAAAAAAAAC)\'s perimeter must fit
and a list of Polygons with which it cannot intersect.*

-   void [[MoveFromTo]{.underline}](#AAAAAAAACT) (Vector3 from, Vector3
    to)

*Moves all Rooms in the [[RoomRow]{.underline}](#AAAAAAAAAE) and the
[[RoomRow]{.underline}](#AAAAAAAAAE) Row along a 3D vector calculated
between the supplied Vector3 points.*

-   void [[Rotate]{.underline}](#AAAAAAAACU) (Vector3 pivot, double
    angle)

*Rotates the [[RoomRow]{.underline}](#AAAAAAAAAE) Row and Rooms in the
horizontal plane around the supplied pivot point.*

-   void [[SetColor]{.underline}](#AAAAAAAACV) (Color color)

*Uniformly sets the color of all Rooms in the
[[RoomRow]{.underline}](#AAAAAAAAAE).*

-   void [[SetHeight]{.underline}](#AAAAAAAACW) (double height)

*Uniformly sets the height of all Rooms in the
[[RoomRow]{.underline}](#AAAAAAAAAE).*

### Properties

-   double [[AreaPlaced]{.underline}](#AAAAAAAACX) \[get\]

*Aggregate area of the Rooms placed on this Row.*

-   double [[AvailableLength]{.underline}](#AAAAAAAACY) \[get\]

*Unallocated length of the [[RoomRow]{.underline}](#AAAAAAAAAE).*

-   Polygon [[Circulation]{.underline}](#AAAAAAAACZ) \[get\]

*Circulation envelope around the row.*

-   double **CirculationWidth** \[get, set\]

-   double [[Depth]{.underline}](#AAAAAAAADB) = 0.0 \[get\]

*Depth of the deepest room along the Row.*

-   double **Elevation** \[get, set\]

-   string [[Name]{.underline}](#AAAAAAAADD) \[get, set\]

*Arbitrary string identifier for this
[[RoomRow]{.underline}](#AAAAAAAAAE).*

-   IList\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Rooms]{.underline}](#AAAAAAAADE) \[get\]

*List of Rooms placed along the Row.*

-   List\< Polygon \> [[RoomsAsPolygons]{.underline}](#AAAAAAAADF)
    \[get\]

*List of all [[Room]{.underline}](#AAAAAAAAAC) perimeters as Polygons.*

-   List\< Space \> [[RoomsAsSpaces]{.underline}](#AAAAAAAADG) \[get\]

*List of all Rooms as Spaces.*

-   Line [[Row]{.underline}](#AAAAAAAADH) \[get\]

*Line along which Rooms can be placed.*

-   double [[SizeX]{.underline}](#AAAAAAAADI) \[get\]

*X dimension of the Circulation orthogonal bounding box.*

-   double [[SizeY]{.underline}](#AAAAAAAADJ) \[get\]

*Y dimension of the Circulation orthogonal bounding box.*

-   string [[UniqueID]{.underline}](#AAAAAAAADK) \[get\]

*UUID for this [[RoomRow]{.underline}](#AAAAAAAAAE) instance, set on
initialization.*

### Detailed Description

Creates and manages Rooms placed along a line.

### Constructor & Destructor Documentation

#### RoomKit.RoomRow.RoomRow (Line *row*)

[]{#AAAAAAAACQ .anchor}

Constructor initializes the [[RoomRow]{.underline}](#AAAAAAAAAE) with a
new Line.

#### RoomKit.RoomRow.RoomRow (Vector3 *start*, Vector3 *end*)

[]{#AAAAAAAACR .anchor}

Constructor initializes the [[RoomRow]{.underline}](#AAAAAAAAAE) with
line endpoints.

### Member Function Documentation

#### bool RoomKit.RoomRow.AddRoom ([[Room]{.underline}](#AAAAAAAAAC) *room*, Polygon *within* = null, IList\< Polygon \> *among* = null)

[]{#AAAAAAAACS .anchor}

Attempts to place a [[Room]{.underline}](#AAAAAAAAAC) perimeter on the
next open segment of the Row, with optional restrictions of a perimeter
within which the [[Room]{.underline}](#AAAAAAAAAC)\'s perimeter must fit
and a list of Polygons with which it cannot intersect.

##### Parameters:

  ---------- ----------------------------------------------------------------------------------------
  *room*     [[Room]{.underline}](#AAAAAAAAAC) from which to derive the Polygon to place.
  *within*   Polygon perimeter within which a new [[Room]{.underline}](#AAAAAAAAAC) must fit.
  *among*    List of Polygon perimeters the new [[Room]{.underline}](#AAAAAAAAAC) cannot intersect.
  ---------- ----------------------------------------------------------------------------------------

##### Returns:

True if the room was successfully placed.

#### void RoomKit.RoomRow.MoveFromTo (Vector3 *from*, Vector3 *to*)

[]{#AAAAAAAACT .anchor}

Moves all Rooms in the [[RoomRow]{.underline}](#AAAAAAAAAE) and the
[[RoomRow]{.underline}](#AAAAAAAAAE) Row along a 3D vector calculated
between the supplied Vector3 points.

##### Parameters:

  -------- -----------------------------------
  *from*   Vector3 base point of the move.
  *to*     Vector3 target point of the move.
  -------- -----------------------------------

##### Returns:

None.

#### void RoomKit.RoomRow.Rotate (Vector3 *pivot*, double *angle*)

[]{#AAAAAAAACU .anchor}

Rotates the [[RoomRow]{.underline}](#AAAAAAAAAE) Row and Rooms in the
horizontal plane around the supplied pivot point.

##### Parameters:

  --------- ---------------------------------------------------------------------------------------------
  *pivot*   Vector3 point around which the [[Room]{.underline}](#AAAAAAAAAC) Perimeter will be rotated.
  *angle*   Angle in degrees to rotate the Perimeter.
  --------- ---------------------------------------------------------------------------------------------

##### Returns:

None.

#### void RoomKit.RoomRow.SetColor (Color *color*)

[]{#AAAAAAAACV .anchor}

Uniformly sets the color of all Rooms in the
[[RoomRow]{.underline}](#AAAAAAAAAE).

##### Parameters:

  --------- -------------------------
  *color*   New color of the Rooms.
  --------- -------------------------

##### Returns:

None.

#### void RoomKit.RoomRow.SetHeight (double *height*)

[]{#AAAAAAAACW .anchor}

Uniformly sets the height of all Rooms in the
[[RoomRow]{.underline}](#AAAAAAAAAE).

##### Parameters:

  ------------- --------------------------
  *elevation*   New height of the Rooms.
  ------------- --------------------------

##### Returns:

None.

### Property Documentation

#### double RoomKit.RoomRow.AreaPlaced\[get\]

[]{#AAAAAAAACX .anchor}

Aggregate area of the Rooms placed on this Row.

#### double RoomKit.RoomRow.AvailableLength\[get\]

[]{#AAAAAAAACY .anchor}

Unallocated length of the [[RoomRow]{.underline}](#AAAAAAAAAE).

#### Polygon RoomKit.RoomRow.Circulation\[get\]

[]{#AAAAAAAACZ .anchor}

Circulation envelope around the row.

#### double RoomKit.RoomRow.Depth = 0.0\[get\]

[]{#AAAAAAAADB .anchor}

Depth of the deepest room along the Row.

#### string RoomKit.RoomRow.Name\[get\], \[set\]

[]{#AAAAAAAADD .anchor}

Arbitrary string identifier for this
[[RoomRow]{.underline}](#AAAAAAAAAE).

#### IList\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.RoomRow.Rooms\[get\]

[]{#AAAAAAAADE .anchor}

List of Rooms placed along the Row.

#### List\<Polygon\> RoomKit.RoomRow.RoomsAsPolygons\[get\]

[]{#AAAAAAAADF .anchor}

List of all [[Room]{.underline}](#AAAAAAAAAC) perimeters as Polygons.

#### List\<Space\> RoomKit.RoomRow.RoomsAsSpaces\[get\]

[]{#AAAAAAAADG .anchor}

List of all Rooms as Spaces.

#### Line RoomKit.RoomRow.Row\[get\]

[]{#AAAAAAAADH .anchor}

Line along which Rooms can be placed.

#### double RoomKit.RoomRow.SizeX\[get\]

[]{#AAAAAAAADI .anchor}

X dimension of the Circulation orthogonal bounding box.

#### double RoomKit.RoomRow.SizeY\[get\]

[]{#AAAAAAAADJ .anchor}

Y dimension of the Circulation orthogonal bounding box.

#### string RoomKit.RoomRow.UniqueID\[get\]

[]{#AAAAAAAADK .anchor}

UUID for this [[RoomRow]{.underline}](#AAAAAAAAAE) instance, set on
initialization.

#### The documentation for this class was generated from the following file:

-   RoomKit/RoomRow.cs

#### 

RoomKit.Scope Class Reference
-----------------------------

[]{#AAAAAAAAAF .anchor}

Data structure recording space program characteristics and the status of
a [[Room]{.underline}](#AAAAAAAAAC) placing process.

### Public Member Functions

-   [[Scope]{.underline}](#AAAAAAAADL) ()

*Contructor creates empty [[Room]{.underline}](#AAAAAAAAAC) lists for
Circulation, Occupation, Service, and Tenant.*

-   [[Room]{.underline}](#AAAAAAAAAC)
    [[FindByDesignArea]{.underline}](#AAAAAAAADM) (double area, bool
    unplaced=true)

*Finds the first Occupant [[Room]{.underline}](#AAAAAAAAAC) with the
DesignArea value closest to the supplied area. C*

-   [[Room]{.underline}](#AAAAAAAAAC)
    [[FindByDesignXY]{.underline}](#AAAAAAAADN) (double designLength,
    double designWidth, bool unplaced=true)

*Finds the first Occupant [[Room]{.underline}](#AAAAAAAAAC) with the
designed x and y dimensions closest to the supplied values.*

-   [[Room]{.underline}](#AAAAAAAAAC)
    [[FindByTypeID]{.underline}](#AAAAAAAADO) (int typeID, bool
    unplaced=true)

*Finds the first unplaced [[Room]{.underline}](#AAAAAAAAAC) with the
specifed TypeID.*

### Properties

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Circulation]{.underline}](#AAAAAAAADP) \[get\]

*List of Rooms designated as circulation.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Occupant]{.underline}](#AAAAAAAADQ) \[get\]

*List of Rooms designated for occupation, rather than circulation.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Service]{.underline}](#AAAAAAAADR) \[get\]

*List of Rooms designated for building services.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Tenant]{.underline}](#AAAAAAAADS) \[get\]

*List of Rooms intended as a series of tenant space containers of other
Rooms.*

-   List\< Polygon \> [[AllocatedAsPolygons]{.underline}](#AAAAAAAADT)
    \[get\]

*List of allocated Circulation, Occupant, and Service
[[Room]{.underline}](#AAAAAAAAAC) Perimeters as Polygons.*

-   double [[AreaDesignAvailable]{.underline}](#AAAAAAAADU) \[get\]

*Area available for horizontal circulation.*

-   double [[AreaDesignCirculation]{.underline}](#AAAAAAAADV) \[get\]

*Intended aggregate area of all Occupant Rooms.*

-   double [[AreaDesignOccupant]{.underline}](#AAAAAAAADW) \[get\]

*Intended aggregate area of all Occupant Rooms.*

-   double [[AreaCirculation]{.underline}](#AAAAAAAADX) \[get\]

*Allocated aggregate area of all placed Circulation Rooms.*

-   double [[AreaOccupant]{.underline}](#AAAAAAAADY) \[get\]

*Allocated aggregate area of all placed Occupant Rooms.*

-   double [[AreaService]{.underline}](#AAAAAAAADZ) \[get\]

*Aggregate area of all Services Rooms.*

-   double [[AreaTenant]{.underline}](#AAAAAAAAEA) \[get\]

*Aggregate area of all occupiable Tenant Rooms.*

-   List\< Polygon \> [[CirculationAsPolygons]{.underline}](#AAAAAAAAEB)
    \[get\]

*List of all Circulation [[Room]{.underline}](#AAAAAAAAAC) Perimeters as
Polygons.*

-   List\< Polygon \> [[OccupantAsPolygons]{.underline}](#AAAAAAAAEC)
    \[get\]

*List of all Occupant [[Room]{.underline}](#AAAAAAAAAC) Perimeters as
Polygons.*

-   List\< Polygon \> [[ServiceAsPolygons]{.underline}](#AAAAAAAAED)
    \[get\]

*List of all Service [[Room]{.underline}](#AAAAAAAAAC) Perimeters as
Polygons.*

-   List\< Polygon \> [[TenantAsPolygons]{.underline}](#AAAAAAAAEE)
    \[get\]

*List of all Tenant [[Room]{.underline}](#AAAAAAAAAC) Perimeter
Polygons.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Placed]{.underline}](#AAAAAAAAEF) \[get\]

*List of all Rooms marked as Placed.*

-   bool [[PlacedAll]{.underline}](#AAAAAAAAEG) \[get\]

*Returns whether all Occupant Rooms have been Placed.*

-   double [[PlacedQuantity]{.underline}](#AAAAAAAAEH) \[get\]

*The quantity of placed Rooms.*

-   double [[RatioCirculation]{.underline}](#AAAAAAAAEI) \[get\]

*Returns the ratio of the aggregate area of all Occupant Rooms against
the Circulation area.*

-   double [[RatioDesignCirculation]{.underline}](#AAAAAAAAEJ) \[get\]

*Returns the ratio of the aggregate area of all designed Occupant Rooms
against the designed Circulation area.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Unplaced]{.underline}](#AAAAAAAAEK) \[get\]

*Returns all Rooms with Placed = false.*

-   double [[UnplacedQuantity]{.underline}](#AAAAAAAAEL) \[get\]

*The quantity of unplaced Rooms.*

### Detailed Description

Data structure recording space program characteristics and the status of
a [[Room]{.underline}](#AAAAAAAAAC) placing process.

### Constructor & Destructor Documentation

#### RoomKit.Scope.Scope ()

[]{#AAAAAAAADL .anchor}

Contructor creates empty [[Room]{.underline}](#AAAAAAAAAC) lists for
Circulation, Occupation, Service, and Tenant.

##### Returns:

A new [[Scope]{.underline}](#AAAAAAAAAF).

### Member Function Documentation

#### [[Room]{.underline}](#AAAAAAAAAC) RoomKit.Scope.FindByDesignArea (double *area*, bool *unplaced* = true)

[]{#AAAAAAAADM .anchor}

Finds the first Occupant [[Room]{.underline}](#AAAAAAAAAC) with the
DesignArea value closest to the supplied area. C

##### Parameters:

  -------- --------------------------------------------------------------------------------------------
  *area*   Area to match from the list of all Occupant [[Room]{.underline}](#AAAAAAAAAC) definitions.
  -------- --------------------------------------------------------------------------------------------

##### Returns:

A [[Room]{.underline}](#AAAAAAAAAC).

#### [[Room]{.underline}](#AAAAAAAAAC) RoomKit.Scope.FindByDesignXY (double *designLength*, double *designWidth*, bool *unplaced* = true)

[]{#AAAAAAAADN .anchor}

Finds the first Occupant [[Room]{.underline}](#AAAAAAAAAC) with the
designed x and y dimensions closest to the supplied values.

##### Parameters:

  ----------- --------------------------------
  *designX*   The x-axis dimension to match.
  *designY*   The y-axis dimension to match.
  ----------- --------------------------------

##### Returns:

A [[Room]{.underline}](#AAAAAAAAAC).

#### [[Room]{.underline}](#AAAAAAAAAC) RoomKit.Scope.FindByTypeID (int *typeID*, bool *unplaced* = true)

[]{#AAAAAAAADO .anchor}

Finds the first unplaced [[Room]{.underline}](#AAAAAAAAAC) with the
specifed TypeID.

##### Parameters:

  ---------- -------------------------------------------------------------
  *typeID*   The integer ID of a [[Room]{.underline}](#AAAAAAAAAC) type.
  ---------- -------------------------------------------------------------

##### Returns:

A [[Room]{.underline}](#AAAAAAAAAC).

### Property Documentation

#### List\<Polygon\> RoomKit.Scope.AllocatedAsPolygons\[get\]

[]{#AAAAAAAADT .anchor}

List of allocated Circulation, Occupant, and Service
[[Room]{.underline}](#AAAAAAAAAC) Perimeters as Polygons.

#### double RoomKit.Scope.AreaCirculation\[get\]

[]{#AAAAAAAADX .anchor}

Allocated aggregate area of all placed Circulation Rooms.

#### double RoomKit.Scope.AreaDesignAvailable\[get\]

[]{#AAAAAAAADU .anchor}

Area available for horizontal circulation.

#### double RoomKit.Scope.AreaDesignCirculation\[get\]

[]{#AAAAAAAADV .anchor}

Intended aggregate area of all Occupant Rooms.

#### double RoomKit.Scope.AreaDesignOccupant\[get\]

[]{#AAAAAAAADW .anchor}

Intended aggregate area of all Occupant Rooms.

#### double RoomKit.Scope.AreaOccupant\[get\]

[]{#AAAAAAAADY .anchor}

Allocated aggregate area of all placed Occupant Rooms.

#### double RoomKit.Scope.AreaService\[get\]

[]{#AAAAAAAADZ .anchor}

Aggregate area of all Services Rooms.

#### double RoomKit.Scope.AreaTenant\[get\]

[]{#AAAAAAAAEA .anchor}

Aggregate area of all occupiable Tenant Rooms.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Scope.Circulation\[get\]

[]{#AAAAAAAADP .anchor}

List of Rooms designated as circulation.

#### List\<Polygon\> RoomKit.Scope.CirculationAsPolygons\[get\]

[]{#AAAAAAAAEB .anchor}

List of all Circulation [[Room]{.underline}](#AAAAAAAAAC) Perimeters as
Polygons.

##### Returns:

A list of Polygons.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Scope.Occupant\[get\]

[]{#AAAAAAAADQ .anchor}

List of Rooms designated for occupation, rather than circulation.

#### List\<Polygon\> RoomKit.Scope.OccupantAsPolygons\[get\]

[]{#AAAAAAAAEC .anchor}

List of all Occupant [[Room]{.underline}](#AAAAAAAAAC) Perimeters as
Polygons.

##### Returns:

A list of Polygons.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Scope.Placed\[get\]

[]{#AAAAAAAAEF .anchor}

List of all Rooms marked as Placed.

##### Returns:

A list of Rooms.

#### bool RoomKit.Scope.PlacedAll\[get\]

[]{#AAAAAAAAEG .anchor}

Returns whether all Occupant Rooms have been Placed.

##### Returns:

Returns true if each [[Room]{.underline}](#AAAAAAAAAC) in Occupant has
been marked with [[Room.Placed]{.underline}](#AAAAAAAABU) = true.

#### double RoomKit.Scope.PlacedQuantity\[get\]

[]{#AAAAAAAAEH .anchor}

The quantity of placed Rooms.

#### double RoomKit.Scope.RatioCirculation\[get\]

[]{#AAAAAAAAEI .anchor}

Returns the ratio of the aggregate area of all Occupant Rooms against
the Circulation area.

##### Returns:

A list of Rooms.

#### double RoomKit.Scope.RatioDesignCirculation\[get\]

[]{#AAAAAAAAEJ .anchor}

Returns the ratio of the aggregate area of all designed Occupant Rooms
against the designed Circulation area.

##### Returns:

A list of Rooms.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Scope.Service\[get\]

[]{#AAAAAAAADR .anchor}

List of Rooms designated for building services.

#### List\<Polygon\> RoomKit.Scope.ServiceAsPolygons\[get\]

[]{#AAAAAAAAED .anchor}

List of all Service [[Room]{.underline}](#AAAAAAAAAC) Perimeters as
Polygons.

##### Returns:

A list of Polygons.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Scope.Tenant\[get\]

[]{#AAAAAAAADS .anchor}

List of Rooms intended as a series of tenant space containers of other
Rooms.

#### List\<Polygon\> RoomKit.Scope.TenantAsPolygons\[get\]

[]{#AAAAAAAAEE .anchor}

List of all Tenant [[Room]{.underline}](#AAAAAAAAAC) Perimeter Polygons.

##### Returns:

A list of Polygons.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Scope.Unplaced\[get\]

[]{#AAAAAAAAEK .anchor}

Returns all Rooms with Placed = false.

##### Returns:

A list of Rooms.

#### double RoomKit.Scope.UnplacedQuantity\[get\]

[]{#AAAAAAAAEL .anchor}

The quantity of unplaced Rooms.

#### The documentation for this class was generated from the following file:

-   RoomKit/Scope.cs

#### 

RoomKit.Story Class Reference
-----------------------------

[]{#AAAAAAAAAG .anchor}

Creates and manages the geometry of a slab and Rooms representing
corridors, occupied rooms, and services.

### Public Member Functions

-   [[Story]{.underline}](#AAAAAAAAEM) ()

*Creates a [[Story]{.underline}](#AAAAAAAAAG) at a 1.0 Height on the
zero plane with new lists for Corridors, Rooms, and Services. Perimeter
is set to null, Name is blank, and SlabThickness is s0.1.*

-   bool [[AddCorridor]{.underline}](#AAAAAAAAEN)
    ([[Room]{.underline}](#AAAAAAAAAC) room, bool fit=true)

*Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Corridors list.*

-   bool [[AddExclusion]{.underline}](#AAAAAAAAEO)
    ([[Room]{.underline}](#AAAAAAAAAC) room, bool fit=true)

*Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Exclusions list.*

-   bool [[AddRoom]{.underline}](#AAAAAAAAEP)
    ([[Room]{.underline}](#AAAAAAAAAC) room, bool fit=true)

*Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Rooms list.*

-   bool [[AddService]{.underline}](#AAAAAAAAEQ)
    ([[Room]{.underline}](#AAAAAAAAAC) room, bool fit=true)

*Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Services list.*

-   void [[MoveFromTo]{.underline}](#AAAAAAAAER) (Vector3 from, Vector3
    to)

*Moves all Rooms in the [[Story]{.underline}](#AAAAAAAAAG) and the
[[Story]{.underline}](#AAAAAAAAAG) Envelope along a 3D vector calculated
between the supplied Vector3 points.*

-   bool [[RoomsByDivision]{.underline}](#AAAAAAAAES) (int xRooms=1, int
    yRooms=1, double height=3.0, double setback=0.0, string name=\"\",
    Color color=null, bool fit=true)

*Creates Rooms by orthogonally dividing the interior of the
[[Story]{.underline}](#AAAAAAAAAG) perimeter by a quantity of x-axis and
y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms
conform to Corridor and Service perimeters.*

-   void [[Rotate]{.underline}](#AAAAAAAAET) (Vector3 pivot, double
    angle)

*Rotates the [[Story]{.underline}](#AAAAAAAAAG) Perimeter and Rooms in
the horizontal plane around the supplied pivot point.*

### Properties

-   double [[Area]{.underline}](#AAAAAAAAEU) \[get\]

*Area of the perimeter.*

-   double [[AreaAvailable]{.underline}](#AAAAAAAAEV) \[get\]

*Unallocated area within the [[Story]{.underline}](#AAAAAAAAAG).*

-   double [[AreaPlaced]{.underline}](#AAAAAAAAEW) \[get\]

*Area allocated to Corridors, Rooms, and Services.*

-   Color **Color** \[get, set\]

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Corridors]{.underline}](#AAAAAAAAEY) \[get\]

*List of Rooms designated as cooridors.*

-   List\< Polygon \> [[CorridorsAsPolygons]{.underline}](#AAAAAAAAEZ)
    \[get\]

*Polygons representing Corridors. Rooms Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Corridor Perimeters.*

-   List\< Space \> [[CorridorsAsSpaces]{.underline}](#AAAAAAAAFA)
    \[get\]

*List of Spaces created from [[Room]{.underline}](#AAAAAAAAAC)
characteristics within the Corridors list.*

-   Color [[CorridorsColor]{.underline}](#AAAAAAAAFB) \[set\]

*Sets the Corridors color.*

-   double **Elevation** \[get, set\]

-   [[Room]{.underline}](#AAAAAAAAAC)
    [[Envelope]{.underline}](#AAAAAAAAFD) \[get\]

*[[Room]{.underline}](#AAAAAAAAAC) representing the
[[Story]{.underline}](#AAAAAAAAAG) envelope.*

-   Polygon [[EnvelopeAsPolygon]{.underline}](#AAAAAAAAFE) \[get\]

*Polygon representation of the [[Story]{.underline}](#AAAAAAAAAG)
Perimeter.*

-   Space [[EnvelopeAsSpace]{.underline}](#AAAAAAAAFF) \[get\]

*Space created from [[Story]{.underline}](#AAAAAAAAAG) characteristics.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Exclusions]{.underline}](#AAAAAAAAFG) \[get\]

*Rooms representing areas that must not be intersected, but which will
not be available as Spaces. All other [[Room]{.underline}](#AAAAAAAAAC)
Perimeters in the [[Story]{.underline}](#AAAAAAAAAG) conform to
Exclusion [[Room]{.underline}](#AAAAAAAAAC) Perimeters.*

-   List\< Polygon \> [[ExclusionsAsPolygons]{.underline}](#AAAAAAAAFH)
    \[get\]

*Polygons representing areas that must not be intersected. All other
[[Room]{.underline}](#AAAAAAAAAC) Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Exclusion
[[Room]{.underline}](#AAAAAAAAAC) Perimeters.*

-   double **Height** \[get, set\]

-   double [[HeightInteriors]{.underline}](#AAAAAAAAFJ) \[set\]

*Sets the height of all Corridors, Rooms, and Services.*

-   IList\< Polygon \> [[InteriorsAsPolygons]{.underline}](#AAAAAAAAFK)
    \[get\]

*Returns all Corridors, Exclusions, Rooms, and Services as Polygons.*

-   IList\< Space \> [[InteriorsAsSpaces]{.underline}](#AAAAAAAAFL)
    \[get\]

*Returns all Corridors, Rooms, and Services as Spaces.*

-   string [[Name]{.underline}](#AAAAAAAAFM) \[get, set\]

*Arbitrary string identifier.*

-   Polygon **Perimeter** \[get, set\]

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Rooms]{.underline}](#AAAAAAAAFO) \[get\]

*List of Rooms designated as occupiable rooms.*

-   List\< Polygon \> [[RoomsAsPolygons]{.underline}](#AAAAAAAAFP)
    \[get\]

*Polygons representing Services. Corridors and Rooms Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Service
[[Room]{.underline}](#AAAAAAAAAC) Perimeters.*

-   List\< Space \> [[RoomsAsSpaces]{.underline}](#AAAAAAAAFQ) \[get\]

*List of Spaces created from [[Room]{.underline}](#AAAAAAAAAC)
characteristics within the Rooms list.*

-   Color [[RoomsColor]{.underline}](#AAAAAAAAFR) \[set\]

*Sets the Rooms rendering color.*

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Services]{.underline}](#AAAAAAAAFS) \[get\]

*A list of Rooms designated as building services.*

-   List\< Polygon \> [[ServicesAsPolygons]{.underline}](#AAAAAAAAFT)
    \[get\]

*Polygons representing Services. Corridors and Rooms Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Service
[[Room]{.underline}](#AAAAAAAAAC) Perimeters.*

-   List\< Space \> [[ServicesAsSpaces]{.underline}](#AAAAAAAAFU)
    \[get\]

*List of Spaces created from [[Room]{.underline}](#AAAAAAAAAC)
characteristics within the Services list.*

-   Color [[ServicesColor]{.underline}](#AAAAAAAAFV) \[set\]

*Sets the Services Space rendering color.*

-   Floor [[Slab]{.underline}](#AAAAAAAAFW) \[get\]

*Concrete Floor created from [[Story]{.underline}](#AAAAAAAAAG) and Slab
characteristics.*

-   double **SlabThickness** \[get, set\]

### Detailed Description

Creates and manages the geometry of a slab and Rooms representing
corridors, occupied rooms, and services.

### Constructor & Destructor Documentation

#### RoomKit.Story.Story ()

[]{#AAAAAAAAEM .anchor}

Creates a [[Story]{.underline}](#AAAAAAAAAG) at a 1.0 Height on the zero
plane with new lists for Corridors, Rooms, and Services. Perimeter is
set to null, Name is blank, and SlabThickness is s0.1.

##### Returns:

A new [[Story]{.underline}](#AAAAAAAAAG).

### Member Function Documentation

#### bool RoomKit.Story.AddCorridor ([[Room]{.underline}](#AAAAAAAAAC) *room*, bool *fit* = true)

[]{#AAAAAAAAEN .anchor}

Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Corridors list.

##### Parameters:

  -------- ---------------------------------------------------------------------------------------------------------------------------
  *room*   [[Room]{.underline}](#AAAAAAAAAC) to add.
  *fit*    Indicates whether the new room should mutually fit to other [[Story]{.underline}](#AAAAAAAAAG) features. Default is true.
  -------- ---------------------------------------------------------------------------------------------------------------------------

##### Returns:

True if one or more rooms were added to the
[[Story]{.underline}](#AAAAAAAAAG).

#### bool RoomKit.Story.AddExclusion ([[Room]{.underline}](#AAAAAAAAAC) *room*, bool *fit* = true)

[]{#AAAAAAAAEO .anchor}

Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Exclusions list.

##### Parameters:

  -------- ---------------------------------------------------------------------------------------------------------------------------
  *room*   [[Room]{.underline}](#AAAAAAAAAC) to add.
  *fit*    Indicates whether the new room should mutually fit to other [[Story]{.underline}](#AAAAAAAAAG) features. Default is true.
  -------- ---------------------------------------------------------------------------------------------------------------------------

##### Returns:

True if one or more rooms were added to the
[[Story]{.underline}](#AAAAAAAAAG).

#### bool RoomKit.Story.AddRoom ([[Room]{.underline}](#AAAAAAAAAC) *room*, bool *fit* = true)

[]{#AAAAAAAAEP .anchor}

Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Rooms list.

##### Parameters:

  -------- ---------------------------------------------------------------------------------------------------------------------------
  *room*   [[Room]{.underline}](#AAAAAAAAAC) to add.
  *fit*    Indicates whether the new room should mutually fit to other [[Story]{.underline}](#AAAAAAAAAG) features. Default is true.
  -------- ---------------------------------------------------------------------------------------------------------------------------

##### Returns:

True if one or more rooms were added to the
[[Story]{.underline}](#AAAAAAAAAG).

#### bool RoomKit.Story.AddService ([[Room]{.underline}](#AAAAAAAAAC) *room*, bool *fit* = true)

[]{#AAAAAAAAEQ .anchor}

Adds a [[Room]{.underline}](#AAAAAAAAAC) to the Services list.

##### Parameters:

  -------- ---------------------------------------------------------------------------------------------------------------------------
  *room*   [[Room]{.underline}](#AAAAAAAAAC) to add.
  *fit*    Indicates whether the new room should mutually fit to other [[Story]{.underline}](#AAAAAAAAAG) features. Default is true.
  -------- ---------------------------------------------------------------------------------------------------------------------------

##### Returns:

True if one or more rooms were added to the
[[Story]{.underline}](#AAAAAAAAAG).

#### void RoomKit.Story.MoveFromTo (Vector3 *from*, Vector3 *to*)

[]{#AAAAAAAAER .anchor}

Moves all Rooms in the [[Story]{.underline}](#AAAAAAAAAG) and the
[[Story]{.underline}](#AAAAAAAAAG) Envelope along a 3D vector calculated
between the supplied Vector3 points.

##### Parameters:

  -------- -----------------------------------
  *from*   Vector3 base point of the move.
  *to*     Vector3 target point of the move.
  -------- -----------------------------------

##### Returns:

None.

#### bool RoomKit.Story.RoomsByDivision (int *xRooms* = 1, int *yRooms* = 1, double *height* = 3.0, double *setback* = 0.0, string *name* = \"\", Color *color* = null, bool *fit* = true)

[]{#AAAAAAAAES .anchor}

Creates Rooms by orthogonally dividing the interior of the
[[Story]{.underline}](#AAAAAAAAAG) perimeter by a quantity of x-axis and
y-axis intervals. Adds the new Rooms to the Rooms list. New Rooms
conform to Corridor and Service perimeters.

##### Parameters:

  ----------- ---------------------------------------------------------------------------
  *xRooms*    Quantity Rooms along the orthogonal x-axis.
  *yRooms*    Quantity Rooms along the orthogonal y-axis.
  *height*    Height of the new Rooms.
  *setback*   Offset from the [[Story]{.underline}](#AAAAAAAAAG) perimeter.
  *name*      String identifier applied to every new [[Room]{.underline}](#AAAAAAAAAC).
  *color*     Rendering color of the [[Room]{.underline}](#AAAAAAAAAC) as a Space.
  ----------- ---------------------------------------------------------------------------

##### Returns:

None.

#### void RoomKit.Story.Rotate (Vector3 *pivot*, double *angle*)

[]{#AAAAAAAAET .anchor}

Rotates the [[Story]{.underline}](#AAAAAAAAAG) Perimeter and Rooms in
the horizontal plane around the supplied pivot point.

##### Parameters:

  --------- ---------------------------------------------------------------------------------------------
  *pivot*   Vector3 point around which the [[Room]{.underline}](#AAAAAAAAAC) Perimeter will be rotated.
  *angle*   Angle in degrees to rotate the Perimeter.
  --------- ---------------------------------------------------------------------------------------------

##### Returns:

None.

### Property Documentation

#### double RoomKit.Story.Area\[get\]

[]{#AAAAAAAAEU .anchor}

Area of the perimeter.

#### double RoomKit.Story.AreaAvailable\[get\]

[]{#AAAAAAAAEV .anchor}

Unallocated area within the [[Story]{.underline}](#AAAAAAAAAG).

#### double RoomKit.Story.AreaPlaced\[get\]

[]{#AAAAAAAAEW .anchor}

Area allocated to Corridors, Rooms, and Services.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Story.Corridors\[get\]

[]{#AAAAAAAAEY .anchor}

List of Rooms designated as cooridors.

#### List\<Polygon\> RoomKit.Story.CorridorsAsPolygons\[get\]

[]{#AAAAAAAAEZ .anchor}

Polygons representing Corridors. Rooms Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Corridor Perimeters.

#### List\<Space\> RoomKit.Story.CorridorsAsSpaces\[get\]

[]{#AAAAAAAAFA .anchor}

List of Spaces created from [[Room]{.underline}](#AAAAAAAAAC)
characteristics within the Corridors list.

#### Color RoomKit.Story.CorridorsColor\[set\]

[]{#AAAAAAAAFB .anchor}

Sets the Corridors color.

#### [[Room]{.underline}](#AAAAAAAAAC) RoomKit.Story.Envelope\[get\]

[]{#AAAAAAAAFD .anchor}

[[Room]{.underline}](#AAAAAAAAAC) representing the
[[Story]{.underline}](#AAAAAAAAAG) envelope.

#### Polygon RoomKit.Story.EnvelopeAsPolygon\[get\]

[]{#AAAAAAAAFE .anchor}

Polygon representation of the [[Story]{.underline}](#AAAAAAAAAG)
Perimeter.

#### Space RoomKit.Story.EnvelopeAsSpace\[get\]

[]{#AAAAAAAAFF .anchor}

Space created from [[Story]{.underline}](#AAAAAAAAAG) characteristics.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Story.Exclusions\[get\]

[]{#AAAAAAAAFG .anchor}

Rooms representing areas that must not be intersected, but which will
not be available as Spaces. All other [[Room]{.underline}](#AAAAAAAAAC)
Perimeters in the [[Story]{.underline}](#AAAAAAAAAG) conform to
Exclusion [[Room]{.underline}](#AAAAAAAAAC) Perimeters.

#### List\<Polygon\> RoomKit.Story.ExclusionsAsPolygons\[get\]

[]{#AAAAAAAAFH .anchor}

Polygons representing areas that must not be intersected. All other
[[Room]{.underline}](#AAAAAAAAAC) Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Exclusion
[[Room]{.underline}](#AAAAAAAAAC) Perimeters.

#### double RoomKit.Story.HeightInteriors\[set\]

[]{#AAAAAAAAFJ .anchor}

Sets the height of all Corridors, Rooms, and Services.

#### IList\<Polygon\> RoomKit.Story.InteriorsAsPolygons\[get\]

[]{#AAAAAAAAFK .anchor}

Returns all Corridors, Exclusions, Rooms, and Services as Polygons.

#### IList\<Space\> RoomKit.Story.InteriorsAsSpaces\[get\]

[]{#AAAAAAAAFL .anchor}

Returns all Corridors, Rooms, and Services as Spaces.

#### string RoomKit.Story.Name\[get\], \[set\]

[]{#AAAAAAAAFM .anchor}

Arbitrary string identifier.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Story.Rooms\[get\]

[]{#AAAAAAAAFO .anchor}

List of Rooms designated as occupiable rooms.

#### List\<Polygon\> RoomKit.Story.RoomsAsPolygons\[get\]

[]{#AAAAAAAAFP .anchor}

Polygons representing Services. Corridors and Rooms Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Service
[[Room]{.underline}](#AAAAAAAAAC) Perimeters.

#### List\<Space\> RoomKit.Story.RoomsAsSpaces\[get\]

[]{#AAAAAAAAFQ .anchor}

List of Spaces created from [[Room]{.underline}](#AAAAAAAAAC)
characteristics within the Rooms list.

#### Color RoomKit.Story.RoomsColor\[set\]

[]{#AAAAAAAAFR .anchor}

Sets the Rooms rendering color.

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Story.Services\[get\]

[]{#AAAAAAAAFS .anchor}

A list of Rooms designated as building services.

#### List\<Polygon\> RoomKit.Story.ServicesAsPolygons\[get\]

[]{#AAAAAAAAFT .anchor}

Polygons representing Services. Corridors and Rooms Perimeters in the
[[Story]{.underline}](#AAAAAAAAAG) conform to Service
[[Room]{.underline}](#AAAAAAAAAC) Perimeters.

#### List\<Space\> RoomKit.Story.ServicesAsSpaces\[get\]

[]{#AAAAAAAAFU .anchor}

List of Spaces created from [[Room]{.underline}](#AAAAAAAAAC)
characteristics within the Services list.

#### Color RoomKit.Story.ServicesColor\[set\]

[]{#AAAAAAAAFV .anchor}

Sets the Services Space rendering color.

#### Floor RoomKit.Story.Slab\[get\]

[]{#AAAAAAAAFW .anchor}

Concrete Floor created from [[Story]{.underline}](#AAAAAAAAAG) and Slab
characteristics.

#### The documentation for this class was generated from the following file:

-   RoomKit/Story.cs

#### 

RoomKit.TopoBox Class Reference
-------------------------------

[]{#AAAAAAAAAH .anchor}

Maintains a set of points on the orthogonal bounding box of a supplied
Polygon corresponding to four divisions of each side.

### Public Member Functions

-   [[TopoBox]{.underline}](#AAAAAAAAFY) (Polygon polygon)

*Constructor creates a new mathematical bounding box from the supplied
Polygon and populates all orientation points.*

-   Vector3 [[PointBy]{.underline}](#AAAAAAAAFZ)
    ([[Orient]{.underline}](#AAAAAAAAAK) orient)

*Returns the requested bounding box location by orientation.*

-   Vector3 [[PointOpposite]{.underline}](#AAAAAAAAGA)
    ([[Orient]{.underline}](#AAAAAAAAAK) orient)

*Returns the reciprocal bounding box location by orientation.*

### Properties

-   Vector3 [[C]{.underline}](#AAAAAAAAGB) \[get\]

*Vector3 location identifier corresponding to the center of the box
perimeter.*

-   Vector3 [[N]{.underline}](#AAAAAAAAGC) \[get\]

*Vector3 location identifier corresponding to the midpoint of the
maximum Y side of the box perimeter.*

-   Vector3 [[NNW]{.underline}](#AAAAAAAAGD) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
NW and N points of the box perimeter.*

-   Vector3 [[NW]{.underline}](#AAAAAAAAGE) \[get\]

*Vector3 location identifier corresponding to the mimimum X and maximum
Y corner of the box perimeter.*

-   Vector3 [[WNW]{.underline}](#AAAAAAAAGF) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
NW and W points of the box perimeter.*

-   Vector3 [[W]{.underline}](#AAAAAAAAGG) \[get\]

*Vector3 location identifier corresponding to the midpoint of the
minimum X side of the box perimeter.*

-   Vector3 [[WSW]{.underline}](#AAAAAAAAGH) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
SW and W points of the box perimeter.*

-   Vector3 [[SW]{.underline}](#AAAAAAAAGI) \[get\]

*Vector3 location identifier corresponding to the mimimum X and Y corner
of the box perimeter.*

-   Vector3 [[SSW]{.underline}](#AAAAAAAAGJ) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
SW and S points of the box perimeter.*

-   Vector3 [[S]{.underline}](#AAAAAAAAGK) \[get\]

*Vector3 location identifier corresponding to the midpoint of the
minimum Y side of the box perimeter.*

-   Vector3 [[SSE]{.underline}](#AAAAAAAAGL) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
SE and S points of the box perimeter.*

-   Vector3 [[SE]{.underline}](#AAAAAAAAGM) \[get\]

*Vector3 location identifier corresponding to the maximum X and minimum
Y corner of the box perimeter.*

-   Vector3 [[ESE]{.underline}](#AAAAAAAAGN) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
SE and E points of the box perimeter.*

-   Vector3 [[E]{.underline}](#AAAAAAAAGO) \[get\]

*Vector3 location identifier corresponding to the midpoint of the
maximum X side of the box perimeter.*

-   Vector3 [[ENE]{.underline}](#AAAAAAAAGP) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
NE and E points of the box perimeter.*

-   Vector3 [[NE]{.underline}](#AAAAAAAAGQ) \[get\]

*Vector3 location identifier corresponding to the maximum X and Y corner
of the box perimeter.*

-   Vector3 [[NNE]{.underline}](#AAAAAAAAGR) \[get\]

*Vector3 location identifier corresponding to the midpoint between the
NE and N points of the box perimeter.*

-   double [[SizeX]{.underline}](#AAAAAAAAGS) \[get\]

*X and Y dimensions of the [[TopoBox]{.underline}](#AAAAAAAAAH)
perimeter.*

-   double **SizeY** \[get\]

### Detailed Description

Maintains a set of points on the orthogonal bounding box of a supplied
Polygon corresponding to four divisions of each side.

### Constructor & Destructor Documentation

#### RoomKit.TopoBox.TopoBox (Polygon *polygon*)

[]{#AAAAAAAAFY .anchor}

Constructor creates a new mathematical bounding box from the supplied
Polygon and populates all orientation points.

##### Returns:

A new [[TopoBox]{.underline}](#AAAAAAAAAH).

### Member Function Documentation

#### Vector3 RoomKit.TopoBox.PointBy ([[Orient]{.underline}](#AAAAAAAAAK) *orient*)

[]{#AAAAAAAAFZ .anchor}

Returns the requested bounding box location by orientation.

##### Parameters:

  ---------- ----------------------------------
  *orient*   The Orient value to index point.
  ---------- ----------------------------------

##### Returns:

A Vector3 point.

#### Vector3 RoomKit.TopoBox.PointOpposite ([[Orient]{.underline}](#AAAAAAAAAK) *orient*)

[]{#AAAAAAAAGA .anchor}

Returns the reciprocal bounding box location by orientation.

##### Parameters:

  ---------- ------------------------------------------------
  *orient*   The Orient value to find the reciprocal point.
  ---------- ------------------------------------------------

##### Returns:

A Vector3 point.

### Property Documentation

#### Vector3 RoomKit.TopoBox.C\[get\]

[]{#AAAAAAAAGB .anchor}

Vector3 location identifier corresponding to the center of the box
perimeter.

#### Vector3 RoomKit.TopoBox.E\[get\]

[]{#AAAAAAAAGO .anchor}

Vector3 location identifier corresponding to the midpoint of the maximum
X side of the box perimeter.

#### Vector3 RoomKit.TopoBox.ENE\[get\]

[]{#AAAAAAAAGP .anchor}

Vector3 location identifier corresponding to the midpoint between the NE
and E points of the box perimeter.

#### Vector3 RoomKit.TopoBox.ESE\[get\]

[]{#AAAAAAAAGN .anchor}

Vector3 location identifier corresponding to the midpoint between the SE
and E points of the box perimeter.

#### Vector3 RoomKit.TopoBox.N\[get\]

[]{#AAAAAAAAGC .anchor}

Vector3 location identifier corresponding to the midpoint of the maximum
Y side of the box perimeter.

#### Vector3 RoomKit.TopoBox.NE\[get\]

[]{#AAAAAAAAGQ .anchor}

Vector3 location identifier corresponding to the maximum X and Y corner
of the box perimeter.

#### Vector3 RoomKit.TopoBox.NNE\[get\]

[]{#AAAAAAAAGR .anchor}

Vector3 location identifier corresponding to the midpoint between the NE
and N points of the box perimeter.

#### Vector3 RoomKit.TopoBox.NNW\[get\]

[]{#AAAAAAAAGD .anchor}

Vector3 location identifier corresponding to the midpoint between the NW
and N points of the box perimeter.

#### Vector3 RoomKit.TopoBox.NW\[get\]

[]{#AAAAAAAAGE .anchor}

Vector3 location identifier corresponding to the mimimum X and maximum Y
corner of the box perimeter.

#### Vector3 RoomKit.TopoBox.S\[get\]

[]{#AAAAAAAAGK .anchor}

Vector3 location identifier corresponding to the midpoint of the minimum
Y side of the box perimeter.

#### Vector3 RoomKit.TopoBox.SE\[get\]

[]{#AAAAAAAAGM .anchor}

Vector3 location identifier corresponding to the maximum X and minimum Y
corner of the box perimeter.

#### double RoomKit.TopoBox.SizeX\[get\]

[]{#AAAAAAAAGS .anchor}

X and Y dimensions of the [[TopoBox]{.underline}](#AAAAAAAAAH)
perimeter.

#### Vector3 RoomKit.TopoBox.SSE\[get\]

[]{#AAAAAAAAGL .anchor}

Vector3 location identifier corresponding to the midpoint between the SE
and S points of the box perimeter.

#### Vector3 RoomKit.TopoBox.SSW\[get\]

[]{#AAAAAAAAGJ .anchor}

Vector3 location identifier corresponding to the midpoint between the SW
and S points of the box perimeter.

#### Vector3 RoomKit.TopoBox.SW\[get\]

[]{#AAAAAAAAGI .anchor}

Vector3 location identifier corresponding to the mimimum X and Y corner
of the box perimeter.

#### Vector3 RoomKit.TopoBox.W\[get\]

[]{#AAAAAAAAGG .anchor}

Vector3 location identifier corresponding to the midpoint of the minimum
X side of the box perimeter.

#### Vector3 RoomKit.TopoBox.WNW\[get\]

[]{#AAAAAAAAGF .anchor}

Vector3 location identifier corresponding to the midpoint between the NW
and W points of the box perimeter.

#### Vector3 RoomKit.TopoBox.WSW\[get\]

[]{#AAAAAAAAGH .anchor}

Vector3 location identifier corresponding to the midpoint between the SW
and W points of the box perimeter.

#### The documentation for this class was generated from the following file:

-   RoomKit/TopoBox.cs

#### 

RoomKit.Tower Class Reference
-----------------------------

### Public Member Functions

-   bool [[AddServiceCore]{.underline}](#AAAAAAAAGU) (Polygon perimeter,
    int baseStory=0, double addHeight=0.0, Color color=null)

*Adds a new service Core to the
[[Tower]{.underline}](#public-member-functions-7).*

-   void [[MoveFromTo]{.underline}](#AAAAAAAAGV) (Vector3 from, Vector3
    to)

*Moves all Cores and Stories in the
[[Tower]{.underline}](#public-member-functions-7) along a 3D vector
calculated between the supplied Vector3 points.*

-   void [[Rotate]{.underline}](#AAAAAAAAGW) (Vector3 pivot, double
    angle)

*Rotates the [[Tower]{.underline}](#public-member-functions-7) Perimeter
and Stories in the horizontal plane around the supplied pivot point.*

-   bool [[Stack]{.underline}](#AAAAAAAAGX) (int floors=0, double
    storyHeight=0.0)

*Creates the [[Tower]{.underline}](#public-member-functions-7) by
stacking a series of [[Story]{.underline}](#AAAAAAAAAG) instances from
the [[Tower]{.underline}](#public-member-functions-7) Elevation.*

-   bool [[SetStoryHeight]{.underline}](#AAAAAAAAGY) (int story, double
    height, bool interiors=true)

*Sets the height of an index-specified
[[Story]{.underline}](#AAAAAAAAAG) and relocates Stories above to
accommodate the [[Story]{.underline}](#AAAAAAAAAG)\'s new height.*

### Public Attributes

-   List\< [[Story]{.underline}](#AAAAAAAAAG) \>
    [[Stories]{.underline}](#AAAAAAAAGZ) = null

*List of all Stories in the
[[Tower]{.underline}](#public-member-functions-7).*

### Properties

-   Color **Color** \[get, set\]

-   List\< [[Room]{.underline}](#AAAAAAAAAC) \>
    [[Cores]{.underline}](#AAAAAAAAHB) \[get\]

*List of all service Cores in the
[[Tower]{.underline}](#public-member-functions-7).*

-   double **Elevation** \[get, set\]

-   int **Floors** \[get, set\]

-   double [[Height]{.underline}](#AAAAAAAAHE) \[get\]

*Total height of all Stories in the
[[Tower]{.underline}](#public-member-functions-7).*

-   Polygon **Perimeter** \[get, set\]

-   List\< Floor \> [[Slabs]{.underline}](#AAAAAAAAHG) \[get\]

*List of all Slabs from every [[Story]{.underline}](#AAAAAAAAAG) in the
[[Tower]{.underline}](#public-member-functions-7).*

-   List\< Space \> [[Spaces]{.underline}](#AAAAAAAAHH) \[get\]

*List of all Spaces from every [[Story]{.underline}](#AAAAAAAAAG) in the
[[Tower]{.underline}](#public-member-functions-7).*

-   double **StoryHeight** \[get, set\]

### Member Function Documentation

#### bool RoomKit.Tower.AddServiceCore (Polygon *perimeter*, int *baseStory* = 0, double *addHeight* = 0.0, Color *color* = null)

[]{#AAAAAAAAGU .anchor}

Adds a new service Core to the
[[Tower]{.underline}](#public-member-functions-7).

##### Parameters:

  ------------- --------------------------------------------------------------------------------------------------------------------
  *perimeter*   Polygon perimeter defining the footprint of the service Core.
  *baseStory*   Index of the lowest [[Story]{.underline}](#AAAAAAAAAG) whose elevation will serve as the lowest level of the Core.
  *addHeight*   Additional height of the Core above the highest [[Story]{.underline}](#AAAAAAAAAG).
  *color*       Color of the Core when it it is accessed as a Space.
  ------------- --------------------------------------------------------------------------------------------------------------------

##### Returns:

True if the Core is successfully added.

#### void RoomKit.Tower.MoveFromTo (Vector3 *from*, Vector3 *to*)

[]{#AAAAAAAAGV .anchor}

Moves all Cores and Stories in the
[[Tower]{.underline}](#public-member-functions-7) along a 3D vector
calculated between the supplied Vector3 points.

##### Parameters:

  -------- -----------------------------------
  *from*   Vector3 base point of the move.
  *to*     Vector3 target point of the move.
  -------- -----------------------------------

##### Returns:

None.

#### void RoomKit.Tower.Rotate (Vector3 *pivot*, double *angle*)

[]{#AAAAAAAAGW .anchor}

Rotates the [[Tower]{.underline}](#public-member-functions-7) Perimeter
and Stories in the horizontal plane around the supplied pivot point.

##### Parameters:

  --------- ---------------------------------------------------------------------------------------------
  *pivot*   Vector3 point around which the [[Room]{.underline}](#AAAAAAAAAC) Perimeter will be rotated.
  *angle*   Angle in degrees to rotate the Perimeter.
  --------- ---------------------------------------------------------------------------------------------

##### Returns:

None.

#### bool RoomKit.Tower.SetStoryHeight (int *story*, double *height*, bool *interiors* = true)

[]{#AAAAAAAAGY .anchor}

Sets the height of an index-specified [[Story]{.underline}](#AAAAAAAAAG)
and relocates Stories above to accommodate the
[[Story]{.underline}](#AAAAAAAAAG)\'s new height.

##### Parameters:

  ------------- --------------------------------------------------------------------------------------------------------
  *story*       Index of the [[Story]{.underline}](#AAAAAAAAAG) to affect.
  *height*      Desired new height of the specified [[Story]{.underline}](#AAAAAAAAAG).
  *interiors*   If true also sets any Corridors and Rooms in the [[Story]{.underline}](#AAAAAAAAAG) to the new Height.
  ------------- --------------------------------------------------------------------------------------------------------

##### Returns:

True if the [[Tower]{.underline}](#public-member-functions-7) is
successfully stacked.

#### bool RoomKit.Tower.Stack (int *floors* = 0, double *storyHeight* = 0.0)

[]{#AAAAAAAAGX .anchor}

Creates the [[Tower]{.underline}](#public-member-functions-7) by
stacking a series of [[Story]{.underline}](#AAAAAAAAAG) instances from
the [[Tower]{.underline}](#public-member-functions-7) Elevation.

##### Parameters:

  --------------- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  *floors*        Desired quantity of stacked Stories to form the [[Tower]{.underline}](#public-member-functions-7). If greater than zero, overrides and resets the current Floors property.
  *storyHeight*   Desired typical [[Story]{.underline}](#AAAAAAAAAG) height of the [[Tower]{.underline}](#public-member-functions-7). If greater than zero, overrides and resets the current StoryHeight property.
  --------------- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

##### Returns:

True if the [[Tower]{.underline}](#public-member-functions-7) is
successfully stacked.

### Member Data Documentation

#### List\<[[Story]{.underline}](#AAAAAAAAAG)\> RoomKit.Tower.Stories = null

[]{#AAAAAAAAGZ .anchor}

List of all Stories in the
[[Tower]{.underline}](#public-member-functions-7).

### Property Documentation

#### List\<[[Room]{.underline}](#AAAAAAAAAC)\> RoomKit.Tower.Cores\[get\]

[]{#AAAAAAAAHB .anchor}

List of all service Cores in the
[[Tower]{.underline}](#public-member-functions-7).

#### double RoomKit.Tower.Height\[get\]

[]{#AAAAAAAAHE .anchor}

Total height of all Stories in the
[[Tower]{.underline}](#public-member-functions-7).

#### List\<Floor\> RoomKit.Tower.Slabs\[get\]

[]{#AAAAAAAAHG .anchor}

List of all Slabs from every [[Story]{.underline}](#AAAAAAAAAG) in the
[[Tower]{.underline}](#public-member-functions-7).

#### List\<Space\> RoomKit.Tower.Spaces\[get\]

[]{#AAAAAAAAHH .anchor}

List of all Spaces from every [[Story]{.underline}](#AAAAAAAAAG) in the
[[Tower]{.underline}](#public-member-functions-7).

#### The documentation for this class was generated from the following file:

-   RoomKit/Tower.cs

Index
=====

INDEX
