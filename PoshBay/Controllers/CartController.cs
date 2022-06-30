using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;

namespace PoshBay.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _prodRepo;
        private readonly IAccountRepository _accountRepo;

        public CartController(ICartRepository cartRepo, IProductRepository prodRepo, IAccountRepository accountRepo)
        {
            _cartRepo = cartRepo;
            _prodRepo = prodRepo;
            _accountRepo = accountRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(string productId, string appUserEmail)
        {
            var cart = await _cartRepo.GetCartAsync(productId);

            if (cart != null)
            {
                cart.SelectedQuantity++;
                await _cartRepo.UpdateCartAsync(cart);
                return RedirectToAction("Index", "Home");
            }

            //Get the user with email with AccountRepo method 
            var user = await _accountRepo.GetAppUser(x => x.Email == appUserEmail);

            var shoppingCart = new ShoppingCart
            {
                AppUserId = user?.AppUserId
            };

            //Save ShoppingCart and grab the ShoppingCartId
            await _cartRepo.AddShoppingCartAsync(shoppingCart);

            //Create a new cart and shopping cart
            var prod = await _prodRepo.GetByIdAsync(productId);

            var newCart = new CartItem
            {
                ProductId = productId,
                SelectedQuantity = 1,
                Product = prod,
                TotalPrice = prod.Price,
                ShoppingCartId = shoppingCart.ShoppingCartId
            };

            var result = await _cartRepo.AddCartAsync(newCart);

            if (result)
            {
                return RedirectToAction("ViewCart");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> ViewCart(string shoppingCartId)
        {
            var shoppingCart = await _cartRepo.GetShoppingCartItemsAsync(shoppingCartId);
            return View(shoppingCart);
        }

        public IActionResult GetCartCount()
        {
            return RedirectToAction("Index");
        }
    }
}
