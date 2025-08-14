// File: Controllers/StaffController.cs
using DotnetBackend.DTO;
using DotnetBackend.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;

        public StaffController(IStaffService service)
        {
            _service = service;
        }

        // ADMIN & HR can view all
        [HttpGet]
        [Authorize(Roles = "ADMIN,HR")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        // ADMIN & HR can view single
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,HR")]
        public async Task<IActionResult> GetById(int id)
        {
            var staff = await _service.GetByIdAsync(id);
            return staff == null ? NotFound() : Ok(staff);
        }

        // Create staff — ADMIN only (you can set [AllowAnonymous] temporarily to seed first admin)
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] StaffDTO dto, [FromQuery] string password)
        {
            var created = await _service.AddAsync(dto, password);
            return CreatedAtAction(nameof(GetById), new { id = created.StaffId }, created);
        }

        // Update staff — ADMIN & HR
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,HR")]
        public async Task<IActionResult> Update(int id, [FromBody] StaffDTO dto)
        {
            if (id != dto.StaffId) return BadRequest("Id mismatch");
            var updated = await _service.UpdateAsync(dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        // Delete — ADMIN only
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        // Login — anyone
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var result = await _service.LoginAsync(dto);
            return result == null ? Unauthorized() : Ok(result);
        }
    }
}
