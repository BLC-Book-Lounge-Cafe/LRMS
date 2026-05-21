using LRMS.Application.Menu.Dto;

namespace LRMS.Application.Menu.Commands;

/// <summary>
///     Команда обновления категории меню.
/// </summary>
/// <param name="Name">Название категории.</param>
/// <param name="MenuItems">Элементы категории.</param>
public record struct UpdateMenuCategoryCommand(string Name, IReadOnlyCollection<MenuItemDto> MenuItems);
