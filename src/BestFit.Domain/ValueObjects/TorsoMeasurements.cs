namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Head & Neck

    // Owned Type: Upper Body
    public class TorsoMeasurements
    {
        /// <summary>
        /// Measured across the back from the tip of one shoulder bone to the other.
        /// Usage: Jackets, structured shirts.
        /// </summary>
        public decimal? ShoulderWidth { get; set; }

        /// <summary>
        /// Fullest part of the chest/bust.
        /// Usage: Shirts, Jackets, Dresses.
        /// </summary>
        public decimal? ChestCircumference { get; set; }

        /// <summary>
        /// Specifically for women: measured directly under the bust.
        /// Usage: Bras, swimwear.
        /// </summary>
        public decimal? UnderBustCircumference { get; set; }

        /// <summary>
        /// From the base of the neck (nape) to the natural waistline.
        /// Usage: Determining "Tall" vs "Regular" fit for tops.
        /// </summary>
        public decimal? BackLength { get; set; }

        /// <summary>
        /// The narrowest part of the torso, usually above the belly button.
        /// Usage: High-waisted skirts, fitted shirts.
        /// </summary>
        public decimal? NaturalWaist { get; set; }
    }
}
#endregion