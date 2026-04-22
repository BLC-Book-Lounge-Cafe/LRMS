using LRMS.Application.Menu.Requests;

namespace LRMS.Application.Menu;

public class MenuService(IMenuRepository repository) : IMenuService
{
    private readonly IMenuRepository _repository = repository;

    public async Task DeleteMenuCategory(int id, CancellationToken ct = default)
    {
        await _repository.DeleteMenuCategory(id, ct);
    }

    public async Task DeleteMenuItem(int id, CancellationToken ct = default)
    {
        await _repository.DeleteMenuItem(id, ct);
    }

    public async Task<GetMenuResponse> GetMenu(CancellationToken ct = default)
    {
        return new(await _repository.GetMenuItems(ct));
    }
}
