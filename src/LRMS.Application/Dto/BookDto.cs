namespace LRMS.Application.Dto;

/// <summary>
///     Данные о книге.
/// </summary>
public class BookDto
{
    /// <summary>
    ///     Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Автор.
    /// </summary>
    public required string Author { get; set; }

    /// <summary>
    ///     Ссылка на картинку.
    /// </summary>
    public required string ImageUrl { get; set; }

    /// <summary>
    ///     Признак резервирования книги.
    /// </summary>
    public bool IsReserved { get; set; }
}
