using JoeBlogs;

namespace PostMagnet.Services.SeoServices
{
    public class NullSeoPlugin : ISEOPlugin
    {
        public void SetupSEOFactors(string title, string desc, string metaKeywords, string addition, string chosenKeyword)
        {
        }

        public CustomField[] createSEOFactors()
        {
            return null;
        }
    }
}
