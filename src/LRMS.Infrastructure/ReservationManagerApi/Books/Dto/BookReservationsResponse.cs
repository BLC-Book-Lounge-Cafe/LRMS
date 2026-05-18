namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

public record struct BookReservationsResponse(IReadOnlyCollection<BookReservationModel> reservations,
    int page_number, int page_size, int total_entries, int total_pages);
