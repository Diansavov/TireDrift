using Microsoft.AspNetCore.Mvc;

namespace TireDrift
{
    public class UsersController : Controller
    {
        public UsersController()
        {

        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}