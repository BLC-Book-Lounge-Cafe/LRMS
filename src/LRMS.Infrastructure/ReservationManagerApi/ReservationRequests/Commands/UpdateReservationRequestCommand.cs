namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Commands;

/// <summary>
///     Команда на обновление статуса заявки на бронирование стола.
/// </summary>
/// <param name="Status">Статус.</param>
public record struct UpdateReservationRequestCommand(string Status);
