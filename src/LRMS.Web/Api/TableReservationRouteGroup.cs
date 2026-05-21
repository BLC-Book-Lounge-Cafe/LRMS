using LRMS.Infrastructure.ReservationManagerApi.Tables;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Commands;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Requests;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LRMS.Web.Api;

public static class TableReservationRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapTableReservationsApi()
        {
            var group = endpointRouteBuilder.MapGroup("/table-reservations");

            group.MapPost("/", CreateTableReservation)
                .WithName("CreateTableReservation")
                .WithDescription("Создаёт новое бронирование стола.")
                .Produces(StatusCodes.Status201Created)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найден стол по указанному идентификатору.",
                    conflictDescription: "В случае, если стол уже был забронирован на указанное время, " +
                        "либо продолжительность бронирования меньше лимита.",
                    badRequest: "В случае, если телефон не соответствует формату.");

            group.MapPost("/slots", GetTableReservationSlots)
                .WithName("GetTableReservationSlots")
                .WithDescription("Возвращает слоты для бронирования стола.")
                .Produces<GetTableReservationSlotsResponse>()
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найден стол по указанному идентификатору.",
                    badRequest: "В случае, если дата не соответствует формату.");

            group.MapGet("/", GetTableReservations)
                .WithName("GetTableReservations")
                .WithDescription("Возвращает список бронирований столов с пагинацией.")
                .Produces<GetTableReservationsResponse>()
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найден стол по указанному идентификатору.",
                    badRequest: "В случае, если дата или время не соответствует формату.")
                .RequireAuthorization();

            group.MapDelete("/{id:long}", DeleteTableReservation)
                .WithName("DeleteTableReservation")
                .WithDescription("Удаляет бронирование стола.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status401Unauthorized)
                .ProducesCommonErrors(notFoundDescription: "В случае, если бронирование стола не найдено.")
                .RequireAuthorization();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateTableReservation(
            [FromServices] ITableReservationRepository service,
            [FromBody] CreateTableReservationCommand tableReservationDto,
            CancellationToken ct = default)
        {
            await service.CreateTableReservation(tableReservationDto, ct);
            return TypedResults.Created();
        }

        private static async Task<IResult> GetTableReservationSlots(
            [FromServices] ITableReservationRepository service,
            GetTableReservationSlotsRequest request,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetSlots(request.TableId, request.Date, ct));
        }

        private static async Task<IResult> GetTableReservations(
            [FromServices] ITableReservationRepository service,
            [Description("Фильтр по ID стола.")]
            long? tableId,
            [Description("Фильтр по дате (UTC, ISO-8601). Принимает либо date YYYY-MM-DD — возвращает брони, активные в указанный день; либо date-time — возвращает брони, активные в указанный момент времени.")]
            string? activeAt,
            [Description("Номер страницы.")]
            int? pageNumber,
            [Description("Количество записей на странице.")]
            int? pageSize,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetTableReservations(tableId, activeAt, pageNumber, pageSize, ct));
        }

        private static async Task<IResult> DeleteTableReservation(
            [FromServices] ITableReservationRepository service,
            [Description("Идентификатор брони стола.")]
            long id,
            CancellationToken ct = default)
        {
            await service.DeleteTableReservation(id, ct);
            return TypedResults.NoContent();
        }
    }
}
