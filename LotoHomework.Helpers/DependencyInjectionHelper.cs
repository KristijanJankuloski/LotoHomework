using LotoHomework.DataAccess.Context;
using LotoHomework.DataAccess.Repositories.Implementations;
using LotoHomework.DataAccess.Repositories.Interfaces;
using LotoHomework.Services.Implementations;
using LotoHomework.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LotoHomework.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LotoDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
