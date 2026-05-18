namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;

/// <summary>
///     Запрос на получение заявок на бронирование.
/// </summary>
/// <param name="Status">Статус заявки на резервирование.</param>
/// <param name="CreatedDate">Дата создания.</param>
/// <param name="PageNumber">Номер страницы.</param>
/// <param name="PageSize">Размер страницы.</param>
public record struct GetReservationRequest(string? Status, string? CreatedDate, int? PageNumber, int? PageSize);
