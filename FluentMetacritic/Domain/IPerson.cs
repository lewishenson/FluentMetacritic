using System;
using System.Collections.Generic;

namespace FluentMetacritic.Domain
{
    public interface IPerson : IEntity
    {
        DateTime? DateOfBirth { get; }

        int? AverageMovieCareerScore { get; }

        int? AverageTelevisionShowCareerScore { get; }

        int? AverageAlbumCareerScore { get; }

        IEnumerable<string> Categories { get; }
    }
}