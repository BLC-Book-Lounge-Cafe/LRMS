using LRMS.Application.Dto;
using LRMS.Application.Repositories;
using LRMS.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Repositories;

public class TableRepository(LrmsDbContext dbContext) : ITableRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<TableDto>> GetTables(CancellationToken ct = default)
    {
        var tables = await _dbContext.Tables.ToListAsync(ct);
        return [.. tables.Select(TableMapper.ToDto)];
    }
}
