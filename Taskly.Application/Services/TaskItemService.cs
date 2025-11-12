using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Taskly.Application.DTOs;
using Taskly.Application.Interfaces;
using Taskly.Domain.Entities;

namespace Taskly.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _repo;
        public TaskItemService(ITaskItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<TaskItemDto> CreateAsync(CreateTaskItemRequest req, CancellationToken ct = default)
        {
            var entity = new TaskItem { Title = req.Title, Description = req.Description, DueDate = req.DueDate };
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return Map(entity);
        }

        public async Task<TaskItemDto?> GetAsync(int id, CancellationToken ct = default) => (await _repo.GetByIdAsync(id, ct)) is { } e ? Map(e) : null;

        public async Task<IReadOnlyList<TaskItemDto>> GetAllAsync(CancellationToken ct = default) => (await _repo.GetAllAsync(ct)).Select(Map).ToList();

        public async Task<TaskItemDto?> UpdateAsync(int id, UpdateTaskItemRequest req, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return null;
            entity.Update(req.Title, req.Description, req.DueDate);
            await _repo.UpdateAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return Map(entity);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;
            await _repo.DeleteAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> CompleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;
            entity.Complete();
            await _repo.UpdateAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> ReopenAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;
            entity.Reopen();
            await _repo.UpdateAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }

        private static TaskItemDto Map(TaskItem e) => new(
        e.Id, e.Title, e.Description, e.IsCompleted, e.DueDate, e.CreatedAt, e.UpdatedAt
        );
    }
}
