using dotnet_backend.DTOs;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        // GET: api/batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchDto>>> GetAllBatches()
        {
            var batches = await _batchService.GetAllBatchesAsync();
            return Ok(batches);
        }

        // GET: api/batch/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchDto>> GetBatchById(int id)
        {
            var batch = await _batchService.GetBatchByIdAsync(id);
            if (batch == null)
                return NotFound();

            return Ok(batch);
        }

        // POST: api/batch
        [HttpPost]
        public async Task<ActionResult<BatchDto>> CreateBatch([FromBody] BatchDto batchDto)
        {
            var createdBatch = await _batchService.CreateBatchAsync(batchDto);
            return CreatedAtAction(nameof(GetBatchById), new { id = createdBatch.BatchId }, createdBatch);
        }

        // PUT: api/batch/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBatch(int id, [FromBody] BatchDto batchDto)
        {
            var updated = await _batchService.UpdateBatchAsync(id, batchDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/batch/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            var deleted = await _batchService.DeleteBatchAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
