using Kanban.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Business.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> RegisterAsync(User user, string password);
        Task<User?> AuthenticateAsync(string email, string password);
    }
}
