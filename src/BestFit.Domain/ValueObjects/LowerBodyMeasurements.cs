namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Head & Neck

    // Owned Type: Upper Body

    // Owned Type: Arms & Hands

    // Owned Type: Lower Body (Pelvis)
    public class LowerBodyMeasurements
    {
        /// <summary>
        /// Where the person prefers to wear their pants (often lower than natural waist).
        /// Usage: Jeans, casual trousers.
        /// </summary>
        public decimal? TrouserWaist { get; set; }

        /// <summary>
        /// The fullest part of the hips/buttocks.
        /// Usage: Trousers, skirts, briefs.
        /// </summary>
        public decimal? HipCircumference { get; set; }

        /// <summary>
        /// Measurement from the crotch seam up to the waistband (front).
        /// Usage: High-rise vs Low-rise pants.
        /// </summary>
        public decimal? FrontRise { get; set; }
    }
}
#endregion