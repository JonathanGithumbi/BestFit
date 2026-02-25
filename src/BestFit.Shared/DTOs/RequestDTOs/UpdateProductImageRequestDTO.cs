namespace BestFit.Shared.DTOs.RequestDTOs
{
    public class UpdateProductImageRequestDTO
    {
        public Guid Id { get; set; }
        public string ImageURL { get; set; }

        public Guid ProductId { get; set; }

    }
}