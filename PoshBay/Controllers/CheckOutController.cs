using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.ViewModels;

namespace PoshBay.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICheckOutRepository _checkOutRepo;

        public CheckOutController(ICheckOutRepository checkOutRepo)
        {
            _checkOutRepo = checkOutRepo;
        }
        public async Task<IActionResult> CheckOutSummary(string shoppingCartId)
        {
            var shoppingCart = await _checkOutRepo.GetShoppingCartAsync(x => x.ShoppingCartId == shoppingCartId);
            if(shoppingCart != null)
            {
                var checkout = new CheckOutViewModel();
                checkout.Amount = shoppingCart.CartItems.Sum(x => x.TotalPrice).ToString();
                return View(checkout);
            }
           return RedirectToAction("ViewCart", "Cart", new { appUserEmail = shoppingCart?.AppUser?.Email });
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
