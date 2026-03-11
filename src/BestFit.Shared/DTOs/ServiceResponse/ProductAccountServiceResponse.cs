using BestFit.Domain.Entities;
using BestFit.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFit.Shared.DTOs.ServiceResponse
{
    public class ProductAccountServiceResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? CellPhone { get; set; }
        //public string? Email { get; set; }

        public IEnumerable<CustomerMeasurementProfile>? CustomerMeasurementProfiles { get; set; }


        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Avatars { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        public int? SelectedAvatarId { get; set; }
        public CustomerMeasurementProfileResponseDTO? SelectedAvatar { get; set; }
        ////public List<CustomerMeasurementProfileResponseDTO> CustomerMeasurementProfiles { get; set; }
    }
}
