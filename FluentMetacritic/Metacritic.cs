using FluentMetacritic.DependencyInjection;
using FluentMetacritic.Search;

namespace FluentMetacritic
{
    public class Metacritic
    {
        public static IMetacriticSearch SearchFor()
        {
            return Factory.Instance.Create<IMetacriticSearch>();
        }
    }
}