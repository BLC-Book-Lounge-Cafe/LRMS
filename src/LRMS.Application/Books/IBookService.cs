using LRMS.Application.Books.Commands;

namespace LRMS.Application.Books;

public interface IBookService
{
    Task CreateBook(CreateBookCommand command, CancellationToken ct = default);
    Task DeleteBook(long id, CancellationToken ct = default);
}
