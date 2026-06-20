using LRMS.Application.Exceptions;
using LRMS.Application.SpaceState;
using LRMS.Application.SpaceState.Commands;
using LRMS.Application.SpaceState.Dto;
using Moq;
using Snapshooter.Xunit3;

namespace LRMS.Application.UnitTests;

public class SpaceStateServiceTests
{
    [Fact]
    public async Task GetSpaceStateSuccessful()
    {
        var service = new SpaceStateService(CreateRepository());

        var result = await service.GetSpaceStateAsync(TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    [Fact]
    public async Task UpdateSpaceStateSuccessful()
    {
        var service = new SpaceStateService(CreateRepository());

        await service.UpdateSpaceStateAsync(new UpdateSpaceStateCommand(1, "Description"), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task UpdateSpaceStateFailure()
    {
        var service = new SpaceStateService(CreateRepository());

        await Assert.ThrowsAsync<DataValidationException>(async () =>
            await service.UpdateSpaceStateAsync(new UpdateSpaceStateCommand(10, "Description"),
            TestContext.Current.CancellationToken));
    }

    private static ISpaceStateRepository CreateRepository()
    {
        var mock = new Mock<ISpaceStateRepository>();

        mock.Setup(mock => mock.GetSpaceStateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SpaceStateDto()
            {
                NoiseLevel = NoiseLevelType.Lively,
                WorkloadLevel = 30,
                Description = "Description",
                CurrentTrack = new CurrentTrackDto("Track", ["Author"], "Url")
            });

        return mock.Object;
    }
}
