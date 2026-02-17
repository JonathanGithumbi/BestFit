using BestFit.Domain.Entities;

namespace BestFit.Web.Models
{
    public class HomeIndexResponseDTO
    {
        public FeaturedContentResponseDTO featuredContent { get; set; }
        public IEnumerable<CategoryResponseDTO> categories { get; set; }

    }
}
