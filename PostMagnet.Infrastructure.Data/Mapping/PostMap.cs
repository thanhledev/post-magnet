using FluentNHibernate.Mapping;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Infrastructure.Data.Mapping
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id).GeneratedBy.Increment().Column("post_id");
            Map(x => x.Code, "post_code");
            Map(x => x.Title, "post_title");
            Map(x => x.Content, "post_content");
            Map(x => x.Keywords, "post_keywords");
            Map(x => x.MetaTitle, "post_meta_title");
            Map(x => x.MetaDescription, "post_meta_description");
            Map(x => x.Tags, "post_tags");
            Map(x => x.Created, "post_created").CustomSqlType("DateTime");
            Map(x => x.Submitted, "post_submitted").Nullable().CustomSqlType("DateTime");
            Map(x => x.Approved, "post_approved").Nullable().CustomSqlType("DateTime");
            Map(x => x.Status, "post_status").CustomType(typeof(PostStatus));
            Map(x => x.UniquePercentage, "post_unique_percentage").Nullable();
            Map(x => x.Note, "post_note").Nullable();
            References(x => x.Contributor, "employee_id");
            References(x => x.Invoice, "invoice_id").Nullable().Cascade.All();
            HasMany(x => x.ExtraPayments).KeyColumn("post_id").Inverse().Cascade.AllDeleteOrphan();
            Map(x => x.Scheduled, "post_scheduled").Nullable().CustomSqlType("DateTime");
            Map(x => x.ScheduledWebsite, "post_scheduled_website").Nullable();
            Map(x => x.ScheduledCategory, "post_scheduled_category").Nullable();
            Map(x => x.Posted, "post_posted").Nullable().CustomSqlType("DateTime");
            Map(x => x.Url, "post_url").Nullable();
            Table("pm_posts");
            BatchSize(1000);
        }
    }
}
