﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RabbitMQ.ExcelCreate.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string Email, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(Email);

            if (hasUser == null) 
            {
                return View();
            }

            var signInREsult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);

            if (!signInREsult.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}
