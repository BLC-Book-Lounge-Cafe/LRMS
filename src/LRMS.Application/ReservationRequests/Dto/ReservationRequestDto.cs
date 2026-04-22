namespace LRMS.Application.ReservationRequests.Dto;

/// <summary>
///     Данные запроса на бронирование стола.
/// </summary>
/// <param name="Id">Идентификатор брони.</param>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Номер телефона клиента.</param>
public record struct ReservationRequestDto(int Id, string CustomerName, string CustomerPhone);
