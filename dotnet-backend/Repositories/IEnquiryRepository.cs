using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IEnquiryRepository
    {
        Task<IEnumerable<Enquiry>> GetAllAsync();
        Task<Enquiry> GetByIdAsync(int id);
        Task AddAsync(Enquiry enquiry);
        Task UpdateAsync(Enquiry enquiry);
        Task DeleteAsync(int id);
    }
}
