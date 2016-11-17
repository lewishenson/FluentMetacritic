using System;

namespace FluentMetacritic.Domain
{
    public interface IAlbum : IEntity
    {
        DateTime? ReleaseDate { get; }

        int? Score { get; }
    }
}