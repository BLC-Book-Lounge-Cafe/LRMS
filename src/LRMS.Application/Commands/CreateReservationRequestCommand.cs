namespace LRMS.Application.Commands;

/// <summary>
///     Команда на создание запроса на бронирование стола.
/// </summary>
/// <param name="CustomerName">Имя клиента.</param>
/// <param name="CustomerPhone">Номер телефона клиента.</param>
public record struct CreateReservationRequestCommand(string CustomerName, string CustomerPhone);
