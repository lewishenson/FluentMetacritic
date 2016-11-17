using System.Text;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class GameSearchResultBuilder : SearchResultBuilderBase<GameSearchResultBuilder>
    {
        protected override GameSearchResultBuilder This => this;

        protected override string ResultType => "Game";

        protected override string ExtendedStatsClass => "more_stats extended_stats";

        protected string Platform { get; private set; }

        protected int? Score { get; private set; }

        public GameSearchResultBuilder WithPlatform(string platform)
        {
            Platform = platform;

            return This;
        }

        public GameSearchResultBuilder WithScore(int score)
        {
            Score = score;

            return This;
        }

        public GameSearchResultBuilder WithMaturityRating(string maturityRating)
        {
            ExtendedStats[ExtendedStatClasses.MaturityRating] = maturityRating;

            return This;
        }

        public GameSearchResultBuilder WithPublisher(string publisher)
        {
            ExtendedStats[ExtendedStatClasses.Publisher] = publisher;

            return This;
        }

        protected override void AppendResultType(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("  <div class=\"result_type\">");
            htmlBuilder.AppendFormat("    <strong>{0}</strong>", "Game");
            htmlBuilder.AppendFormat("    <span class=\"platform\">{0}</span>", Platform);
            htmlBuilder.Append("  </div>");
        }

        protected override void AppendMainStats(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("      <div class=\"main_stats\">");
            htmlBuilder.Append("        <h3 class=\"product_title basic_stat\">");
            htmlBuilder.AppendFormat("          <a href=\"#\">{0}</a>", Name);
            htmlBuilder.Append("        </h3>");

            if (Score.HasValue)
            {
                htmlBuilder.AppendFormat("        <span class=\"metascore_w medium game positive\">{0}</span>", Score);
            }

            htmlBuilder.Append("      </div>");
        }
    }
}