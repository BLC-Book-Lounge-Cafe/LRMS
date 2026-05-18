namespace LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

/// <summary>
///     Ответ на запрос получения броней книг
/// </summary>
/// <param name="BookReservations">Брони книг.</param>
/// <param name="PageNumber">Номер страницы.</param>
/// <param name="PageSize">Количество сущностей на странице.</param>
/// <param name="TotalEntries">Общее количество сущностей.</param>
/// <param name="TotalPages">Общее количество страниц.</param>
public record struct GetBookReservationsResponse(IReadOnlyCollection<BookReservationDto> BookReservations,
    int PageNumber, int PageSize, int TotalEntries, int TotalPages);