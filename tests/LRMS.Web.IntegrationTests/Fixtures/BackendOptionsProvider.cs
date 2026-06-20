using LRMS.IntegrationTests.Core;

namespace LRMS.Web.IntegrationTests.Fixtures;

public static class BackendOptionsProvider
{
    private const string BackendHostVarName = "BACKEND_HOST";
    private const string BackendPortVarName = "BACKEND_PORT";
    private const string AdminLoginVarName = "ADMIN_LOGIN";
    private const string AdminPasswordVarName = "ADMIN_PASSWORD";
    private static readonly List<string> _backendEnvironmentVariables =
        [BackendHostVarName, BackendPortVarName, AdminLoginVarName, AdminPasswordVarName];
    private static readonly Dictionary<string, string> _environmentVariables =
        EnvironmentVariablesExtractor.Get(AppContext.BaseDirectory, _backendEnvironmentVariables);

    public static string BaseAddress =>
        $"http://{_environmentVariables[BackendHostVarName]}:{_environmentVariables[BackendPortVarName]}/";
    public static string AdminLogin => _environmentVariables[AdminLoginVarName];
    public static string AdminPassword => _environmentVariables[AdminPasswordVarName];
}
