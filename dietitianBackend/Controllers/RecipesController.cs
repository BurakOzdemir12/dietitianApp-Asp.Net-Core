using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

      public RecipesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Recipes>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRecipe(int id)
        {
            var prop = await _context.Recipes.FindAsync(id);
            if (prop == null)
            {
                return NotFound("Recipe Not Found");
            }

            return Ok(prop);
        }
        /*[HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] Recipes recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return Ok(recipe);
            }
            return BadRequest(ModelState);
        }*/
        [HttpPost]
        public async Task<ActionResult<Recipes>> AddRecipe(Recipes recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.RecipeCategory.FindAsync(recipe.RecipeCategoryId);
            if (category == null)
            {
                return BadRequest("Invalid RecipeCategoryId");
            }

            recipe.RecipeCategory = category;

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipes recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(recipe).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(id))
                    {
                        return NotFound("Recipe not found.");
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }


    }
}
