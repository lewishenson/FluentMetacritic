using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Domain
{
    public class Movie : Entity, IMovie
    {
        public Movie(string name)
            : base(name)
        {
            Starring = Enumerable.Empty<string>();
            Genres = Enumerable.Empty<string>();
        }

        public DateTime? ReleaseDate { get; set; }

        public int? CriticScore { get; set; }

        public decimal? UserScore { get; set; }

        public string Rated { get; set; }

        public IEnumerable<string> Starring { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public int? Runtime { get; set; }
    }
}