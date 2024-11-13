using dietitianBackend.Data;
using dietitianBackend.Dto;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Blogs>> GetBlogs()
        {
            return await _context.Blogs.Include(f=>f.BlogCategories).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult>GetBlog(int id)
        {
            var prop =await _context.Blogs.FindAsync(id);
            if (prop == null) { 
            return NotFound("Blog Not Found");
            }
            return Ok(prop);
        }
        /* [HttpPost]
         public async Task<IActionResult> CreateBlog([FromBody] Blogs blog)
         {
             if (blog == null)
             {
                 return BadRequest("Blog verisi sağlanmadı.");
             }

             _context.Blogs.Add(blog);
             await _context.SaveChangesAsync();
             return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
         }
        */
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromForm] BlogDTO blogDto)
        {
            if (blogDto == null)
            {
                return BadRequest("Blog data not provided.");
            }

            var blog = new Blogs
            {
                Name = blogDto.Name,
                Description = blogDto.Description,
                Date = DateTime.UtcNow
            };
            if (blogDto.CategoryIds != null && blogDto.CategoryIds.Any())
            {
                var categories = await _context.BlogCategory
                    .Where(c => blogDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();
                blog.BlogCategories = categories;
            }

            if (blogDto.Image != null && blogDto.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Path.GetFileName(blogDto.Image.FileName); // Orijinal dosya ismi

                //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(blogDto.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await blogDto.Image.CopyToAsync(stream);
                }

                blog.Img = $"/uploads/{fileName}";
            }

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateBlog(int id, [FromForm] Blogs blog)
        {
            if (id !=blog.Id)
            {
                return BadRequest("Id Mismatch");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(blog).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!BlogExists(id))
                    {
                        return NotFound("Blog Not Found");
                    }
                    else {
                        throw;

                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task <IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return NoContent();
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        return NoContent();

        }
        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(b => b.Id == id);
        }
    }
}
