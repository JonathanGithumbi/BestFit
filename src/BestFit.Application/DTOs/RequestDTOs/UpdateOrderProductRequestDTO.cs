namespace BestFit.Application.DTOs.RequestDTOs
{
    public class UpdateOrderProductRequestDTO
    {
        public DateTime OrderDate { get; set; }
        public double OrderPrice { get; set; }
        public string OrderStatus { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }

        public string AppUserId { get; set; }
    }
}