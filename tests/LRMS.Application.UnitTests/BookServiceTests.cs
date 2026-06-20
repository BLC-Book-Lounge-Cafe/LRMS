using LRMS.Application.Books;
using LRMS.Application.Exceptions;
using LRMS.Infrastructure.Exceptions;
using Moq;

namespace LRMS.Application.UnitTests;

public class BookServiceTests
{
    [Theory]
    [InlineData("рассказ", "Достоевский", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSByaHVrAK_LZz8G12q8kn6S14y0S9-6g_Rog&s")]
    [InlineData("поэма", "Толстой", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVRDk6EeQc7RhnJmF4SNjI91DDsfv5JJNRjQ&s")]
    [InlineData("стишок", "Саша Белый", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUAX23TLmLUDFMRc-yPictpjVntgpNb0cI2A&s")]
    public async Task CreateBookSuccessful(string name, string author, string imageUrl)
    {
        var service = new BookService(CreateBookRepository(name, author, imageUrl));

        var result = await service.CreateBook(new(name, author, imageUrl), TestContext.Current.CancellationToken);

        Assert.Equal(name, result.Name);
        Assert.Equal(author, result.Author);
        Assert.Equal(imageUrl, result.ImageUrl);
    }

    [Theory]
    [InlineData("", "Достоевский", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSByaHVrAK_LZz8G12q8kn6S14y0S9-6g_Rog&s")]
    [InlineData("поэма", "", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVRDk6EeQc7RhnJmF4SNjI91DDsfv5JJNRjQ&s")]
    [InlineData("стишок", "Саша Белый", "")]
    [InlineData("стишок", "Саша Белый", "бла-бла")]
    public async Task CreateBookFailure(string name, string author, string imageUrl)
    {
        var service = new BookService(CreateBookRepository());

        await Assert.ThrowsAsync<DataValidationException>(async () => await service.CreateBook(new(name, author, imageUrl),
            TestContext.Current.CancellationToken));
    }

    [Theory]
    [InlineData("рассказ", "Достоевский", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSByaHVrAK_LZz8G12q8kn6S14y0S9-6g_Rog&s")]
    [InlineData("поэма", "Толстой", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVRDk6EeQc7RhnJmF4SNjI91DDsfv5JJNRjQ&s")]
    [InlineData("стишок", "Саша Белый", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUAX23TLmLUDFMRc-yPictpjVntgpNb0cI2A&s")]
    public async Task UpdateBookSuccessful(string name, string author, string imageUrl)
    {
        var service = new BookService(CreateBookRepository(name, author, imageUrl));

        await service.UpdateBook(1, new(name, author, imageUrl), TestContext.Current.CancellationToken);
    }

    [Theory]
    [InlineData("", "Достоевский", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSByaHVrAK_LZz8G12q8kn6S14y0S9-6g_Rog&s")]
    [InlineData("поэма", "", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVRDk6EeQc7RhnJmF4SNjI91DDsfv5JJNRjQ&s")]
    [InlineData("стишок", "Саша Белый", "")]
    [InlineData("стишок", "Саша Белый", "бла-бла")]
    public async Task UpdateBookFailure(string name, string author, string imageUrl)
    {
        var service = new BookService(CreateBookRepository());

        await Assert.ThrowsAsync<DataValidationException>(async () => await service.UpdateBook(1, new(name, author, imageUrl),
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ThrowsEntityNotFoundExceptionOnUpdate()
    {
        var service = new BookService(CreateBookRepository(withException: true));

        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.UpdateBook(1,
            new("рассказ", "Достоевский", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSByaHVrAK_LZz8G12q8kn6S14y0S9-6g_Rog&s"),
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task DeleteBookSuccessful()
    {
        var service = new BookService(CreateBookRepository());

        await service.DeleteBook(2, TestContext.Current.CancellationToken);
    }


    [Fact]
    public async Task ThrowsEntityNotFoundExceptionOnDelete()
    {
        var service = new BookService(CreateBookRepository(withException: true));

        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.DeleteBook(1,
            TestContext.Current.CancellationToken));
    }


    [Fact]
    public async Task LongNameStringOnCreate()
    {
        string repeated = new('A', 300);
        var service = new BookService(CreateBookRepository(withException: true));

        await Assert.ThrowsAsync<DataValidationException>(async () => await service.CreateBook(new(repeated, "a", "a"),
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task LongAuthorStringOnCreate()
    {
        string repeated = new('A', 300);
        var service = new BookService(CreateBookRepository(withException: true));

        await Assert.ThrowsAsync<DataValidationException>(async () => await service.CreateBook(new("a", repeated, "a"),
            TestContext.Current.CancellationToken));
    }

    private static IBookRepository CreateBookRepository(
        string? name = null,
        string? author = null,
        string? imagePath = null,
        bool withException = false)
    {
        var mock = new Mock<IBookRepository>();

        if (withException)
        {
            mock.Setup(mock => mock.UpdateBook(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws(new EntityNotFoundException());
            mock.Setup(mock => mock.DeleteBook(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Throws(new EntityNotFoundException());
        }
        else if (name is not null && author is not null && imagePath is not null)
        {
            mock.Setup(mock => mock.CreateBook(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Books.Dto.BookDto()
                {
                    Name = name,
                    Author = author,
                    ImageUrl = imagePath,
                });
        }

        return mock.Object;
    }
}
