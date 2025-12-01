using System.ComponentModel.DataAnnotations;
using BestFit.Domain.ValueObjects;
namespace BestFit.Application.DTOs.RequestDTOs.UpdateCustomerMeasurementProfileRequestDTO

{
    /// <summary>
    /// Represents the comprehensive physical dimensions of a customer.
    /// Uses Value Objects to group measurements by body region.
    /// </summary>
    public class UpdateCustomerMeasurementProfileRequestDTO
    {
        
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