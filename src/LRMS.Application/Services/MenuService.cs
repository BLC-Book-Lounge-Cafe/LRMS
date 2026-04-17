using LRMS.Application.Queries;
using LRMS.Application.Repositories;

namespace LRMS.Application.Services;

public class MenuService(IMenuRepository repository) : IMenuService
{
    private readonly IMenuRepository _repository = repository;

    public async Task<GetMenuResponse> GetMenu(CancellationToken ct = default)
    {
        return new(await _repository.GetMenuItems(ct));
    }
}
