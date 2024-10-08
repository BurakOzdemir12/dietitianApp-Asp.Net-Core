using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _uploadsFolder;

        public ImageUploadController(AppDbContext context)
        {
            _context = context;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads"); // wwwroot dışında
        }

        [HttpPost("upload/recipe/{id}")]
        public async Task<IActionResult> UploadRecipeImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound("Tarif bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            if (!string.IsNullOrEmpty(recipe.Img))
            {
                var oldFilePath = Path.Combine(_uploadsFolder, Path.GetFileName(recipe.Img));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            recipe.Img = $"/uploads/{file.FileName}";
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = recipe.Img });
        }

        [HttpPost("upload/food/{id}")]
        public async Task<IActionResult> UploadFoodImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
                return NotFound("Yemek bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            if (!string.IsNullOrEmpty(food.Img))
            {
                var oldFilePath = Path.Combine(_uploadsFolder, Path.GetFileName(food.Img));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            food.Img = $"/uploads/{file.FileName}";
            _context.Entry(food).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = food.Img });
        }

        [HttpPost("upload/foodCategory/{id}")]
        public async Task<IActionResult> UploadFoodCategoryImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var foodCat = await _context.FoodCategories.FindAsync(id);
            if (foodCat == null)
                return NotFound("Kategori bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            if (!string.IsNullOrEmpty(foodCat.Img))
            {
                var oldFilePath = Path.Combine(_uploadsFolder, Path.GetFileName(foodCat.Img));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            foodCat.Img = $"/uploads/{file.FileName}";
            _context.Entry(foodCat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = foodCat.Img });
        }
        [HttpPut("update/recipe/{id}")]
        public async Task<IActionResult>UpdateRecipeImage(int id,IFormFile file )
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound("Tarif Bulunamadı");

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            if (!string.IsNullOrEmpty(recipe.Img))
            {
                var oldFilePath = Path.Combine(_uploadsFolder,Path.GetFileName(recipe.Img));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            using (var stream=new FileStream(filePath,FileMode.Create))
            { await file.CopyToAsync(stream); }

            recipe.Img = $"/uploads/{file.FileName}";
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new {filePath=recipe.Img});
        }
        [HttpPut("update/food/{id}")]
        public async Task<IActionResult> UpdateFoodImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var food = await _context.Foods.FindAsync(id);
            if (food== null) return NotFound("Yemek Bulunamadı");

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            if (!string.IsNullOrEmpty(food.Img))
            {
                var oldFilePath = Path.Combine(_uploadsFolder, Path.GetFileName(food.Img));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            { await file.CopyToAsync(stream); }

            food.Img = $"/uploads/{file.FileName}";
            _context.Entry(food).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { filePath = food.Img });
        }

        [HttpGet("food/{id}")]
        public async Task<IActionResult> GetFoodImage(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null || string.IsNullOrEmpty(food.Img))
                return NotFound("Yemek veya resim bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, Path.GetFileName(food.Img));
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpg");
        }

        [HttpGet("recipe/{id}")]
        public async Task<IActionResult> GetRecipeImage(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null || string.IsNullOrEmpty(recipe.Img))
                return NotFound("Tarif veya resim bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, Path.GetFileName(recipe.Img));
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpg");
        }

        [HttpDelete("delete/recipe/{id}")]
        public async Task<IActionResult> DeleteRecipeImage(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return NotFound("Tarif bulunamadı.");

            var fileName = Path.GetFileName(recipe.Img);
            var filePath = Path.Combine(_uploadsFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            recipe.Img = null;
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/food/{id}")]
        public async Task<IActionResult> DeleteFoodImage(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
                return NotFound("Yemek bulunamadı.");

            var fileName = Path.GetFileName(food.Img);
            var filePath = Path.Combine(_uploadsFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            food.Img = null;
            _context.Entry(food).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

//if you want to manage image upload with one crud functions (BaseEntity ) use down belows code
// !!!!! Dont forget to create BaseEntity Entity and use with other entities 


/*
using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _uploadsFolder;

        public ImageUploadController(AppDbContext context)
        {
            _context = context;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads"); // wwwroot dışında
        }

        [HttpPost("upload/{entity}/{id}")]
        public async Task<IActionResult> UploadImage(string entity, int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenmedi.");

            var entityType = GetEntityType(entity);
            if (entityType == null)
                return BadRequest("Geçersiz entity adı.");

            var item = await _context.Set(entityType).FindAsync(id) as BaseEntity;
            if (item == null)
                return NotFound($"{entity} bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, file.FileName);
            if (!string.IsNullOrEmpty(item.Img))
            {
                var oldFilePath = Path.Combine(_uploadsFolder, Path.GetFileName(item.Img));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            // Dosyayı sunucuya kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Entity'nin resim yolunu veritabanında güncelle
            item.Img = $"/uploads/{file.FileName}";
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = item.Img });
        }

        [HttpGet("image/{filename}")]
        public IActionResult GetImage(string filename)
        {
            var filePath = Path.Combine(_uploadsFolder, filename);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpg");
        }

        [HttpGet("food/{id}")]
        public async Task<IActionResult> GetFoodImage(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null || string.IsNullOrEmpty(food.Img))
                return NotFound("Yemek veya resim bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, Path.GetFileName(food.Img));
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpg");
        }

        [HttpGet("recipe/{id}")]
        public async Task<IActionResult> GetRecipeImage(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null || string.IsNullOrEmpty(recipe.Img))
                return NotFound("Tarif veya resim bulunamadı.");

            var filePath = Path.Combine(_uploadsFolder, Path.GetFileName(recipe.Img));
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpg");
        }

        [HttpDelete("delete/{entity}/{id}")]
        public async Task<IActionResult> DeleteImage(string entity, int id)
        {
            var entityType = GetEntityType(entity);
            if (entityType == null)
                return BadRequest("Geçersiz entity adı.");

            var item = await _context.Set(entityType).FindAsync(id) as BaseEntity;
            if (item == null)
                return NotFound($"{entity} bulunamadı.");

            var fileName = Path.GetFileName(item.Img);
            var filePath = Path.Combine(_uploadsFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            item.Img = null;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private Type GetEntityType(string entityName)
        {
            return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
*/