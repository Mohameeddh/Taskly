using Microsoft.EntityFrameworkCore;
using Taskly.Application.Interfaces;
using Taskly.Domain.Entities;
using Taskly.Infrastructure.Persistence;

namespace Taskly.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly TasklyDbContext _db;
        public TaskItemRepository(TasklyDbContext db)
        {
            _db = db;
        }

        public Task<TaskItem?> GetByIdAsync(int id, CancellationToken ct = default)
            => _db.TaskItems.FirstOrDefaultAsync(x => x.Id == id, ct);

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken ct = default)
            => await _db.TaskItems.OrderByDescending(t => t.CreatedAt).ToListAsync(ct);

        public async Task AddAsync(TaskItem item, CancellationToken ct = default)
            => await _db.TaskItems.AddAsync(item, ct);

        public Task UpdateAsync(TaskItem item, CancellationToken ct = default)
        {
            _db.TaskItems.Update(item);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskItem item, CancellationToken ct = default)
        {
            _db.TaskItems.Remove(item);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);
    }
}
