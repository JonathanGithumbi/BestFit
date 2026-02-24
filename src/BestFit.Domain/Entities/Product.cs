
using Microsoft.AspNetCore.Http;
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
        public string? ImageUrl { get; set; }
        
        [NotMapped]
        public IFormFile File { get; set; }
        
        public Guid? CategoryId {  get; set; }                                                                                                           
        public Category? Category { get; set; }

        public ICollection<ProductImage>? ProductImages { get; set; }
        
    }
}
