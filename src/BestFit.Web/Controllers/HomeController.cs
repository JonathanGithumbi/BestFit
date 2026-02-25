using BestFit.Shared.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using BestFit.Shared.DTOs;
namespace BestFit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfigurationManager configurationManager;

        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
            
        }

        public async Task<IActionResult> Index()
        {
            HomeIndexResponseDTO response  = new HomeIndexResponseDTO(); 
            try
            {
                var client = httpClientFactory.CreateClient();

                
                var httpResponseMessage = await client.GetAsync( "https://localhost:7198/api/Home");

                httpResponseMessage.EnsureSuccessStatusCode();

                response = await httpResponseMessage.Content.ReadFromJsonAsync<HomeIndexResponseDTO>();

                
            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }
            return View(response);
           
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
