using LRMS.Application.ReservationRequests.Dto;

namespace LRMS.Application.ReservationRequests;

public interface IReservationRequestRepository
{
    Task CreateReservationRequest(string customerName, string CustomerPhone, CancellationToken ct = default);
    Task DeleteReservationRequest(int id, CancellationToken ct = default);
    Task<IReadOnlyCollection<ReservationRequestDto>> GetReservationRequests(CancellationToken ct = default);
}
