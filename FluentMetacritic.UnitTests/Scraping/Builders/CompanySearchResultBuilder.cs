using System.Globalization;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class CompanySearchResultBuilder : SearchResultBuilderBase<CompanySearchResultBuilder>
    {
        protected override CompanySearchResultBuilder This
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
                return "Company";
            }
        }

        protected override string ExtendedStatsClass
        {
            get
            {
                return "basic_stat";
            }
        }

        public CompanySearchResultBuilder WithAverageCareerScore(int averageCareerScore)
        {
            ExtendedStats[ExtendedStatClasses.AverageCareerScore] = averageCareerScore.ToString(CultureInfo.InvariantCulture);

            return This;
        }
    }
}