using LRMS.Application.Menu.Requests;

namespace LRMS.Application.Menu;

public class MenuService(IMenuRepository repository) : IMenuService
{
    private readonly IMenuRepository _repository = repository;

    public async Task<GetMenuResponse> GetMenu(CancellationToken ct = default)
    {
        return new(await _repository.GetMenuItems(ct));
    }
}
