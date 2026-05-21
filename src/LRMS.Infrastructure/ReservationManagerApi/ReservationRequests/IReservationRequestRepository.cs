using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Commands;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;

namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests;

public interface IReservationRequestRepository
{
    Task CreateReservationRequest(CreateReservationRequestCommand command, CancellationToken ct = default);
    Task<GetReservationRequestsResponse> GetReservationRequests(GetReservationRequest request, CancellationToken ct = default);
    Task<ReservationRequestDto> UpdateReservationRequest(long id, UpdateReservationRequestCommand command, CancellationToken ct = default);
}
