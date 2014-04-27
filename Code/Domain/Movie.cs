using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Domain
{
    public class Movie : Entity, IMovie
    {
        private readonly DateTime _releaseDate;

        public Movie(string name, DateTime releaseDate)
            : base(name)
        {
            _releaseDate = releaseDate;

            Starring = Enumerable.Empty<string>();
            Genres = Enumerable.Empty<string>();
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _releaseDate;
            }
        }

        public int? CriticScore { get; set; }

        public decimal? UserScore { get; set; }

        public string Rated { get; set; }

        public IEnumerable<string> Starring { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public int? Runtime { get; set; }
    }
}