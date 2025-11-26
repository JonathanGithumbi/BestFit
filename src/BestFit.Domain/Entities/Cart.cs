using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Entities
{
    public class Cart
    {
        public Cart()
        {
            ///Setting the count propertty to 1 automatically so that
            ///when initializing a cart, the quantity of the added product
            ///starts at 1 by default
            ///
            Count = 1;
        }

        [Key]
        public Guid Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }


        public string AppUserId { get; set; }
        public Guid ProductId { get; set; }

        [ForeignKey("AppUserId")]
        public ApplicationUser AppUser { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
