using LRMS.Application.Menu.Requests;

namespace LRMS.Application.Menu;

public interface IMenuService
{
    Task<GetMenuResponse> GetMenu(CancellationToken ct = default);
    Task DeleteMenuCategory(int id, CancellationToken ct = default);
    Task DeleteMenuItem(int id, CancellationToken ct = default);
}
