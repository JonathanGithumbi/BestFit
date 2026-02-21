using Microsoft.AspNetCore.Mvc;

namespace BestFit.Web.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult CheckoutLogin()
        {
            return View();
        }
        public IActionResult CheckoutDelivery()
        {
            return View();
        }
        public IActionResult CheckoutPayment()
        {
            return View();
        }
        public IActionResult CheckoutReceipt()
        {
            return View();
        }
    }
}
