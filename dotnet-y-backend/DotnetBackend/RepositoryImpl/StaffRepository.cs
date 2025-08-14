using DotnetBackend.Data;
using DotnetBackend.IRepository;
using DotnetBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.RepositoryImpl
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync() => await _context.Staff.ToListAsync();

        public async Task<Staff?> GetByIdAsync(int id) => await _context.Staff.FindAsync(id);

        public async Task<Staff?> GetByUsernameAsync(string username) =>
            await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);

        public async Task<Staff> AddAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<Staff> UpdateAsync(Staff staff)
        {
            _context.Staff.Update(staff);
            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
