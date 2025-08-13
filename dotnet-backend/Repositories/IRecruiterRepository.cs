using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_backend.Models;

namespace dotnet_backend.Repositories
{
    public interface IRecruiterRepository
    {
        Task<IEnumerable<Recruiter>> GetAllAsync();
        Task<Recruiter> GetByIdAsync(int id);
        Task AddAsync(Recruiter recruiter);
        Task UpdateAsync(Recruiter recruiter);
        Task<bool> DeleteAsync(Recruiter recruiter);
    }
}
