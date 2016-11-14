using FluentMetacritic.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FluentMetacritic.Query
{
    [DebuggerDisplay("Name = {Name}")]
    public class Category
    {
        public static readonly Category All = new Category("All", "all", typeof(IEntity));
        public static readonly Category Movies = new Category("Movies", "movie", typeof(IMovie));
        public static readonly Category Games = new Category("Games", "game", typeof(IGame));
        public static readonly Category Albums = new Category("Albums", "album", typeof(IAlbum));
        public static readonly Category TelevisionShows = new Category("TV Shows", "tv", typeof(ITelevisionShow));
        public static readonly Category People = new Category("People", "person", typeof(IPerson));
        public static readonly Category Trailers = new Category("Trailers", "video", typeof(ITrailer));
        public static readonly Category Companies = new Category("Companies", "company", typeof(ICompany));
        public static readonly Category Features = new Category("Features", "all", typeof(IFeature));

        public static readonly IEnumerable<Category> AllCategories = new[] { All, Movies, Games, Albums, TelevisionShows, People, Trailers, Companies, Features };

        private readonly string _name;

        private readonly string _uriValue;

        private readonly Type _entityType;

        private Category(string name, string uriValue, Type entityType)
        {
            _name = name;
            _uriValue = uriValue;
            _entityType = entityType;
        }

        public string Name => _name;

        public string UriValue => _uriValue;

        public Type EntityType => _entityType;

        public static Category FromEntity<T>() where T : IEntity
        {
            return AllCategories.SingleOrDefault(c => c.EntityType == typeof(T));
        }
    }
}