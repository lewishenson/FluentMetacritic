using System.Globalization;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class CompanySearchResultBuilder : SearchResultBuilderBase<CompanySearchResultBuilder>
    {
        protected override CompanySearchResultBuilder This => this;

        protected override string ResultType => "Company";

        protected override string ExtendedStatsClass => "basic_stat";

        public CompanySearchResultBuilder WithAverageCareerScore(int averageCareerScore)
        {
            ExtendedStats[ExtendedStatClasses.AverageCareerScore] = averageCareerScore.ToString(CultureInfo.InvariantCulture);

            return This;
        }
    }
}