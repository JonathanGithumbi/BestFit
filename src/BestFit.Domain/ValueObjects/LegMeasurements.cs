namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Head & Neck

    // Owned Type: Upper Body

    // Owned Type: Arms & Hands

    // Owned Type: Lower Body (Pelvis)

    // Owned Type: Legs
    public class LegMeasurements
    {
        /// <summary>
        /// From the crotch seam down to the ankle/floor.
        /// Usage: Pant length.
        /// </summary>
        public decimal? Inseam { get; set; }

        /// <summary>
        /// From the waistband down to the floor (along the outside of the leg).
        /// Usage: Skirt length, formal trousers.
        /// </summary>
        public decimal? Outseam { get; set; }

        /// <summary>
        /// Circumference of the thigh at the widest point.
        /// Usage: Skinny jeans fit, athletic fit trousers.
        /// </summary>
        public decimal? ThighCircumference { get; set; }

        /// <summary>
        /// Circumference of the thickest part of the calf.
        /// Usage: Tall boots, skinny jeans.
        /// </summary>
        public decimal? CalfCircumference { get; set; }
    }
}
#endregion