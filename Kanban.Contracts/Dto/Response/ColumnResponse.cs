namespace Kanban.Contracts.Dto.Response
{
    public class ColumnResponse
    {
        public string Name { get; set; } = default!;
        public int Order { get; set; }
        public Guid BoardId { get; set; }
        public BoardResponse Board { get; set; } = default!;
        public ICollection<TaskResponse> Tasks { get; set; } = new List<TaskResponse>();
    }
}
