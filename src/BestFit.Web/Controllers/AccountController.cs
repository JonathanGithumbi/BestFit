using BestFit.Shared.DTOs.RequestDTOs;
using BestFit.Shared.DTOs.ResponseDTOs;
using BestFit.Shared.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BestFit.Web.Controllers
{
    public class AccountController : Controller
    {
        [TempData]
        public string RegisterSucceeded { get; set; }

        [TempData] 
        public string LoginSucceeded { get; set; }

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController( IHttpClientFactory httpClientFactory,IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IndexPageDTO indexResponseDTO)
        {
            if (!ModelState.IsValid)
                return View(indexResponseDTO);
            //Consume API
            try
            {

                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.PostAsJsonAsync("https://localhost:7198/api/auth/Register", indexResponseDTO.NavbarComponentsDTO.RegisterRequest);
                httpResponseMessage.EnsureSuccessStatusCode();

                var responseDTO = await httpResponseMessage.Content.ReadFromJsonAsync<RegisterResponseDTO>();

                if(responseDTO.Succeeded)
                {
                    RegisterSucceeded = "Registration successfull, please log in.";
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", responseDTO.Message);
                    return View(indexResponseDTO);
                }



            }
            catch
            {
                ModelState.AddModelError("", "Bad Request, please try again.");
                return View(indexResponseDTO);
            }


        }

        
        [HttpPost]
        public async Task<IActionResult> Login(IndexPageDTO indexResponseDTO)
        {
            if (!ModelState.IsValid)
                return View(indexResponseDTO);
            //Consume API
            try
            {

                var client = httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7198/api/auth/Login", indexResponseDTO.NavbarComponentsDTO.LoginRequest);

                if (!response.IsSuccessStatusCode)
                {
                    LoginSucceeded = "Incorrect username or password";
                    return RedirectToAction("Index","Home");
                }

                var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if(loginResult.Succeeded)
                {
                  

                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    LoginSucceeded = "Incorrect username or password";
                    return RedirectToAction("Index","Home");
                }
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name,loginResult.Name),
                //    new Claim(ClaimTypes.Email ,loginResult.Email),
                //    new Claim("AccessToken",loginResult.jwtToken)
                //};

                //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //var principal = new ClaimsPrincipal(identity);

                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                
            }
            catch
            {
                LoginSucceeded = "Bad Request, please try again.";
                return RedirectToAction("Index", "Home");
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutRequestDTO logoutRequestDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7198/api/Logout",logoutRequestDTO);

                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            
        }
    }
}
