using dotnet_backend.Models;
using dotnet_backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Services
{
    public class PlacementService : IPlacementService
    {
        private readonly AppDbContext _context;

        public PlacementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Placement>> GetAllAsync()
        {
            return await _context.Placements
                .Include(p => p.Recruiter)
                .Include(p => p.Batch)
                .ToListAsync();
        }

        public async Task<Placement?> GetByIdAsync(int id)
        {
            return await _context.Placements
                .Include(p => p.Recruiter)
                .Include(p => p.Batch)
                .FirstOrDefaultAsync(p => p.PlacementId == id);
        }

        public async Task<Placement> CreateAsync(Placement placement)
        {
            _context.Placements.Add(placement);
            await _context.SaveChangesAsync();
            return placement;
        }

        public async Task<bool> UpdateAsync(int id, Placement placement)
        {
            var existing = await _context.Placements.FindAsync(id);
            if (existing == null) return false;

            existing.StudentId = placement.StudentId;
            existing.StudentName = placement.StudentName;
            existing.StudentPhoto = placement.StudentPhoto;
            existing.RecruiterId = placement.RecruiterId;
            existing.BatchId = placement.BatchId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Placements.FindAsync(id);
            if (existing == null) return false;

            _context.Placements.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }


        public IEnumerable<Placement> GetPlacementsByBatchId(int batchId)
        {
            return _context.Placements
                           .Where(p => p.BatchId == batchId)
                           .Include(p => p.Recruiter)
                           .Include(p => p.Batch)
                           .ToList();
        }
    }
}
