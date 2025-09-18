using System.ComponentModel.DataAnnotations;

namespace Kanban.Domain.Models
{
    public class User : BaseModel
    {
        [EmailAddress]
        public string Email { get; set; } = default!;

        [DataType(DataType.Password)]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? Name { get; set; } = default!;
        public string? DNI { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public string? Image { get; set; }
        public string? City { get; set; } = default!;
        public string? Country { get; set; } = default!;
        public List<Task> Tasks { get; set; } = new List<Task>(); 
        public ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
