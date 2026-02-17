using BestFit.Domain.Entities;

namespace BestFit.Application.DTOs.ResponseDTOs
{
    public class HomeIndexResponseDTO
    {
        public List<FeaturedContentResponse> featuredContent { get; set; }
        public IEnumerable<CategoryResponseDTO> categories { get; set; }
        public List<CategoryResponseDTO> sportCollectionCategories { get  ;set; }
        public List<CategoryResponseDTO> popularCategories { get  ;set; }
        public List<ProductResponseDTO> blackFridaySales { get  ;set; }
        public List<ProductResponseDTO> popularWomenProducts { get  ;set; }
        public List<CategoryResponseDTO> clothingCategories { get  ;set; }
    }
}
