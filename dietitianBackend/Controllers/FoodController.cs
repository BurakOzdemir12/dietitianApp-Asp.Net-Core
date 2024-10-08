using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        private readonly AppDbContext _context;

        public FoodController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Food>> GetFoods()
        {
            return await _context.Foods.Include(f=>f.Measurements).ToListAsync();
        }
        /*[HttpGet("{id}")]
        public async Task<ActionResult> GetFood(int id)
        {
            var prop = await _context.Foods.FindAsync(id);
            if (prop == null)
            {
                return NotFound("Food Not Found");
            }
            return Ok(prop);
        }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _context.Foods
                .Include(f => f.Measurements) // Measurements'ı dahil ediyoruz
                .FirstOrDefaultAsync(f => f.Id == id);

            if (food == null)
            {
                return NotFound("Food Not Found");
            }
            return Ok(food);
        }
        [HttpPost]
        public async Task<ActionResult<Food>> AddFood([FromBody]Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foodcategory = await _context.FoodCategories.FindAsync(food.FoodcategoryId);
            if (foodcategory == null)
            {
                return BadRequest("Invalid FoodCategoryId");
            }
            food.FoodCategory = foodcategory;

            foreach (var measurement in food.Measurements)
            {
                measurement.FoodId = food.Id;
            }

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFood", new { id = food.Id }, food);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id, [FromBody] Food food)
        {
            if (id != food.Id)
            {
                return BadRequest("Id mismatch");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(food).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(food);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(id))
                    {
                        return NotFound("Food Not Found");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest(ModelState);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFood(int id) 
        { 
            var food = await _context.Foods.FindAsync(id);
            if (food == null) { 
            return NoContent();
            }
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
        return NoContent();
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);

        }
    }
}