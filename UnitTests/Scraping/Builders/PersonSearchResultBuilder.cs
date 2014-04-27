using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class PersonSearchResultBuilder : SearchResultBuilderBase<PersonSearchResultBuilder>
    {
        protected override PersonSearchResultBuilder This
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
                return "Person";
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
                return ExtendedStats.Any() || AverageAlbumCareerScore.HasValue || AverageMovieCareerScore.HasValue || AverageTelevisionShowCareerScore.HasValue;
            }
        }

        protected int? AverageMovieCareerScore { get; set; }

        protected int? AverageTelevisionShowCareerScore { get; set; }

        protected int? AverageAlbumCareerScore { get; set; }

        public PersonSearchResultBuilder WithAverageMovieCareerScore(int averageMovieCareerScore)
        {
            AverageMovieCareerScore = averageMovieCareerScore;

            return This;
        }

        public PersonSearchResultBuilder WithAverageTelevisionShowCareerScore(int averageTelevisionShowCareerScore)
        {
            AverageTelevisionShowCareerScore = averageTelevisionShowCareerScore;

            return This;
        }

        public PersonSearchResultBuilder WithAverageAlbumCareerScore(int averageAlbumCareerScore)
        {
            AverageAlbumCareerScore = averageAlbumCareerScore;

            return This;
        }

        public PersonSearchResultBuilder WithCategories(IEnumerable<string> categories)
        {
            ExtendedStats[ExtendedStatClasses.Categories] = string.Join(", ", categories);

            return This;
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

            if (AverageAlbumCareerScore.HasValue)
            {
                AppendExtendedStat(htmlBuilder, ExtendedStatClasses.AverageCareerScore, "Average Album career score", AverageAlbumCareerScore.ToString());
            }

            if (AverageMovieCareerScore.HasValue)
            {
                AppendExtendedStat(htmlBuilder, ExtendedStatClasses.AverageCareerScore, "Average Movie career score", AverageMovieCareerScore.ToString());
            }

            if (AverageTelevisionShowCareerScore.HasValue)
            {
                AppendExtendedStat(htmlBuilder, ExtendedStatClasses.AverageCareerScore, "Average TV Show career score", AverageTelevisionShowCareerScore.ToString());
            }

            htmlBuilder.Append("        </ul>");
            htmlBuilder.Append("      </div>");
        }
    }
}