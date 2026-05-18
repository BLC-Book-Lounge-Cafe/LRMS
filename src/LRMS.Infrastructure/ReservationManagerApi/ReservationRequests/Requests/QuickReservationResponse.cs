using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;

namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;

public record struct QuickReservationResponse(IReadOnlyCollection<QuickReservationModel> reservations,
    int? page_number, int? page_size, int total_entries, int? total_pages);
