// Owned Type: Head & Neck
namespace BestFit.Domain.ValueObjects
{
    public class HeadMeasurements
    {
        /// <summary>
        /// Circumference around the widest part of the head, above ears.
        /// Usage: Hats, Caps, Helmets.
        /// </summary>
        public decimal? HeadCircumference { get; set; }

        /// <summary>
        /// Circumference of the neck (at the Adam's apple).
        /// Usage: Dress shirt collars, chokers.
        /// </summary>
        public decimal? NeckCircumference { get; set; }
    }
}