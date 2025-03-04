using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TireDrift.Models;
using TireDrift.Models.ViewModels;
using TireDrift.Services;

namespace TireDrift.Controllers;

public class EmployeesController : Controller
{
    private readonly IUserService _userService;
    private readonly ISupplierService _supplierService;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
    };
    public EmployeesController(IUserService userService, ISupplierService supplierService)
    {
        _userService = userService;
        _supplierService = supplierService;
    }
    public IActionResult Suppliers()
    {

        return View();
    }
    public IActionResult AddSupplier()
    {

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddSupplier(SupplierViewModel supplierViewModel)
    {
        if (ModelState.IsValid)
        {

            await _supplierService.AddSupplierAsync(supplierViewModel);
            return RedirectToAction("Suppliers");
        }

        return View(supplierViewModel);
    }
    public async Task<IActionResult> EditSupplier(string id)
    {
        Supplier supplier = await _supplierService.GetAsync(id);
        SupplierViewModel supplierViewModel = new SupplierViewModel
        {
            Id = id,
            Name = supplier.Name,
            PhoneNumber = supplier.PhoneNumber
        };

        return View(supplierViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> EditSupplier(SupplierViewModel supplierViewModel)
    {
        if (ModelState.IsValid)
        {

            await _supplierService.EditAsync(supplierViewModel);
            return RedirectToAction("Suppliers");
        }
        return View(supplierViewModel);
    }
    public async Task<IActionResult> DeleteSupplier(string id)
    {
        await _supplierService.DeleteAsync(id);
        return RedirectToAction("Suppliers");
    }
    public string GetSuppliers(string name)
    {
        List<Supplier> users = _supplierService.GetSearched(name);
        string json = JsonSerializer.Serialize(users, _options);

        return json;
    }
}
