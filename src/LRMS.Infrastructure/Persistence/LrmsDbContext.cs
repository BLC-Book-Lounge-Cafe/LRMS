using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence;

public class LrmsDbContext : DbContext
{
    public LrmsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected LrmsDbContext()
    {
    }
}
