using BestFit.Domain.Entities;
using BestFit.Domain.ValueObjects;

namespace BestFit.Application.DTOs.RequestDTOs
{
    public class UpdateProductMeasurementProfileRequestDTO
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Links to the specific Stock Keeping Unit (Size S, M, L each have their own profile).
        /// </summary>
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public MeasurementUnit UnitSystem { get; set; }

        // Metadata to help the algorithm know how forgiving the fit is
        public FabricProperties MaterialInfo { get; set; } = new();

        public HeadWearDimensions HeadWear { get; set; } = new();
        public TopWearDimensions Tops { get; set; } = new();
        public BottomWearDimensions Bottoms { get; set; } = new();
        public FootWearDimensions Shoes { get; set; } = new();
        public AccessoryDimensions Accessories { get; set; } = new();
    }
}