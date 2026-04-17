using LRMS.Application.Dto;

namespace LRMS.Application.Repositories;

public interface IMenuRepository
{
    Task<IReadOnlyCollection<MenuCategoryDto>> GetMenuItems(CancellationToken ct = default);
}
