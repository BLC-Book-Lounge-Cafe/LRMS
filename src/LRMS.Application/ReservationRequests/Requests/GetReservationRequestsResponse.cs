using LRMS.Application.ReservationRequests.Dto;

namespace LRMS.Application.ReservationRequests.Requests;

/// <summary>
///     Ответ на запрос запросов на бронирование столов.
/// </summary>
/// <remarks>Запросы сортируются по дате создания по возрастанию.</remarks>
/// <param name="ReservationRequests">Коллекция запросов на бронирование столов.</param>
public record struct GetReservationRequestsResponse(IReadOnlyCollection<ReservationRequestDto> ReservationRequests);
