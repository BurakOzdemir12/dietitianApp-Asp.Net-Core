using dietitianBackend.Data;
using dietitianBackend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dietitianBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MeasurementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/measurements
        [HttpGet]
        public async Task<IActionResult> GetMeasurements()
        {
            return Ok(await _context.Measurements.ToListAsync());
        }

        // GET: api/measurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Measurements>> GetMeasurement(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound("Measurement Not Found");
            }
            return Ok(measurement);
        }

        // POST: api/measurements
        [HttpPost]
        public async Task<ActionResult<Measurements>> PostMeasurement([FromBody] Measurements measurement)
        {
            var food = await _context.Measurements.FindAsync(measurement.FoodId);
            if (food == null)
            {
                return NotFound("Food Not Found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMeasurement), new { id = measurement.Id }, measurement);
        }


        // PUT: api/measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] Measurements measurement)
        {
            if (id != measurement.Id)
            {
                return BadRequest("Id Mismatch");
            }

            if (!_context.Measurements.Any(m => m.Id == id))
            {
                return NotFound("Measurement Not Found");
            }

            _context.Entry(measurement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(measurement);
        }

        // DELETE: api/measurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound("Measurement Not Found");
            }

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
