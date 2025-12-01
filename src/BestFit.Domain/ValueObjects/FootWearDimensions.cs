namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Shoes, Socks
    public class FootWearDimensions
    {
        /// <summary>
        /// The internal length of the shoe (space available for the foot).
        /// Match logic: Must be > Customer.Feet.FootLength + 1.5cm (approx)
        /// </summary>
        public decimal? InnerSoleLength { get; set; }

        /// <summary>
        /// The internal width at the widest point.
        /// </summary>
        public decimal? InnerSoleWidth { get; set; }

        public decimal? HeelHeight { get; set; }

        /// <summary>
        /// Height of the shaft (for boots).
        /// </summary>
        public decimal? BootShaftHeight { get; set; }

        /// <summary>
        /// Circumference of the boot opening.
        /// Match logic: Must be > Customer.Legs.CalfCircumference
        /// </summary>
        public decimal? BootOpeningCircumference { get; set; }
    }

    #endregion
}
