using BestFit.Domain.Entities;

namespace BestFit.Application.DTOs.ResponseDTOs
{
    public class ProductResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        
        public CategoryResponseDTO Category { get; set; }
        public List<ProductImageResponseDTO> ProductImages { get; set; }

    }
}