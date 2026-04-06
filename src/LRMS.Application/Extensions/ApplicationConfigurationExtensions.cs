using LRMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LRMS.Application.Extensions;

public static class ApplicationConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterApplicationServices()
        {
            services.AddScoped<IReservationRequestService, ReservationRequestService>();
            services.AddScoped<ISpaceStateService, SpaceStateService>();
            return services;
        }
    }
}
