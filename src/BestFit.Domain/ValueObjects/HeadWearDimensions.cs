namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Hats, Helmets, Glasses
    public class HeadWearDimensions
    {
        /// <summary>
        /// The inner circumference of the hat band.
        /// Match logic: Must be >= Customer.Head.HeadCircumference
        /// </summary>
        public decimal? InnerCircumference { get; set; }

        /// <summary>
        /// Length of the brim. (Aesthetic data, but useful for filters).
        /// </summary>
        public decimal? BrimWidth { get; set; }

        /// <summary>
        /// Height of the crown from brim to top button.
        /// </summary>
        public decimal? CrownHeight { get; set; }
    }

    #endregion
}
