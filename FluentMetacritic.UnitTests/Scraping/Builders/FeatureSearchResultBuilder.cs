namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class FeatureSearchResultBuilder : SearchResultBuilderBase<FeatureSearchResultBuilder>
    {
        protected override FeatureSearchResultBuilder This => this;

        protected override string ResultType => "Feature";

        protected override string ExtendedStatsClass => "basic_stat";

        public FeatureSearchResultBuilder WithPostedBy(string postedBy)
        {
            ExtendedStats[ExtendedStatClasses.Posted] = postedBy;

            return This;
        }
    }
}