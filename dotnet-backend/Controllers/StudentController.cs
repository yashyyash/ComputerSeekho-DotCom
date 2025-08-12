using Microsoft.AspNetCore.Mvc;
using dotnet_backend.Services;
using dotnet_backend.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace dotnet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAll()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Create(StudentCreateDTO dto)
        {
            var student = await _studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StudentUpdateDTO dto)
        {
            var updatedStudent = await _studentService.UpdateAsync(id, dto);
            if (updatedStudent == null) return NotFound();

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _studentService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
