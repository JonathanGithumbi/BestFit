namespace BestFit.Application.DTOs.RequestDTOs
{
    public class AddOrderDetailsRequestDTO
    {
        public int Count { get; set; }
        public double Price { get; set; }

        public Guid OrderProductId { get; set; }
        public Guid ProductId { get; set; }
    }
}