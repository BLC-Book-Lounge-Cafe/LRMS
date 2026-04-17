using LRMS.Application.Dto;
using LRMS.Infrastructure.GraphQL;
using LRMS.Infrastructure.Mappers;
using LRMS.Infrastructure.Persistence.Models;
using LRMS.Infrastructure.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LRMS.Infrastructure.Persistence.Repositories;

public class BookRepository(LrmsDbContext dbContext) : IBookRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<OffsetPagingResponse<BookDto>> GetBooks(
        FilteringSpecification? filteringSpecification,
        OffsetPagingSpecification pagingSpecification,
        SortingSpecification? sortingSpecification,
        CancellationToken ct = default)
    {
        var expression = await BuildBookFilters(filteringSpecification);
        var books = expression is null ? _dbContext.Books.AsNoTracking() : _dbContext.Books.Where(expression).AsNoTracking();

        books = SortBooks(books, sortingSpecification);

        var totalEntityCount = await books.CountAsync(ct);
        var entities = await books
            .Skip(pagingSpecification.Skip ?? 0)
            .Take(pagingSpecification.Take ?? 10)
            .ToListAsync(ct);

        var reservedBookIds = await _dbContext.BookReservations
            .Where(b => b.ReservationDate == DateTime.UtcNow.Date)
            .Select(b => b.Id)
            .ToDictionaryAsync(b => b, ct);

        List<BookDto> bookDtos = [];
        foreach (var entity in entities)
        {
            var bookDto = BookMapper.ToDto(entity);
            bookDto.IsReserved = reservedBookIds.ContainsKey(bookDto.Id);
            bookDtos.Add(bookDto);
        }

        return new OffsetPagingResponse<BookDto>(
            bookDtos,
            totalEntityCount,
            HasPrevPage(pagingSpecification.Skip),
            HasNextPage(pagingSpecification.Skip, pagingSpecification.Take, totalEntityCount));
    }

    private static bool HasPrevPage(int? skip)
    {
        return skip != null && skip > 0;
    }

    private static bool HasNextPage(int? skip, int? take, int totalCount)
    {
        totalCount -= skip ?? 0;
        totalCount -= take ?? 50;
        return totalCount > 0;
    }

    private static async Task<Expression<Func<BookEntity, bool>>?> BuildBookFilters(FilteringSpecification? filteringSpecification)
    {
        if (filteringSpecification is null)
            return null;

        var builder = new FilterExpressionBuilder<BookEntity>();

        if (TryGetValueFromFilter(filteringSpecification, nameof(BookDto.Name), out var name))
        {
            builder.AddFilter(b => b.Name.ToLower().Contains(name.ToLower()));
        }

        if (TryGetValueFromFilter(filteringSpecification, nameof(BookDto.Author), out var author))
        {
            builder.AddFilter(b => b.Author.ToLower().Contains(author.ToLower()));
        }

        return builder.BuildExpression();
    }

    private static bool TryGetValueFromFilter(FilteringSpecification filteringSpecification, string filterName, out string value)
    {
        value = string.Empty;

        var filter = filteringSpecification.PropertyFilters
            .FirstOrDefault(f => f.FieldName.ToLower().Replace("_", string.Empty) == filterName.ToLower().Replace("_", string.Empty));
        if (filter is null)
            return false;

        if (string.IsNullOrEmpty(filter.Value))
            throw new ArgumentException($"'{filterName}' must not be empty");

        value = filter.Value;

        return true;
    }

    private static IOrderedQueryable<BookEntity> SortBooks(IQueryable<BookEntity> books, SortingSpecification? sortingSpecification)
    {
        if (sortingSpecification is null)
        {
            return (IOrderedQueryable<BookEntity>)books;
        }

        if (sortingSpecification.PropertyName.ToLower() == nameof(BookDto.Name))
        {
            return sortingSpecification.DescendingOrder
                ? books.OrderByDescending(b => b.Name)
                : books.OrderBy(b => b.Name);
        }
        else
        {
            return sortingSpecification.DescendingOrder
                ? books.OrderByDescending(b => b.Author)
                : books.OrderBy(b => b.Author);
        }
    }
}
