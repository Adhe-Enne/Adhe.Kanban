namespace Kanban.Domain.Models
{
    public class Task : BaseModel
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo; // ToDo, InProgress, Done
        public TaskPriority Priority { get; set; } = TaskPriority.Medium; // Low, Medium, High
        public DateTime? DueDate { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; } = default!;
        public Guid ColumnId { get; set; }
        public Column Column { get; set; } = default!;
        public Guid? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}