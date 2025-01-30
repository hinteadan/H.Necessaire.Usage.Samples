using System;
using System.Collections.Generic;
using System.Linq;

namespace H.Necessaire.Samples.SortingAndPagination
{
    internal class SampleDataProvider
    {
        public Page<int> GetSampleData<TFilter>(TFilter filter) where TFilter : IPageFilter, ISortFilter
        {
            if ((filter?.PageFilter?.PageSize ?? 0) <= 0)
                return Page<int>.Empty();

            IEnumerable<int> data
                = Enumerable
                .Range(0, filter.PageFilter.PageSize)
                .Select(
                    index => Random.Shared.Next(int.MinValue, int.MaxValue)
                )
                ;

            if ((filter?.SortFilters).IsEmpty())
                return Page<int>.For(filter, filter.PageFilter.PageSize * 10, data.ToArray());

            if (filter.SortFilters.First().Direction == SortFilter.SortDirection.Descending)
                return Page<int>.For(filter, filter.PageFilter.PageSize * 10, data.OrderDescending().ToArray());

            return Page<int>.For(filter, filter.PageFilter.PageSize * 10, data.Order().ToArray());
        }
    }
}
