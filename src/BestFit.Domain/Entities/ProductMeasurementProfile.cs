using BestFit.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFit.Domain.Entities
{
    /// <summary>
    /// Represents the actual physical dimensions of a specific Product SKU (e.g., The Small Red Shirt).
    /// These are usually measured "Flat Lay" (garment laid flat on a table).
    /// </summary>
    public class ProductMeasurementProfile
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Links to the specific Stock Keeping Unit (Size S, M, L each have their own profile).
        /// </summary>

        public MeasurementUnit? UnitSystem { get; set; }

        // Metadata to help the algorithm know how forgiving the fit is
        public FabricProperties? MaterialInfo { get; set; } = new();

        public HeadWearDimensions? HeadWear { get; set; } = new();
        public TopWearDimensions? Tops { get; set; } = new();
        public BottomWearDimensions? Bottoms { get; set; } = new();
        public FootWearDimensions? Shoes { get; set; } = new();
        public AccessoryDimensions? Accessories { get; set; } = new();

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }

   
}