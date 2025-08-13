using dotnet_backend.DTOs;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacementController : ControllerBase
    {
        private readonly IPlacementService _placementService;

        public PlacementController(IPlacementService placementService)
        {
            _placementService = placementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var placements = await _placementService.GetAllPlacementsAsync();
            return Ok(placements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var placement = await _placementService.GetPlacementByIdAsync(id);
            if (placement == null) return NotFound();
            return Ok(placement);
        }

        [HttpGet("batch/{batchId}")]
        public async Task<IActionResult> GetByBatchId(int batchId)
        {
            var placements = await _placementService.GetPlacementsByBatchIdAsync(batchId);
            return Ok(placements);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlacementDto dto)
        {
            var created = await _placementService.CreatePlacementAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PlacementId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlacementDto dto)
        {
            var updated = await _placementService.UpdatePlacementAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _placementService.DeletePlacementAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
