namespace LRMS.Infrastructure.GraphQL;

/// <summary>
///     Данные для сортировки.
/// </summary>
/// <param name="PropertyName">Название свойства, по которому нужно сортировать.</param>
/// <param name="DescendingOrder">true - по убыванию, иначе - false.</param>
public record SortingSpecification(string PropertyName, bool DescendingOrder);
