using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Recruiter)
                .ToListAsync();
        }

        public async Task<Placement> GetByIdAsync(int id)
        {
            return await _context.Placements
                .Include(p => p.Batch)
                .Include(p => p.Course)
                .Include(p => p.Recruiter)
                .FirstOrDefaultAsync(p => p.PlacementId == id);
        }

        public async Task<Placement> AddAsync(Placement placement)
        {
            _context.Placements.Add(placement);
            await _context.SaveChangesAsync();
            return placement;
        }

        public async Task UpdateAsync(Placement placement)
        {
            _context.Placements.Update(placement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Placement placement)
        {
            _context.Placements.Remove(placement);
            await _context.SaveChangesAsync();
        }
    }
}
