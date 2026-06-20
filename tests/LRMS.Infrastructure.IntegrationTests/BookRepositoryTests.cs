using LRMS.Application.Books.Dto;
using LRMS.Infrastructure.Exceptions;
using LRMS.Infrastructure.IntegrationTests.Fixtures;
using LRMS.Infrastructure.Persistence.Books;
using Microsoft.Extensions.DependencyInjection;

namespace LRMS.Infrastructure.IntegrationTests;

[Trait("Category", "Integration")]
[Collection("Database collection")]
public class BookRepositoryTests(ServiceFixture serviceFixture) : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _serviceFixture = serviceFixture;

    [Fact]
    public async Task CreateBookSuccessful()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();
        var name = "book";
        var author = "author";
        var url = "url";

        var result = await bookRepository.CreateBook(name, author, url, TestContext.Current.CancellationToken);

        Assert.Equal(name, result.Name);
        Assert.Equal(author, result.Author);
        Assert.Equal(url, result.ImageUrl);
    }

    [Fact]
    public async Task DeleteBookSuccessful()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        await bookRepository.DeleteBook(1, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task UpdateBookSuccessful()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        await bookRepository.UpdateBook(2, "book", "author", "url", TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task UpdateBookFailure()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await bookRepository.UpdateBook(1000000, "book", "author", "url", TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GetBooksWithSorting()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        _ = bookRepository.GetBooks(null, null, new(nameof(BookDto.Name), false), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetBooksWithSortingDescending()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        _ = bookRepository.GetBooks(null, null, new(nameof(BookDto.Author), true), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetBooksWithFilteringName()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        _ = bookRepository.GetBooks(new([new(nameof(BookDto.Name), "а")]), null, new(nameof(BookDto.Author), false), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetBooksWithFilteringAuthor()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        _ = bookRepository.GetBooks(new([new(nameof(BookDto.Author), "а")]), null, new(nameof(BookDto.Author), false), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetBooksWithPaging()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        _ = bookRepository.GetBooks(null, new(10, 10), new(nameof(BookDto.Author), false), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task DeleteBookFailure()
    {
        var bookRepository = _serviceFixture.GetRequiredService<IBookGraphQLRepository>();

        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await bookRepository.DeleteBook(1000000, TestContext.Current.CancellationToken));
    }
}
