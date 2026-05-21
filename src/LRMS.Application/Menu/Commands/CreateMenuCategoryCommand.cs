using LRMS.Application.Menu.Dto;

namespace LRMS.Application.Menu.Commands;

/// <summary>
///     Команда создания категории меню.
/// </summary>
/// <param name="Name">Название категории меню.</param>
/// <param name="MenuItems">Элементы категории меню.</param>
public record struct CreateMenuCategoryCommand(string Name, IReadOnlyCollection<MenuItemForCreateDto> MenuItems);
