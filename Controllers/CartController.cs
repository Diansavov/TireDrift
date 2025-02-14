using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Services;
using TireDrift.Extenstions;
using TireDrift.Models;
using TireDrift.Models.ViewModels;
using TireDrift.Services;

namespace TireDrift.Controllers;

public class CartController : Controller
{
    private readonly IOrdersService _ordersService;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public CartController(IOrdersService orderservice)
    {
        _ordersService = orderservice;
    }


    public IActionResult Cart()
    {
        return View();
    }
    public string GetCartJson()
    {
        Cart cart = GetCart();
        string json = JsonSerializer.Serialize(cart, _options);

        return json;
    }


    public async Task<IActionResult> AddTireToCart(string id)
    {
        var cart = GetCart();

        Tire tire = await _ordersService.GetTireAsync(id);

        if (tire != null)
        {
            cart.Tires.Add(tire);
            cart.TotalPrice += tire.Price;
        }

        SaveCart(cart);

        return RedirectToAction("Tires", "Tires");
    }
    public async Task<IActionResult> AddServiceToCart(string id)
    {
        var cart = GetCart();

        Service service = await _ordersService.GetServiceAsync(id);

        if (service != null)
        {
            cart.Services.Add(service);
            cart.TotalPrice += service.Price;
        }

        SaveCart(cart);

        return RedirectToAction("Services", "Tires");

    }

    private Cart GetCart()
    {
        Cart cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");

        if (cart == null)
        {
            cart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                Tires = new List<Tire>(),
                Services = new List<Service>(),
                TotalPrice = 0
            };
        }

        return cart;
    }
    private void SaveCart(Cart cart)
    {
        HttpContext.Session.SetObjectAsJson("Cart", cart);
    }
}
