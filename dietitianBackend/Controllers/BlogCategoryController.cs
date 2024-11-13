using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlogCategoryController (AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<BlogCategory>>GetBlogCategories()
        {
            return await _context.BlogCategory.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlogCategory(int id)
        {
            var bcat = await _context.BlogCategory.FindAsync(id);
            if (bcat==null)
            {
                return NotFound("Blog Category Not Found");
            }
            return Ok(bcat);
        }
        [HttpPost]
        public async Task<ActionResult<List<BlogCategory>>> CreateBlogCategory(BlogCategory blogCategory)
        {
            _context.BlogCategory.Add(blogCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBlogCategories), new { id = blogCategory.Id }, blogCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateBlogCategory(int id, [FromForm] BlogCategory blogCategory)
        {
            if (id != blogCategory.Id)
            {
                return BadRequest("Id Mismatch");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(blogCategory).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(blogCategory);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BlogCatExists(id))
                    {
                        return NotFound("Blog Category Not Found");
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
        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            var blogcat = await _context.BlogCategory.FindAsync(id);
            if (blogcat == null)
            {
                return NotFound();
            }
            _context.BlogCategory.Remove(blogcat);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool BlogCatExists(int id)
        {
            return _context.BlogCategory.Any(b => b.Id == id);
        }
    }
}
