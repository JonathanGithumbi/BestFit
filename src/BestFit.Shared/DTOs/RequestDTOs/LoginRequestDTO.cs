using System.ComponentModel.DataAnnotations;

namespace BestFit.Shared.DTOs.RequestDTOs
{
    public class LoginRequestDTO
    {
        [Required,EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}