using Microsoft.AspNetCore.Mvc;
using dotnet_backend.Models;
using dotnet_backend.Services;

namespace dotnet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClosureReasonController : ControllerBase
    {
        private readonly IClosureReasonService _closureReasonService;

        public ClosureReasonController(IClosureReasonService closureReasonService)
        {
            _closureReasonService = closureReasonService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClosureReason>>> GetClosureReasons()
        {
            return await _closureReasonService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClosureReason>> GetClosureReason(int id)
        {
            var closureReason = await _closureReasonService.GetByIdAsync(id);
            if (closureReason == null) return NotFound();
            return closureReason;
        }

        [HttpPost]
        public async Task<ActionResult<ClosureReason>> PostClosureReason(ClosureReason closureReason)
        {
            var created = await _closureReasonService.CreateAsync(closureReason);
            return CreatedAtAction(nameof(GetClosureReason), new { id = created.ClosureReasonId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClosureReason(int id, ClosureReason closureReason)
        {
            var updated = await _closureReasonService.UpdateAsync(id, closureReason);
            if (updated == null) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClosureReason(int id)
        {
            var deleted = await _closureReasonService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
