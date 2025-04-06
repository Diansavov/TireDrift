using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using TireDrift.Models;
using TireDrift.Models.ViewModels;
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
    [Authorize(Roles = "Manager")]
    public IActionResult AddEmployee()
    {
        return View();  
    }
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> AddEmployee(EmployeeRegister employeeRegister)
    {
        if (ModelState.IsValid)
        {
            await _userService.AddEmployee(employeeRegister);
            return RedirectToAction("ManagerPanel");
        }
        return View(employeeRegister);  
    }
    [Authorize(Roles = "Manager")]
    public IActionResult EditEmployee(string id)
    {
        var user = _userService.Get(id);

        EmployeeRegister employeeRegister = new EmployeeRegister(){
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
        };
        return View(employeeRegister);  
    }
    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> EditEmployee(EmployeeRegister employeeRegister)
    {
        ModelState.Remove("Password");
        if (ModelState.IsValid)
        {
            await _userService.EditEmployee(employeeRegister);
            return RedirectToAction("ManagerPanel");
        }
        return View(employeeRegister);  
    }
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        await _userService.DeleteAsync(id);
        return RedirectToAction("ManagerPanel");  
    }
}
