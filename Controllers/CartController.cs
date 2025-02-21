using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Services;
using TireDrift.Extensions;
using TireDrift.Extenstions;
using TireDrift.Models;
using TireDrift.Models.ViewModels;
using TireDrift.Services;

namespace TireDrift.Controllers;

public class CartController : Controller
{
    private readonly IOrdersService _ordersService;
    private readonly IUserService _userService;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public CartController(IOrdersService orderservice, IUserService userService)
    {
        _ordersService = orderservice;
        _userService = userService;
    }


    public IActionResult Cart()
    {
        return View();
    }
    public IActionResult Invoice()
    {
        InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
        User user = _userService.Get(User.Id());
        invoiceViewModel.ClientFirstName = user.FirstName;
        invoiceViewModel.ClientLastName = user.LastName;
        return View(invoiceViewModel);
    }

    public string GetCartJson()
    {
        Cart cart = GetCart();
        string json = JsonSerializer.Serialize(cart, _options);

        return json;
    }


    public async Task<IActionResult> AddTireToCart(string id, int quantity)
    {
        var cart = GetCart();

        Tire tire = await _ordersService.GetTireAsync(id);
        if (quantity <= 0)
        {
            tire.Quantity = 1;
        }
        else
        {
            tire.Quantity = quantity;
        }
        //Invalid Stock
        if (tire != null && tire.Stock - tire.Quantity >= 0)
        {
            cart.Tires.Add(tire);
            cart.TotalPrice += tire.Price * tire.Quantity;
        }

        SaveCart(cart);

        return RedirectToAction("Tires", "Tires");
    }
    public async Task<IActionResult> AddServiceToCart(string id, int quantity)
    {
        var cart = GetCart();

        Service service = await _ordersService.GetServiceAsync(id);
        if (quantity <= 0)
        {
            service.Quantity = 1;
        }
        else
        {
            service.Quantity = quantity;
        }

        if (service != null)
        {
            cart.Services.Add(service);
            cart.TotalPrice += service.Price * service.Quantity;
        }

        SaveCart(cart);

        return RedirectToAction("Services", "Tires");

    }
    public IActionResult RemoveFromCart(string id)
    {
        Cart cart = GetCart();
        Tire tireToRemove = cart.Tires.FirstOrDefault(tire => tire.Id == id);

        if (tireToRemove != null)
        {
            cart.Tires.Remove(tireToRemove);
            cart.TotalPrice -= tireToRemove.Price * tireToRemove.Quantity;
        }
        else
        {
            var serviceToRemove = cart.Services.FirstOrDefault(service => service.Id == id);
            if (serviceToRemove != null)
            {
                cart.Services.Remove(serviceToRemove);
                cart.TotalPrice -= serviceToRemove.Price * serviceToRemove.Quantity;
            }
        }
        SaveCart(cart);
        return RedirectToAction("Cart");
    }
    public async Task<IActionResult> FinishOrder()
    {
        var cart = GetCart();

        await _ordersService.FinishOrder(cart, User.Id());

        SaveCart(null);

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> FinishInvoice(InvoiceViewModel invoiceViewModel)
    {
        var cart = GetCart();
        string orderId = await _ordersService.FinishOrder(cart, User.Id());
        SaveCart(null);
        await _ordersService.FinishInvoice(invoiceViewModel, orderId, User.Id());

        return RedirectToAction("Index", "Home");
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
