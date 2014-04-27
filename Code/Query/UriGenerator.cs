using System;
using System.Web;

namespace FluentMetacritic.Query
{
    public class UriGenerator : IUriGenerator
    {
        public UriGenerator()
        {
            Category = Category.All;
            OrderBy = OrderBy.Relevancy;
        }

        public Category Category { get; set; }

        public OrderBy OrderBy { get; set; }

        public int? Page { get; set; }

        public static IUriGenerator Create()
        {
            return new UriGenerator();
        }

        public string Generate(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException("Search term is required.", "searchTerm");
            }

            var encodedSearchTerm = EncodeSearchTerm(searchTerm);
            var uri = string.Format("http://www.metacritic.com/search/{0}/{1}/results", Category.UriValue, encodedSearchTerm);

            var pageAdded = false;
            if (Page.HasValue && Page.Value > 1)
            {
                // Metacritic uses zero-based pages.
                uri += "?page=" + (Page.Value - 1);
                pageAdded = true;
            }

            if (OrderBy != OrderBy.Relevancy)
            {
                if (pageAdded)
                {
                    uri += "&";
                }
                else
                {
                    uri += "?";
                }

                uri += "sort=" + OrderBy.QueryStringValue;
            }

            return uri;
        }

        private static string EncodeSearchTerm(string input)
        {
            return HttpUtility.UrlEncode(input.Trim().Replace(" ", "+"));
        }
    }
}