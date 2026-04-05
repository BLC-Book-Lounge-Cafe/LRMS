using LRMS.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRMS.Infrastructure.Persistence.Configurations;

internal class BookReservationEntityTypeConfiguration : IEntityTypeConfiguration<BookReservationEntity>
{
    public void Configure(EntityTypeBuilder<BookReservationEntity> builder)
    {
        builder.HasOne<BookEntity>()
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
