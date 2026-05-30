using LRMS.Application.Books;
using LRMS.Application.Menu;
using LRMS.Application.SpaceState;
using LRMS.Application.Tables;
using LRMS.Infrastructure.Music;
using LRMS.Infrastructure.Options;
using LRMS.Infrastructure.Persistence;
using LRMS.Infrastructure.Persistence.Books;
using LRMS.Infrastructure.Persistence.Menu;
using LRMS.Infrastructure.Persistence.Seeder;
using LRMS.Infrastructure.Persistence.SpaceState;
using LRMS.Infrastructure.Persistence.Tables;
using LRMS.Infrastructure.ReservationManagerApi.Books;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests;
using LRMS.Infrastructure.ReservationManagerApi.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace LRMS.Infrastructure.Extensions;

public static class InfrastructureConfigurationExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        public IServiceCollection UseNpgsql(string? connectionString)
        {
            builder.Services.AddDbContext<LrmsDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseSeeding((context, _) =>
                {
                    if (context is not LrmsDbContext lrmsDbContext)
                        throw new Exception("Invalid type for DbContext.");

                    var seeder = new LrmsSeeder(lrmsDbContext);
                    seeder.Seed();
                }));

            builder.Services.AddScoped<IReservationRequestRepository, ReservationRequestRepository>();
            builder.Services.AddScoped<ISpaceStateRepository, SpaceStateRepository>();
            builder.Services.AddScoped<ITableReservationRepository, TableReservationRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IBookGraphQLRepository, BookRepository>();
            builder.Services.AddScoped<IBookRepository>(p => p.GetRequiredService<IBookGraphQLRepository>());
            builder.Services.AddScoped<IBookReservationRepository, BookReservationRepository>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddHostedService<MusicManager>();

            var reservationManagerApiOptions = builder.Configuration
                .GetSection("ReservationManagerApiOptions")
                .Get<ReservationManagerApiOptions>();

            builder.Services
                .AddRefitClient<IReservationRequestApi>()
                .AddRefitClient<ITableApi>()
                .AddRefitClient<IBookApi>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(reservationManagerApiOptions.Address);
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {reservationManagerApiOptions.Token}");
                });

            return builder.Services;
        }
    }
}
