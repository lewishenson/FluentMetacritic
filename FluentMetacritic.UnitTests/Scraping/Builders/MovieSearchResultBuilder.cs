using System.Collections.Generic;
using System.Text;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class MovieSearchResultBuilder : SearchResultBuilderBase<MovieSearchResultBuilder>
    {
        protected override MovieSearchResultBuilder This => this;

        protected override string ResultType => "Movie";

        protected override string ExtendedStatsClass => "more_stats extended_stats";

        protected int? CriticScore { get; private set; }

        public MovieSearchResultBuilder WithCriticScore(int score)
        {
            CriticScore = score;

            return This;
        }

        public MovieSearchResultBuilder WithUserScore(decimal score)
        {
            ExtendedStats[ExtendedStatClasses.UserScore] = string.Format("{0:.#}", score);

            return This;
        }

        public MovieSearchResultBuilder WithRating(string rating)
        {
            ExtendedStats[ExtendedStatClasses.Rating] = rating;

            return This;
        }

        public MovieSearchResultBuilder WithCast(IEnumerable<string> castMembers)
        {
            ExtendedStats[ExtendedStatClasses.Cast] = string.Join(", ", castMembers);

            return This;
        }

        public MovieSearchResultBuilder WithGenres(IEnumerable<string> genres)
        {
            ExtendedStats[ExtendedStatClasses.Genres] = string.Join(", ", genres);

            return This;
        }

        public MovieSearchResultBuilder WithRuntime(int minutes)
        {
            ExtendedStats[ExtendedStatClasses.Runtime] = string.Format("{0} min", minutes);

            return This;
        }

        protected override void AppendMainStats(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("      <div class=\"main_stats\">");
            htmlBuilder.Append("        <h3 class=\"product_title basic_stat\">");
            htmlBuilder.AppendFormat("          <a href=\"#\">{0}</a>", Name);
            htmlBuilder.Append("        </h3>");

            if (CriticScore.HasValue)
            {
                htmlBuilder.AppendFormat("        <span class=\"metascore_w medium movie positive\">{0}</span>", CriticScore);
            }

            htmlBuilder.Append("      </div>");
        }
    }
}