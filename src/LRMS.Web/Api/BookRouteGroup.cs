using LRMS.Application.Books;
using LRMS.Application.Books.Commands;
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
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(conflictDescription: "В случае если имя, автор или адрес картинки пустые.");

            group.MapDelete("/{id:long}", DeleteBook)
                .WithName("DeleteBook")
                .WithDescription("Удаляет книгу.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не удалось найти книгу.");

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateBook(
            CreateBookCommand command,
            [FromServices] IBookService service,
            CancellationToken ct = default)
        {
            await service.CreateBook(command, ct);
            return TypedResults.Ok();
        }

        private static async Task<IResult> DeleteBook(
            [Description("Идентификатор книги.")]
            long id,
            [FromServices] IBookService service,
            CancellationToken ct = default)
        {
            await service.DeleteBook(id, ct);
            return TypedResults.Ok();
        }
    }
}
