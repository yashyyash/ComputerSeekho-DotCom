using dotnet_backend.DTOs;
using dotnet_backend.Models;

namespace dotnet_backend.Mappers
{
    public static class RecruiterMapper
    {
        public static RecruiterDTO ToDto(Recruiter recruiter)
        {
            if (recruiter == null) return null;

            return new RecruiterDTO
            {
                RecruiterId = recruiter.RecruiterId,
                CompanyName = recruiter.CompanyName,
                RecruiterPhotoUrl = recruiter.RecruiterPhotoUrl
            };
        }

        public static Recruiter ToEntity(RecruiterCreateDTO dto)
        {
            if (dto == null) return null;

            return new Recruiter
            {
                CompanyName = dto.CompanyName,
                RecruiterPhotoUrl = dto.RecruiterPhotoUrl
            };
        }

        public static void UpdateEntity(Recruiter entity, RecruiterUpdateDTO dto)
        {
            if (entity == null || dto == null) return;

            entity.CompanyName = dto.CompanyName;
            entity.RecruiterPhotoUrl = dto.RecruiterPhotoUrl;
        }
    }
}
