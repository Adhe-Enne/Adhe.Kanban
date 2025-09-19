namespace Kanban.Contracts.Dto.Response
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
