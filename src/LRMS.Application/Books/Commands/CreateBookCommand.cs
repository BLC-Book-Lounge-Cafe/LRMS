namespace LRMS.Application.Books.Commands;

/// <summary>
///     Команда на создание книги.
/// </summary>
/// <param name="Name">Название.</param>
/// <param name="Author">Автор.</param>
/// <param name="ImageUrl">Адрес картинки.</param>
public record struct CreateBookCommand(string Name, string Author, string ImageUrl);
