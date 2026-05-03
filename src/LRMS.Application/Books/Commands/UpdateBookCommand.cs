namespace LRMS.Application.Books.Commands;

/// <summary>
///     Команда на обновление книги.
/// </summary>
/// <param name="Name">Название.</param>
/// <param name="Author">Автор.</param>
/// <param name="ImageUrl">Адрес картинки.</param>
public record struct UpdateBookCommand(string Name, string Author, string ImageUrl);
