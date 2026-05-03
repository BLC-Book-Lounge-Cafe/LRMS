using LRMS.Application.Menu.Dto;
using LRMS.Application.Menu.Requests;

namespace LRMS.Application.Menu;

public interface IMenuService
{
    Task<GetMenuResponse> GetMenu(CancellationToken ct = default);
    Task DeleteMenuCategory(int id, CancellationToken ct = default);
    Task<MenuCategoryDto> CreateMenuCategory(MenuCategoryForCreateDto category, CancellationToken ct = default);
    Task UpdateMenuCategory(MenuCategoryDto category, CancellationToken ct = default);
}
