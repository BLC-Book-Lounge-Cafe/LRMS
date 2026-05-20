namespace LRMS.Infrastructure.GraphQL;

/// <summary>
///     Фильтр по свойству.
/// </summary>
/// <param name="FieldName">Название свойства.</param>
/// <param name="Value">Значение фильтра.</param>
public record EntityPropertyFilter(string FieldName, string Value);
