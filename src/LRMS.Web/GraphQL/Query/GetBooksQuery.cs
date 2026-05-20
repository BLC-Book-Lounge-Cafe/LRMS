using HotChocolate.Types.Pagination;
using LRMS.Application.Books.Dto;
using LRMS.Infrastructure.GraphQL;
using LRMS.Infrastructure.Persistence.Books;

namespace LRMS.Web.GraphQL.Query;

[ExtendObjectType(OperationTypeNames.Query)]
public class GetBooksQuery
{
    /// <summary>
    ///     Возвращает список книг.
    /// </summary>
    /// <param name="skip">Количество книг, которое нужно пропустить.</param>
    /// <param name="take">Количество книг, которое нужно получить.</param>
    /// <param name="filter">Данные о фильтрах.</param>
    /// <param name="sorter">Данные о сортировке.</param>
    /// <param name="repository">Репозиторий для работы с книгами.</param>
    /// <returns>Список книг.</returns>
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<BookDto>> GetBooksAsync(
        int? skip,
        int? take,
        FilteringSpecification? filter,
        SortingSpecification? sorter,
        [Service] IBookGraphQLRepository repository)
    {
        var result = await repository.GetBooks(filter, new(skip, take), sorter);
        return new CollectionSegment<BookDto>([.. result.Collection], new(result.HasNextPage, result.HasPrevPage),
            result.TotalCount);
    }
}
