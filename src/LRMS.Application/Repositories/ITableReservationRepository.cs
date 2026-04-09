using LRMS.Application.Dto;

namespace LRMS.Application.Repositories;

public interface ITableReservationRepository
{
    Task<IReadOnlyCollection<ReservationSlotDto>> GetSlots(int tableId, DateTime date, CancellationToken ct = default);
    Task CreateTableReservation(TableReservationDto tableReservationDto, CancellationToken ct = default);
}
