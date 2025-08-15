using dotnet_backend.DTOs;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusLifeController : ControllerBase
    {
        private readonly ICampusLifeService _service;

        public CampusLifeController(ICampusLifeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CampusLifeDto>>> GetAll()
        {
            var list = await _service.GetAllCampusLifeAsync();
            return Ok(list); // returns JSON with title, description, imageUrl
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampusLifeDto>> GetById(int id)
        {
            var item = await _service.GetCampusLifeByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CampusLifeDto>> Create([FromBody] CampusLifeDto dto)
        {
            var created = await _service.CreateCampusLifeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CampusLifeId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampusLifeDto>> Update(int id, [FromBody] CampusLifeDto dto)
        {
            var updated = await _service.UpdateCampusLifeAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteCampusLifeAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
