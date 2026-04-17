using LRMS.Application.Queries;

namespace LRMS.Application.Services;

public interface IMenuService
{
    Task<GetMenuResponse> GetMenu(CancellationToken ct = default);
}
