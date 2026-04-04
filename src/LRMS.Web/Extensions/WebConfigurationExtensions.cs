namespace LRMS.Web.Extensions;

internal static class WebConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ConfigureOptions()
        {
            return services;
        }
    }
}
