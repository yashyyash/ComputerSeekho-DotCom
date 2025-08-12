using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_backend.Models;
using dotnet_backend.AppDbContext;

namespace dotnet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FollowUpController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FollowUp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowUp>>> GetFollowUps()
        {
            return await _context.FollowUps
                .Include(f => f.Enquiry) // If you want to load related data
                .ToListAsync();
        }

        // GET: api/FollowUp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FollowUp>> GetFollowUp(int id)
        {
            var followUp = await _context.FollowUps
                .Include(f => f.Enquiry)
                .FirstOrDefaultAsync(f => f.FollowupId == id);

            if (followUp == null)
            {
                return NotFound();
            }

            return followUp;
        }

        // POST: api/FollowUp
        [HttpPost]
        public async Task<ActionResult<FollowUp>> CreateFollowUp(FollowUp followUp)
        {
            _context.FollowUps.Add(followUp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFollowUp), new { id = followUp.FollowupId }, followUp);
        }

        // PUT: api/FollowUp/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFollowUp(int id, FollowUp followUp)
        {
            if (id != followUp.FollowupId)
            {
                return BadRequest();
            }

            _context.Entry(followUp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FollowUps.Any(e => e.FollowupId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/FollowUp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowUp(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);
            if (followUp == null)
            {
                return NotFound();
            }

            _context.FollowUps.Remove(followUp);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
