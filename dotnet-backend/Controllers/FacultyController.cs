using dotnet_backend.DTOs;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _service;

        public FacultyController(IFacultyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FacultyDto>>> GetAll()
        {
            var faculties = await _service.GetAllFacultiesAsync();
            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyDto>> GetById(int id)
        {
            var faculty = await _service.GetFacultyByIdAsync(id);
            if (faculty == null)
                return NotFound();

            return Ok(faculty);
        }

        [HttpPost]
        public async Task<ActionResult<FacultyDto>> Create([FromBody] FacultyDto dto)
        {
            var created = await _service.CreateFacultyAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.FacultyId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FacultyDto>> Update(int id, [FromBody] FacultyDto dto)
        {
            var updated = await _service.UpdateFacultyAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteFacultyAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
