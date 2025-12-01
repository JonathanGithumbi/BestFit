namespace BestFit.Application.DTOs.ResponseDTOs
{
    public class ProductImageResponseDTO
    {
        public Guid Id { get; set; }
        public string ImageURL { get; set; }

        public Guid ProductId { get; set; }

    }
}