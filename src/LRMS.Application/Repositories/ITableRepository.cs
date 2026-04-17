using LRMS.Application.Dto;

namespace LRMS.Application.Repositories;

public interface ITableRepository
{
    Task<IReadOnlyCollection<TableDto>> GetTables(CancellationToken ct = default);
}
