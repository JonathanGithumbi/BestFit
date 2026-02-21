using BestFit.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;


namespace BestFit.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public ProductController(ILogger<ProductController> logger,IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index([FromQuery] string productId)
        {
            ProductDetailsResponseDTO response = new ProductDetailsResponseDTO();
            try
            {
                var client = httpClientFactory.CreateClient();


                var httpResponseMessage = await client.GetAsync($"https://localhost:7198/api/Product/{productId}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response = await httpResponseMessage.Content.ReadFromJsonAsync<ProductDetailsResponseDTO>();


            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }
            return View(response);
        }
    }
}
