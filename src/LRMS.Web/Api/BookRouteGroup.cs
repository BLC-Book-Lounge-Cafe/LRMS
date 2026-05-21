using LRMS.Application.Books;
using LRMS.Application.Books.Commands;
using LRMS.Application.Books.Dto;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LRMS.Web.Api;

public static class BookRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapBooksApi()
        {
            var group = endpointRouteBuilder.MapGroup("/books");

            group.MapPost("/", CreateBook)
                .WithName("CreateBook")
                .WithDescription("Создает книгу.")
                .Produces<BookDto>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(unprocessableErrorDescription: "В случае если имя, автор или адрес картинки пустые, " +
                    "имя или автор больше 255 символов, либо адрес картинки не соответствует формату URL.")
                .RequireAuthorization();

            group.MapPut("/{id:long}", UpdateBook)
                .WithName("UpdateBook")
                .WithDescription("Обновляет книгу.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(unprocessableErrorDescription: "В случае если имя, автор или адрес картинки пустые, " +
                    "имя или автор больше 255 символов, либо адрес картинки не соответствует формату URL.",
                    notFoundDescription: "В случае, если не удалось найти книгу.")
                .RequireAuthorization();

            group.MapDelete("/{id:long}", DeleteBook)
                .WithName("DeleteBook")
                .WithDescription("Удаляет книгу.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не удалось найти книгу.")
                .RequireAuthorization();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateBook(
            CreateBookCommand command,
            [FromServices] IBookService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.CreateBook(command, ct));
        }

        private static async Task<IResult> UpdateBook(
            [Description("Идентификатор книги.")]
            long id,
            UpdateBookCommand command,
            [FromServices] IBookService service,
            CancellationToken ct = default)
        {
            await service.UpdateBook(id, command, ct);
            return TypedResults.NoContent();
        }

        private static async Task<IResult> DeleteBook(
            [Description("Идентификатор книги.")]
            long id,
            [FromServices] IBookService service,
            CancellationToken ct = default)
        {
            await service.DeleteBook(id, ct);
            return TypedResults.NoContent();
        }
    }
}
