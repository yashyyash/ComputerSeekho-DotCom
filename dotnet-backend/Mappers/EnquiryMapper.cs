using dotnet_backend.DTOs;
using dotnet_backend.Models;
using System.Linq;

namespace dotnet_backend.Mappers
{
    public static class EnquiryMapper
    {
        public static Enquiry ToEntity(this EnquiryRequestDto dto)
        {
            return new Enquiry
            {
                StaffId = dto.StaffId,
                CourseId = dto.CourseId,
                EnquirerName = dto.EnquirerName,
                EnquiryAddress = dto.EnquiryAddress,
                InquirerEmail = dto.InquirerEmail,
                StudentName = dto.StudentName,
                StudentAge = dto.StudentAge,
                StudentGender = dto.StudentGender,
                StudentDob = dto.StudentDob,
                StudentEmail = dto.StudentEmail,
                StudentPhotoUrl = dto.StudentPhotoUrl,
                EnquiryQuery = dto.EnquiryQuery,
                Status = EnquiryStatus.Open,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                FollowUps = new List<FollowUp>()
            };
        }

        public static EnquiryResponseDto ToDto(this Enquiry entity)
        {
            return new EnquiryResponseDto
            {
                EnquiryId = entity.EnquiryId,
                EnquirerName = entity.EnquirerName,
                StudentName = entity.StudentName,
                Status = entity.Status.ToString(),
                ClosureReason = entity.ClosureReason?.ReasonText,
                CreatedAt = entity.CreatedAt,
                FollowUps = entity.FollowUps?
                    .Select(f => new FollowUpDto
                    {
                        FollowupId = f.FollowupId,
                        FollowupDate = f.FollowupDate,
                        Notes = f.Notes,
                        Status = f.Status
                    }).ToList()
            };
        }
    }
}
