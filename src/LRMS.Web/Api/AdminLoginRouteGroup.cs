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
                .WithDescription("Вход для админа.")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

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
            return Results.Ok(new { token });
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
    ///     Данные для входа в админку.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    public record LoginRequest(string Login, string Password);
}
