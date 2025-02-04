using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TireDrift.Models;

namespace TireDrift.Controllers;

public class ManagerController : Controller
{
    public IActionResult ManagerPanel()
    {

        return View();
    }
    public IActionResult AddWorker()
    {
        
        return View();
    }
    public IActionResult EditWorker()
    {
        
        return View();
    }
    public IActionResult Users()
    {
        
        return View();
    }
}
