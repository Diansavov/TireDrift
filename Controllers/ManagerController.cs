using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TireDrift.Models;

namespace TireDrift.Controllers;

public class ManagerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
