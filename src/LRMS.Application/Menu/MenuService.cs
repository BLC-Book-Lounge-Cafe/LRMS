using LRMS.Application.Menu.Dto;
using LRMS.Application.Menu.Requests;

namespace LRMS.Application.Menu;

public class MenuService(IMenuRepository repository) : IMenuService
{
    private readonly IMenuRepository _repository = repository;

    public async Task<MenuCategoryDto> CreateMenuCategory(MenuCategoryForCreateDto category, CancellationToken ct = default)
    {
        return await _repository.CreateMenuCategory(category, ct);
    }

    public async Task DeleteMenuCategory(int id, CancellationToken ct = default)
    {
        await _repository.DeleteMenuCategory(id, ct);
    }

    public async Task<GetMenuResponse> GetMenu(CancellationToken ct = default)
    {
        return new(await _repository.GetMenuItems(ct));
    }

    public async Task UpdateMenuCategory(MenuCategoryDto category, CancellationToken ct = default)
    {
        await _repository.UpdateMenuCategory(category, ct);
    }
}
