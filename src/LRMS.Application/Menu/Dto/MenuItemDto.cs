namespace LRMS.Application.Menu.Dto;

/// <summary>
///     Элемент меню.
/// </summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="Name">Название.</param>
/// <param name="Price">Цена.</param>
public record struct MenuItemDto(int Id, string Name, double Price);
