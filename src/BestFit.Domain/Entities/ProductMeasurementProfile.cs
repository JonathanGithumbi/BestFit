using BestFit.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Domain.Entities
{
    /// <summary>
    /// Represents the actual physical dimensions of a specific Product SKU (e.g., The Small Red Shirt).
    /// These are usually measured "Flat Lay" (garment laid flat on a table).
    /// </summary>
    public class ProductMeasurementProfile
    {
        [Key, ForeignKey("Product")]
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

    public enum ElasticityLevel { None, Low, Medium, High }
    public enum FitStyle { Skinny, Slim, Regular, Relaxed, Oversized }

    // Owned Type: Hats, Helmets, Glasses
    public class HeadWearDimensions
    {
        /// <summary>
        /// The inner circumference of the hat band.
        /// Match logic: Must be >= Customer.Head.HeadCircumference
        /// </summary>
        public decimal? InnerCircumference { get; set; }

        /// <summary>
        /// Length of the brim. (Aesthetic data, but useful for filters).
        /// </summary>
        public decimal? BrimWidth { get; set; }

        /// <summary>
        /// Height of the crown from brim to top button.
        /// </summary>
        public decimal? CrownHeight { get; set; }
    }

    // Owned Type: Shirts, Jackets, Coats, Dresses
    public class TopWearDimensions
    {
        /// <summary>
        /// Measured from shoulder seam to shoulder seam.
        /// Match logic: Compare to Customer.Torso.ShoulderWidth
        /// </summary>
        public decimal? ShoulderWidth { get; set; }

        /// <summary>
        /// "Pit-to-Pit". Measured flat across the chest just below the armhole.
        /// Match logic: (Value * 2) must be > Customer.Torso.ChestCircumference + Ease
        /// </summary>
        public decimal? ChestWidthFlat { get; set; }

        /// <summary>
        /// Width at the narrowest part of the garment's waist.
        /// </summary>
        public decimal? WaistWidthFlat { get; set; }

        /// <summary>
        /// Width at the very bottom hem.
        /// Usage: Important for hips in long jackets/tunics.
        /// </summary>
        public decimal? HemWidthFlat { get; set; }

        /// <summary>
        /// From shoulder seam to cuff edge.
        /// Match logic: Compare to Customer.Arms.SleeveLength
        /// </summary>
        public decimal? SleeveLength { get; set; }

        /// <summary>
        /// From High Point Shoulder (HPS) to bottom hem.
        /// </summary>
        public decimal? TotalLength { get; set; }
    }

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

    // Owned Type: Shoes, Socks
    public class FootWearDimensions
    {
        /// <summary>
        /// The internal length of the shoe (space available for the foot).
        /// Match logic: Must be > Customer.Feet.FootLength + 1.5cm (approx)
        /// </summary>
        public decimal? InnerSoleLength { get; set; }

        /// <summary>
        /// The internal width at the widest point.
        /// </summary>
        public decimal? InnerSoleWidth { get; set; }

        public decimal? HeelHeight { get; set; }

        /// <summary>
        /// Height of the shaft (for boots).
        /// </summary>
        public decimal? BootShaftHeight { get; set; }

        /// <summary>
        /// Circumference of the boot opening.
        /// Match logic: Must be > Customer.Legs.CalfCircumference
        /// </summary>
        public decimal? BootOpeningCircumference { get; set; }
    }

    // Owned Type: Belts, Watches, Rings
    public class AccessoryDimensions
    {
        /// <summary>
        /// Total length of object (Belt, Tie, Scarf).
        /// </summary>
        public decimal? TotalLength { get; set; }

        /// <summary>
        /// For Belts/Watches: The distance to the first and last hole.
        /// </summary>
        public decimal? MinCircumference { get; set; }
        public decimal? MaxCircumference { get; set; }

        /// <summary>
        /// Inner diameter or circumference (for Rings).
        /// </summary>
        public decimal? RingInnerCircumference { get; set; }
    }

    #endregion
}