using System;
using System.Collections.Generic;
using System.Text;

namespace RoomKit
{
    /// <summary>
    /// Common exception messages.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DIAGONAL_DIRECTION_EXCEPTION = "You've supplied a non-orthogonal direction.";

        /// <summary>
        /// 
        /// </summary>
        public const string INVALID_POINT_EXCEPTION = "You've supplied a point outside the expected area.";

        /// <summary>
        /// 
        /// </summary>
        public const string POLYGON_SHAPE_EXCEPTION = "You've supplied one or more values that would result in an unexpected shape. Examine polygon relationships or requested dimensions.";
    }
}
