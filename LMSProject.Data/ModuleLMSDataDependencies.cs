using LMSProject.Data.Abstracts;
using LMSProject.Data.Data;
using LMSProject.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMSProject.Data
{
    public static class ModuleLMSDataDependencies
    {
        public static IServiceCollection AddLMSDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
