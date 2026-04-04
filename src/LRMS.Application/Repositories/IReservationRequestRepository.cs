using LRMS.Application.Dto;

namespace LRMS.Application.Repositories;

public interface IReservationRequestRepository
{
    Task CreateReservationRequest(string customerName, string CustomerPhone, CancellationToken ct = default);
    Task DeleteReservationRequest(int id, CancellationToken ct = default);
    Task<IReadOnlyCollection<ReservationRequestDto>> GetReservationRequests(CancellationToken ct = default);
}
