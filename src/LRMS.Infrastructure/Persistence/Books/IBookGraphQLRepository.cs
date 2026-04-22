using LRMS.Application.Books;
using LRMS.Application.Books.Dto;
using LRMS.Infrastructure.GraphQL;

namespace LRMS.Infrastructure.Persistence.Books;

public interface IBookGraphQLRepository : IBookRepository
{
    Task<OffsetPagingResponse<BookDto>> GetBooks(
        FilteringSpecification? filteringSpecification,
        OffsetPagingSpecification? pagingSpecification,
        SortingSpecification? sortingSpecification,
        CancellationToken ct = default);
}
