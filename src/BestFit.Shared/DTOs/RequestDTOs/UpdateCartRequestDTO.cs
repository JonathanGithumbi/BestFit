namespace BestFit.Shared.DTOs.RequestDTOs
{
    public class UpdateCartRequestDTO
    {
        
        public int Count { get; set; }
        public double Price { get; set; }


        public string AppUserId { get; set; }
        public Guid ProductId { get; set; }
    }
}