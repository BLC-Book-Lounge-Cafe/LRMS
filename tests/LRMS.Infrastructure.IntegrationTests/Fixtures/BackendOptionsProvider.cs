using LRMS.IntegrationTests.Core;

namespace LRMS.Infrastructure.IntegrationTests.Fixtures;

public static class BackendOptionsProvider
{
    private const string AddressVarName = "RESERVATION_MANAGER_ADDRESS";
    private const string TokenVarName = "RESERVATION_MANAGER_TOKEN";
    private static readonly List<string> _backendEnvironmentVariables =
        [AddressVarName, TokenVarName];
    private static readonly Dictionary<string, string> _environmentVariables =
        EnvironmentVariablesExtractor.Get(AppContext.BaseDirectory, _backendEnvironmentVariables);
    public static string Address => _environmentVariables[AddressVarName];
    public static string Token => _environmentVariables[TokenVarName];
}
