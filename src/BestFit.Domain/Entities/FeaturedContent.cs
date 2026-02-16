using BestFit.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFit.Web.Models
{
    public class FeaturedContent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }

        public Guid ImageId { get; set; }

        [ForeignKey("ImageId")]
        public FeaturedContentImage Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime RunFromDate { get; set; }
        public DateTime RunToDate { get; set; }
        public bool isActive { get; set; }                                         
    }
}