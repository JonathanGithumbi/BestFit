using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Entities
{
    public class OrderProduct
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderPrice { get; set; }
        public string OrderStatus { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }

        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public ApplicationUser AppUser { get; set; }
    }
}
