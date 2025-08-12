using dotnet_backend.AppDbContext;
using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Repositories
{
    public class EnquiryRepository : IEnquiryRepository
    {
        private readonly ApplicationDbContext _context;

        public EnquiryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enquiry>> GetAllAsync()
        {
            return await _context.Enquiries
                .Include(e => e.FollowUps)
                .Include(e => e.ClosureReason)
                .ToListAsync();
        }

        public async Task<Enquiry> GetByIdAsync(int id)
        {
            return await _context.Enquiries
                .Include(e => e.FollowUps)
                .Include(e => e.ClosureReason)
                .FirstOrDefaultAsync(e => e.EnquiryId == id);
        }

        public async Task AddAsync(Enquiry enquiry)
        {
            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Enquiry enquiry)
        {
            _context.Enquiries.Update(enquiry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry != null)
            {
                _context.Enquiries.Remove(enquiry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
