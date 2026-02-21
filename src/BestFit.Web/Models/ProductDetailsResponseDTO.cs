namespace BestFit.Web.Models
{
    public class ProductDetailsResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
