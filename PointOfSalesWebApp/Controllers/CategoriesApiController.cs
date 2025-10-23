// Lokasi: PointOfSalesWebApp/Controllers/CategoriesApiController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesWebApp.Data; // Ganti dengan namespace Data Anda
using PointOfSalesWebApp.Models; // Ganti dengan namespace Models Anda

[Route("api/[controller]")]
[ApiController]
public class CategoriesApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriesApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/CategoriesApi
    // Mendapatkan semua daftar kategori
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        // Untuk Category, kita tidak perlu .Include() karena tidak ada data parent-nya.
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }

    // GET: api/CategoriesApi/5
    // Mendapatkan satu kategori berdasarkan ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        // Jika kategori tidak ditemukan, kembalikan status 404 Not Found.
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    // POST: api/CategoriesApi
    // Membuat kategori baru
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        // Best practice: Kembalikan status 201 Created.
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    // PUT: api/CategoriesApi/5
    // Memperbarui kategori yang sudah ada
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Best practice: Kembalikan status 204 No Content.
        return NoContent();
    }


    // DELETE: api/CategoriesApi/5
    // Menghapus kategori
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        // Best practice: Kembalikan status 204 No Content.
        return NoContent();
    }

    // Metode helper pribadi untuk mengecek apakah kategori ada
    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}