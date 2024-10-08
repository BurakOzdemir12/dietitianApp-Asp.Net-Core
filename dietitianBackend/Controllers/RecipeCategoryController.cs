using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipeCategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<RecipeCategory>>GetCategories()
        {
            return await _context.RecipeCategory.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRecipeCategory(int id)
        {
            var cat = await _context.RecipeCategory.FindAsync(id);
            if (cat == null)
            {
                return NotFound("Recipe Category Not Found");
            }
            return Ok(cat);
        }
        [HttpPost]
        public async Task<ActionResult<List<RecipeCategory>>> AddRecipeCategory(RecipeCategory recipeCategory)
        {
            _context.RecipeCategory.Add(recipeCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories), new { id = recipeCategory.Id }, recipeCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeCategory(int id, [FromBody] RecipeCategory recipeCategory)
        {
            if (id != recipeCategory.Id)
            {
                return BadRequest("ID mismatch");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(recipeCategory).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(recipeCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeCatExists(id))
                    {
                        return NotFound("Recipe Category Not Found");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeCategory(int id)
        {
            var recipeCat = await _context.RecipeCategory.FindAsync(id);
            if (recipeCat != null)
            {
                return NotFound();
            }
            _context.RecipeCategory.Remove(recipeCat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool RecipeCatExists(int id )
        {
            return _context.RecipeCategory.Any(x => x.Id == id);
        }
    }
}
