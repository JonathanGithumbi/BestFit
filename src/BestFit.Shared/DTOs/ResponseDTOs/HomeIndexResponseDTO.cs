using BestFit.Domain.Entities;

namespace BestFit.Shared.DTOs.ResponseDTOs
{
    public class HomeIndexResponseDTO
    {
        public List<FeaturedContentResponseDTO> featuredContent { get; set; }
        public List<CategoryResponseDTO> categories { get; set; }
        public List<CategoryResponseDTO> sportCollectionCategories { get; set; }
        public List<CategoryResponseDTO> popularCategories { get; set; }
        public List<ProductResponseDTO> blackFridaySales { get; set; }
        public List<ProductResponseDTO> popularWomenProducts { get; set; }
        public List<CategoryResponseDTO> clothingCategories { get; set; }

    }
}
