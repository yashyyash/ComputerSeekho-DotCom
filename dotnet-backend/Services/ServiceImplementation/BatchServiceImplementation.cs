using dotnet_backend.AppDbContext;
using dotnet_backend.DTOs;
using dotnet_backend.Mappers;
using dotnet_backend.Models;
using dotnet_backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend.Services
{
    public class BatchServiceImplementation : IBatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBatchRepository _batchRepository;

        public BatchServiceImplementation(ApplicationDbContext context, IBatchRepository batchRepository)
        {
            _context = context;
            _batchRepository = batchRepository;
        }

        public async Task<IEnumerable<BatchDto>> GetAllBatchesAsync()
        {
            var batches = await _context.Batches
                .Include(b => b.Course) // direct relation now
                .ToListAsync();

            return batches.Select(BatchMapper.ToDto).ToList();
        }

        public async Task<BatchDto> GetBatchByIdAsync(int id)
        {
            var batch = await _context.Batches
                .Include(b => b.Course) // direct relation now
                .FirstOrDefaultAsync(b => b.BatchId == id);

            return BatchMapper.ToDto(batch);
        }

        public async Task<BatchDto> CreateBatchAsync(BatchDto batchDto)
        {
            var batch = BatchMapper.ToEntity(batchDto);
            await _batchRepository.AddAsync(batch);

            // Reload with related Course for CourseName
            var savedBatch = await _context.Batches
                .Include(b => b.Course)
                .FirstOrDefaultAsync(b => b.BatchId == batch.BatchId);

            return BatchMapper.ToDto(savedBatch);
        }

        public async Task<bool> UpdateBatchAsync(int id, BatchDto batchDto)
        {
            if (!await _batchRepository.ExistsAsync(id))
                return false;

            var batch = await _batchRepository.GetByIdAsync(id);
            if (batch == null) return false;

            batch.BatchName = batchDto.BatchName;
            batch.BatchPhotoUrl = batchDto.BatchPhotoUrl;
            batch.StartDate = batchDto.StartDate;
            batch.EndDate = batchDto.EndDate;
            batch.CourseId = batchDto.CourseId; // if updating course

            await _batchRepository.UpdateAsync(batch);
            return true;
        }

        public async Task<bool> DeleteBatchAsync(int id)
        {
            if (!await _batchRepository.ExistsAsync(id))
                return false;

            await _batchRepository.DeleteAsync(id);
            return true;
        }
    }
}
