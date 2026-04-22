using LRMS.Application.Tables.Requests;

namespace LRMS.Application.Tables;

public class TableService(ITableRepository repository) : ITableService
{
    private readonly ITableRepository _repository = repository;

    public async Task<GetTablesResponse> GetTables(CancellationToken ct = default)
    {
        return new(await _repository.GetTables(ct));
    }
}
