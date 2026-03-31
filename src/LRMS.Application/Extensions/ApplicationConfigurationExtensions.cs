using Microsoft.Extensions.DependencyInjection;

namespace LRMS.Application.Extensions;

public static class ApplicationConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterApplicationServices()
        {
            return services;
        }
    }
}
