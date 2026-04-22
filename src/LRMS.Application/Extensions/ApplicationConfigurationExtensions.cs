using LRMS.Application.BookReservations;
using LRMS.Application.Menu;
using LRMS.Application.ReservationRequests;
using LRMS.Application.Services;
using LRMS.Application.SpaceState;
using LRMS.Application.TableReservations;
using LRMS.Application.Tables;
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
            services.AddScoped<ITableReservationService, TableReservationService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IBookReservationService, BookReservationService>();
            services.AddScoped<IMenuService, MenuService>();
            return services;
        }
    }
}
