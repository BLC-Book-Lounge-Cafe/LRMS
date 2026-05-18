namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

/// <summary>
///     Данные бронирования книги.
/// </summary>
/// <param name="Id">Идентификатор бронирования.</param>
/// <param name="BookId">Идентификатор книги.</param>
/// <param name="Date">Дата бронирования.</param>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Телефон клиента.</param>
public record struct BookReservationDto(int Id, int BookId, DateTime Date, string CustomerName, string CustomerPhone);
