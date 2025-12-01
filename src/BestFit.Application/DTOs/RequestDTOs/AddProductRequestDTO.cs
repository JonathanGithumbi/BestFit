namespace BestFit.Application.DTOs.RequestDTOs
{
    public class AddProductRequestDTO
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Price { get; set; }
        public string? Picture { get; set; }

        public Guid CategoryId { get; set; }
        public Guid ProductMeasurementProfileId { get; set; }
    }
}