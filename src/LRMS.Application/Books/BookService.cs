using LRMS.Application.Books.Commands;
using LRMS.Application.Books.Dto;
using LRMS.Application.Exceptions;

namespace LRMS.Application.Books;

public class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public async Task<BookDto> CreateBook(CreateBookCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(command.Name))
            throw new DataValidationException("Имя не может быть пустым.");

        if (string.IsNullOrEmpty(command.Author))
            throw new DataValidationException("Автор не может быть пустым.");

        if (string.IsNullOrEmpty(command.ImageUrl))
            throw new DataValidationException("Адрес картинки не может быть пустым.");

        return await _repository.CreateBook(command.Name, command.Author, command.ImageUrl, ct);
    }

    public async Task DeleteBook(long id, CancellationToken ct = default)
    {
        await _repository.DeleteBook(id, ct);
    }

    public async Task UpdateBook(long id, UpdateBookCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(command.Name))
            throw new DataValidationException("Имя не может быть пустым.");

        if (string.IsNullOrEmpty(command.Author))
            throw new DataValidationException("Автор не может быть пустым.");

        if (string.IsNullOrEmpty(command.ImageUrl))
            throw new DataValidationException("Адрес картинки не может быть пустым.");

        await _repository.UpdateBook(id, command.Name, command.Author, command.ImageUrl, ct);
    }
}
