using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountRepository accRepo,
                                 IEmailService emailService,
                                 IMapper mapper,
                                 UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _accRepo = accRepo;
            _emailService = emailService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["EmailNotFound"] = "Email address is not registered";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Sign in attempt failled");
            return View(model);
        }


        [HttpGet]
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
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null)
            {
                ModelState.AddModelError("Email", "Email is already assigned to a user. Try a different email.");
                return View(model);
            }

            //Assign Model properties to newUser
            var newUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsAdmin = model.IsAdmin
            };

            //Create new user with userManager
            var result = await _userManager.CreateAsync(newUser, model.Password);
            var ErrorList = new List<string>();

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error => ErrorList.Add(error.Description));
                ModelState.AddModelError("", String.Join(", ", ErrorList));
                return View(model);
            }

            //Get user email and password to compose email body
            var IsMailSent = await _emailService.SendEmail(model);
            if (IsMailSent)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult SendPassWordResetToken()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendPassWordResetToken(ResetPassWordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["EmailNotFound"] = "Email not found. Do you want to register?";
                return View(model);
            }

            //Generate reset token
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            //set the action method that will recieve the token from email link
            var resetLink = Url.Action("ResetPassword", "Account", new { token = token }, protocol: HttpContext.Request.Scheme);

            var sendEmail = await _emailService.SendResetToKen(model.Email, resetLink);

            if (sendEmail)
            {
                return RedirectToAction("PassWordResetTokenSent");
            }

            return View(model);
        }

        public IActionResult PassWordResetTokenSent()
        {
            return View();
        }


        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPassWordViewModel();
            model.Token = token;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassWordViewModel model)
        {
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            IdentityResult result = null;
            try
            {
                result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (result.Succeeded)
            {
                ViewBag.Message = "Password reset successful";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Message = "Error while resetting the password";
                return View(model);
            }

        }
    }
}
