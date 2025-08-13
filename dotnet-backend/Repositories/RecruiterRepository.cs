using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_backend.Models;
using dotnet_backend.AppDbContext;

namespace dotnet_backend.Repositories
{
    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly ApplicationDbContext _context;

        public RecruiterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recruiter>> GetAllAsync()
        {
            return await _context.Recruiters.ToListAsync();
        }

        public async Task<Recruiter> GetByIdAsync(int id)
        {
            return await _context.Recruiters.FindAsync(id);
        }

        public async Task AddAsync(Recruiter recruiter)
        {
            await _context.Recruiters.AddAsync(recruiter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recruiter recruiter)
        {
            _context.Recruiters.Update(recruiter);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Recruiter recruiter)
        {
            _context.Recruiters.Remove(recruiter);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
