using BestFit.Domain.ValueObjects;

namespace BestFit.Shared.DTOs.ResponseDTOs
{
    public class CustomerMeasurementProfileResponseDTO
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        //public ApplicationUser Customer { get; set; }
        public string ProfileName { get; set; } // e.g., "John's Winter Bulk" or "Sarah"

        /// <summary>
        /// Defines if the values stored are in Centimeters or Inches.
        /// </summary>
        public MeasurementUnit UnitSystem { get; set; }

        public HeadMeasurements Head { get; set; } = new();
        public TorsoMeasurements Torso { get; set; } = new();
        public ArmAndHandMeasurements Arms { get; set; } = new();
        public LowerBodyMeasurements HipsAndWaist { get; set; } = new();
        public LegMeasurements Legs { get; set; } = new();
        public FootMeasurements Feet { get; set; } = new();

        public DateTime LastUpdated { get; set; }
    }
    

    
}