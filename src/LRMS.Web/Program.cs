using LRMS.Application.Books.Dto;
using LRMS.Application.Extensions;
using LRMS.Infrastructure.Extensions;
using LRMS.Infrastructure.Persistence;
using LRMS.Web.Extensions;
using LRMS.Web.GraphQL.Query;
using LRMS.Web.Middleware;
using LRMS.Web.OpenApi;
using Scalar.AspNetCore;
using System.Reflection;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi(c => c.AddOperationTransformer(new ReturnCodeOpenApiOperationTransformer()));
        builder.Services
            .AddGraphQLServer()
            .AddQueryType(q => q.Name("Query"))
            .AddType<GetBooksQuery>()
            .AddType<BookDto>()
            .AddSorting()
            .AddFiltering()
            .AddPagingArguments();

        builder.Services.ConfigureOptions();

        if (!IsBuildTask())
            RegisterServices(builder);

        var app = builder.Build();

        if (!IsBuildTask())
            app.UseExceptionHandler(_ => { });

        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapApi();
        app.MapGraphQL();

        if (!IsBuildTask())
            InitializeDatabase(app.Services);

        app.Run();
    }

    private static bool IsBuildTask() => Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";

    private static void RegisterServices(IHostApplicationBuilder builder)
    {
        builder.Services.UseNpgsql(builder.Configuration.GetConnectionString(nameof(LrmsDbContext)));
        builder.Services.RegisterApplicationServices();
        builder.Services.AddExceptionHandler<CommonExceptionHandler>();
    }

    private static void InitializeDatabase(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LrmsDbContext>().Database;
        db.EnsureDeleted();
        db.EnsureCreated();
    }
}
