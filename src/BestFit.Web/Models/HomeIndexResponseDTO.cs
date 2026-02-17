using BestFit.Domain.Entities;

namespace BestFit.Web.Models
{
    public class HomeIndexResponseDTO
    {
        public FeaturedContentResponseDTO featuredContent { get; set; }
        public List<CategoryResponseDTO> categories { get; set; }
        public List<CategoryResponseDTO> sportCollectionCategories { get; set; }
        public List<CategoryResponseDTO> popularCategories { get; set; }
        public List<ProductResponseDTO> blackFridaySales { get; set; }
    }
}
