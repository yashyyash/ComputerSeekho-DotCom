using dotnet_backend.DTOs;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnnouncementDto>>> GetAll()
        {
            var announcements = await _service.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDto>> GetById(int id)
        {
            var announcement = await _service.GetAnnouncementByIdAsync(id);
            if (announcement == null)
                return NotFound();

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncementDto>> Create([FromBody] AnnouncementDto dto)
        {
            var created = await _service.CreateAnnouncementAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AnnouncementId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnnouncementDto>> Update(int id, [FromBody] AnnouncementDto dto)
        {
            var updated = await _service.UpdateAnnouncementAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAnnouncementAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
