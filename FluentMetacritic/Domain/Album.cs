using System;

namespace FluentMetacritic.Domain
{
    public class Album : Entity, IAlbum
    {
        public Album(string name)
            : base(name)
        {
        }

        public DateTime? ReleaseDate { get; set; }

        public int? Score { get; set; }
    }
}