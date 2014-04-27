namespace FluentMetacritic.DependencyInjection
{
    public interface IFactory
    {
        T Create<T>();
    }
}