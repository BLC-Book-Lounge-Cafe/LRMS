using Microsoft.Kiota.Abstractions.Authentication;

namespace LRMS.Web.IntegrationTests.Fixtures;

public class BearerTokenProvider(string token) : IAccessTokenProvider
{
    private readonly string _token = token;

    public AllowedHostsValidator AllowedHostsValidator { get; } = new AllowedHostsValidator();

    public Task<string> GetAuthorizationTokenAsync(
        Uri uri,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_token);
    }
}
