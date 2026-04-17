using LRMS.Application.Dto;

namespace LRMS.Application.Queries;

/// <summary>
///     Ответ на запрос меню.
/// </summary>
/// <param name="MenuCategories">Категории меню.</param>
public record struct GetMenuResponse(IReadOnlyCollection<MenuCategoryDto> MenuCategories);
