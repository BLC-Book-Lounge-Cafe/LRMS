using LRMS.Application.Dto;

namespace LRMS.Application.Requests;

/// <summary>
///     Ответ на запрос получения состояния пространства.
/// </summary>
/// <param name="SpaceState">Состояние пространства.</param>
public record struct GetSpaceStateResponse(SpaceStateDto SpaceState);
