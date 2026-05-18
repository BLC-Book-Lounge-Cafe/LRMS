using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using System.Text.Json.Serialization;

namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;

/// <summary>
///     Ответ на запрос запросов на бронирование столов.
/// </summary>
/// <remarks>Запросы сортируются по дате создания по возрастанию.</remarks>
/// <param name="ReservationRequests">Коллекция запросов на бронирование столов.</param>
/// <param name="PageNumber">Номер страницы.</param>
/// <param name="PageSize">Размер страницы.</param>
/// <param name="TotalEntries">Общее количество заявок на бронирование.</param>
/// <param name="TotalPages">Общее количество страниц.</param>
public record struct GetReservationRequestsResponse(IReadOnlyCollection<ReservationRequestDto> ReservationRequests,
    int? PageNumber, int? PageSize, int TotalEntries, int? TotalPages);
