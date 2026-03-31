using LRMS.Web.Options;

namespace LRMS.Web.Extensions;

internal static class WebConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ConfigureOptions()
        {
            services.AddOptions<SpaceSettingsOptions>().BindConfiguration("SpaceSettingsOptions");
            return services;
        }
    }
}
