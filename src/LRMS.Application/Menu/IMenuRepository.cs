using LRMS.Application.Menu.Dto;

namespace LRMS.Application.Menu;

public interface IMenuRepository
{
    Task<IReadOnlyCollection<MenuCategoryDto>> GetMenuItems(CancellationToken ct = default);
    Task DeleteMenuCategory(long id, CancellationToken ct = default);
    Task<MenuCategoryDto> CreateMenuCategory(MenuCategoryForCreateDto category, CancellationToken ct = default);
    Task UpdateMenuCategory(MenuCategoryDto category, CancellationToken ct = default);
}
