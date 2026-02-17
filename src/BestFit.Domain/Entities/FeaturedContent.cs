using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFit.Domain.Entities
{
    public class FeaturedContent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? File { get; set; }

        public string ImageUrl { get; set; }

        
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime RunFromDate { get; set; }
        public DateTime RunToDate { get; set; }
        public bool isActive { get; set; }                                         
    }
}