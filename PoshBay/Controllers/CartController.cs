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
            //Get the signed in user with email 
            var user = await _accountRepo.GetAppUser(x => x.Email == appUserEmail);


            //Get the shopping cart associated with the user
            var shoppingCart = await _cartRepo.GetShoppingCartAsync(user.Id);
            if (shoppingCart != null)
            {
                //Get cart associated with this shoppingcart
                var cart = await _cartRepo.GetCartAsync(shoppingCart.ShoppingCartId);
                if (cart != null)
                {
                    cart.SelectedQuantity++;
                    cart.TotalPrice = cart.SelectedQuantity * cart.Product.Price;
                    await _cartRepo.UpdateCartAsync(cart);
                    ViewBag.CartCount = shoppingCart.CartItems.Count;
                    return RedirectToAction("Index", "Home");
                }

                //Create a new cart and shopping cart
                var newProd = await _prodRepo.GetByIdAsync(productId);

                var newCartItem = new CartItem
                {
                    SelectedQuantity = 1,
                    Product = newProd,
                    TotalPrice = newProd.Price,
                    ShoppingCartId = shoppingCart.ShoppingCartId
                };

                var create = await _cartRepo.AddCartAsync(newCartItem);
                return RedirectToAction("ViewCart", new { shoppingCartId = newCartItem.ShoppingCartId });
            }

            var newshoppingCart = new ShoppingCart
            {
                AppUserId = user?.Id
            };

            //Save ShoppingCart and grab the ShoppingCartId
            await _cartRepo.AddShoppingCartAsync(newshoppingCart);

            //Create a new cart and shopping cart
            var prod = await _prodRepo.GetByIdAsync(productId);

            var NewCartItem = new CartItem
            {
                SelectedQuantity = 1,
                Product = prod,
                TotalPrice = prod.Price,
                ShoppingCartId = shoppingCart.ShoppingCartId
            };

            var result = await _cartRepo.AddCartAsync(NewCartItem);

            if (result)
            {
                ViewBag.CartIemCount = shoppingCart.CartItems.Count;
                return RedirectToAction("ViewCart", new { shoppingCartId = NewCartItem.ShoppingCartId });
            }

            ViewBag.CartIemCount = shoppingCart.CartItems.Count;
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> ViewCart(string appUserEmail)
        {
            var user = await _accountRepo.GetAppUser(x => x.Email == appUserEmail);
            var shoppingCart = await _cartRepo.GetShoppingCartItemsAsync(user.Id);
            return View(shoppingCart);
        }
        
        public IActionResult GetCartCount()
        {
            return RedirectToAction("Index");
        }
    }
}
