using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Requests;

/// <summary>
///     Ответ на запрос бронирований столов.
/// </summary>
/// <param name="TableReservations">Список бронирований столов.</param>
/// <param name="PageNumber">Номер страницы.</param>
/// <param name="PageSize">Размер страницы.</param>
/// <param name="TotalEntries">Общее количество сущностей.</param>
/// <param name="TotalPages">Общее количество страниц.</param>
public record struct GetTableReservationsResponse(IReadOnlyCollection<TableReservationDto> TableReservations,
    int PageNumber, int PageSize, int TotalEntries, int TotalPages);
