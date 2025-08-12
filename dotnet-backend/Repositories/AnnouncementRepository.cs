using dotnet_backend.Models;
using dotnet_backend.AppDbContext; 
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context; 

        public AnnouncementRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> GetByIdAsync(int id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task<Announcement> AddAsync(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<Announcement> UpdateAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Announcements.FindAsync(id);
            if (entity == null) return false;

            _context.Announcements.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
