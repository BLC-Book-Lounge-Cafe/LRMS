using LRMS.Application.TableReservations.Dto;
using LRMS.Application.TableReservations.Requests;

namespace LRMS.Application.TableReservations;

public interface ITableReservationService
{
    Task CreateTableReservation(TableReservationDto tableReservationDto, CancellationToken ct = default);
    Task<GetTableReservationSlotsResponse> GetSlots(GetTableReservationSlotsRequest request, CancellationToken ct = default);
}
