using LRMS.Application.ReservationRequests;
using LRMS.Application.ReservationRequests.Commands;
using LRMS.Application.ReservationRequests.Requests;
using LRMS.Web.Extensions;
using LRMS.Web.OpenApi;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LRMS.Web.Api;

internal static class ReservationRequestRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapReservationRequestsApi()
        {
            var group = endpointRouteBuilder.MapGroup("/reservation-requests");

            group.MapPost("/", CreateReservationRequest)
                .WithName("CreateReservationRequest")
                .WithDescription("Создает запрос на бронирование стола.")
                .Produces(StatusCodes.Status201Created)
                .ProducesCommonErrors(unprocessableErrorDescription:
                    "В случае, если номер клиента не соответствует формату или имя клиента слишком длинное.");

            group.MapDelete("/{id:int}", DeleteReservationRequest)
                .WithName("DeleteReservationRequest")
                .WithDescription("Удаляет запрос на бронирование стола.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если запрос на бронирование не найден.");

            group.MapGet("/", GetReservationRequests)
                .WithName("GetReservationRequests")
                .WithDescription("Возвращает запросы на бронирование стола.")
                .Produces<GetReservationRequestsResponse>(StatusCodes.Status200OK)
                .ProducesCommonErrors();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateReservationRequest(
            [FromServices] IReservationRequestService service,
            CreateReservationRequestCommand command,
            CancellationToken ct = default)
        {
            await service.CreateReservationRequest(command, ct);
            return TypedResults.Created();
        }

        private static async Task<IResult> DeleteReservationRequest(
            [FromServices] IReservationRequestService service,
            [Description("Идентификатор запроса на бронирование стола.")]
            int id,
            CancellationToken ct = default)
        {
            await service.DeleteReservationRequest(new(id), ct);
            return TypedResults.Ok();
        }

        private static async Task<IResult> GetReservationRequests(
            [FromServices] IReservationRequestService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetReservationRequests(ct));
        }
    }
}
