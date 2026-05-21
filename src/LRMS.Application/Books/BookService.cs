using LRMS.Application.Books.Commands;
using LRMS.Application.Books.Dto;
using LRMS.Application.Exceptions;
using System.Text.RegularExpressions;

namespace LRMS.Application.Books;

public partial class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;

    public async Task<BookDto> CreateBook(CreateBookCommand command, CancellationToken ct = default)
    {
        ValidateData(command.Name, command.Author, command.ImageUrl);
        return await _repository.CreateBook(command.Name, command.Author, command.ImageUrl, ct);
    }

    public async Task DeleteBook(long id, CancellationToken ct = default)
    {
        await _repository.DeleteBook(id, ct);
    }

    public async Task UpdateBook(long id, UpdateBookCommand command, CancellationToken ct = default)
    {
        ValidateData(command.Name, command.Author, command.ImageUrl);
        await _repository.UpdateBook(id, command.Name, command.Author, command.ImageUrl, ct);
    }

    private static void ValidateData(string name, string author, string imageUrl)
    {
        if (string.IsNullOrEmpty(name))
            throw new DataValidationException("Имя не может быть пустым.");

        if (name.Length > 255)
            throw new DataValidationException("Имя не может превышать 255 символов.");

        if (string.IsNullOrEmpty(author))
            throw new DataValidationException("Автор не может быть пустым.");

        if (author.Length > 255)
            throw new DataValidationException("Автор не может превышать 255 символов.");

        if (string.IsNullOrEmpty(imageUrl))
            throw new DataValidationException("Адрес картинки не может быть пустым.");

        if (!UrlRegex().IsMatch(imageUrl))
            throw new DataValidationException("Адрес картинки не соответствует формату URL.");
    }

    [GeneratedRegex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")]
    private static partial Regex UrlRegex();
}
