using Kanban.Domain.Models;
using Kanban.Repository.Interfaces;
using Kanban.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Task = Kanban.Domain.Models.Task;
namespace Kanban.Repository
{
    public static class Startup
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<Board>, Repository<Board>>();
            services.AddScoped<IRepository<Task>, Repository<Task>>();
        }
    }
}
