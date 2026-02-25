namespace BestFit.Shared.DTOs.RequestDTOs
{
    public class UpdateOrderDetailsRequestDTO
    {
        public int Count { get; set; }
        public double Price { get; set; }

        public Guid OrderProductId { get; set; }
        public Guid ProductId { get; set; }
    }
}