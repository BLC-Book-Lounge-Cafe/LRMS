using LRMS.Infrastructure.Persistence.Models;

namespace LRMS.Infrastructure.Persistence.Seeder;

internal class LrmsSeeder(LrmsDbContext dbContext)
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public void Seed()
    {
        AddSpaceSettings();
    }

    private void AddSpaceSettings()
    {
        if (_dbContext.SpaceSettings.Any())
            return;

        var spaceSettings = new SpaceSettingsEntity
        {
            StartTime = new(9, 0),
            EndTime = new(21, 0),
            MinTableReservationTime = TimeSpan.FromMinutes(60)
        };

        _dbContext.SpaceSettings.Add(spaceSettings);
        _dbContext.SaveChanges();
    }
}
