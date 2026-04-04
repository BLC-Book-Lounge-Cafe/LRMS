using LRMS.Application.Commands;
using LRMS.Application.Requests;

namespace LRMS.Application.Services;

public interface IReservationRequestService
{
    Task CreateReservationRequest(CreateReservationRequestCommand command, CancellationToken ct = default);
    Task DeleteReservationRequest(DeleteReservationRequestCommand command, CancellationToken ct = default);
    Task<GetReservationRequestsResponse> GetReservationRequests(CancellationToken ct = default);
}
