using Kanban.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Business.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
