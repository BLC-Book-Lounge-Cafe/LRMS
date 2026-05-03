namespace LRMS.Application.Menu.Dto;

/// <summary>
///     Данные об элементе категории меню при создании или обновлении категории.
/// </summary>
/// <param name="Name">Название элемента.</param>
/// <param name="Price">Цена элемента.</param>
public record struct MenuItemForCreateDto(string Name, double Price);
