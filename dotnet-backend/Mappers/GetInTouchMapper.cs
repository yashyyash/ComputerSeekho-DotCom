using dotnet_backend.DTOs;
using dotnet_backend.Models;

namespace dotnet_backend.Mappers
{
    public static class GetInTouchMapper
    {
        public static GetInTouch ToEntity(GetInTouchDTO dto)
        {
            return new GetInTouch
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message
            };
        }

        public static GetInTouchDTO ToDTO(GetInTouch entity)
        {
            return new GetInTouchDTO
            {
                Name = entity.Name,
                Email = entity.Email,
                Message = entity.Message
            };
        }
    }
}
