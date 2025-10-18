using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesWebApp.Data;

namespace PointOfSalesWebApp.Controllers
{
    public class ProductController : Controller
    {

        public readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();

            return View();
        }
    }
}
