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
        public const string DIAGONAL_DIRECTION_EXCEPTION = "Value must be an orthoganl direction.";

        /// <summary>
        /// 
        /// </summary>
        public const string INVALID_POINT_EXCEPTION = "Point is outside expected area.";

        /// <summary>
        /// 
        /// </summary>
        public const string NONPOSITIVE_VALUE_EXCEPTION = "Value must be greater than zero.";

        /// <summary>
        /// 
        /// </summary>
        public const string PERIMETER_PLACEMENT_EXCEPTION = "Polygon placement inconsistent with current relationships.";

        /// <summary>
        /// 
        /// </summary>
        public const string PERIMETER_NULL_EXCEPTION = "Referenced perimeter value is null.";
    }
}
