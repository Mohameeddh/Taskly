using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; private set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        public void Complete()
        {
            if (IsCompleted)
                return;
            IsCompleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reopen()
        {
            if (!IsCompleted)
                return;
            IsCompleted = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string title, string? description, DateTime? dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
