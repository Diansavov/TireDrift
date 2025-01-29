using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TireDrift.Models.ViewModels;
using TireDrift.Services;

namespace TireDrift
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginViewModel logInRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LogIn(logInRequest);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(logInRequest);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel registerRequest)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _userService.Register(registerRequest);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                TempData["error"] = "Потребителското име вече се използва!";
            }
            return View(registerRequest);
        }
        public IActionResult LogOut()
        {
            _userService.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}