namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Commands;

/// <summary>
///     Команда создания бронирования стола.
/// </summary>
/// <param name="TableId">Идентификатор стола.</param>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Номер телефона клиента.</param>
/// <param name="StartTime">Время начала.</param>
/// <param name="EndTime">Время конца.</param>
public record struct CreateTableReservationCommand(long TableId, string CustomerName, string CustomerPhone, DateTime StartTime, DateTime EndTime);
