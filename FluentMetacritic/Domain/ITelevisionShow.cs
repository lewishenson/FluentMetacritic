using System;
using System.Collections.Generic;

namespace FluentMetacritic.Domain
{
    public interface ITelevisionShow : IEntity
    {
        int? Score { get; }

        DateTime StartDate { get; }

        IEnumerable<string> Starring { get; }

        IEnumerable<string> Genres { get; }
    }
}