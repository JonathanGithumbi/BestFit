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
        public  IActionResult Index()
        {
            //ShopIndexResponseDTO response = new ShopIndexResponseDTO();

            //try
            //{
            //    var client = httpClientFactory.CreateClient();
            //    var httpResponseMessage = await client.GetAsync("https://localhost:7198/api/Shop");
            //    httpResponseMessage.EnsureSuccessStatusCode();
            //    response = await httpResponseMessage.Content.ReadFromJsonAsync<ShopIndexResponseDTO>(); 
            //}
            //catch(Exception)
            //{
            //    //Log
            //    throw;
            //}
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
