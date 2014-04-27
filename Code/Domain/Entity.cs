using System;
using System.Diagnostics;

namespace FluentMetacritic.Domain
{
    [DebuggerDisplay("Name = {Name}")]
    public abstract class Entity : IEntity
    {
        protected Entity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.", "name");
            }

            Name = name.Trim();
        }

        public string Name { get; private set; }

        public string Description { get; set; }
    }
}