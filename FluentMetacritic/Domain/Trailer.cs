using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Domain
{
    public class Trailer : Entity, ITrailer
    {
        private readonly DateTime _releaseDate;

        public Trailer(string name, DateTime releaseDate)
            : base(name)
        {
            _releaseDate = releaseDate;

            Starring = Enumerable.Empty<string>();
            Genres = Enumerable.Empty<string>();
        }

        public DateTime ReleaseDate => _releaseDate;

        public string Rated { get; set; }

        public IEnumerable<string> Starring { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public decimal? UserScore { get; set; }

        public int? Runtime { get; set; }

        public string MaturityRating { get; set; }

        public string Publisher { get; set; }
    }
}