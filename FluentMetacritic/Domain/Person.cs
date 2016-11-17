using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Domain
{
    public class Person : Entity, IPerson
    {
        public Person(string name)
            : base(name)
        {
            Categories = Enumerable.Empty<string>();
        }

        public DateTime? DateOfBirth { get; set; }

        public int? AverageMovieCareerScore { get; set; }

        public int? AverageTelevisionShowCareerScore { get; set; }

        public int? AverageAlbumCareerScore { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}