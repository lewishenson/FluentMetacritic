namespace FluentMetacritic.Query
{
    public interface IUriGenerator
    {
        Category Category { get; set; }

        OrderBy OrderBy { get; set; }

        int? Page { get; set; }

        string Generate(string searchTerm);
    }
}