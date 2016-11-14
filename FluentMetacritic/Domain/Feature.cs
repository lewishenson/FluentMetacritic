using System;

namespace FluentMetacritic.Domain
{
    public class Feature : Entity, IFeature
    {
        private readonly string _postedBy;

        private readonly DateTime _postedOn;

        public Feature(string name, string postedBy, DateTime postedOn)
            : base(name)
        {
            _postedBy = postedBy;
            _postedOn = postedOn;
        }

        public string PostedBy => _postedBy;

        public DateTime PostedOn => _postedOn;
    }
}