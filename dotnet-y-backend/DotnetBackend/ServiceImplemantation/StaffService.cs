using DotnetBackend.DTO;
using DotnetBackend.Model;
using DotnetBackend.IService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using DotnetBackend.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotnetBackend.ServiceImplementation
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public StaffService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<StaffDTO> AddAsync(StaffDTO dto, string password)
        {
            var hash = HashPassword(password);

            var staff = new Staff
            {
                Name = dto.Name,
                Email = dto.Email,
                Username = dto.Username,
                PasswordHash = hash,
                PrimaryNumber = dto.PrimaryNumber,
                Role = dto.Role
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            dto.StaffId = staff.StaffId;
            return dto;
        }

        public async Task<IEnumerable<StaffDTO>> GetAllAsync()
        {
            return await _context.Staff
                .Select(s => new StaffDTO
                {
                    StaffId = s.StaffId,
                    Name = s.Name,
                    Email = s.Email,
                    Username = s.Username,
                    PrimaryNumber = s.PrimaryNumber,
                    Role = s.Role
                })
                .ToListAsync();
        }

        public async Task<StaffDTO?> GetByIdAsync(int id)
        {
            var s = await _context.Staff.FindAsync(id);
            if (s == null) return null;

            return new StaffDTO
            {
                StaffId = s.StaffId,
                Name = s.Name,
                Email = s.Email,
                Username = s.Username,
                PrimaryNumber = s.PrimaryNumber,
                Role = s.Role
            };
        }

        public async Task<StaffDTO?> UpdateAsync(StaffDTO dto)
        {
            var staff = await _context.Staff.FindAsync(dto.StaffId);
            if (staff == null) return null;

            staff.Name = dto.Name;
            staff.Email = dto.Email;
            staff.Username = dto.Username;
            staff.PrimaryNumber = dto.PrimaryNumber;
            staff.Role = dto.Role;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private string GenerateJwtToken(Staff staff)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, staff.StaffId.ToString()),
                new Claim(ClaimTypes.Name, staff.Username),
                new Claim(ClaimTypes.Role, staff.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO dto)
        {
            var hashedPassword = HashPassword(dto.Password);
            var staff = await _context.Staff
                .FirstOrDefaultAsync(s => s.Username == dto.Username && s.PasswordHash == hashedPassword);

            if (staff == null) return null;

            var token = GenerateJwtToken(staff);

            return new LoginResponseDTO
            {
                StaffId = staff.StaffId,
                Username = staff.Username,
                Role = staff.Role,
                Token = token
            };
        }
    }
}
