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
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyDTO>>> GetAll()
        {
            var faculties = await _facultyService.GetAllAsync();
            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyDTO>> GetById(int id)
        {
            var faculty = await _facultyService.GetByIdAsync(id);
            if (faculty == null)
                return NotFound();

            return Ok(faculty);
        }

        [HttpPost]
        public async Task<ActionResult<FacultyDTO>> Create(FacultyDTO facultyDto)
        {
            var createdFaculty = await _facultyService.CreateAsync(facultyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdFaculty.FacultyId }, createdFaculty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FacultyDTO facultyDto)
        {
            if (id != facultyDto.FacultyId)
                return BadRequest("ID mismatch");

            var existing = await _facultyService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _facultyService.UpdateAsync(facultyDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _facultyService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
