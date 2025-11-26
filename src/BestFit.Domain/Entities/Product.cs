using ClothingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Price { get; set; }
        public string? Picture { get; set; }

        public Guid CategoryId { get; set; }
        public Guid ProductMeasurementProfileId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        
        public ProductMeasurementProfile ProductMeasurementProfile { get; set; }
    }
}
