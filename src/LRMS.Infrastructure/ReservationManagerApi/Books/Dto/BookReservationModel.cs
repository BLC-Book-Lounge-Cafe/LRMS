namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

public record struct BookReservationModel(long id, long book_id, string name, string phone, string reserved_at);
