using LRMS.Application.Commands;
using LRMS.Application.Requests;
using LRMS.Application.Services;
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
                .ProducesWithDescription(StatusCodes.Status200OK, "Запрос на бронирование успешно создан.")
                .ProducesCommonErrors(conflictDescription: "Если номер клиента не соответствует формату или имя клиента слишком длинное.");

            group.MapDelete("/{id:int}", DeleteReservationRequest)
                .WithName("DeleteReservationRequest")
                .WithDescription("Удаляет запрос на бронирование стола.")
                .ProducesWithDescription(StatusCodes.Status200OK, "Запрос на бронирование успешно удален.")
                .ProducesCommonErrors(notFoundDescription: "Если запрос на бронирование не найден.");

            group.MapGet("/", GetReservationRequests)
                .WithName("GetReservationRequests")
                .WithDescription("Возвращает запросы на бронирование стола.")
                .ProducesWithDescription<GetReservationRequestsResponse>(StatusCodes.Status200OK, "Запрос на бронирование успешно удален.")
                .ProducesCommonErrors();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateReservationRequest(
            [FromServices] IReservationRequestService service,
            CreateReservationRequestCommand command,
            CancellationToken ct = default)
        {
            await service.CreateReservationRequest(command, ct);
            return TypedResults.Ok();
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
