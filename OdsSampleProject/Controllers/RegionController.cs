using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OdsSampleProject.Context;
using OdsSampleProject.Models;

namespace OdsSampleProject.Controllers
{
    public class RegionController : Controller
    {
        private readonly ILogger<RegionController> _logger;
        private readonly OdsDbContext _context;
        public RegionController(ILogger<RegionController> logger, OdsDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.RegionList = _context.Region.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Index(Region region)
        {
            region.RegionName = region.RegionName.Trim();
            var regionCount = _context.Region.Count();
            if (_context.Region.Any(r => r.RegionName.ToUpper() == region.RegionName.ToUpper()))
            {
                TempData["ErrorMessage"] = "Bu isimde bir bölge zaten var.";
            }
            if (regionCount >= 7)
            {
                TempData["ErrorMessage"] = "Bölge sayısı 7'den fazla olduğu için yeni kayıt eklenemez.";
            }
            else
            {
                _context.Add(region);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt başarıyla oluşturuldu.";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteRegion(int Id)
        {
            var removedRegion = await _context.Region.FindAsync(Id);
            _context.Remove(removedRegion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult GetRegionInfo(int Id)
        {
            var region = _context.Region.Find(Id);
            return View(region);
        }
        [HttpPost]
        public IActionResult GetRegionInfo(Region region)
        {
            region.RegionName = region.RegionName.Trim();
            if (_context.Region.Any(r => r.RegionName.ToUpper() == region.RegionName.ToUpper()))
            {
                TempData["ErrorMessage"] = "Bu isimde bir bölge zaten var.";
            }
            else
            {
                _context.Update(region);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt başarıyla güncellenmiştir.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
