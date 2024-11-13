using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodCategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<FoodCategories>> GetFoodCategories()
        {
            return await _context.FoodCategories.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFoodCategory(int id)
        {
            var cat = await _context.FoodCategories.FindAsync(id);
            if (cat == null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(cat);
        }
        [HttpPost]
        public async Task<ActionResult<List<FoodCategories>>> PostFoodCategory(FoodCategories foodCat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.FoodCategories.Add(foodCat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodCategories), new { id = foodCat.Id }, foodCat);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodCategory(int id, [FromBody] FoodCategories foodCat)
        {
            if (id != foodCat.Id)
            {
                return BadRequest("ID mismatch");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(foodCat).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(foodCat);
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!FoodCatExists(id))
                    {
                        return NotFound("FoodCategory Not Found");
                    }
                    else {
                        throw;
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodCategory(int id)
        {
            var foodCat = await _context.FoodCategories.FindAsync(id);
            if (foodCat == null)
            {
                return NotFound();
            }
            _context.FoodCategories.Remove(foodCat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool FoodCatExists(int id)
        {
            return _context.FoodCategories.Any(e => e.Id == id);
        }
    }
}
