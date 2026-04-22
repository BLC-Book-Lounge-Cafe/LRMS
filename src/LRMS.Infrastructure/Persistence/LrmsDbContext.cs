using LRMS.Infrastructure.Persistence.BookReservations;
using LRMS.Infrastructure.Persistence.Books;
using LRMS.Infrastructure.Persistence.Menu;
using LRMS.Infrastructure.Persistence.ReservationRequests;
using LRMS.Infrastructure.Persistence.SpaceSettings;
using LRMS.Infrastructure.Persistence.SpaceState;
using LRMS.Infrastructure.Persistence.TableReservations;
using LRMS.Infrastructure.Persistence.Tables;
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
    public virtual DbSet<BookEntity> Books { get; set; }
    public virtual DbSet<BookReservationEntity> BookReservations { get; set; }
    public virtual DbSet<SpaceStateEntity> SpaceStates { get; set; }
    public virtual DbSet<TableEntity> Tables { get; set; }
    public virtual DbSet<TableReservationEntity> TableReservations { get; set; }
    public virtual DbSet<MenuCategoryEntity> MenuCategories { get; set; }
    public virtual DbSet<MenuItemEntity> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new BookReservationEntityTypeConfiguration().Configure(modelBuilder.Entity<BookReservationEntity>());
        new TableReservationEntityTypeConfiguration().Configure(modelBuilder.Entity<TableReservationEntity>());
        new MenuItemEntityTypeConfiguration().Configure(modelBuilder.Entity<MenuItemEntity>());
    }
}
