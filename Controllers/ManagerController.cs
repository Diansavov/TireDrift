using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TireDrift.Models;
using TireDrift.Services;

namespace TireDrift.Controllers;

public class ManagerController : Controller
{
    private readonly IUserService _userService;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Ensures readable strings
    };
    public ManagerController(IUserService userService)
    {
        _userService = userService;
    }
    [Authorize(Roles = "Manager, Technician, Logistics")]
    public IActionResult ManagerPanel()
    {

        return View();
    }
    [Authorize(Roles = "Manager")]
    public IActionResult Employees()
    {
        
        return View();  
    }
    [Authorize(Roles = "Manager")]
    public async Task<string> GetEmployees(string firstName)
    {
       List<object> users = await _userService.GetEmployeesAsync(firstName);
        string json = JsonSerializer.Serialize(users, _options);

        return json;       
    }
}
