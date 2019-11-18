using System;

namespace PostMagnet.Domain.Entities
{
    public class Website
    {
        public virtual int Id { get; set; }
        public virtual string Host { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual SeoPluginType SeoPlugin { get; set; }
        public virtual DateTime Tested { get; set; }
        public virtual string Note { get; set; }

        public override string ToString()
        {
            return String.Format("[ Host: {0}, Username: {1},", Host, Username);
        }
    }
}
