namespace LRMS.Application.Dto;

/// <summary>
///     Категория меню.
/// </summary>
public class MenuCategoryDto
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
    ///     Элементы категории.
    /// </summary>
    public IReadOnlyCollection<MenuItemDto> MenuItems { get; set; }
}
