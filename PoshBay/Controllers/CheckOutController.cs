using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using PoshBay.Services;

namespace PoshBay.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICheckOutRepository _checkOutRepo;
        private readonly IPaymentService _payment;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _manager;

        public CheckOutController(ICheckOutRepository checkOutRepo,
                                  IPaymentService payment,
                                  IMapper mapper,
                                  UserManager<ApplicationUser> manager)
        {
            _checkOutRepo   = checkOutRepo;
            _payment        = payment;
            _mapper         = mapper;
            _manager = manager;
        }


        public async Task<IActionResult> CheckOutSummary(string shoppingCartId)
        {
            var shoppingCart = await _checkOutRepo.GetShoppingCartAsync(x => x.ShoppingCartId == shoppingCartId);
            if(shoppingCart != null)
            {
                var checkout        = new TransactionViewModel();
                checkout.Amount     = (int)shoppingCart?.CartItems?.Sum(x => x.TotalPrice);
                checkout.AppUser    = shoppingCart?.AppUser;
                checkout.Email      = shoppingCart?.AppUser?.Email; 
                return View(checkout);
            }
           return RedirectToAction("ViewCart", "Cart", new { appUserEmail = shoppingCart?.AppUser?.Email });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutSummary(TransactionViewModel model)
        {
            var user = await _manager.FindByEmailAsync(model.Email);
            var request  = await _payment.Request(model);
            var response = _payment.Response(request);
            if (response.Status) 
            {
                var transaction = new Transaction()
                {
                    Amount = model.Amount,
                    AppUser = user,
                    Email = model.Email,
                    TransactionRef = request.Reference,
                    PaymentStatus = "Pending",
                    AppUserId = model.AppUser?.Id
                }; 

                //Insert new transaction to database
                await _checkOutRepo.AddTransactionAsync(transaction);

                return Redirect(response.Data.AuthorizationUrl);
            }

            TempData["PaymentError"] = response.Message; 
            var updateTransaction = await _checkOutRepo.GetTransactionAsync(user => user.Email == model.Email);
            if (updateTransaction != null)
            {
                updateTransaction.PaymentStatus = "Failed";
                await _checkOutRepo.UpdateTransactionAsync(updateTransaction);  
            }
            return View(model);
        }

        public async Task<IActionResult> Verify(string reference)
        {
            var verify = await _payment.VerifyPayment(reference);
            if(verify.Data.Status == "success")
            {
                var transaction = await _checkOutRepo.GetTransactionAsync(r => r.TransactionRef == reference);
                if(transaction != null)
                {
                    transaction.PaymentStatus = "Completed"; 
                    await _checkOutRepo.UpdateTransactionAsync(transaction);
                    RedirectToAction();
                }
            }
            TempData["PaymentError"] = verify.Data.GatewayResponse;
            return RedirectToAction("Payments");  
        }

        public async Task<IActionResult> Payments(string email)
        {
            var transction = await _checkOutRepo.GetTransactionsAsync(e => e.Email == email);
            return View(transction);
        }

    }
}
