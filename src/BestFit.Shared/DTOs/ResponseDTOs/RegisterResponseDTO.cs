using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Shared.DTOs.ResponseDTOs
{
    public class RegisterResponseDTO
    {
        public IdentityResult identityResult { get; set; }
        public string Message { get; set; }
    }
}
