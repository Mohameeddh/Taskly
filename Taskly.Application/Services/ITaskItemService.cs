using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly.Application.DTOs;


namespace Taskly.Application.Services;


public interface ITaskItemService
{
    Task<TaskItemDto> CreateAsync(CreateTaskItemRequest req, CancellationToken ct = default);
    Task<TaskItemDto?> GetAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<TaskItemDto>> GetAllAsync(CancellationToken ct = default);
    Task<TaskItemDto?> UpdateAsync(int id, UpdateTaskItemRequest req, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<bool> CompleteAsync(int id, CancellationToken ct = default);
    Task<bool> ReopenAsync(int id, CancellationToken ct = default);
}
