using TaskStatus = Kanban.Contracts.Enums.TaskStatus;
using TaskPriority = Kanban.Contracts.Enums.TaskPriority;

namespace Kanban.Contracts.Dto.Response
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid BoardId { get; set; }
        public Guid ColumnId { get; set; }
        public Guid? AssignedUserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
