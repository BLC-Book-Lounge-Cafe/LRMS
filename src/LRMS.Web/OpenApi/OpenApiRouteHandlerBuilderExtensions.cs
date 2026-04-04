namespace LRMS.Web.OpenApi;

internal static class OpenApiRouteHandlerBuilderExtensions
{
    extension(RouteHandlerBuilder builder)
    {
        public RouteHandlerBuilder ProducesWithDescription<TResponse>(int statusCode, string description) =>
            builder.ProducesWithDescription(statusCode, description, typeof(TResponse));

        public RouteHandlerBuilder ProducesWithDescription(int statusCode, string description) =>
            builder.ProducesWithDescription(statusCode, description, null);

        public RouteHandlerBuilder ProducesWithDescription(int statusCode, string description, Type? responseType)
        {
            return builder.WithMetadata(new ProducesResponseTypeMetadata(statusCode, responseType)
            {
                Description = description
            });
        }
    }
}
