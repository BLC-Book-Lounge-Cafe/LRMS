using LRMS.Application.Books.Dto;

namespace LRMS.Application.Books;

public interface IBookRepository
{
    Task<BookDto> CreateBook(string name, string author, string imageUrl, CancellationToken ct = default);
    Task UpdateBook(long id, string name, string author, string imageUrl, CancellationToken ct = default);
    Task DeleteBook(long id, CancellationToken ct = default);
}
