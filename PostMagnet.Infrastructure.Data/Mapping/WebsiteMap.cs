using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class WebsiteMap : ClassMap<Website>
    {
        public WebsiteMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("website_id");
            Map(x => x.Host, "website_host");
            Map(x => x.Username, "website_username");
            Map(x => x.Password, "website_password");
            Map(x => x.TimeZone, "website_timezone");
            Map(x => x.SeoPlugin, "website_seo_plugin").CustomType(typeof(SeoPluginType));
            Map(x => x.Tested, "website_tested").CustomSqlType("DateTime");
            Map(x => x.Note, "website_note").Nullable();
            Table("pm_websites");
            BatchSize(5000);
        }
    }
}
