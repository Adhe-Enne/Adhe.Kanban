using Kanban.Business.Services;
using Kanban.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Business
{
    public static class Startup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ITaskService, TaskService>();
            //services.AddScoped<IStateService, StateService<>();

            // Add business services here
        }
    }
}
