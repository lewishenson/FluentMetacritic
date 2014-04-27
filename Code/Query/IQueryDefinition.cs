namespace FluentMetacritic.Query
{
    public interface IQueryDefinition<out T>
    {
        Category Category { get; set; }

        OrderBy OrderBy { get; set; }

        int? Page { get; set; }

        string Text { get; set; }
    }
}