namespace LRMS.Web.Extensions;

public static class EndpointBuilderExtensions
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapApi()
        {
            return endpointRouteBuilder;
        }
    }

}
