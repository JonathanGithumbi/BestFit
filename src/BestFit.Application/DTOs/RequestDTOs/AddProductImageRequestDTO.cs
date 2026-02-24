using Microsoft.AspNetCore.Http;

namespace BestFit.Application.DTOs.RequestDTOs
{
    public class AddProductImageRequestDTO
    {

        

        public List<IFormFile> Files { get; set; }
        public Guid ProductId { get; set; }

    }
}