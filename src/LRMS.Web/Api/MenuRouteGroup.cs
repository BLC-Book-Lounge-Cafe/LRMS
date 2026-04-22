using LRMS.Application.Menu;
using LRMS.Application.Menu.Requests;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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

            group.MapDelete("/category/{id:int}", DeleteMenuCategory)
                .WithName("DeleteMenuCategory")
                .WithDescription("Удаляет категорию меню.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не удалось найти категорию меню.");

            group.MapDelete("/item/{id:int}", DeleteMenuItem)
                .WithName("DeleteMenuItem")
                .WithDescription("Удаляет элемент меню.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не удалось найти элемент меню.");

            return endpointRouteBuilder;
        }

        private static async Task<IResult> GetMenu(
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetMenu(ct));
        }

        private static async Task<IResult> DeleteMenuCategory(
            [Description("Идентификатор категории меню.")]
            int id,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            await service.DeleteMenuCategory(id, ct);
            return TypedResults.Ok();
        }

        private static async Task<IResult> DeleteMenuItem(
            [Description("Идентификатор элемента меню.")]
            int id,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            await service.DeleteMenuItem(id, ct);
            return TypedResults.Ok();
        }
    }
}
