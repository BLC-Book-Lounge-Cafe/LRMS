namespace LRMS.Application.ReservationRequests.Commands;

/// <summary>
///     Команда на удаление запроса на бронирование стола.
/// </summary>
/// <param name="Id">Идентификатор брони.</param>
public record struct DeleteReservationRequestCommand(int Id);
