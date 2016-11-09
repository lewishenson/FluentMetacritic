using System;
using System.Collections.Generic;

namespace FluentMetacritic.Domain
{
    public interface IMovie : IEntity
    {
        DateTime ReleaseDate { get; }

        int? CriticScore { get; }

        decimal? UserScore { get; }

        string Rated { get; }

        IEnumerable<string> Starring { get; }

        IEnumerable<string> Genres { get; }

        int? Runtime { get; }
    }
}