using FluentMetacritic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.TestClient
{
    public class ConsoleWriter
    {
        public void Output(string description, IEnumerable<IEntity> entities)
        {
            var heading = string.Format("{0} ({1} entities returned)", description, entities.Count());
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));

            foreach (var entity in entities)
            {
                Console.WriteLine("[{0}] Name: {1}", entity.GetType().Name, entity.Name);
            }

            Console.WriteLine();
        }
    }
}