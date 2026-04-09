using LRMS.Application.Dto;
using LRMS.Application.Requests;

namespace LRMS.Application.Services;

public interface ITableReservationService
{
    Task CreateTableReservation(TableReservationDto tableReservationDto, CancellationToken ct = default);
    Task<GetTableReservationSlotsResponse> GetSlots(GetTableReservationSlotsRequest request, CancellationToken ct = default);
}
