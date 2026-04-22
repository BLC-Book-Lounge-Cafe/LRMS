using LRMS.Application.ReservationRequests.Commands;
using LRMS.Application.ReservationRequests.Requests;

namespace LRMS.Application.ReservationRequests;

public interface IReservationRequestService
{
    Task CreateReservationRequest(CreateReservationRequestCommand command, CancellationToken ct = default);
    Task DeleteReservationRequest(DeleteReservationRequestCommand command, CancellationToken ct = default);
    Task<GetReservationRequestsResponse> GetReservationRequests(CancellationToken ct = default);
}
