using LRMS.Application.Menu;
using LRMS.Application.Menu.Dto;
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

            group.MapPost("/", CreateMenuCategory)
                .WithName("CreateMenuCategory")
                .WithDescription("Создает категорию меню с элементами.")
                .Produces<MenuCategoryDto>(StatusCodes.Status201Created)
                .ProducesCommonErrors()
                .RequireAuthorization();

            group.MapPut("/", UpdateMenuCategory)
                .WithName("UpdateMenuCategory")
                .WithDescription("Обновляет категорию меню с элементами.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если категория меню не найдена.")
                .RequireAuthorization();

            group.MapDelete("/category/{id:int}", DeleteMenuCategory)
                .WithName("DeleteMenuCategory")
                .WithDescription("Удаляет категорию меню.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не удалось найти категорию меню.")
                .RequireAuthorization();

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

        private static async Task<IResult> CreateMenuCategory(
            MenuCategoryForCreateDto menuCategoryForCreateDto,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.CreateMenuCategory(menuCategoryForCreateDto, ct));
        }

        private static async Task<IResult> UpdateMenuCategory(
            MenuCategoryDto menuCategoryDto,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            await service.UpdateMenuCategory(menuCategoryDto, ct);
            return TypedResults.Ok();
        }
    }
}
