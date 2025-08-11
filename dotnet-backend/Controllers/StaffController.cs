using dotnet_backend.Models;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Staff>>> GetAll()
        {
            var list = await _staffService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetById(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);
            if (staff == null)
                return NotFound();
            return Ok(staff);
        }

        // Create new staff with password
        [HttpPost]
        public async Task<ActionResult<Staff>> Create([FromBody] CreateStaffRequest request)
        {
            var staff = new Staff
            {
                StaffName = request.StaffName,
                StaffUsername = request.StaffUsername,
                StaffEmail = request.StaffEmail,
                StaffPhotoUrl = request.StaffPhotoUrl,
                StaffRoleId = request.StaffRoleId,
                IsActive = request.IsActive
            };

            var created = await _staffService.CreateAsync(staff, request.Password);
            return CreatedAtAction(nameof(GetById), new { id = created.StaffId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Staff>> Update(int id, [FromBody] Staff staff)
        {
            if (id != staff.StaffId)
                return BadRequest("ID mismatch");

            var updated = await _staffService.UpdateAsync(id, staff);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _staffService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

    // DTO for creating staff (with plain password)
    public class CreateStaffRequest
    {
        public string StaffName { get; set; }
        public string StaffUsername { get; set; }
        public string Password { get; set; }  // plain password, hashed in service
        public string StaffEmail { get; set; }
        public string StaffPhotoUrl { get; set; }
        public int StaffRoleId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
