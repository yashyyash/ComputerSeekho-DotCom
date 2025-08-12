using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class FollowUpRepository : IFollowUpRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowUpRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FollowUp>> GetAllAsync()
        {
            return await _context.FollowUps
                .Include(f => f.Enquiry)
                .ToListAsync();
        }

        public async Task<FollowUp?> GetByIdAsync(int id)
        {
            return await _context.FollowUps
                .Include(f => f.Enquiry)
                .FirstOrDefaultAsync(f => f.FollowupId == id);
        }

        public async Task<FollowUp> AddAsync(FollowUp followUp)
        {
            _context.FollowUps.Add(followUp);
            await _context.SaveChangesAsync();
            return followUp;
        }

        public async Task<bool> UpdateAsync(FollowUp followUp)
        {
            _context.Entry(followUp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(followUp.FollowupId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);
            if (followUp == null)
            {
                return false;
            }

            _context.FollowUps.Remove(followUp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.FollowUps.AnyAsync(e => e.FollowupId == id);
        }
    }
}
