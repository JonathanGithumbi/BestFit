using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BestFit.Application.DTOs.RequestDTOs
{
    public class AddProductRequestDTO
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public double Price { get; set; }
        public IFormFile File { get; set; }

    }
}