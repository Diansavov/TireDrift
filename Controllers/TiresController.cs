using Microsoft.AspNetCore.Mvc;

namespace TireDrift
{
    public class TiresController : Controller
    {
        public TiresController()
        {

        }
        public IActionResult Tires()
        {
            return View();
        }
    }
}