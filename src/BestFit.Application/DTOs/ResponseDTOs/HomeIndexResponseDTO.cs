using BestFit.Domain.Entities;

namespace BestFit.Application.DTOs.ResponseDTOs
{
    public class HomeIndexResponseDTO
    {
        public FeaturedContentResponse featuredContent { get; set; }
        public IEnumerable<CategoryResponseDTO> categories { get; set; }
        public IList<CategoryResponseDTO> sportCollectionCategories { get  ;set; }
    }
}
