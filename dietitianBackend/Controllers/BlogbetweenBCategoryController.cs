using dietitianBackend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BlogbetweenBCategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlogbetweenBCategoryController(AppDbContext context)
        {
            _context = context;
        }
        /* [HttpGet("GetBlogsByCategory/{blogCategoryId}")]
         public async Task<IActionResult> GetBlogsByCategory(int blogCategoryId)
         {
             var blogs = await _context.Blogs
                 .Where(b => b.BlogCategories.Any(c => c.Id == blogCategoryId))
                 .ToListAsync();

             return Ok(blogs);
         }
        */
        [HttpGet("GetBlogsByCategory/{categoryId}")]
        public async Task<IActionResult> GetBlogsByCategory(int categoryId)
        {
            var blogs = await _context.Blogs
                .Where(b => b.BlogCategories.Any(c => c.Id == categoryId))
                .ToListAsync();

            return Ok(blogs);
        }

    }
}
