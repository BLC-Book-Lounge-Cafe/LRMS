using LRMS.Infrastructure.Persistence.Models;
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

    public virtual DbSet<SpaceSettingsEntity> SpaceSettings { get; set; }
    public virtual DbSet<ReservationRequestEntity> ReservationRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
