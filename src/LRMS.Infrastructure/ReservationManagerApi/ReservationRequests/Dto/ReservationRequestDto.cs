namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;

/// <summary>
///     Данные запроса на бронирование стола.
/// </summary>
/// <param name="Id">Идентификатор брони.</param>
/// <param name="Status">Статус заявки на бронирование.</param>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Номер телефона клиента.</param>
public record struct ReservationRequestDto(long Id, string Status, string CustomerName, string CustomerPhone);
