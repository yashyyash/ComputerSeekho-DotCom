using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class StaffServiceImplementations : IStaffService
    {
        private readonly ApplicationDbContext _context;

        public StaffServiceImplementations(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staffs.Include(s => s.StaffRole).ToListAsync();
        }

        public async Task<Staff> GetByIdAsync(int id)
        {
            return await _context.Staffs.Include(s => s.StaffRole).FirstOrDefaultAsync(s => s.StaffId == id);
        }

        public async Task<Staff> CreateAsync(Staff staff, string plainPassword)
        {
            staff.PasswordHash = HashPassword(plainPassword);
            staff.CreatedAt = DateTime.UtcNow;
            staff.UpdatedAt = DateTime.UtcNow;
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateAsync(int id, Staff updatedStaff)
        {
            var existing = await _context.Staffs.FindAsync(id);
            if (existing == null)
                return null;

            existing.StaffName = updatedStaff.StaffName;
            existing.StaffUsername = updatedStaff.StaffUsername;
            existing.StaffEmail = updatedStaff.StaffEmail;
            existing.StaffPhotoUrl = updatedStaff.StaffPhotoUrl;
            existing.StaffRoleId = updatedStaff.StaffRoleId;
            existing.IsActive = updatedStaff.IsActive;
            existing.LastLogin = updatedStaff.LastLogin;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Staffs.FindAsync(id);
            if (existing == null)
                return false;

            _context.Staffs.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

}
