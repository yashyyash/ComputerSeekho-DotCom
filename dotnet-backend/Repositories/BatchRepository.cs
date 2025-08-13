using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly ApplicationDbContext _context;

        public BatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Batch>> GetAllAsync()
        {
            return await _context.Batches.ToListAsync();
        }

        public async Task<Batch> GetByIdAsync(int id)
        {
            return await _context.Batches.FindAsync(id);
        }

        public async Task AddAsync(Batch batch)
        {
            _context.Batches.Add(batch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Batch batch)
        {
            _context.Batches.Update(batch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch != null)
            {
                _context.Batches.Remove(batch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Batches.AnyAsync(b => b.BatchId == id);
        }
    }
}
