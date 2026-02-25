using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Shared.DTOs.RequestDTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get;set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string? Address { get; set; } = null;
        public string? PostalCode { get; set; } = null;
        public string? CellPhone { get; set; } = null;



        
       
    }
}
