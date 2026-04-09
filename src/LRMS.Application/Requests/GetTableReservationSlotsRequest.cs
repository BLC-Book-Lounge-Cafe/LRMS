namespace LRMS.Application.Requests;

/// <summary>
///     Запрос получения слотов для резервирования стола.
/// </summary>
/// <param name="TableId">Идентификатор стола.</param>
/// <param name="Date">Дата бронирования.</param>
public record struct GetTableReservationSlotsRequest(int TableId, DateTime Date);
