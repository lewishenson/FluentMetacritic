using System.Collections.Generic;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public class TrailerSearchResultBuilder : SearchResultBuilderBase<TrailerSearchResultBuilder>
    {
        protected override TrailerSearchResultBuilder This => this;

        protected override string ResultType => "Trailer";

        protected override string ExtendedStatsClass => "more_stats extended_stats";

        public TrailerSearchResultBuilder WithUserScore(decimal score)
        {
            ExtendedStats[ExtendedStatClasses.UserScore] = string.Format("{0:.#}", score);

            return This;
        }

        public TrailerSearchResultBuilder WithRating(string rating)
        {
            ExtendedStats[ExtendedStatClasses.Rating] = rating;

            return This;
        }

        public TrailerSearchResultBuilder WithCast(IEnumerable<string> castMembers)
        {
            ExtendedStats[ExtendedStatClasses.Cast] = string.Join(",", castMembers);

            return This;
        }

        public TrailerSearchResultBuilder WithGenres(IEnumerable<string> genres)
        {
            ExtendedStats[ExtendedStatClasses.Genres] = string.Join(",", genres);

            return This;
        }

        public TrailerSearchResultBuilder WithRuntime(int minutes)
        {
            ExtendedStats[ExtendedStatClasses.Runtime] = string.Format("{0} min", minutes);

            return This;
        }

        public TrailerSearchResultBuilder WithMaturityRating(string maturityRating)
        {
            ExtendedStats[ExtendedStatClasses.MaturityRating] = maturityRating;

            return This;
        }

        public TrailerSearchResultBuilder WithPublisher(string publisher)
        {
            ExtendedStats[ExtendedStatClasses.Publisher] = publisher;

            return This;
        }
    }
}