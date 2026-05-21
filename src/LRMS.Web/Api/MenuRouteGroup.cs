using LRMS.Application.Menu;
using LRMS.Application.Menu.Commands;
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

            group.MapPost("/category", CreateMenuCategory)
                .WithName("CreateMenuCategory")
                .WithDescription("Создает категорию меню с элементами.")
                .Produces<MenuCategoryDto>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(unprocessableErrorDescription: "В случае, если название категории меню или " +
                    "элемента категории меню пустое или длина превышает 255 символов, либо цена элемента категории меню ниже 0.")
                .RequireAuthorization();

            group.MapPut("/category/{id:long}", UpdateMenuCategory)
                .WithName("UpdateMenuCategory")
                .WithDescription("Обновляет категорию меню с элементами.")
                .Produces<MenuCategoryDto>()
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(notFoundDescription: "В случае, если категория меню не найдена.",
                    unprocessableErrorDescription: "В случае, если название категории меню или " +
                    "элемента категории меню пустое или длина превышает 255 символов, либо цена элемента категории меню ниже 0.")
                .RequireAuthorization();

            group.MapDelete("/category/{id:long}", DeleteMenuCategory)
                .WithName("DeleteMenuCategory")
                .WithDescription("Удаляет категорию меню.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status401Unauthorized)
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
            long id,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            await service.DeleteMenuCategory(id, ct);
            return TypedResults.NoContent();
        }

        private static async Task<IResult> CreateMenuCategory(
            CreateMenuCategoryCommand menuCategoryForCreateDto,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.CreateMenuCategory(menuCategoryForCreateDto, ct));
        }

        private static async Task<IResult> UpdateMenuCategory(
            [Description("Идентификатор категории меню.")]
            long id,
            UpdateMenuCategoryCommand menuCategoryDto,
            [FromServices] IMenuService service,
            CancellationToken ct = default)
        {
            await service.UpdateMenuCategory(id, menuCategoryDto, ct);
            return TypedResults.Ok();
        }
    }
}
