using AutoMapper;

namespace PostMagnet.Web.Backend
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EmployeeMapperProfile());
                cfg.AddProfile(new RoleMapperProfile());
                cfg.AddProfile(new PermissionMapperProfile());
                cfg.AddProfile(new LogMapperProfile());
                cfg.AddProfile(new WebsiteMapperProfile());
                cfg.AddProfile(new PostMapperProfile());
                cfg.AddProfile(new MessageMapperProfile());
                cfg.AddProfile(new NotificationMapperProfile());
            });
        }
    }
}