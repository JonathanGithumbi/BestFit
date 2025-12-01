using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.ValueObjects
{

    #region Value Objects / Groupings

    // Owned Type: Belts, Watches, Rings
    public class AccessoryDimensions
    {
        /// <summary>
        /// Total length of object (Belt, Tie, Scarf).
        /// </summary>
        public decimal? TotalLength { get; set; }

        /// <summary>
        /// For Belts/Watches: The distance to the first and last hole.
        /// </summary>
        public decimal? MinCircumference { get; set; }
        public decimal? MaxCircumference { get; set; }

        /// <summary>
        /// Inner diameter or circumference (for Rings).
        /// </summary>
        public decimal? RingInnerCircumference { get; set; }
    }

    #endregion
}
