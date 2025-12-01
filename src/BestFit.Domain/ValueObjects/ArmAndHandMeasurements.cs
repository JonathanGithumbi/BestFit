namespace BestFit.Domain.ValueObjects
{


    #region Value Objects / Groupings

    // Owned Type: Head & Neck

    // Owned Type: Upper Body

    // Owned Type: Arms & Hands
    public class ArmAndHandMeasurements
    {
        /// <summary>
        /// From shoulder tip down to the wrist bone.
        /// Usage: Shirt sleeve length.
        /// </summary>
        public decimal? SleeveLength { get; set; }

        /// <summary>
        /// Circumference of the flexed bicep.
        /// Usage: Slim fit vs. regular fit shirts.
        /// </summary>
        public decimal? BicepCircumference { get; set; }

        /// <summary>
        /// Circumference of the wrist bone.
        /// Usage: Cuffs, watches, bracelets.
        /// </summary>
        public decimal? WristCircumference { get; set; }

        /// <summary>
        /// Circumference of the dominant hand at the knuckles (excluding thumb).
        /// Usage: Gloves.
        /// </summary>
        public decimal? HandCircumference { get; set; }

        /// <summary>
        /// Inner circumference of the ring finger.
        /// Usage: Rings.
        /// </summary>
        public decimal? RingFingerSize { get; set; }
    }
}
#endregion