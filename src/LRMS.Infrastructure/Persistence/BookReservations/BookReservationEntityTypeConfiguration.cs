using LRMS.Infrastructure.Persistence.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRMS.Infrastructure.Persistence.BookReservations;

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
