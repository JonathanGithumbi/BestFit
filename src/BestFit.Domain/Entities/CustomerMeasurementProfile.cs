using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestFit.Domain.Entities
{
    /// <summary>
    /// Represents the comprehensive physical dimensions of a customer.
    /// Uses Value Objects to group measurements by body region.
    /// </summary>
    public class CustomerMeasurementProfile
    {
        [Key]
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
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

    public enum MeasurementUnit
    {
        Metric_CM,
        Imperial_Inches
    }

    #region Value Objects / Groupings

    // Owned Type: Head & Neck
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

    // Owned Type: Upper Body
    public class TorsoMeasurements
    {
        /// <summary>
        /// Measured across the back from the tip of one shoulder bone to the other.
        /// Usage: Jackets, structured shirts.
        /// </summary>
        public decimal? ShoulderWidth { get; set; }

        /// <summary>
        /// Fullest part of the chest/bust.
        /// Usage: Shirts, Jackets, Dresses.
        /// </summary>
        public decimal? ChestCircumference { get; set; }

        /// <summary>
        /// Specifically for women: measured directly under the bust.
        /// Usage: Bras, swimwear.
        /// </summary>
        public decimal? UnderBustCircumference { get; set; }

        /// <summary>
        /// From the base of the neck (nape) to the natural waistline.
        /// Usage: Determining "Tall" vs "Regular" fit for tops.
        /// </summary>
        public decimal? BackLength { get; set; }

        /// <summary>
        /// The narrowest part of the torso, usually above the belly button.
        /// Usage: High-waisted skirts, fitted shirts.
        /// </summary>
        public decimal? NaturalWaist { get; set; }
    }

    // Owned Type: Arms & Hands
    public class ArmAndHandMeasurements
    {
        /// <summary>
        /// From shoulder tip down to the wrist bone.
        /// Usage: Shirt sleeve length.
        /// </summary>
        public decimal? SleeveLength { get; set; }

        /// <summary>
        /// Circumference of the flexed bicep.
        /// Usage: Slim fit vs. regular fit shirts.
        /// </summary>
        public decimal? BicepCircumference { get; set; }

        /// <summary>
        /// Circumference of the wrist bone.
        /// Usage: Cuffs, watches, bracelets.
        /// </summary>
        public decimal? WristCircumference { get; set; }

        /// <summary>
        /// Circumference of the dominant hand at the knuckles (excluding thumb).
        /// Usage: Gloves.
        /// </summary>
        public decimal? HandCircumference { get; set; }

        /// <summary>
        /// Inner circumference of the ring finger.
        /// Usage: Rings.
        /// </summary>
        public decimal? RingFingerSize { get; set; }
    }

    // Owned Type: Lower Body (Pelvis)
    public class LowerBodyMeasurements
    {
        /// <summary>
        /// Where the person prefers to wear their pants (often lower than natural waist).
        /// Usage: Jeans, casual trousers.
        /// </summary>
        public decimal? TrouserWaist { get; set; }

        /// <summary>
        /// The fullest part of the hips/buttocks.
        /// Usage: Trousers, skirts, briefs.
        /// </summary>
        public decimal? HipCircumference { get; set; }

        /// <summary>
        /// Measurement from the crotch seam up to the waistband (front).
        /// Usage: High-rise vs Low-rise pants.
        /// </summary>
        public decimal? FrontRise { get; set; }
    }

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

    #endregion
}