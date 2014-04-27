using System;
using System.Collections.Generic;

namespace FluentMetacritic.Domain
{
    public interface ITrailer : IEntity
    {
        DateTime ReleaseDate { get; }

        decimal? UserScore { get; }

        string Rated { get; }

        IEnumerable<string> Starring { get; }

        IEnumerable<string> Genres { get; }

        int? Runtime { get; }

        string MaturityRating { get; }

        string Publisher { get; }
    }
}