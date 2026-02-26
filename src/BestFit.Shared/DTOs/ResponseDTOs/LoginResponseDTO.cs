using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Shared.DTOs.ResponseDTOs
{
    public class LoginResponseDTO
    {
        public string jwtToken { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Succeeded { get; set; }
        public bool? IsLockedOut { get;set; }
        public bool? IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
    }
}
