using LRMS.Application.Extensions;
using LRMS.Infrastructure.Extensions;
using LRMS.Infrastructure.Persistence;
using LRMS.Web.Extensions;
using Scalar.AspNetCore;
using System.Reflection;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi();
        builder.Services.ConfigureOptions();

        if (!IsBuildTask())
            RegisterServices(builder);

        var app = builder.Build();
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapApi();

        if (!IsBuildTask())
            InitializeDatabase(app.Services);

        app.Run();
    }

    private static bool IsBuildTask() => Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";

    private static void RegisterServices(IHostApplicationBuilder builder)
    {
        builder.Services.UseNpgsql(builder.Configuration.GetConnectionString(nameof(LrmsDbContext)));
        builder.Services.RegisterApplicationServices();
    }

    private static void InitializeDatabase(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LrmsDbContext>().Database;
        //db.EnsureDeleted();
        db.EnsureCreated();
    }
}
