using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesWebApp.Data;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPIController : ControllerBase
    {
        public readonly ApplicationDbContext _context;

        public CategoriesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }


        //GET
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }

        //GET Category by ID

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(long id)
        {
            if (id == 0) return NotFound();

            var category = await _context.Categories.FindAsync(id);

            return Accepted(category);
        }

        //POST, nambah data 
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new
            {
                id = category.Id
            }, category);
        }

        //update data
        [HttpPatch("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(long id, [FromBody] Category category)
        {
            if (category == null) return BadRequest("Invalid Body");

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) return NotFound("Category not found");

            if (category.CategoryName != null) existingCategory.CategoryName = category.CategoryName;
            if (Request.Body.CanSeek) Request.Body.Position = 0;

            existingCategory.Active = category.Active;

            existingCategory.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating category : {ex.Message}");
            }
            return Ok(existingCategory);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(long id, Category category)
        {
            if (id != category.Id) return BadRequest();

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null) return NotFound("Category Not Found");

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Active = category.Active;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating category: {ex.Message}");
            }

            return Accepted(existingCategory);
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            if (category.Active == true)
            {
                return BadRequest();
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        

        
    }
}
