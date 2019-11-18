using System.Collections.Generic;
using System.Web.Optimization;

namespace PostMagnet.Web.Backend.Helpers
{
    public class BundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}