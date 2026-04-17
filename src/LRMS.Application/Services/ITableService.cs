using LRMS.Application.Requests;

namespace LRMS.Application.Services;

public interface ITableService
{
    Task<GetTablesResponse> GetTables(CancellationToken ct = default);
}
