using LRMS.Application.Dto;
using LRMS.Infrastructure.GraphQL;

namespace LRMS.Infrastructure.Persistence.Repositories;

public interface IBookRepository
{
    Task<OffsetPagingResponse<BookDto>> GetBooks(
        FilteringSpecification? filteringSpecification,
        OffsetPagingSpecification? pagingSpecification,
        SortingSpecification? sortingSpecification,
        CancellationToken ct = default);
}
