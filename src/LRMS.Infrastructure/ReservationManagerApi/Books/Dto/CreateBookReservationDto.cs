namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

public record struct CreateBookReservationDto(int book_id, string name, string phone, string reserved_at);
