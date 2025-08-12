using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class GetInTouchRepository : IGetInTouchRepository
    {
        private readonly ApplicationDbContext _context;

        public GetInTouchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetInTouch>> GetAllAsync()
        {
            return await _context.GetInTouches.ToListAsync();
        }

        public async Task<GetInTouch> GetByIdAsync(int id)
        {
            return await _context.GetInTouches.FindAsync(id);
        }

        public async Task<GetInTouch> AddAsync(GetInTouch contact)
        {
            _context.GetInTouches.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.GetInTouches.FindAsync(id);
            if (entity == null) return false;

            _context.GetInTouches.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
