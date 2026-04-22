using LRMS.Application.Books.Commands;
using LRMS.Application.Exceptions;

namespace LRMS.Application.Books;

public class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public async Task CreateBook(CreateBookCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(command.Name))
            throw new DomainException("Имя не может быть пустым.");

        if (string.IsNullOrEmpty(command.Author))
            throw new DomainException("Автор не может быть пустым.");

        if (string.IsNullOrEmpty(command.ImageUrl))
            throw new DomainException("Адрес картинки не может быть пустым.");

        await _repository.CreateBook(command.Name, command.Author, command.ImageUrl, ct);
    }

    public async Task DeleteBook(long id, CancellationToken ct = default)
    {
        await _repository.DeleteBook(id, ct);
    }
}
