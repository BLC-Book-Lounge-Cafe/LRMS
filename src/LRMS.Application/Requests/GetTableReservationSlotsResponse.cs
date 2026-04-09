using LRMS.Application.Dto;

namespace LRMS.Application.Requests;

/// <summary>
///     Ответ на запрос получения слотов для резервирования стола.
/// </summary>
/// <param name="ReservationSlots">Слоты для резервирования стола.</param>
public record struct GetTableReservationSlotsResponse(IReadOnlyCollection<ReservationSlotDto> ReservationSlots);
