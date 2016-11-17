using System;

namespace FluentMetacritic.Domain
{
    public interface IGame : IEntity
    {
        string Platform { get; }

        DateTime? ReleaseDate { get; }

        int? Score { get; }

        string MaturityRating { get; }

        string Publisher { get; }
    }
}