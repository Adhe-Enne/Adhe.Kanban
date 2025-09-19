using Kanban.Domain.Models;

namespace Kanban.Business.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
