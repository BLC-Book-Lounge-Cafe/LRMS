namespace LRMS.Application.Dto;

/// <summary>
///     Данные о столе.
/// </summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="SeatsCount">Количество мест.</param>
public record struct TableDto(int Id, byte SeatsCount);
