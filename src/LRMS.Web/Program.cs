using LRMS.Application.Books.Dto;
using LRMS.Application.Extensions;
using LRMS.Infrastructure.Extensions;
using LRMS.Infrastructure.Persistence;
using LRMS.Web.Extensions;
using LRMS.Web.GraphQL.Query;
using LRMS.Web.Middleware;
using LRMS.Web.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi(c => c.AddOperationTransformer(new ReturnCodeOpenApiOperationTransformer())
            .AddSchemaTransformer(new EnumSchemaTransformer(new(false), false))
            .AddDocumentTransformer((document, context, cancellationToken) =>
            {
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Введите ваш JWT токен"
                };

                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
                {
                    ["Bearer"] = bearerScheme
                };

                return Task.CompletedTask;
            })
            .AddOperationTransformer((operation, context, cancellationToken) =>
            {
                var metadata = context.Description.ActionDescriptor.EndpointMetadata;
                var requiresAuthorization = metadata.Any(m =>
                    m is Microsoft.AspNetCore.Authorization.AuthorizeAttribute);

                if (requiresAuthorization)
                {
                    operation.Security =
                    [
                        new OpenApiSecurityRequirement
                        {
                            [new OpenApiSecuritySchemeReference("Bearer")] = []
                        }
                    ];
                }

                return Task.CompletedTask;
            }));

        builder.Services
            .AddGraphQLServer()
            .AddQueryType(q => q.Name("Query"))
            .AddType<GetBooksQuery>()
            .AddType<BookDto>()
            .AddSorting()
            .AddFiltering()
            .AddPagingArguments();

        builder.Services.ConfigureOptions();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();

        if (!IsBuildTask())
            RegisterServices(builder);

        var app = builder.Build();

        if (!IsBuildTask())
            app.UseExceptionHandler(_ => { });

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapApi();
        app.MapGraphQL();

        if (!IsBuildTask())
        {
            app.UseFileServer();
            InitializeDatabase(app.Services);
        }

        app.Run();
    }

    private static bool IsBuildTask() => Assembly.GetEntryAssembly()?.GetName().Name == "GetDocument.Insider";

    private static void RegisterServices(IHostApplicationBuilder builder)
    {
        builder.UseNpgsql(builder.Configuration.GetConnectionString(nameof(LrmsDbContext)));
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
