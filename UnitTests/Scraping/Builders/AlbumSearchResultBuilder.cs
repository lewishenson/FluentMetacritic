using System.Text;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class AlbumSearchResultBuilder : SearchResultBuilderBase<AlbumSearchResultBuilder>
    {
        protected override AlbumSearchResultBuilder This
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
                return "Album";
            }
        }

        protected override string ExtendedStatsClass
        {
            get
            {
                return "basic_stat";
            }
        }

        protected int? Score { get; private set; }

        public AlbumSearchResultBuilder WithScore(int score)
        {
            Score = score;

            return This;
        }

        protected override void AppendMainStats(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("      <div class=\"main_stats\">");
            htmlBuilder.Append("        <h3 class=\"product_title basic_stat\">");
            htmlBuilder.AppendFormat("          <a href=\"#\">{0}</a>", Name);
            htmlBuilder.Append("        </h3>");

            if (Score.HasValue)
            {
                htmlBuilder.AppendFormat("        <span class=\"metascore_w medium album positive\">{0}</span>", Score);
            }

            htmlBuilder.Append("      </div>");
        }
    }
}