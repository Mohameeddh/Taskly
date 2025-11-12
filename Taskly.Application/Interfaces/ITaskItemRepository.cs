using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly.Domain.Entities;

namespace Taskly.Application.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem?> GetByIdAsync(int Id, CancellationToken ct = default);
        Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(TaskItem item, CancellationToken ct = default);
        Task UpdateAsync(TaskItem item, CancellationToken ct = default);
        Task DeleteAsync(TaskItem item, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
