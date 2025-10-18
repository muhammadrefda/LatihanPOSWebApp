using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PointOfSalesWebApp.Data;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == 0) return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null ) return NotFound();

            ViewBag.ActiveList = new List<SelectListItem> {
    new SelectListItem { Value = "true",  Text = "Aktif",    Selected = category.Active },
    new SelectListItem { Value = "false", Text = "Nonaktif", Selected = !category.Active }
};
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long id, Category category)
        {
            if ( id != category.Id) return NotFound();

            var inUse = await _context.Products.AnyAsync(p => p.CategoryId == category.Id);

            if(!category.Active && inUse)
            {
                ModelState.AddModelError(nameof(category.Active), "Masih ada produk yang menggunakan kategori ini, mohon ubah kategori/hapus produk terlebih dahulu");
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!_context.Categories.Any(e => e.Id == category.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ActiveList = new List<SelectListItem> {
    new SelectListItem { Value = "true",  Text = "Aktif",    Selected = category.Active },
    new SelectListItem { Value = "false", Text = "Nonaktif", Selected = !category.Active }
};
            return View(category) ;
        }

    }
}
