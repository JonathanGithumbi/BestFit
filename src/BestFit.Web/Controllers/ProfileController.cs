using Microsoft.AspNetCore.Mvc;

namespace BestFit.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ProfileNotifications()
        {
            return View();
        }
        public IActionResult ProfileOrders()
        {
            return View();
        }
        public IActionResult ProfilePayment()
        {
            return View();
        }
        public IActionResult ProfileResetPassword()
        {
            return View();
        }
        public IActionResult ProfileWishList()
        {
            return View();
        }
    }
}
