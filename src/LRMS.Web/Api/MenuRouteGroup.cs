using LRMS.Application.Queries;
using LRMS.Application.Requests;
using LRMS.Application.Services;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LRMS.Web.Api;

public static class MenuRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapMenuApi()
        {
            var group = endpointRouteBuilder.MapGroup("/menu");

            group.MapGet("/", GetMenu)
                .WithName("GetMenu")
                .WithDescription("Возвращает информацию о меню.")
                .Produces<GetMenuResponse>()
                .ProducesCommonErrors();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> GetMenu(
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetMenu(ct));
        }
    }
}
