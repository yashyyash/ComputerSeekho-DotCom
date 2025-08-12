using dotnet_backend.DTOs;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using dotnet_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace dotnet_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var staff = await _context.Staffs
                .Include(s => s.StaffRole)
                .FirstOrDefaultAsync(s => s.StaffUsername == request.StaffUsername);

            if (staff == null || !VerifyPassword(request.Password, staff.PasswordHash))
                return Unauthorized("Invalid username or password");

            var token = _tokenService.GenerateToken(staff.StaffUsername, staff.StaffRole?.RoleName ?? "User");

            return Ok(new LoginResponseDto
            {
                Token = token,
                StaffName = staff.StaffName,
                Role = staff.StaffRole?.RoleName ?? "User"
            });
        }

        // Example password hash check
        private bool VerifyPassword(string password, string storedHash)
        {
            // If stored as plain text (NOT RECOMMENDED) just compare directly
            // return password == storedHash;

            using var sha256 = SHA256.Create();
            var hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hash == storedHash;
        }
    }
}
