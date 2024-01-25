using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdsSampleProject.Context;
using OdsSampleProject.Models;

namespace OdsSampleProject.Controllers
{
    public class CityController : Controller
    {
        private readonly ILogger<CityController> _logger;
        private readonly OdsDbContext _context;

        public CityController(ILogger<CityController> logger,OdsDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.RegionList = _context.Region.ToList();
            ViewBag.CityList = _context.City.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Index(City city)
        {
            city.CityName = city.CityName.Trim();
            var regionCount = _context.Region.Count();
            if (_context.City.Any(r => r.CityName.ToUpper() == city.CityName.ToUpper()))
            {
                TempData["ErrorMessage"] = "Bu isimde bir şehir zaten var.";
            }
            else
            {
                _context.Add(city);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt başarıyla oluşturuldu.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCity(int Id)
        {
            var removedCity = await _context.City.FindAsync(Id);
            _context.Remove(removedCity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult GetCityInfo(int Id)
        {
            var city = _context.City
                  .Include(c => c.Region)
                  .FirstOrDefault(c => c.CityId == Id);

            var cityInfoViewModel = new CityInfoViewModel
            {
                City = city,
                RegionList = _context.Region.ToList()
            };

            return View(cityInfoViewModel);
            //var city = _context.City
            //  .Include(c => c.Region)
            //  .FirstOrDefault(c => c.CityId == Id);
            //ViewBag.RegionList = _context.Region.ToList();
            //return View(city);
        }

        [HttpPost]
        public IActionResult GetCityInfo(City city)
        {
            //city.CityName = city.CityName.Trim();
            
            //if (_context.Region.Any(r => r.RegionName.ToUpper() == city.CityName.ToUpper()))
            //{
            //    TempData["ErrorMessage"] = "Bu isimde bir şehir zaten var.";
            //}
            //else
            //{
                _context.Update(city);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Şehir başarıyla güncellenmiştir.";
            //}
            return RedirectToAction(nameof(Index));
        }

    }
}
