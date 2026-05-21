using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRMS.Infrastructure.Persistence.SpaceState;

public class SpaceStateEntityTypeConfiguration : IEntityTypeConfiguration<SpaceStateEntity>
{
    public void Configure(EntityTypeBuilder<SpaceStateEntity> builder)
    {
        builder.ToTable(t => t.HasCheckConstraint("CK_Noise_Level", "noise_level >= 0 and noise_level <= 5"));
    }
}
