using LRMS.Application.Repositories;
using LRMS.Infrastructure.Persistence;
using LRMS.Infrastructure.Persistence.Repositories;
using LRMS.Infrastructure.Persistence.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LRMS.Infrastructure.Extensions;

public static class InfrastructureConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection UseNpgsql(string? connectionString)
        {
            services.AddDbContext<LrmsDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseSeeding((context, _) =>
                {
                    if (context is not LrmsDbContext lrmsDbContext)
                        throw new Exception("Invalid type for DbContext.");

                    var seeder = new LrmsSeeder(lrmsDbContext);
                    seeder.Seed();
                }));

            services.AddScoped<IReservationRequestRepository, ReservationRequestRepository>();
            services.AddScoped<ISpaceStateRepository, SpaceStateRepository>();
            services.AddScoped<ITableReservationRepository, TableReservationRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookReservationRepository, BookReservationRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            return services;
        }
    }
}
