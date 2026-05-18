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
            var adminPassword = config["AdminPassword"];

            if (request.Password != adminPassword)
                return Results.Unauthorized();

            var token = GenerateJwtToken(config);

            return Results.Ok(new { token });
        }

        private static string GenerateJwtToken(IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: null, // Токен без срока действия
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public record LoginRequest(string Password);
}
