namespace LRMS.Application.Menu.Dto;

/// <summary>
///     Данные категории меню при создании и обновлении.
/// </summary>
/// <param name="Name">Название категории меню.</param>
/// <param name="MenuItems">Элементы категории меню.</param>
public record struct MenuCategoryForCreateDto(string Name, IReadOnlyCollection<MenuItemForCreateDto> MenuItems);
