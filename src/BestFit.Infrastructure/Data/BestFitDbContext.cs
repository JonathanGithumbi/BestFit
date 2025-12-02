using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Infrastructure.Data
{
    public class BestFitDbContext : IdentityDbContext<ApplicationUser>
    {
        public BestFitDbContext(DbContextOptions<BestFitDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductMeasurementProfile> ProductMeasurementProfiles { get; set; }
        public DbSet<CustomerMeasurementProfile> CustomerMeasurementProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Seeding roles
            var handlerRoleId = "3c7b2bba-99ab-4768-b124-4abb40a94daa";
            var shopperRoleId = "6afa6e31-910f-4963-94ac-d7a23c6d377d";
            var administratorRoleId = "38c6732b-f36b-4e3a-b8f6-26be2dce3973";
            var staffRoleId = "cfab018d-40e3-4f0c-af89-00e7cd9f9724";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = handlerRoleId,
                    ConcurrencyStamp = handlerRoleId,
                    Name ="Handler",
                    NormalizedName ="Handler".ToUpper()
                },
                new IdentityRole
                {
                    Id = shopperRoleId,
                    ConcurrencyStamp = shopperRoleId,
                    Name ="Shopper",
                    NormalizedName ="Shopper".ToUpper()
                },
                new IdentityRole
                {
                    Id = administratorRoleId,
                    ConcurrencyStamp = administratorRoleId,
                    Name ="Administrator",
                    NormalizedName ="Administrator".ToUpper()
                },
                new IdentityRole
                {
                    Id = staffRoleId,
                    ConcurrencyStamp = staffRoleId,
                    Name ="Staff",
                    NormalizedName ="Staff".ToUpper()
                },
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

             modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductMeasurementProfile)           // Product has one profile
            .WithOne(pmp => pmp.Product)                        // Profile has one product
            .HasForeignKey<ProductMeasurementProfile>(pmp => pmp.Id) // PK of profile is FK to Product
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductMeasurementProfile>(entity =>
            {
                entity.HasKey(p => p.Id);

                // 🔹 Owned: FabricProperties
                entity.OwnsOne(p => p.MaterialInfo, owned =>
                {
                    owned.Property(f => f.Elasticity).HasColumnName("Fabric_Elasticity");
                    owned.Property(f => f.DesignedFit).HasColumnName("Fabric_DesignedFit");
                });

                // 🔹 Owned: HeadWearDimensions
                entity.OwnsOne(p => p.HeadWear, owned =>
                {
                    owned.Property(x => x.InnerCircumference).HasColumnName("Head_InnerCircumference");
                    owned.Property(x => x.BrimWidth).HasColumnName("Head_BrimWidth");
                    owned.Property(x => x.CrownHeight).HasColumnName("Head_CrownHeight");
                });

                // 🔹 Owned: TopWearDimensions
                entity.OwnsOne(p => p.Tops, owned =>
                {
                    owned.Property(x => x.ShoulderWidth).HasColumnName("Top_ShoulderWidth");
                    owned.Property(x => x.ChestWidthFlat).HasColumnName("Top_ChestWidthFlat");
                    owned.Property(x => x.WaistWidthFlat).HasColumnName("Top_WaistWidthFlat");
                    owned.Property(x => x.HemWidthFlat).HasColumnName("Top_HemWidthFlat");
                    owned.Property(x => x.SleeveLength).HasColumnName("Top_SleeveLength");
                    owned.Property(x => x.TotalLength).HasColumnName("Top_TotalLength");
                });

                // 🔹 Owned: BottomWearDimensions
                entity.OwnsOne(p => p.Bottoms, owned =>
                {
                    owned.Property(x => x.WaistWidthFlat).HasColumnName("Bottom_WaistWidthFlat");
                    owned.Property(x => x.HipWidthFlat).HasColumnName("Bottom_HipWidthFlat");
                    owned.Property(x => x.ThighWidthFlat).HasColumnName("Bottom_ThighWidthFlat");
                    owned.Property(x => x.FrontRise).HasColumnName("Bottom_FrontRise");
                    owned.Property(x => x.Inseam).HasColumnName("Bottom_Inseam");
                    owned.Property(x => x.LegOpeningWidth).HasColumnName("Bottom_LegOpeningWidth");
                });

                // 🔹 Owned: FootWearDimensions
                entity.OwnsOne(p => p.Shoes, owned =>
                {
                    owned.Property(x => x.InnerSoleLength).HasColumnName("Shoe_InnerSoleLength");
                    owned.Property(x => x.InnerSoleWidth).HasColumnName("Shoe_InnerSoleWidth");
                    owned.Property(x => x.HeelHeight).HasColumnName("Shoe_HeelHeight");
                    owned.Property(x => x.BootShaftHeight).HasColumnName("Shoe_BootShaftHeight");
                    owned.Property(x => x.BootOpeningCircumference).HasColumnName("Shoe_BootOpeningCircumference");
                });

                // 🔹 Owned: AccessoryDimensions
                entity.OwnsOne(p => p.Accessories, owned =>
                {
                    owned.Property(x => x.TotalLength).HasColumnName("Acc_TotalLength");
                    owned.Property(x => x.MinCircumference).HasColumnName("Acc_MinCircumference");
                    owned.Property(x => x.MaxCircumference).HasColumnName("Acc_MaxCircumference");
                    owned.Property(x => x.RingInnerCircumference).HasColumnName("Acc_RingInnerCircumference");
                });
            });

            var profile = modelBuilder.Entity<CustomerMeasurementProfile>();


            profile.HasOne(c => c.Customer)
           .WithMany(u => u.CustomerMeasurementProfiles)
           .HasForeignKey(c => c.CustomerId)
           .OnDelete(DeleteBehavior.Cascade);
            // HEAD
            profile.OwnsOne(p => p.Head, h =>
            {
                h.Property(x => x.HeadCircumference).HasColumnName("Head_HeadCircumference");
                h.Property(x => x.NeckCircumference).HasColumnName("Head_NeckCircumference");
            });

            // TORSO
            profile.OwnsOne(p => p.Torso, t =>
            {
                t.Property(x => x.ShoulderWidth).HasColumnName("Torso_ShoulderWidth");
                t.Property(x => x.ChestCircumference).HasColumnName("Torso_ChestCircumference");
                t.Property(x => x.UnderBustCircumference).HasColumnName("Torso_UnderBustCircumference");
                t.Property(x => x.BackLength).HasColumnName("Torso_BackLength");
                t.Property(x => x.NaturalWaist).HasColumnName("Torso_NaturalWaist");
            });

            // ARMS / HANDS
            profile.OwnsOne(p => p.Arms, a =>
            {
                a.Property(x => x.SleeveLength).HasColumnName("Arms_SleeveLength");
                a.Property(x => x.BicepCircumference).HasColumnName("Arms_BicepCircumference");
                a.Property(x => x.WristCircumference).HasColumnName("Arms_WristCircumference");
                a.Property(x => x.HandCircumference).HasColumnName("Arms_HandCircumference");
                a.Property(x => x.RingFingerSize).HasColumnName("Arms_RingFingerSize");
            });

            // LOWER BODY
            profile.OwnsOne(p => p.HipsAndWaist, h =>
            {
                h.Property(x => x.TrouserWaist).HasColumnName("Lower_TrouserWaist");
                h.Property(x => x.HipCircumference).HasColumnName("Lower_HipCircumference");
                h.Property(x => x.FrontRise).HasColumnName("Lower_FrontRise");
            });

            // LEGS
            profile.OwnsOne(p => p.Legs, l =>
            {
                l.Property(x => x.Inseam).HasColumnName("Legs_Inseam");
                l.Property(x => x.Outseam).HasColumnName("Legs_Outseam");
                l.Property(x => x.ThighCircumference).HasColumnName("Legs_ThighCircumference");
                l.Property(x => x.CalfCircumference).HasColumnName("Legs_CalfCircumference");
            });

            // FEET
            profile.OwnsOne(p => p.Feet, f =>
            {
                f.Property(x => x.FootLength).HasColumnName("Feet_FootLength");
                f.Property(x => x.FootWidth).HasColumnName("Feet_FootWidth");
            });

            // Optional: Add index on Customer + Profile name
            profile.HasIndex(x => new { x.CustomerId, x.ProfileName }).IsUnique(false);
        }
    }
    }





        

 

