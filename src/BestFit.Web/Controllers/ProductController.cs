using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using BestFit.Shared.DTOs.RequestDTOs;
using BestFit.Shared.DTOs.ResponseDTOs;

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

        public async Task<IActionResult> Index([FromRoute] string id)
        {
            ProductResponseDTO response = new ProductResponseDTO();
            try
            {
                var client = httpClientFactory.CreateClient();


                var httpResponseMessage = await client.GetAsync($"https://localhost:7198/api/Product/{id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                response = await httpResponseMessage.Content.ReadFromJsonAsync<ProductResponseDTO>();


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
