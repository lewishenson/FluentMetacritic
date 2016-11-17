using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Domain
{
    public class TelevisionShow : Entity, ITelevisionShow
    {
        public TelevisionShow(string name, DateTime startDate)
            : base(name)
        {
            StartDate = startDate;

            Starring = Enumerable.Empty<string>();
            Genres = Enumerable.Empty<string>();
        }

        public int? Score { get; set; }

        public DateTime StartDate { get; }

        public IEnumerable<string> Starring { get; set; }

        public IEnumerable<string> Genres { get; set; }
    }
}