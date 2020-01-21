using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.
        }


        public SearchResults Search(SearchOptions options)
        {
            List<Shirt> searchResult = null;

            if (options.Colors.Any())
            {
                searchResult = (searchResult ?? _shirts)
                    .Where(
                    s => options.Colors.Any(c => c.Id == s.Color.Id)
                ).AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .ToList();
            }

            if (options.Sizes.Any())
            {
                searchResult = (searchResult ?? _shirts)
                    .Where(
                    s => options.Sizes.Any(z => z.Id == s.Size.Id)
                ).AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .ToList();
            }

            if (searchResult == null)
                return new SearchResults
                {
                    Shirts = new List<Shirt>(),
                    ColorCounts = Color.All.Select(c => new ColorCount { Color = c }).ToList(),
                    SizeCounts = Size.All.Select(z => new SizeCount { Size = z }).ToList(),
                };

            

            return new SearchResults
            {
                Shirts = searchResult,
                ColorCounts = Color.All.Select(c => new ColorCount
                {
                    Color = c,
                    Count = searchResult.Count(r => r.Color.Id == c.Id)
                }).ToList(),

                SizeCounts = Size.All.Select(z => new SizeCount
                {
                    Size = z,
                    Count = searchResult.Count(r => r.Size.Id == z.Id)
                }).ToList(),

            };
        }
    }
}