using System;

namespace FluentMetacritic.Domain
{
    public class Game : Entity, IGame
    {
        private readonly string _platform;

        private readonly DateTime _releaseDate;

        public Game(string name, string platform, DateTime releaseDate)
            : base(name)
        {
            _platform = platform;
            _releaseDate = releaseDate;
        }

        public string Platform
        {
            get
            {
                return _platform;
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _releaseDate;
            }
        }

        public int? Score { get; set; }

        public string MaturityRating { get; set; }

        public string Publisher { get; set; }
    }
}