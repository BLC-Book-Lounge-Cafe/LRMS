using LRMS.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRMS.Infrastructure.Persistence.Configurations;

internal class TableReservationEntityTypeConfiguration : IEntityTypeConfiguration<TableReservationEntity>
{
    public void Configure(EntityTypeBuilder<TableReservationEntity> builder)
    {
        builder.HasOne<TableEntity>()
            .WithMany()
            .HasForeignKey(x => x.TableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
