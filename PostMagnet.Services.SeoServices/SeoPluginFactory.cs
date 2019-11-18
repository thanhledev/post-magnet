using PostMagnet.Domain.Entities;

namespace PostMagnet.Services.SeoServices
{
    public static class SeoPluginFactory
    {
        public static ISEOPlugin CreatePlugin(SeoPluginType type)
        {
            if (type == SeoPluginType.YoastSeo)
                return new YoastSeoPlugin();
            return new NullSeoPlugin();
        }
    }
}
