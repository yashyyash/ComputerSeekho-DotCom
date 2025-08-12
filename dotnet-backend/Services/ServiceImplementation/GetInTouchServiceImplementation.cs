using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Repositories;

namespace dotnet_backend.Services.ServiceImplementation
{
    public class GetInTouchServiceImplementation : IGetInTouchService
    {
        private readonly IGetInTouchRepository _repository;

        public GetInTouchServiceImplementation(IGetInTouchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetInTouchDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(GetInTouchMapper.ToDTO);
        }

        public async Task<GetInTouchDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : GetInTouchMapper.ToDTO(entity);
        }

        public async Task<GetInTouchDTO> AddAsync(GetInTouchDTO dto)
        {
            var entity = GetInTouchMapper.ToEntity(dto);
            var created = await _repository.AddAsync(entity);
            return GetInTouchMapper.ToDTO(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
