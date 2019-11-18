using System;

namespace PostMagnet.Domain.Entities
{
    public class Log
    {
        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Content { get; set; }
    }
}
