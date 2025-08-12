using dotnet_backend.Models;
using dotnet_backend.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_backend.Mappers
{
    public static class StudentMapper
    {
        public static StudentDTO ToDto(Student student)
        {
            if (student == null) return null;

            return new StudentDTO
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Email = student.Email,
                Age = student.Age,
                DueAmount = student.DueAmount
            };
        }

        public static List<StudentDTO> ToDtoList(IEnumerable<Student> students)
        {
            if (students == null) return new List<StudentDTO>();
            return students.Select(s => ToDto(s)).ToList();
        }

        public static Student ToEntity(StudentCreateDTO dto)
        {
            if (dto == null) return null;

            return new Student
            {
                EnquiryId = dto.EnquiryId,
                StudentName = dto.StudentName,
                StudentPhotoUrl = dto.StudentPhotoUrl,
                Age = dto.Age,
                Dob = dto.Dob,
                Email = dto.Email,
                CourseId = dto.CourseId,
                BatchId = dto.BatchId,
                RecruiterId = dto.RecruiterId,
                DueAmount = dto.DueAmount
            };
        }

        public static void UpdateEntity(Student student, StudentUpdateDTO dto)
        {
            if (student == null || dto == null) return;

            student.StudentName = dto.StudentName;
            student.StudentPhotoUrl = dto.StudentPhotoUrl;
            student.Age = dto.Age;
            student.Dob = dto.Dob;
            student.Email = dto.Email;
            student.DueAmount = dto.DueAmount;
        }
    }
}
