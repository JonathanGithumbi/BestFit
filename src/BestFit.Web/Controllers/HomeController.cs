using BestFit.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

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
            List<CategoryResponseDTO> response = new List<CategoryResponseDTO>();  
            try
            {
                var client = httpClientFactory.CreateClient();

                
                var httpResponseMessage = await client.GetAsync( "https://localhost:7198/api/Categories");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange (await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<CategoryResponseDTO>>());

                
            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }
            return View(response);
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
