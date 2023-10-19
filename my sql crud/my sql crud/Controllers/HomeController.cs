using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY_SQL_CRUD.Models;
using MY_SQL_CRUD.Models.ViewModel;
using System.Diagnostics;

namespace MY_SQL_CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,UserManager<IdentityUser> userManager
            ,SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Login Controller
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "The login is successful!";
                        return RedirectToAction("Dashboard", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("Index", model);
        }
        #endregion




        #region Register  Controller 
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName,
                };
                var userWithEmail = await _userManager.FindByEmailAsync(model.Email);
                var userWithUsername = await _userManager.FindByNameAsync(model.UserName);
                
                if (userWithEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(model);
                }
                else if (userWithUsername != null)
                {
                    ModelState.AddModelError("UserName", "Username already exists.");
                    return View(model);
                }
                //UserManager
                var isVaidData = await _userManager.CreateAsync(user, model.Password);
                if (isVaidData != null)
                {
                    TempData["SuccessMessage"] = "Register created successfully!";
                    return RedirectToAction("Index", "Home");
                }
                //All error
                foreach (var item in isVaidData.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
            return View(model);
        }
        #endregion

        #region Logout  Controller
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "Logout successfully!";
            return RedirectToAction("Index", "Home");
        }
        #endregion


        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}