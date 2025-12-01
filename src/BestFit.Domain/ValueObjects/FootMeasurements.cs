namespace BestFit.Domain.ValueObjects
{


    #region Value Objects / Groupings

    // Owned Type: Head & Neck

    // Owned Type: Upper Body

    // Owned Type: Arms & Hands

    // Owned Type: Lower Body (Pelvis)

    // Owned Type: Legs

    // Owned Type: Feet
    public class FootMeasurements
    {
        /// <summary>
        /// Length of the foot from heel to longest toe.
        /// Usage: Shoe size.
        /// </summary>
        public decimal? FootLength { get; set; }

        /// <summary>
        /// Width of the foot at the widest part (ball of foot).
        /// Usage: Wide-fit shoes (E, EE sizing).
        /// </summary>
        public decimal? FootWidth { get; set; }
    }
}
#endregion