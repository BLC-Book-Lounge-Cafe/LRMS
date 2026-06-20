using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace LRMS.Web.IntegrationTests.Fixtures;

public class BackendFixture
{
    public const string TestsCollectionName = "Backend Tests";
    public ApiClient.ApiClient Client { get; }
    public ApiClient.ApiClient ClientWithAuthorization { get; set; }

    public BackendFixture()
    {
        var authProvider = new AnonymousAuthenticationProvider();
        var adapter = new HttpClientRequestAdapter(authProvider)
        {
            BaseUrl = BackendOptionsProvider.BaseAddress
        };
        Client = new ApiClient.ApiClient(adapter);
        CreateClientWithAuthorization().GetAwaiter().GetResult();
    }

    private async Task CreateClientWithAuthorization()
    {
        var token = await Client.Admin.Login.PostAsync(new ApiClient.Models.LoginRequest()
        {
            Login = BackendOptionsProvider.AdminLogin,
            Password = BackendOptionsProvider.AdminPassword
        });

        var accessTokenProvider = new BearerTokenProvider(token.Token);
        var authProvider = new BaseBearerTokenAuthenticationProvider(accessTokenProvider);
        var adapter = new HttpClientRequestAdapter(authProvider)
        {
            BaseUrl = BackendOptionsProvider.BaseAddress
        };
        ClientWithAuthorization = new ApiClient.ApiClient(adapter);
    }
}
