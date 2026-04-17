namespace LRMS.Infrastructure.GraphQL;

public record OffsetPagingResponse<T>(IReadOnlyCollection<T> Collection, int TotalCount, bool HasPrevPage, bool HasNextPage);