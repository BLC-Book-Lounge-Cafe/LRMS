using LRMS.Application.TableReservations;
using LRMS.Application.TableReservations.Dto;
using LRMS.Application.TableReservations.Requests;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

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
                .WithDescription("Бронирует стол.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найден стол по указанному идентификатору.",
                    conflictDescription: "В случае, если данные для бронирования указаны неверно или стол уже был забронирован на указанное время.");

            group.MapPost("/slots", GetTableReservationSlots)
                .WithName("GetTableReservationSlots")
                .WithDescription("Возвращает слоты для бронирования стола.")
                .Produces<GetTableReservationSlotsResponse>()
                .ProducesCommonErrors(notFoundDescription: "В случае, если не найден стол по указанному идентификатору.");

            return endpointRouteBuilder;
        }

        private static async Task<IResult> CreateTableReservation(
            [FromServices] ITableReservationService service,
            [FromBody] TableReservationDto tableReservationDto,
            CancellationToken ct = default)
        {
            await service.CreateTableReservation(tableReservationDto, ct);
            return TypedResults.Ok();
        }

        private static async Task<IResult> GetTableReservationSlots(
            [FromServices] ITableReservationService service,
            GetTableReservationSlotsRequest request,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetSlots(request, ct));
        }
    }
}
