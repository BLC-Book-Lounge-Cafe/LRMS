using LRMS.Application.Tables;
using LRMS.Application.Tables.Dto;
using Moq;
using Snapshooter.Xunit3;

namespace LRMS.Application.UnitTests;

public class TableServiceTests
{
    [Fact]
    public async Task GetTablesSuccessful()
    {
        var service = new TableService(CreateRepository());

        var result = await service.GetTables(TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    private static ITableRepository CreateRepository()
    {
        var mock = new Mock<ITableRepository>();

        mock.Setup(mock => mock.GetTables(It.IsAny<CancellationToken>()))
            .ReturnsAsync([
                    new TableDto(1, 1),
                    new TableDto(2, 10),
                    new TableDto(4, 6),
                    new TableDto(5, 9),
                    new TableDto(6, 5),
                ]);

        return mock.Object;
    }
}
