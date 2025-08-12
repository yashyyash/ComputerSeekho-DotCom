using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public class GetInTouchRepository : IGetInTouchRepository
    {
        private readonly ApplicationDbContext _context;

        public GetInTouchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetInTouch>> GetAllAsync()
        {
            return await _context.GetInTouch.ToListAsync();
        }

        public async Task<GetInTouch> GetByIdAsync(int id)
        {
            return await _context.GetInTouch.FindAsync(id);
        }

        public async Task<GetInTouch> AddAsync(GetInTouch entity)
        {
            _context.GetInTouch.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<GetInTouch> UpdateAsync(GetInTouch entity)
        {
            _context.GetInTouch.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.GetInTouch.FindAsync(id);
            if (entity == null)
                return false;

            _context.GetInTouch.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
