using LRMS.Application.SpaceState;
using LRMS.Infrastructure.IntegrationTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit3;

namespace LRMS.Infrastructure.IntegrationTests;

[Trait("Category", "Integration")]
[Collection("Database collection")]
public class SpaceStateRepositoryTests(ServiceFixture serviceFixture) : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _serviceFixture = serviceFixture;

    [Fact]
    public async Task UpdateSpaceStateAsync()
    {
        var spaceStateRepository = _serviceFixture.GetRequiredService<ISpaceStateRepository>();

        await spaceStateRepository.UpdateSpaceStateAsync(2, "description", TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetSpaceStateSuccessful()
    {
        var spaceStateRepository = _serviceFixture.GetRequiredService<ISpaceStateRepository>();

        var result = await spaceStateRepository.GetSpaceStateAsync(TestContext.Current.CancellationToken);
    }
}
