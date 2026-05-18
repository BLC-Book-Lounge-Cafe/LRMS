namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

public record struct TableReservationsResponse(IReadOnlyCollection<TableReservationModel> reservations,
    int page_number, int page_size, int total_entries, int total_pages);
