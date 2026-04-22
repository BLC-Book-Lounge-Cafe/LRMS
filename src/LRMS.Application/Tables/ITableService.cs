using LRMS.Application.Tables.Requests;

namespace LRMS.Application.Tables;

public interface ITableService
{
    Task<GetTablesResponse> GetTables(CancellationToken ct = default);
}
