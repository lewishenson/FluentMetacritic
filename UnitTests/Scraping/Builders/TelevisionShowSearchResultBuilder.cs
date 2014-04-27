using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class TelevisionShowSearchResultBuilder : SearchResultBuilderBase<TelevisionShowSearchResultBuilder>
    {
        public TelevisionShowSearchResultBuilder()
        {
            Cast = Enumerable.Empty<string>();
        }

        protected override TelevisionShowSearchResultBuilder This
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
                return "TV Show";
            }
        }

        protected override string ExtendedStatsClass
        {
            get
            {
                return "basic_stat";
            }
        }

        protected bool HasExtendedStats
        {
            get
            {
                return ExtendedStats.Any() || Cast.Any();
            }
        }

        protected int? Score { get; private set; }

        protected IEnumerable<string> Cast { get; private set; }

        public TelevisionShowSearchResultBuilder WithScore(int score)
        {
            Score = score;

            return This;
        }

        public TelevisionShowSearchResultBuilder WithCast(IEnumerable<string> cast)
        {
            Cast = cast;

            return This;
        }

        public TelevisionShowSearchResultBuilder WithGenres(IEnumerable<string> genres)
        {
            ExtendedStats[ExtendedStatClasses.Genres] = string.Join(", ", genres);

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
                htmlBuilder.AppendFormat("        <span class=\"metascore_w medium tvshow positive\">{0}</span>", Score);
            }

            htmlBuilder.Append("      </div>");
        }

        protected override void AppendExtendedStats(StringBuilder htmlBuilder)
        {
            if (!HasExtendedStats)
            {
                return;
            }

            htmlBuilder.Append("      <div class=\"basic_stat\">");
            htmlBuilder.Append("        <ul class=\"more_stats\">");

            foreach (var extendedStat in ExtendedStats)
            {
                AppendExtendedStat(htmlBuilder, extendedStat.Key, extendedStat.Key, extendedStat.Value);
            }

            if (Cast.Any())
            {
                AppendCast(htmlBuilder);
            }

            htmlBuilder.Append("        </ul>");
            htmlBuilder.Append("      </div>");
        }

        private void AppendCast(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("          <li class=\"stat\">");
            htmlBuilder.Append("            <span class=\"label cast\">Starring: </span>");

            foreach (var castMember in Cast)
            {
                htmlBuilder.AppendFormat("            <span class=\"data\">{0}</span>", castMember);
            }

            htmlBuilder.Append("          </li>");
        }
    }
}