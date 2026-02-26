using BestFit.Shared.DTOs.RequestDTOs;
using BestFit.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Shared.DTOs
{
    public class NavbarComponentsDTO
    {
        public LoginRequestDTO? LoginRequest { get; set; }
        public RegisterRequestDTO? RegisterRequest { get; set; }
        public CartResponseDTO? CartResponse { get; set; }
        public SearchContentRequestDTO? SearchContentRequest {get;set;}

    }
}
