using BestFit.Shared.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Security.Claims;

namespace BestFit.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContext;

        public ProfileController(IHttpClientFactory httpClientFactory,IHttpContextAccessor httpContext)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpContext = httpContext;
        }

        public async Task<IActionResult> Profile()
        {
            
            var email = httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.Email).Value;
            var response = new ProfileAccountResponseDTO();
            try
            {
                var client = httpClientFactory.CreateClient();
               

                var httpResponseMessage = await client.GetAsync($"https://localhost:7198/api/Profile?email={email}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response = await httpResponseMessage.Content.ReadFromJsonAsync<ProfileAccountResponseDTO>();


                var profile = httpContext.HttpContext.Session.GetString("customerMeasurementProfileName");
                var profileId = httpContext.HttpContext.Session.GetString("customerMeasurementProfileId");
                if (!(profile != null && profileId !=null))
                {
                    response.Avatars.Add(new SelectListItem {Value=profileId,Text=profile});
                }
                foreach (var customerProfile in response.CustomerMeasurementProfiles)
                {
                    response.Avatars.Add(new SelectListItem { Value = $"{customerProfile.Id}", Text = $"{customerProfile.ProfileName}" });                    
                }
                //response.Avatars.Add(new SelectListItem { Value = "0", Text = "None" });


            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }
            return View(response);
        }

        [HttpPost]
        public IActionResult SetAvatar(int SelectedAvatarId)
        {
            //if(profileId == Guid.Empty)
            //{
            //    TempData["AvatarMessage"] = "All avatars unapplied";
            //    RedirectToAction("Profile");

            //}
            //gte the profile and save it to the session

            //try
            //{
            //    var client = httpClientFactory.CreateClient();

            //    client.GetAsync("http://")
            //}

            //httpContext.HttpContext.Session.SetString("customerMeasurementProfileName",);
            //httpContext.HttpContext.Session.SetString("customerMeasurementProfileId",profileId.ToString());
            //profileAccountResponseDTO.Avatars = _items;
            //if(profileAccountResponseDTO.SelectedAvatarId != 0)
            //{
            //    return View();

            //}
            return RedirectToAction("Profile");
        }

        public IActionResult ProfileNotifications()
        {
            return View();
        }
        public IActionResult ProfileOrders()
        {
            return View();
        }
        public IActionResult ProfilePayment()
        {
            return View();
        }
        public IActionResult ProfileResetPassword()
        {
            return View();
        }
        public IActionResult ProfileWishList()
        {
            return View();
        }
    }
}
