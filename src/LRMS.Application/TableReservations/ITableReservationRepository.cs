using LRMS.Application.TableReservations.Dto;

namespace LRMS.Application.TableReservations;

public interface ITableReservationRepository
{
    Task<IReadOnlyCollection<ReservationSlotDto>> GetSlots(int tableId, DateTime date, CancellationToken ct = default);
    Task CreateTableReservation(TableReservationDto tableReservationDto, CancellationToken ct = default);
}
