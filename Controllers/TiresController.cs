using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Services;
using TireDrift.Extensions;
using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace TireDrift
{
    public class TiresController : Controller
    {
        private readonly ITiresService _tiresService;
        private readonly IServiceService _serviceService;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        public TiresController(ITiresService tiresService, IServiceService serviceService)
        {
            _tiresService = tiresService;
            _serviceService = serviceService;
        }
        public IActionResult Tires()
        {
            List<Tire> tires = _tiresService.SearchProducts(null, null);
            return View(tires);
        }
        public IActionResult Services()
        {
            List<Service> services = _serviceService.GetAll();
            return View(services);
        }
        [Authorize]
        public IActionResult TireHotel()
        {
            List<HotelTires> tires = _tiresService.GetUserHotelTires(User.Id());
            int totalTireQuantity = tires.Sum(x => x.TireQuanity);
            if (totalTireQuantity >= 25)
            {
                ViewData["Discount"] = -25;

            }
            else if (totalTireQuantity >= 5)
            {
                ViewData["Discount"] = -totalTireQuantity;
            }
            else if (totalTireQuantity < 5)
            {
                ViewData["Discount"] = 0;
            }
            return View(tires);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddTireToHotel()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTireToHotel(TireViewModel tireViewModel)
        {
            await _tiresService.AddUserHotelTire(tireViewModel, User.Id());
            return RedirectToAction("TireHotel");
        }
        public async Task<IActionResult> RemoveTireFromHotel(string id)
        {
            await _tiresService.RemoveUserHotelTire(id);
            return RedirectToAction("TireHotel");
        }
        [Authorize]
        public string GetUserHotelTiresCount()
        {
            int tiresCount = _tiresService.GetUserHotelTires(User.Id()).Sum(x => x.TireQuanity);
            string json = JsonSerializer.Serialize(tiresCount, _options);
            return json;
        }

        //Search
        [HttpPost]
        public IActionResult Tires(string name, string filter)
        {
            List<Tire> tires = _tiresService.SearchProducts(name, filter);
            ViewData["filter"] = filter;
            ViewData["search-param"] = name;
            return View(tires);
        }
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public IActionResult AddTire()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public async Task<IActionResult> AddTire(TireViewModel addTireViewModel)
        {
            if (ModelState.IsValid)
            {
                await _tiresService.AddTireAsync(addTireViewModel);
                return RedirectToAction("Tires");
            }
            return View(addTireViewModel);
        }
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public async Task<IActionResult> EditTire(string id)
        {
            Tire tire = await _tiresService.GetAsync(id);

            TireViewModel tireViewModel = new TireViewModel(tire);

            return View(tireViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public async Task<IActionResult> EditTire(TireViewModel editTireViewModel)
        {
            await _tiresService.EditAsync(editTireViewModel);
            return RedirectToAction("Tires");
        }
        public async Task<IActionResult> DeleteTire(string id)
        {
            await _tiresService.DeleteAsync(id);
            return RedirectToAction("Tires");
        }
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public async Task<IActionResult> EditService(string id)
        {
            Service service = await _serviceService.GetAsync(id);

            ServiceViewModel serviceViewModel = new ServiceViewModel(service);

            return View(serviceViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public async Task<IActionResult> EditService(ServiceViewModel editServiceViewModel)
        {
            await _serviceService.EditAsync(editServiceViewModel);
            return RedirectToAction("Services");
        }
        [Authorize(Roles = "Manager, Technician, Logistics")]
        public async Task<IActionResult> AllHotelTires()
        {
            return View();
        }
        [Authorize]
        public string GetAllHotelTires()
        {
            var hotelTires = _tiresService.GetAllHotelTiresDto();
            string json = JsonSerializer.Serialize(hotelTires, _options);
            return json;
        }
    }
}