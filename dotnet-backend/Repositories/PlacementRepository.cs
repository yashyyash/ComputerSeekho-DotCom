using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public class PlacementRepository : IPlacementRepository
    {
        private readonly ApplicationDbContext _context;

        public PlacementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Placement>> GetAllAsync()
        {
            return await _context.Placements
                .Include(p => p.Student)
                .Include(p => p.Batch)
                .Include(p => p.Recruiter)
                .Include(p => p.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<Placement>> GetByBatchIdAsync(int batchId)
        {
            return await _context.Placements
                .Where(p => p.BatchId == batchId)
                .Include(p => p.Student)
                .Include(p => p.Batch)
                .Include(p => p.Recruiter)
                .Include(p => p.Course)
                .ToListAsync();
        }

        public async Task<Placement> GetByIdAsync(int id)
        {
            return await _context.Placements
                .Include(p => p.Student)
                .Include(p => p.Batch)
                .Include(p => p.Recruiter)
                .Include(p => p.Course)
                .FirstOrDefaultAsync(p => p.PlacementId == id);
        }

        public async Task AddAsync(Placement placement)
        {
            await _context.Placements.AddAsync(placement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Placement placement)
        {
            _context.Placements.Update(placement);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Placement placement)
        {
            _context.Placements.Remove(placement);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
