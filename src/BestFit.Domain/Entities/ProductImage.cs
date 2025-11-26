using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Entities
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageURL { get; set; }
        
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
