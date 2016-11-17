using System;

namespace FluentMetacritic.Domain
{
    public class Game : Entity, IGame
    {
        public Game(string name, string platform)
            : base(name)
        {
            Platform = platform;
        }

        public string Platform { get; }

        public DateTime? ReleaseDate { get; set; }

        public int? Score { get; set; }

        public string MaturityRating { get; set; }

        public string Publisher { get; set; }
    }
}