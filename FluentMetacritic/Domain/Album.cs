using System;

namespace FluentMetacritic.Domain
{
    public class Album : Entity, IAlbum
    {
        private readonly DateTime _releaseDate;

        public Album(string name, DateTime releaseDate)
            : base(name)
        {
            _releaseDate = releaseDate;
        }

        public DateTime ReleaseDate => _releaseDate;

        public int? Score { get; set; }
    }
}