namespace BestFit.Domain.ValueObjects
{
    #region Value Objects / Groupings

    // Critical for the Algorithm: Does the item stretch?
    public class FabricProperties
    {
        /// <summary>
        /// None (0%), Mechanical (1-5%), High (Spandex/Lycra >5%)
        /// The algorithm will use this to allow a tighter product measurement to fit a larger body.
        /// </summary>
        public ElasticityLevel Elasticity { get; set; }

        /// <summary>
        /// Intended fit style. The algorithm uses this to decide how much "Ease" is required.
        /// </summary>
        public FitStyle DesignedFit { get; set; }
    }

    #endregion
}
