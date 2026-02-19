using BestFit.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BestFit.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public ShopController(ILogger<ShopController> logger,IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? Name = null,[FromQuery]string? filterOn=null, [FromQuery] string? filterQuery=null, string? sortBy = null,bool isAscending=true)
        {
            if(string.IsNullOrEmpty(Name) ==false)
            {
                filterOn = "Name";
                filterQuery = Name;

            }
            ShopIndexResponseDTO response = new ShopIndexResponseDTO();

            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync($"https://localhost:7198/api/Shop?filterOn={filterOn}&filterQuery={filterQuery}&sortBy={sortBy}&isAscending={isAscending}");
                httpResponseMessage.EnsureSuccessStatusCode();
                response = await httpResponseMessage.Content.ReadFromJsonAsync<ShopIndexResponseDTO>();
            }
            catch (Exception)
            {
                //Log
                throw;
            }
            return View(response);
        }


        [HttpGet]
        public async Task<IActionResult> ListView(string? Name = null, [FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            return View();
        }


        public IActionResult NewArrivals()
        {
            return View();
        }
        public IActionResult DiscountSales()
        {
            return View();
        }

        
    }
}
