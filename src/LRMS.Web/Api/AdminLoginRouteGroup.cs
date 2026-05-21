using LRMS.Web.OpenApi;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LRMS.Web.Api;

internal static class AdminLoginRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapAdminLoginApi()
        {
            var group = endpointRouteBuilder.MapGroup("/admin");

            group.MapPost("/login", Login)
                .WithName("Login")
                .WithDescription("Вход для администратора.")
                .Produces<LoginResult>()
                .ProducesWithDescription(StatusCodes.Status401Unauthorized,
                    "В случае, если не найден логин или не совпадает пароль.");

            return endpointRouteBuilder;
        }

        private static async Task<IResult> Login(
            LoginRequest request,
            IConfiguration config)
        {
            var adminsSection = config.GetSection("Admins");
            var admins = adminsSection.GetChildren();

            var admin = admins.FirstOrDefault(a =>
                a["Login"]?.Equals(request.Login, StringComparison.OrdinalIgnoreCase) == true);

            if (admin is null || admin["Password"] != request.Password)
                return Results.Unauthorized();

            var token = GenerateJwtToken(config, request.Login, admin["Role"] ?? "Admin");
            return Results.Ok(new LoginResult(token));
        }

        private static string GenerateJwtToken(IConfiguration config, string login, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, role),
                new Claim("login_time", DateTime.UtcNow.ToString("o"))
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: null,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    /// <summary>
    ///     Данные для входа для администратора.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    public record struct LoginRequest(string Login, string Password);

    /// <summary>
    ///     Результат входа администратора.
    /// </summary>
    /// <param name="Token">Токен.</param>
    public record struct LoginResult(string Token);
}
