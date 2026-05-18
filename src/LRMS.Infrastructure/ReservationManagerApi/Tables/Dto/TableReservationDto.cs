namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

/// <summary>
///     Информация о бронировании стола.
/// </summary>
/// <param name="Id">Идентификатор брони.</param>
/// <param name="TableId">Идентификатор стола.</param>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Номер телефона клиента.</param>
/// <param name="StartTime">Время начала.</param>
/// <param name="EndTime">Время конца.</param>
public record struct TableReservationDto(int Id, int TableId, string CustomerName, string CustomerPhone, DateTime StartTime, DateTime EndTime);
