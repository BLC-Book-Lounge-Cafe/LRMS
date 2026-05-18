using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using Refit;

namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests;

public interface IReservationRequestApi
{
    [Post("/api/v1/quicks")]
    Task<HttpResponseMessage> CreateReservationRequest([Body] CreateQuickReservationDto dto, CancellationToken ct = default);

    [Get("/api/v1/quicks")]
    Task<HttpResponseMessage> GetReservationRequests([Refit.Query] string? status, [Refit.Query] string? createdDate,
        [Refit.Query] int? pageNumber, [Refit.Query] int? pageSize, CancellationToken ct = default);

    [Put("/api/v1/quicks/{id}/status")]
    Task<HttpResponseMessage> UpdateReservationRequest(int id, [Body] UpdateQuickReservationDto dto, CancellationToken ct = default);
}
