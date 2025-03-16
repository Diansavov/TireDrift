using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using TireDrift.Extensions;
using TireDrift.Models;
using TireDrift.Services;

namespace TireDrift.Controllers;
[Authorize]
public class OrderController : Controller
{
    private readonly IOrdersService _ordersService;
    private readonly IUserService _userService;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public OrderController(IOrdersService orderservice, IUserService userService)
    {
        _ordersService = orderservice;
        _userService = userService;
    }


    public IActionResult Orders()
    {
        return View();
    }
    public IActionResult Invoices()
    {
        return View();
    }

    public async Task<string> GetOrdersJson()
    {
        List<UserOrderDto> orders = _ordersService.GetUserOrder(User.Id());
        string json = JsonSerializer.Serialize(orders, _options);

        return json;
    }
    public async Task<string> GetInvoicesJson()
    {
        List<UserInvoiceDto> invoices = _ordersService.GetUserInvoices(User.Id());
        string json = JsonSerializer.Serialize(invoices, _options);

        return json;
    }

}