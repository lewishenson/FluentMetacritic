namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class FeatureSearchResultBuilder : SearchResultBuilderBase<FeatureSearchResultBuilder>
    {
        protected override FeatureSearchResultBuilder This
        {
            get
            {
                return this;
            }
        }

        protected override string ResultType
        {
            get
            {
                return "Feature";
            }
        }

        protected override string ExtendedStatsClass
        {
            get
            {
                return "basic_stat";
            }
        }

        public FeatureSearchResultBuilder WithPostedBy(string postedBy)
        {
            ExtendedStats[ExtendedStatClasses.Posted] = postedBy;

            return This;
        }
    }
}