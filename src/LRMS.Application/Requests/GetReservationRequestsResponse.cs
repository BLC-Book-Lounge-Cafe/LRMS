using LRMS.Application.Dto;

namespace LRMS.Application.Requests;

/// <summary>
///     Ответ на запрос запросов на бронирование столов.
/// </summary>
/// <remarks>Запросы сортируются по дате создания по возрастанию.</remarks>
/// <param name="ReservationRequests">Коллекция запросов на бронирование столов.</param>
public record struct GetReservationRequestsResponse(IReadOnlyCollection<ReservationRequestDto> ReservationRequests);
