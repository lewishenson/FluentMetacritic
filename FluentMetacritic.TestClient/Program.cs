using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentMetacritic.TestClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            var writer = new ConsoleWriter();

            var saltMovies = await Metacritic.SearchFor().Movies().UsingTextAsync("Salt");
            writer.Output("Salt movies", saltMovies.ToList());

            Thread.Sleep(1000);

            var darkKnightAlbums = await Metacritic.SearchFor().Albums().UsingTextAsync("dark knight");
            writer.Output("Dark Knight albums", darkKnightAlbums.ToList());

            Thread.Sleep(1000);

            var starTrekItems = await Metacritic.SearchFor().AllItems().OrderedBy().Score().GoTo().Page(2).UsingTextAsync("star trek");
            writer.Output("Star Trek items (ordered by score, page 2)", starTrekItems.ToList());

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}