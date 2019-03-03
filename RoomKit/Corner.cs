using System;
using System.Collections.Generic;
using System.Text;

namespace RoomKit
{
    /// <summary>
    /// A list of box corners as compass designations.
    /// NE = maximum X and Y corner.
    /// SE = maximum X and minimum Y corner.
    /// SW = minimum X and Y corner.
    /// NW = minimum X and maximum Y corner.
    /// </summary>
    public enum Corner { NE, SE, SW, NW };    
}
