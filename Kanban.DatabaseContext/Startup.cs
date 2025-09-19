using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.DatabaseContext
{
    public static class Startup
    {
        public static void AddContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<TechnicalTestDbContext>(options => 
                options.UseMySql(
                    connectionString, new MySqlServerVersion(new Version(8, 0, 43)), mySqlOptions => mySqlOptions.EnableRetryOnFailure()
                    )
                );
        }
    }
}
