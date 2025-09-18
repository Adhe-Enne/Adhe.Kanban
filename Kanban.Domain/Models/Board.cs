using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Domain.Models
{
    public class Board : BaseModel
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = default!;

        public ICollection<Column> Columns { get; set; } = new List<Column>();
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
