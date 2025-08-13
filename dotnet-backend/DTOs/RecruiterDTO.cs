// RecruiterDTO.cs
namespace dotnet_backend.DTOs
{
    public class RecruiterDTO
    {
        public int RecruiterId { get; set; }
        public string CompanyName { get; set; }
        public string RecruiterPhotoUrl { get; set; }
    }

    public class RecruiterCreateDTO
    {
        public string CompanyName { get; set; }
        public string RecruiterPhotoUrl { get; set; }
    }

    public class RecruiterUpdateDTO
    {
        public string CompanyName { get; set; }
        public string RecruiterPhotoUrl { get; set; }
    }
}
