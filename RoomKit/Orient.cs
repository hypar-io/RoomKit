using System;
using System.Collections.Generic;
using System.Text;

namespace RoomKit
{
    /// <summary>
    /// A list of compass orientations used to designate locations on a 2D box.
    /// N, S, E, and W define middle points on each orthogonal side of the box.
    /// NE, NW, SE, and SW correspond to the corners of the box.
    /// Other compass points define points along the relevant side between the cardinal and corner points.
    /// C corresponds to the center of the box.
    /// </summary>
    public enum Orient { C, N, NNE, NE, ENE, E, ESE, SE, SSE, S, SSW, SW, WSW, W, WNW, NW, NNW };    
}
