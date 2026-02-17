using BestFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Web.Models
{
    public class FeaturedContentResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public string ImageUrl { get; set; }
    }
}
