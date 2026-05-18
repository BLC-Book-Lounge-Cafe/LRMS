using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Commands;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;
using LRMS.Web.Extensions;
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
                .WithDescription("Создаёт новую заявку на быстрое бронирование. " +
                    "Заявка создаётся в статусе pending и требует подтверждения администратором.")
                .Produces(StatusCodes.Status201Created)
                .ProducesCommonErrors(badRequest: "В случае, если телефон указан в неверном формате.");

            group.MapGet("/", GetReservationRequests)
                .WithName("GetReservationRequests")
                .WithDescription("Возвращает список заявок на быстрое бронирование (имя + телефон) с пагинацией. " +
                    "Поддерживает фильтрацию по статусу.")
                .Produces<GetReservationRequestsResponse>(StatusCodes.Status200OK)
                .ProducesCommonErrors(badRequest: "В случае, если дата создания указана в неверном формате.")
                .RequireAuthorization();

            group.MapPut("/{id:int}", UpdateReservationRequest)
                .WithName("UpdateReservationRequest")
                .WithDescription("Переводит заявку из статуса pending в confirmed или cancelled.")
                .Produces<ReservationRequestDto>(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если заявка на быстрое бронирование не найдена.",
                    conflictDescription: "В случае, если статус заявки уже изменен и недоступен для редактирования.")
                .RequireAuthorization();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateReservationRequest(
            [FromServices] IReservationRequestRepository service,
            CreateReservationRequestCommand command,
            CancellationToken ct = default)
        {
            await service.CreateReservationRequest(command, ct);
            return TypedResults.Created();
        }

        private static async Task<IResult> GetReservationRequests(
            [FromServices] IReservationRequestRepository service,
            [Description("Фильтр по статусу заявки.")]
            string? status,
            [Description("Фильтр по дате создания заявки (YYYY-MM-DD) — возвращает заявки, созданные в указанный день.")]
            string? createdDate,
            [Description("Номер страницы.")]
            int? pageNumber,
            [Description("Количество записей на странице.")]
            int? pageSize,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetReservationRequests(new(status, createdDate, pageNumber, pageSize), ct));
        }

        private static async Task<IResult> UpdateReservationRequest(
            [FromServices] IReservationRequestRepository service,
            [Description("Идентификатор заявки на бронирование.")]
            int id,
            UpdateReservationRequestCommand command,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.UpdateReservationRequest(id, command, ct));
        }
    }
}
