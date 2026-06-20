using LRMS.Application.Books;
using LRMS.Application.Menu;
using LRMS.Application.SpaceState;
using LRMS.Application.Tables;
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
using LRMS.IntegrationTests.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Refit;

namespace LRMS.Infrastructure.IntegrationTests.Fixtures;

public class ServiceFixture : IServiceProvider, IDisposable
{
    private readonly Dictionary<string, string> _connectionStringParts = new()
    {
        { "Host", "POSTGRES_HOST" },
        { "Port", "POSTGRES_PORT" },
        { "User Id", "POSTGRES_USER" },
        { "Password", "POSTGRES_PASSWORD" }
    };
    private readonly ServiceProvider _services;
    public LrmsDbContext Context { get; private set; }
    private bool _IsDisposed;

    public ServiceFixture()
    {
        var services = new ServiceCollection();

        services.AddDbContext<LrmsDbContext>(options => options
            .UseNpgsql(GetConnectionString())
            .UseSeeding((context, _) =>
            {
                if (context is not LrmsDbContext lrmsDbContext)
                    throw new Exception("Invalid type for DbContext.");

                var seeder = new LrmsSeeder(lrmsDbContext);
                seeder.Seed();
            }))
            .AddLogging()
            .AddScoped<IBookGraphQLRepository, BookRepository>()
            .AddScoped<IBookRepository>(p => p.GetRequiredService<IBookGraphQLRepository>())
            .AddScoped<ITableRepository, TableRepository>()
            .AddScoped<ISpaceStateRepository, SpaceStateRepository>()
            .AddScoped<IMenuRepository, MenuRepository>();

        services
            .AddRefitClient<IReservationRequestApi>()
            .AddRefitClient<ITableApi>()
            .AddRefitClient<IBookApi>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(BackendOptionsProvider.Address);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {BackendOptionsProvider.Token}");
            });

        _services = services.BuildServiceProvider();

        Context = _services.GetRequiredService<LrmsDbContext>();
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    private string GetConnectionString()
    {
        var environmentVariables = EnvironmentVariablesExtractor.Get(AppContext.BaseDirectory,
            [.. _connectionStringParts.Values]);
        var connectionString = new NpgsqlConnectionStringBuilder();
        foreach (var connectionStringPart in _connectionStringParts)
            connectionString[connectionStringPart.Key] = environmentVariables[connectionStringPart.Value];

        string dbContextName = typeof(LrmsDbContext).Name;
        connectionString.Database = $"{connectionString.Database}-{dbContextName}";
        return connectionString.ConnectionString;
    }

    public void Dispose()
    {
        if (_IsDisposed)
            return;

        _IsDisposed = true;
        Context.Database.EnsureDeleted();
        _services.Dispose();
        Context.Dispose();
    }

    public object? GetService(Type serviceType)
    {
        return _services.GetService(serviceType);
    }
}
