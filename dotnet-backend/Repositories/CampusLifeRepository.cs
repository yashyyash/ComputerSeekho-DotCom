using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public class CampusLifeRepository : ICampusLifeRepository
    {
        private readonly ApplicationDbContext _context;

        public CampusLifeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CampusLife>> GetAllAsync()
        {
            return await _context.CampusLife.ToListAsync();
        }

        public async Task<CampusLife> GetByIdAsync(int id)
        {
            return await _context.CampusLife.FindAsync(id);
        }

        public async Task<CampusLife> AddAsync(CampusLife campusLife)
        {
            _context.CampusLife.Add(campusLife);
            await _context.SaveChangesAsync();
            return campusLife;
        }

        public async Task<CampusLife> UpdateAsync(CampusLife campusLife)
        {
            _context.CampusLife.Update(campusLife);
            await _context.SaveChangesAsync();
            return campusLife;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var campusLife = await _context.CampusLife.FindAsync(id);
            if (campusLife == null)
                return false;

            _context.CampusLife.Remove(campusLife);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
