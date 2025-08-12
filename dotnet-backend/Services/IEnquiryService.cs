using dotnet_backend.DTOs;

namespace dotnet_backend.Services
{
    public interface IEnquiryService
    {
        Task<IEnumerable<EnquiryResponseDto>> GetAllAsync();
        Task<EnquiryResponseDto> GetByIdAsync(int id);
        Task<EnquiryResponseDto> CreateAsync(EnquiryRequestDto dto);
        Task<EnquiryResponseDto> AddFollowUpAsync(int enquiryId, FollowUpDto followUpDto);
        Task<EnquiryResponseDto?> UpdateAsync(int id, EnquiryRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
