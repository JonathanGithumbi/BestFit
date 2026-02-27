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
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequest,string returnUrl = "/")
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Invlaid Submission, please try again.";
                TempData["OpenRegistration"] = true;
                return Redirect(returnUrl);
            }

            //Consume API
            try
            {

                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.PostAsJsonAsync("https://localhost:7198/api/auth/Register", registerRequest);
                var responseDTO = await httpResponseMessage.Content.ReadFromJsonAsync<RegisterResponseDTO>();
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    TempData["OpenRegistration"] = true;
                    TempData["RegisterError"] = responseDTO?.Message ?? "Registration failed.";
                    return Redirect(returnUrl);
                }


                if(responseDTO.Succeeded)
                {
                    TempData["OpenLogin"] = true;
                    TempData["LoginInfo"] = "Registration successful. Please log in.";
                    return Redirect(returnUrl);
                    



                }
                else
                {
                    TempData["OpenRegistration"] = true;
                    TempData["RegisterError"] = responseDTO?.Message ?? "Registration failed.";
                    return Redirect(returnUrl);
                    
                }



            }
            catch
            {
                TempData["OpenRegister"] = true;
                TempData["RegisterError"] = "Bad request, please try again.";
                return Redirect(returnUrl);
            }


        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO,string returnUrl="/")
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Invlaid Submission, please try again.";
                TempData["OpenLogin"] = true;
                return Redirect(returnUrl);
            }

            //Consume API
            try
            {

                var client = httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7198/api/auth/Login", loginRequestDTO);
                var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["OpenLogin"] = true;
                    TempData["LoginError"] = loginResult?.Message ?? "Login failed.";
                    return Redirect(returnUrl);
                }


                if (loginResult.Succeeded)
                {

                    //  Create Claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loginResult.Name),
                        new Claim(ClaimTypes.Email, loginResult.Email),
                        new Claim("AccessToken", loginResult.jwtToken)
                    };
                    foreach (var role in loginResult.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    // 🔹 Sign In (creates authentication cookie)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["OpenLogin"] = true;
                    TempData["LoginError"] = "Incorrect username or password";
                    return Redirect(returnUrl);
                }

            }
            catch
            {
                TempData["OpenLogin"] = true;
                TempData["LoginError"] = "\"Bad Request, please try again.";
                return Redirect(returnUrl);
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutRequestDTO logoutRequestDTO,string returnUrl ="/")
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }
            try
            {
                var client = httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7198/api/Logout",logoutRequestDTO);

                if (response.IsSuccessStatusCode)
                {
                    await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    TempData["LogoutSucceeded"] = "You have been logged out successfully.";

                    return RedirectToAction("Index", "Home");
                }
                TempData["LogoutSucceeded"] = "Logout Failed";


                return RedirectToAction("Index", "Home");
            }
            catch
            {
                TempData["LogoutSucceeded"] = "Bad Request, please try again";

                return RedirectToAction("Index", "Home");
            }


            
        }
    }
}
