using LRMS.Application.Books;
using LRMS.Application.Menu;
using LRMS.Application.SpaceState;
using LRMS.Application.Tables;
using Microsoft.Extensions.DependencyInjection;

namespace LRMS.Application.Extensions;

public static class ApplicationConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterApplicationServices()
        {
            services.AddScoped<ISpaceStateService, SpaceStateService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}
