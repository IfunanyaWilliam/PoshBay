using Microsoft.AspNetCore.Mvc;
using PoshBay.Data.ViewModels;

namespace PoshBay.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult CheckOutSummary()
        {
            var checkout = new CheckOutViewModel();
            return View(checkout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOutSummary(string customerEmail, string stripeToken)
        {
            return View();
        }

        public IActionResult StripePayment()
        {
            return View();
        }
    }
}
