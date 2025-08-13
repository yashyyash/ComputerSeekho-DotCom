using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_backend.DTOs;
using dotnet_backend.Services;

namespace dotnet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementController : ControllerBase
    {
        private readonly IPlacementService _placementService;

        public PlacementController(IPlacementService placementService)
        {
            _placementService = placementService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlacementDTO>>> GetAll()
        {
            var placements = await _placementService.GetAllAsync();
            return Ok(placements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlacementDTO>> GetById(int id)
        {
            var placement = await _placementService.GetByIdAsync(id);
            if (placement == null) return NotFound();
            return Ok(placement);
        }

        [HttpPost]
        public async Task<ActionResult<PlacementDTO>> Create(PlacementCreateDTO dto)
        {
            var createdPlacement = await _placementService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdPlacement.PlacementId }, createdPlacement);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlacementDTO>> Update(int id, PlacementUpdateDTO dto)
        {
            var updatedPlacement = await _placementService.UpdateAsync(id, dto);
            if (updatedPlacement == null) return NotFound();
            return Ok(updatedPlacement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _placementService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
