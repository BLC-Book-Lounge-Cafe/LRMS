using LRMS.Application.Menu.Dto;

namespace LRMS.Application.Menu;

public interface IMenuRepository
{
    Task<IReadOnlyCollection<MenuCategoryDto>> GetMenuItems(CancellationToken ct = default);
}
