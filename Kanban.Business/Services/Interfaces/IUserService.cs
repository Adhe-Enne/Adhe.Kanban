using Kanban.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace Kanban.Business.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> RegisterAsync(User user, string password);
        Task<User?> AuthenticateAsync(string email, string password);
        Task UpdateRoleAsync(Guid userId, string newRole);
         Task ActivateUserAsync(Guid userId);
         Task InactivateUserAsync(Guid userId);
    }
}
