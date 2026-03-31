using LRMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LRMS.Infrastructure.Extensions;

public static class InfrastructureConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection UseNpgsql(string? connectionString)
        {
            services.AddDbContext<LrmsDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
