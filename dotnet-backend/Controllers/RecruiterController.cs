using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_backend.DTOs;
using dotnet_backend.Services;

namespace dotnet_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;

        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecruiterDTO>>> GetAll()
        {
            var recruiters = await _recruiterService.GetAllAsync();
            return Ok(recruiters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecruiterDTO>> GetById(int id)
        {
            var recruiter = await _recruiterService.GetByIdAsync(id);
            if (recruiter == null) return NotFound();
            return Ok(recruiter);
        }

        [HttpPost]
        public async Task<ActionResult<RecruiterDTO>> Create(RecruiterCreateDTO dto)
        {
            var createdRecruiter = await _recruiterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdRecruiter.RecruiterId }, createdRecruiter);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecruiterDTO>> Update(int id, RecruiterUpdateDTO dto)
        {
            var updatedRecruiter = await _recruiterService.UpdateAsync(id, dto);
            if (updatedRecruiter == null) return NotFound();
            return Ok(updatedRecruiter);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _recruiterService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
