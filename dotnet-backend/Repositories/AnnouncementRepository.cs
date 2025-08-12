using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_backend.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Announcement>> GetAllAsync()
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
            // Since AnnouncementId is primary key and tracked by EF,
            // just update the entity normally
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
                return false;

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
