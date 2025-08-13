using dotnet_backend.Models;
using dotnet_backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Services
{
    public class StaffService : IStaffService
    {
        private readonly AppDbContext _context;

        public StaffService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            // If you want to include Enquiries too, add: .Include(s => s.Enquiries)
            return await _context.Staffs.AsNoTracking().ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(long id)
        {
            // Include Enquiries if needed:
            // return await _context.Staffs.Include(s => s.Enquiries).FirstOrDefaultAsync(s => s.StaffId == id);
            return await _context.Staffs.AsNoTracking().FirstOrDefaultAsync(s => s.StaffId == id);
        }

        public async Task<Staff> CreateAsync(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> UpdateAsync(long id, Staff staff)
        {
            var existing = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
            if (existing == null) return false;

            existing.StaffName = staff.StaffName;
            existing.PhotoUrl = staff.PhotoUrl;
            existing.StaffMobile = staff.StaffMobile;
            existing.StaffEmail = staff.StaffEmail;
            existing.StaffUsername = staff.StaffUsername;
            existing.StaffPassword = staff.StaffPassword;
            existing.StaffRole = staff.StaffRole;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
            if (existing == null) return false;

            _context.Staffs.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
