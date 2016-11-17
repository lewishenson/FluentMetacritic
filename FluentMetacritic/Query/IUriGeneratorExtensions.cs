namespace FluentMetacritic.Query
{
    public static class IUriGeneratorExtensions
    {
        public static IUriGenerator ForCategory(this IUriGenerator uriGenerator, Category category)
        {
            uriGenerator.Category = category;

            return uriGenerator;
        }

        public static IUriGenerator OrderedBy(this IUriGenerator uriGenerator, OrderBy orderBy)
        {
            uriGenerator.OrderBy = orderBy;

            return uriGenerator;
        }

        public static IUriGenerator Page(this IUriGenerator uriGenerator, int? page)
        {
            uriGenerator.Page = page;

            return uriGenerator;
        }
    }
}