using LRMS.Application.Tables;
using LRMS.Infrastructure.IntegrationTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit3;

namespace LRMS.Infrastructure.IntegrationTests
{
    [Trait("Category", "Integration")]
    [Collection("Database collection")]
    public class TableRepositoryTests(ServiceFixture serviceFixture) : IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _serviceFixture = serviceFixture;

        [Fact]
        public async Task GetTablesSuccessful()
        {
            var tableRepository = _serviceFixture.GetRequiredService<ITableRepository>();

            var result = await tableRepository.GetTables(TestContext.Current.CancellationToken);

            result.MatchSnapshot();
        }
    }
}
