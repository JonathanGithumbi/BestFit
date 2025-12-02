using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string CellPhone { get; set; }

        public ICollection<CustomerMeasurementProfile> CustomerMeasurementProfiles { get; set; } = new List<CustomerMeasurementProfile>();

        [NotMapped]
        public string Role { get; set; }
    }
}
