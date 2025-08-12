using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class ClosureReasonServiceImplementation : IClosureReasonService
    {
        private readonly ApplicationDbContext _context;

        public ClosureReasonServiceImplementation(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClosureReason>> GetAllAsync()
        {
            return await _context.ClosureReasons.ToListAsync();
        }

        public async Task<ClosureReason> GetByIdAsync(int id)
        {
            return await _context.ClosureReasons
                                 .FirstOrDefaultAsync(c => c.ClosureReasonId == id);
        }

        public async Task<ClosureReason> CreateAsync(ClosureReason closureReason)
        {
            _context.ClosureReasons.Add(closureReason);
            await _context.SaveChangesAsync();
            return closureReason;
        }

        public async Task<ClosureReason> UpdateAsync(int id, ClosureReason updatedClosureReason)
        {
            var existing = await _context.ClosureReasons.FindAsync(id);
            if (existing == null)
                return null;

            existing.ReasonText = updatedClosureReason.ReasonText;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.ClosureReasons.FindAsync(id);
            if (existing == null)
                return false;

            _context.ClosureReasons.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
