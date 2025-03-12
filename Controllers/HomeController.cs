using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Services;
using TireDrift.Models;

namespace TireDrift.Controllers;

public class HomeController : Controller
{
    private readonly ITiresService _tiresService;

    public HomeController(ITiresService tiresService)
    {
        _tiresService = tiresService;
    }

    public IActionResult Index()
    {
        List<Tire> tires = _tiresService.GetAll();
        return View(tires);
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
