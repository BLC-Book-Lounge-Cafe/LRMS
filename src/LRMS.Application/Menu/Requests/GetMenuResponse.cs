using LRMS.Application.Menu.Dto;

namespace LRMS.Application.Menu.Requests;

/// <summary>
///     Ответ на запрос меню.
/// </summary>
/// <param name="MenuCategories">Категории меню.</param>
public record struct GetMenuResponse(IReadOnlyCollection<MenuCategoryDto> MenuCategories);
