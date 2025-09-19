using TaskStatus = Kanban.Contracts.Enums.TaskStatus;
using TaskPriority = Kanban.Contracts.Enums.TaskPriority;

namespace Kanban.Contracts.Dto.Request
{
    public class TaskRequest
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime? DueDate { get; set; }
        public Guid BoardId { get; set; }
        public Guid ColumnId { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}
