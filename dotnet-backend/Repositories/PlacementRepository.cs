using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_backend.Models;
using dotnet_backend.AppDbContext;

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
            return await _context.Placements.ToListAsync();
        }

        public async Task<Placement> GetByIdAsync(int id)
        {
            return await _context.Placements.FindAsync(id);
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
