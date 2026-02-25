using BestFit.Shared.DTOs.RequestDTOs;
using BestFit.Shared.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IHttpClientFactory httpClientFactory;

        public AccountController( IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            if (!ModelState.IsValid)
                return View(registerRequestDTO);
            //Consume API
            try
            {

                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.PostAsJsonAsync("https://localhost:7198/api/Register", registerRequestDTO);
                httpResponseMessage.EnsureSuccessStatusCode();

                RegisterResponseDTO responseDTO = await httpResponseMessage.Content.ReadFromJsonAsync<RegisterResponseDTO>();

                if(responseDTO.identityResult.Succeeded)
                {
                    return RedirectToAction("Login", "Account", responseDTO.Message);

                }
                else
                {
                    return View(responseDTO.identityResult);
                }



            }
            catch
            {
                ModelState.AddModelError("", "Bad Request");
                return View(registerRequestDTO);
            }


        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (!ModelState.IsValid)
                return View(loginRequestDTO);
            //Consume API
            try
            {

                var client = httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7198/api/Login", loginRequestDTO);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Incorrect Email or Password");
                    return View(loginRequestDTO);
                }

                var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if(loginResult.signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(loginRequestDTO);
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
                ModelState.AddModelError("", "Bad Request");
                return View(loginRequestDTO);
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
