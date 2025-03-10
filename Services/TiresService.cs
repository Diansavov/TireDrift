
using System.Security.AccessControl;
using Microsoft.IdentityModel.Tokens;
using TireDrift.Data;
using TireDrift.Models;
using TireDrift.Models.ViewModels;

namespace Services
{
    public class TiresService : ITiresService
    {
        private readonly TiresDbContext _tiresDbContext;
        public TiresService(TiresDbContext tiresDbContext)
        {
            _tiresDbContext = tiresDbContext;
        }

        public async Task AddTireAsync(TireViewModel addTireViewModel)
        {
            Tire tire = new Tire()
            {
                Name = addTireViewModel.Name,
                Description = addTireViewModel.Description,
                Price = addTireViewModel.Price,
                Stock = addTireViewModel.Stock,
            };

            string imagePath = "";
            using (var memoryStream = new MemoryStream())
            {
                addTireViewModel.Image.CopyTo(memoryStream);

                imagePath = $"wwwroot/images/tires/{Guid.NewGuid().ToString()}.png";
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                FileStream fileStream = new FileStream(imagePath, FileMode.Create);
                addTireViewModel.Image.CopyTo(fileStream);
            }

            tire.ImagePath = imagePath.Substring(7);

            await _tiresDbContext.Tires.AddAsync(tire);
            await _tiresDbContext.SaveChangesAsync();
        }

        public async Task<Tire> GetAsync(string id)
        {
            return await _tiresDbContext.Tires.FindAsync(id);
        }

        public List<Tire> GetAll()
        {
            return _tiresDbContext.Tires.ToList();
        }

        public async Task EditAsync(TireViewModel editTireViewModel)
        {
            Tire tire = await GetAsync(editTireViewModel.Id);
            tire.Name = editTireViewModel.Name;
            tire.Description = editTireViewModel.Description;
            tire.Price = editTireViewModel.Price;
            tire.Stock = editTireViewModel.Stock;

            if (editTireViewModel.Image != null)
            {
                string imagePath = "";
                using (var memoryStream = new MemoryStream())
                {
                    editTireViewModel.Image.CopyTo(memoryStream);

                    imagePath = $"wwwroot/images/tires/{tire.ImagePath.Substring(14)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    FileStream fileStream = new FileStream(imagePath, FileMode.Create);
                    editTireViewModel.Image.CopyTo(fileStream);
                }

                tire.ImagePath = imagePath.Substring(7);
            }

            _tiresDbContext.Update(tire);
            await _tiresDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Tire product = await GetAsync(id);
            if (File.Exists($"wwwroot{product.ImagePath}"))
            {
                File.Delete($"wwwroot{product.ImagePath}");
            }

            _tiresDbContext.Tires.Remove(product);
            await _tiresDbContext.SaveChangesAsync();
        }

        public List<Tire> SearchProducts(string name, string filter)
        {
            var tires = _tiresDbContext.Tires.ToList();
            //Search
            if (!name.IsNullOrEmpty())
            {
                tires = tires.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }
            //Filter
            switch (filter)
            {
                case "name":
                    tires = tires.OrderBy(x => x.Name).ToList();
                    break;
                case "-name":
                    tires = tires.OrderByDescending(x => x.Name).ToList();
                    break;
                case "stock":
                    tires = tires.OrderBy(x => x.Stock).ToList();
                    break;
                case "-stock":
                    tires = tires.OrderByDescending(x => x.Stock).ToList();
                    break;
                case "price":
                    tires = tires.OrderBy(x => x.Price).ToList();

                    break;
                case "-price":
                    tires = tires.OrderByDescending(x => x.Price).ToList();

                    break;
                default:
                    tires = tires.OrderBy(x => x.Name).ToList();
                    break;
            }
            return tires;
        }

        public List<HotelTires> GetUserHotelTires(string userId)
        {
            return _tiresDbContext.HotelTires.Where(x => x.UserId == userId).ToList();
        }

        public async Task AddUserHotelTire(TireViewModel tireViewModel, string userId)
        {
            HotelTires hotelTires = new HotelTires()
            {
                TireName = tireViewModel.Name,
                UserId = userId,
                TireQuanity = tireViewModel.Stock
            };
            string imagePath = "";
            using (var memoryStream = new MemoryStream())
            {
                tireViewModel.Image.CopyTo(memoryStream);

                imagePath = $"wwwroot/images/tires/{Guid.NewGuid().ToString()}.png";
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                FileStream fileStream = new FileStream(imagePath, FileMode.Create);
                tireViewModel.Image.CopyTo(fileStream);
            }
            hotelTires.TireImageUrl = imagePath.Substring(7);

            _tiresDbContext.HotelTires.Add(hotelTires);
            await _tiresDbContext.SaveChangesAsync();
        }

        public async Task RemoveUserHotelTire(string hotelTireId)
        {
            HotelTires hotelTire = _tiresDbContext.HotelTires.Where(x => x.Id == hotelTireId).FirstOrDefault();

            _tiresDbContext.Remove(hotelTire);
            await _tiresDbContext.SaveChangesAsync();

        }
    }
}