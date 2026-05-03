using LRMS.Application.Books.Commands;
using LRMS.Application.Books.Dto;

namespace LRMS.Application.Books;

public interface IBookService
{
    Task<BookDto> CreateBook(CreateBookCommand command, CancellationToken ct = default);
    Task UpdateBook(long id, UpdateBookCommand command, CancellationToken ct = default);
    Task DeleteBook(long id, CancellationToken ct = default);
}
