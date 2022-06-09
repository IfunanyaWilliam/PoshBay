using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using PoshBay.Services;

namespace PoshBay.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accRepo;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accRepo,
                                 IEmailService emailService,
                                 IMapper mapper)
        {
            _accRepo = accRepo;
            _emailService = emailService;
            _mapper = mapper;
        }
        
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TempData["Message"] = "logged in successfully.";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Check if email is already in use
            var email = model.Email;
            if (await _accRepo.EmailExisAsync(model.Email))
            {
                ModelState.AddModelError("Email", "Email is already assigned to a user. Try a different email.");
                return View(model);
            }


            //Get user email and password to compose email body
            var emailSuccessfullySent = await _emailService.SendEmail(model);
            if (emailSuccessfullySent)
            {
                var appUser = _mapper.Map<ApplicationUser>(model);
                _accRepo.Add(appUser);
                TempData["Success"] = "Registeration Successful.";
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Registration Unsuccessful. Try again.";
            return View(model);
        }
    }
} 
