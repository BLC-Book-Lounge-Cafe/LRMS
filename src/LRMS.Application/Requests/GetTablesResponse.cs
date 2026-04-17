using LRMS.Application.Dto;

namespace LRMS.Application.Requests;

/// <summary>
///     Ответ на запрос получения информации о столах.
/// </summary>
/// <param name="Tables">Информация о столах.</param>
public record struct GetTablesResponse(IReadOnlyCollection<TableDto> Tables);
