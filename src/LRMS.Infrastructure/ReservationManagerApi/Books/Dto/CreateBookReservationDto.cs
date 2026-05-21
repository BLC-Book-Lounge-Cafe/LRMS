namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

public record struct CreateBookReservationDto(long book_id, string name, string phone, string reserved_at);
