using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentMetacritic.UnitTests.Scraping.Builders
{
    public abstract class SearchResultBuilderBase<TBuilder> where TBuilder : SearchResultBuilderBase<TBuilder>
    {
        protected SearchResultBuilderBase()
        {
            ExtendedStats = new Dictionary<string, string>();
        }

        protected abstract TBuilder This { get; }

        protected abstract string ResultType { get; }

        protected abstract string ExtendedStatsClass { get; }

        protected string Name { get; private set; }

        protected string Description { get; private set; }

        protected IDictionary<string, string> ExtendedStats { get; private set; }

        public TBuilder WithName(string name)
        {
            Name = name;

            return This;
        }

        public TBuilder WithDescription(string description)
        {
            Description = description;

            return This;
        }

        public TBuilder WithReleaseDate(DateTime releaseDate)
        {
            ExtendedStats[ExtendedStatClasses.ReleaseDate] = releaseDate.ToString("MMM d, yyyy");

            return This;
        }

        public virtual HtmlNode Build()
        {
            var htmlBuilder = new StringBuilder();

            AppendResultType(htmlBuilder);

            htmlBuilder.Append("  <div class=\"result_wrap\">");

            htmlBuilder.Append("    <div class=\"basic_stats\">");
            AppendMainStats(htmlBuilder);
            AppendExtendedStats(htmlBuilder);
            htmlBuilder.Append("    </div>");

            AppendDescription(htmlBuilder);

            htmlBuilder.Append("  </div>");

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlBuilder.ToString());

            return htmlDocument.DocumentNode;
        }

        protected virtual void AppendResultType(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("  <div class=\"result_type\">");
            htmlBuilder.AppendFormat("    <strong>{0}</strong>", ResultType);
            htmlBuilder.Append("  </div>");
        }

        protected virtual void AppendMainStats(StringBuilder htmlBuilder)
        {
            htmlBuilder.Append("      <div class=\"main_stats\">");
            htmlBuilder.Append("        <h3 class=\"product_title basic_stat\">");
            htmlBuilder.AppendFormat("          <a href=\"#\">{0}</a>", Name);
            htmlBuilder.Append("        </h3>");

            htmlBuilder.Append("      </div>");
        }

        protected virtual void AppendExtendedStats(StringBuilder htmlBuilder)
        {
            if (!ExtendedStats.Any())
            {
                return;
            }

            htmlBuilder.AppendFormat("      <div class=\"{0}\">", ExtendedStatsClass);
            htmlBuilder.Append("        <ul class=\"more_stats\">");

            foreach (var extendedStat in ExtendedStats)
            {
                AppendExtendedStat(htmlBuilder, extendedStat.Key, extendedStat.Key, extendedStat.Value);
            }

            htmlBuilder.Append("        </ul>");
            htmlBuilder.Append("      </div>");
        }

        protected virtual void AppendExtendedStat(StringBuilder htmlBuilder, string statClass, string labelText, string value)
        {
            htmlBuilder.AppendFormat("          <li class=\"stat {0}\">", statClass);
            htmlBuilder.AppendFormat("            <span class=\"label\">{0}: </span>", labelText);
            htmlBuilder.AppendFormat("            <span class=\"data\">{0}</span>", value);
            htmlBuilder.Append("          </li>");
        }

        protected virtual void AppendDescription(StringBuilder htmlBuilder)
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                return;
            }

            htmlBuilder.Append("    <p class=\"deck basic_stat\">");
            htmlBuilder.Append(Description);
        }
    }
}