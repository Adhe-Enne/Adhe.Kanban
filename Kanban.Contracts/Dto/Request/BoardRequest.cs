namespace Kanban.Contracts.Dto.Request
{
    public class BoardRequestDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
