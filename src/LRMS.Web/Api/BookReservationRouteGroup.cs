using LRMS.Infrastructure.ReservationManagerApi.Books;
using LRMS.Infrastructure.ReservationManagerApi.Books.Dto;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LRMS.Web.Api;

internal static class BookReservationRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapBookReservationsApi()
        {
            var group = endpointRouteBuilder.MapGroup("/book-reservations");

            group.MapPost("/", CreateBookReservation)
                .WithName("CreateBookReservation")
                .WithDescription("Бронирует книгу.")
                .Produces(StatusCodes.Status201Created)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найдена книга по указанному идентификатору.",
                    conflictDescription: "В случае, если книга уже была забронирована на указанную дату.",
                    unprocessableErrorDescription: "В случае, если данные для бронирования указаны неверно.");

            group.MapGet("/", GetBookReservations)
                .WithName("GetBookReservations")
                .WithDescription("Возвращает список бронирований книг с пагинацией.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найдена книга по указанному идентификатору.",
                    badRequest: "В случае, если дата или номер телефона не соответствуют формату.")
                .RequireAuthorization();

            group.MapDelete("/", DeleteBookReservation)
                .WithName("DeleteBookReservation")
                .WithDescription("Удаляет бронирование книги по его ID.")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesCommonErrors(notFoundDescription: "В случае, если бронь книги не найдена.")
                .RequireAuthorization();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateBookReservation(
            [FromServices] IBookReservationRepository service,
            [FromBody] CreateBookReservationCommand bookReservationDto,
            CancellationToken ct = default)
        {
            await service.CreateBookReservation(bookReservationDto, ct);
            return TypedResults.Created();
        }

        private static async Task<IResult> GetBookReservations(
            [FromServices] IBookReservationRepository service,
            [Description("Фильтр по ID книги.")]
            int? bookId,
            [Description("Фильтр по дате бронирования.")]
            DateTime? date,
            [Description("Номер страницы.")]
            int? pageNumber,
            [Description("Количество записей на странице.")]
            int? pageSize,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetBookReservations(bookId, date, pageNumber, pageSize, ct));
        }

        private static async Task<IResult> DeleteBookReservation(
            [FromServices] IBookReservationRepository service,
            [Description("ID бронирования книги.")]
            int id,
            CancellationToken ct = default)
        {
            await service.DeleteBookReservation(id, ct);
            return TypedResults.NoContent();
        }
    }
}
