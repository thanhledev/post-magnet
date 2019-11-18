using JoeBlogs;

namespace PostMagnet.Services.SeoServices
{
    public class YoastSeoPlugin : ISEOPlugin
    {
        private static string _titleMetaKey = "_yoast_wpseo_title";
        private static string _descMetaKey = "_yoast_wpseo_metadesc";
        private static string _keywordsMetaKey = "_yoast_wpseo_metakeywords";
        private static string _focusKwMetaKey = "_yoast_wpseo_focuskw";

        private string yoastSEOTitle;
        private string yoastSEODescription;
        private string yoastSEOKeywordsMeta;
        private string focusKwValue;
        private string keyword;

        public void SetupSEOFactors(string title, string desc, string metaKeywords, string addition, string chosenKeyword)
        {
            yoastSEOTitle = title;
            yoastSEODescription = desc;
            yoastSEOKeywordsMeta = metaKeywords;
            focusKwValue = addition;
            keyword = chosenKeyword;
        }

        public CustomField[] createSEOFactors()
        {
            var cfs = new CustomField[]
                {
                    new CustomField()
                    {
                        Key = _titleMetaKey,
                        Value = yoastSEOTitle
                    },
                    new CustomField()
                    {
                        Key = _descMetaKey,
                        Value = yoastSEODescription
                    },                
                    new CustomField()
                    {
                        Key = _focusKwMetaKey,
                        Value = keyword
                    }
                };

            return cfs;
        }
    }
}
