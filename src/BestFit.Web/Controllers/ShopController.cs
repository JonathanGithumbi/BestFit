using Microsoft.AspNetCore.Mvc;

namespace BestFit.Web.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
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
