using System;

namespace FluentMetacritic.Domain
{
    public interface IFeature : IEntity
    {
        string PostedBy { get; }

        DateTime PostedOn { get; }
    }
}