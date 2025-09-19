namespace Kanban.Domain.Models
{
    public class Column : BaseModel
    {
        public string Name { get; set; } = default!;
        public int Order { get; set; }

        public Guid BoardId { get; set; }
        public Board Board { get; set; } = default!;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
