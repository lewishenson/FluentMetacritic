using System.Diagnostics;

namespace FluentMetacritic.Query
{
    [DebuggerDisplay("Name = {Name}")]
    public class OrderBy
    {
        public static readonly OrderBy Relevancy = new OrderBy("Relevancy", "relevancy");
        public static readonly OrderBy Score = new OrderBy("Score", "score");
        public static readonly OrderBy MostRecent = new OrderBy("Most Recent", "recent");

        private readonly string _name;

        private readonly string _queryStringValue;

        private OrderBy(string name, string queryStringValue)
        {
            _name = name;
            _queryStringValue = queryStringValue;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string QueryStringValue
        {
            get
            {
                return _queryStringValue;
            }
        }
    }
}