using System.Collections.Generic;
using JoeBlogs;

namespace PostMagnet.Services.SeoServices
{
    public interface ISEOPlugin
    {
        void SetupSEOFactors(string title, string desc, string metaKeywords, string addition, string chosenKeyword);
        CustomField[] createSEOFactors();
    }
}
