using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Services;
using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace TireDrift
{
    public class TiresController : Controller
    {
        private readonly ITiresService _tiresService;
        private readonly IServiceService _serviceService;
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
        //Search
        [HttpPost]
        public IActionResult Tires(string name, string filter)
        {
            List<Tire> tires = _tiresService.SearchProducts(name, filter);
            ViewData["filter"] = filter;
            return View(tires);
        }
        public IActionResult AddTire()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTire(TireViewModel addTireViewModel)
        {
            if (ModelState.IsValid)
            {
                await _tiresService.AddTireAsync(addTireViewModel);
                return RedirectToAction("Tires");
            }
            return View(addTireViewModel);
        }
        public async Task<IActionResult> EditTire(string id)
        {
            Tire tire = await _tiresService.GetAsync(id);

            TireViewModel tireViewModel = new TireViewModel(tire);

            return View(tireViewModel);
        }
        [HttpPost]
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
    }
}