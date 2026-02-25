namespace BestFit.Shared.DTOs.ResponseDTOs
{
    public class CartResponseDTO
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }


        public string AppUserId { get; set; }
        public Guid ProductId { get; set; }
    }
}