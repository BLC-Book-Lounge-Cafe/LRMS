using LRMS.Infrastructure.Options;

namespace LRMS.Web.Extensions;

internal static class WebConfigurationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ConfigureOptions()
        {
            services.AddOptions<YandexMusicOptions>().BindConfiguration("YandexMusicOptions");
            return services;
        }
    }
}
