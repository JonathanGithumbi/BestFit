using BestFit.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace BestFit.Shared.DTOs.ResponseDTOs
{
    public class ProfileAccountResponseDTO
    {
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? CellPhone { get; set; }
        //public string? Email { get; set; }

        public IEnumerable<CustomerMeasurementProfileResponseDTO>? CustomerMeasurementProfiles { get; set; }


        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Avatars { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        public int? SelectedAvatarId { get; set; }
        public CustomerMeasurementProfileResponseDTO? SelectedAvatar { get; set; }
        ////public List<CustomerMeasurementProfileResponseDTO> CustomerMeasurementProfiles { get; set; }
    }
}
