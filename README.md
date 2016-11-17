# FluentMetacritic [![NuGet package](https://buildstats.info/nuget/FluentMetacritic)](https://www.nuget.org/packages/FluentMetacritic)

C# API for scraping Metacritic review scores.

FluentMetacritic works by scraping the Metacritic website after running a search. The API has the same functionality as the website - search by text and type, choose what you want to order the results by and go to different pages.

The API uses the fluid builder pattern so the below examples can be combined.

## Version 2.x

FluentMetacritic now runs on both .NET Core and the full .NET Framework.

Asynchronous support has also been added.

## Usage

Getting the first page of search results containing all item types:

```csharp
var searchResults = await Metacritic.SearchFor().AllItems().UsingTextAsync("Dark Knight");
```

Run a search for a specific item type:

```csharp
var searchResults = await Metacritic.SearchFor().Movies().UsingTextAsync("Back to the Future");
```

Run a search and order the results by score (the default order is relevancy):

```csharp
var searchResults = await Metacritic.SearchFor().AllItems().OrderedBy().Score().UsingTextAsync("Game of Thrones");
```

Run a search and get page two of the results:

```csharp
var searchResults = await Metacritic.SearchFor().AllItems().GoTo().Page(2).UsingTextAsync("Lord of the Rings");
```


## NuGet

If you don't care about the source code you can just install FluentMetacritic using NuGet.

```powershell
PM> Install-Package FluentMetacritic
```
