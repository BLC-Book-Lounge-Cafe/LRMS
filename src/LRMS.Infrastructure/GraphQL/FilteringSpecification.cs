namespace LRMS.Infrastructure.GraphQL;

/// <summary>
///     Данные о фильтрации.
/// </summary>
/// <param name="PropertyFilters">Список фильтров по свойствам.</param>
public record FilteringSpecification(List<EntityPropertyFilter> PropertyFilters);
