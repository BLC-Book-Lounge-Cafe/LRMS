using LRMS.Application.BookReservations;
using LRMS.Application.Books;
using LRMS.Application.Menu;
using LRMS.Application.ReservationRequests;
using LRMS.Application.SpaceState;
using LRMS.Application.TableReservations;
using LRMS.Application.Tables;
using LRMS.Infrastructure.Music;
using LRMS.Infrastructure.Persistence;
using LRMS.Infrastructure.Persistence.BookReservations;
using LRMS.Infrastructure.Persistence.Books;
using LRMS.Infrastructure.Persistence.Menu;
using LRMS.Infrastructure.Persistence.ReservationRequests;
using LRMS.Infrastructure.Persistence.Seeder;
using LRMS.Infrastructure.Persistence.SpaceState;
using LRMS.Infrastructure.Persistence.TableReservations;
using LRMS.Infrastructure.Persistence.Tables;
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
            services.AddScoped<IBookGraphQLRepository, BookRepository>();
            services.AddScoped<IBookRepository>(p => p.GetRequiredService<IBookGraphQLRepository>());
            services.AddScoped<IBookReservationRepository, BookReservationRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddHostedService<MusicManager>();

            return services;
        }
    }
}
