using LRMS.Infrastructure.ReservationManagerApi.Tables.Commands;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Requests;

namespace LRMS.Infrastructure.ReservationManagerApi.Tables;

public interface ITableReservationRepository
{
    Task<IReadOnlyCollection<ReservationSlotDto>> GetSlots(int tableId, DateTime date, CancellationToken ct = default);
    Task CreateTableReservation(CreateTableReservationCommand tableReservationDto, CancellationToken ct = default);
    Task<GetTableReservationsResponse> GetTableReservations(int? tableId, string? activeAt, int? pageNumber,
        int? pageSize, CancellationToken ct = default);
    Task DeleteTableReservation(int id, CancellationToken ct = default);
}
