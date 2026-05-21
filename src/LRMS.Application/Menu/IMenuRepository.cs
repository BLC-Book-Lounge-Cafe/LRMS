using LRMS.Application.Menu.Commands;
using LRMS.Application.Menu.Dto;

namespace LRMS.Application.Menu;

public interface IMenuRepository
{
    Task<IReadOnlyCollection<MenuCategoryDto>> GetMenuItems(CancellationToken ct = default);
    Task DeleteMenuCategory(long id, CancellationToken ct = default);
    Task<MenuCategoryDto> CreateMenuCategory(CreateMenuCategoryCommand category, CancellationToken ct = default);
    Task<MenuCategoryDto> UpdateMenuCategory(long id, UpdateMenuCategoryCommand command, CancellationToken ct = default);
}
