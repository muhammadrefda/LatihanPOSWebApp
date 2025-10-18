using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSalesWebApp.Data;
using PointOfSalesWebApp.Models;

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

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product); // 1. Tambahkan produk ke DbContext
                await _context.SaveChangesAsync(); // 2. Simpan perubahan ke database
                return RedirectToAction(nameof(Index)); // 3. Kembali ke halaman daftar produk
            }
            // Jika data tidak valid, tampilkan kembali form dengan data yang sudah diisi
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }
    }
}
