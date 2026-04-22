using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using LRMS.Application.Books.Dto;
using LRMS.Infrastructure.GraphQL;
using LRMS.Infrastructure.Persistence.Books;

namespace LRMS.Web.GraphQL.Query;

[ExtendObjectType(OperationTypeNames.Query)]
public class GetBooksQuery
{
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
