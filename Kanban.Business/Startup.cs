using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanban.Business.Services;
using Kanban.Business.Services.Interfaces;

namespace Kanban.Business
{
    public static class Startup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService,JwtService >();
            //services.AddScoped<IService, Service>();
            // Add business services here
        }
    }
}
