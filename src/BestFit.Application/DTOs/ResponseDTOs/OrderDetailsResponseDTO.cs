namespace BestFit.Application.DTOs.ResponseDTOs
{
    public class OrderDetailsResponseDTO
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

        public Guid OrderProductId { get; set; }
        public Guid ProductId { get; set; }

    }
}