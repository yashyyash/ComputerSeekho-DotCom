using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class ClosureReasonRepository : IClosureReasonRepository
    {
        private readonly ApplicationDbContext _context;

        public ClosureReasonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClosureReason>> GetAllAsync()
        {
            return await _context.ClosureReasons.ToListAsync();
        }

        public async Task<ClosureReason> GetByIdAsync(int id)
        {
            return await _context.ClosureReasons.FirstOrDefaultAsync(c => c.ClosureReasonId == id);
        }

        public async Task<ClosureReason> AddAsync(ClosureReason closureReason)
        {
            _context.ClosureReasons.Add(closureReason);
            await _context.SaveChangesAsync();
            return closureReason;
        }

        public async Task<ClosureReason> UpdateAsync(ClosureReason closureReason)
        {
            _context.Entry(closureReason).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return closureReason;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ClosureReasons.FindAsync(id);
            if (entity == null)
                return false;

            _context.ClosureReasons.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
