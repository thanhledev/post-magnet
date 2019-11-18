using System;
using PostMagnet.Domain.Entities;
using PostMagnet.Domain.SpecificationFramework;

namespace PostMagnet.Web.Backend.Specifications
{
    public class WebsiteByHostSpecification : SpecificationBase<Website>
    {
        private string _host;

        public WebsiteByHostSpecification(string host)
        {
            _host = host;
        }

        public override System.Linq.Expressions.Expression<Func<Website, bool>> SpecExpression
        {
            get { return w => w.Host == _host; }
        }
    }
}