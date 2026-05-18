namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

public record struct BookReservationModel(int id, int book_id, string name, string phone, string reserved_at);
