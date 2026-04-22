using LRMS.Application.Tables.Dto;

namespace LRMS.Application.Tables;

public interface ITableRepository
{
    Task<IReadOnlyCollection<TableDto>> GetTables(CancellationToken ct = default);
}
