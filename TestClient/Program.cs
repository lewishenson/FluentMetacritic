using System;
using System.Linq;

namespace FluentMetacritic.TestClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var writer = new ConsoleWriter();

            var darkKnightAlbums = Metacritic.SearchFor().Albums().UsingText("dark knight");
            writer.Output("Dark Knight albums", darkKnightAlbums.ToList());

            var starTrekItems = Metacritic.SearchFor().AllItems().OrderedBy().Score().GoTo().Page(2).UsingText("star trek");
            writer.Output("Star Trek items (ordered by score, page 2)", starTrekItems.ToList());

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}