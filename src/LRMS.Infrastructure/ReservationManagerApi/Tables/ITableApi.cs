using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using Refit;

namespace LRMS.Infrastructure.ReservationManagerApi.Tables;

public interface ITableApi
{
    [Post("/api/v1/tables")]
    Task<HttpResponseMessage> CreateTableReservation([Body] CreateTableReservationDto dto, CancellationToken ct = default);

    [Get("/api/v1/tables")]
    Task<HttpResponseMessage> GetTableReservations([Refit.Query] long? tableId, [Refit.Query] string? activeAt,
        [Refit.Query] int? pageNumber, [Refit.Query] int? pageSize, CancellationToken ct = default);

    [Get("/api/v1/tables/{tableId}/slots")]
    Task<HttpResponseMessage> GetTableReservationSlots(long tableId, [Refit.Query] string date, CancellationToken ct = default);

    [Delete("/api/v1/tables/{id}")]
    Task<HttpResponseMessage> DeleteTableReservation(long id, CancellationToken ct = default);
}
