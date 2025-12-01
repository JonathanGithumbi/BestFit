namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Shirts, Jackets, Coats, Dresses
    public class TopWearDimensions
    {
        /// <summary>
        /// Measured from shoulder seam to shoulder seam.
        /// Match logic: Compare to Customer.Torso.ShoulderWidth
        /// </summary>
        public decimal? ShoulderWidth { get; set; }

        /// <summary>
        /// "Pit-to-Pit". Measured flat across the chest just below the armhole.
        /// Match logic: (Value * 2) must be > Customer.Torso.ChestCircumference + Ease
        /// </summary>
        public decimal? ChestWidthFlat { get; set; }

        /// <summary>
        /// Width at the narrowest part of the garment's waist.
        /// </summary>
        public decimal? WaistWidthFlat { get; set; }

        /// <summary>
        /// Width at the very bottom hem.
        /// Usage: Important for hips in long jackets/tunics.
        /// </summary>
        public decimal? HemWidthFlat { get; set; }

        /// <summary>
        /// From shoulder seam to cuff edge.
        /// Match logic: Compare to Customer.Arms.SleeveLength
        /// </summary>
        public decimal? SleeveLength { get; set; }

        /// <summary>
        /// From High Point Shoulder (HPS) to bottom hem.
        /// </summary>
        public decimal? TotalLength { get; set; }
    }

    #endregion
}
