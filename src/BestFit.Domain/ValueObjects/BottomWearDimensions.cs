namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Owned Type: Trousers, Skirts, Shorts, Underwear
    public class BottomWearDimensions
    {
        /// <summary>
        /// Width of the waistband when laid flat.
        /// Match logic: (Value * 2) ~= Customer.HipsAndWaist.TrouserWaist
        /// </summary>
        public decimal? WaistWidthFlat { get; set; }

        /// <summary>
        /// Width at the widest point of the hips (usually 7-9 inches below waist).
        /// Match logic: (Value * 2) must be > Customer.HipsAndWaist.HipCircumference
        /// </summary>
        public decimal? HipWidthFlat { get; set; }

        /// <summary>
        /// Width of the thigh measuring 1 inch below crotch.
        /// Match logic: (Value * 2) > Customer.Legs.ThighCircumference
        /// </summary>
        public decimal? ThighWidthFlat { get; set; }

        /// <summary>
        /// From crotch seam to top of waistband.
        /// Usage: Determines if High-Rise or Low-Rise.
        /// </summary>
        public decimal? FrontRise { get; set; }

        /// <summary>
        /// From crotch seam to hem.
        /// Match logic: Compare to Customer.Legs.Inseam
        /// </summary>
        public decimal? Inseam { get; set; }

        /// <summary>
        /// Width of the leg opening at the hem.
        /// Usage: Determines "Bootcut" vs "Tapered".
        /// </summary>
        public decimal? LegOpeningWidth { get; set; }
    }

    #endregion
}
