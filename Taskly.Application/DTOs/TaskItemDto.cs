using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly.Application.DTOs
{
    public record TaskItemDto
    (
        int Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime? DueDate,
        DateTime CreatedAt,
        DateTime? UpdatedAt

    );

    public record CreateTaskItemRequest(string Title, string? Description, DateTime? DueDate);
    public record UpdateTaskItemRequest(string Title, string? Description, DateTime? DueDate);
}
