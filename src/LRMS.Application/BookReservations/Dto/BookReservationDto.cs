namespace LRMS.Application.BookReservations.Dto;

/// <summary>
///     Данные для бронирования книги.
/// </summary>
/// <param name="BookId">Идентификатор книги.</param>
/// <param name="Date">Дата бронирования.</param>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Телефон клиента.</param>
public record struct BookReservationDto(int BookId, DateTime Date, string CustomerName, string CustomerPhone);
