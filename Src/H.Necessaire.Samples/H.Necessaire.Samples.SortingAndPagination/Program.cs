using System;

namespace H.Necessaire.Samples.SortingAndPagination
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Page<int> emptyPage = new SampleDataProvider().GetSampleData(null as SampleFilter);
            Page<int> unsortedPage = new SampleDataProvider().GetSampleData(
                new SampleFilter { 
                    PageFilter = new PageFilter { PageSize = 10 }
                }
            );
            Page<int> sortedAscPage = new SampleDataProvider().GetSampleData(
                new SampleFilter
                {
                    PageFilter = new PageFilter { PageSize = 10 },
                    SortFilters = [new SortFilter { Direction = SortFilter.SortDirection.Ascending }]
                }
            );
            Page<int> sortedDescPage = new SampleDataProvider().GetSampleData(
                new SampleFilter
                {
                    PageFilter = new PageFilter { PageSize = 10 },
                    SortFilters = [new SortFilter { Direction = SortFilter.SortDirection.Descending }]
                }
            );
        }
    }
}
