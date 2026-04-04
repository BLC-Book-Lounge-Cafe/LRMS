using LRMS.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LRMS.Infrastructure.Persistence.Configurations;

public class SpaceSettingsEntityTypeConfiguration : IEntityTypeConfiguration<SpaceSettingsEntity>
{
    public void Configure(EntityTypeBuilder<SpaceSettingsEntity> builder)
    {
        builder.HasKey(s => s.Id);
    }
}
