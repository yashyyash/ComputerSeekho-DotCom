using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class EnquiryService : IEnquiryService
    {
        private readonly IEnquiryRepository _repository;

        public EnquiryService(IEnquiryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EnquiryResponseDto>> GetAllAsync()
        {
            var enquiries = await _repository.GetAllAsync();
            return enquiries.Select(e => e.ToDto());
        }

        public async Task<EnquiryResponseDto> GetByIdAsync(int id)
        {
            var enquiry = await _repository.GetByIdAsync(id);
            return enquiry?.ToDto();
        }

        public async Task<EnquiryResponseDto> CreateAsync(EnquiryRequestDto dto)
        {
            var entity = dto.ToEntity();
            await _repository.AddAsync(entity);
            return entity.ToDto();
        }

        public async Task<EnquiryResponseDto> AddFollowUpAsync(int enquiryId, FollowUpDto followUpDto)
        {
            var enquiry = await _repository.GetByIdAsync(enquiryId);
            if (enquiry == null) return null;

            enquiry.FollowUps.Add(new FollowUp
            {
                EnquiryId = enquiryId,
                FollowupDate = followUpDto.FollowupDate,
                Notes = followUpDto.Notes,
                Status = followUpDto.Status
            });

            // Custom Logic: Auto-close if 5 follow-ups
            if (enquiry.FollowUps.Count >= 5 && enquiry.Status == EnquiryStatus.Open)
            {
                enquiry.Status = EnquiryStatus.Close;
            }
            
            await _repository.UpdateAsync(enquiry);
            return enquiry.ToDto();
        }
        public async Task<EnquiryResponseDto?> UpdateAsync(int id, EnquiryRequestDto dto)
        {
            var enquiry = await _repository.GetByIdAsync(id);
            if (enquiry == null)
                return null;

            // Update fields from DTO
            enquiry.StaffId = dto.StaffId;
            enquiry.CourseId = dto.CourseId;
            enquiry.EnquirerName = dto.EnquirerName;
            enquiry.EnquiryAddress = dto.EnquiryAddress;
            enquiry.InquirerEmail = dto.InquirerEmail;
            enquiry.StudentName = dto.StudentName;
            enquiry.StudentAge = dto.StudentAge;
            enquiry.StudentGender = dto.StudentGender;
            enquiry.StudentDob = dto.StudentDob;
            enquiry.StudentEmail = dto.StudentEmail;
            enquiry.StudentPhotoUrl = dto.StudentPhotoUrl;
            enquiry.EnquiryQuery = dto.EnquiryQuery;
            enquiry.Status = dto.Status;
            //enquiry.CreatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(enquiry);

            return enquiry.ToDto();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
