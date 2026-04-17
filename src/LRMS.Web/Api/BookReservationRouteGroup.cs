using LRMS.Application.Dto;
using LRMS.Application.Services;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

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
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найдена книга по указанному идентификатору.",
                    conflictDescription: "В случае, если данные для бронирования указаны неверно или книга уже была забронирована на указанную дату.");

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateBookReservation(
            [FromServices] IBookReservationService service,
            [FromBody] BookReservationDto bookReservationDto,
            CancellationToken ct = default)
        {
            await service.CreateBookReservation(bookReservationDto, ct);
            return TypedResults.Ok();
        }
    }
}
