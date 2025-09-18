using Kanban.Repository.Interfaces;
using Kanban.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Repository
{
    public static class Startup
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IService, Service>();
            // Add business services here
        }
    }
}
