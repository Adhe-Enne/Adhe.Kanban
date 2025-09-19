namespace Kanban.Contracts.Dto.Response
{
    public class BoardResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<ColumnResponse> Columns { get; set; } = new List<ColumnResponse>();
        public ICollection<TaskResponse> Tasks { get; set; } = new List<TaskResponse>();
    }
}
