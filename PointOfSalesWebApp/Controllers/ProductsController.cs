using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesWebApp.Data;

namespace PointOfSalesWebApp.Controllers
{
    public class ProductsController : Controller
    {
        public readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }
    }
}
