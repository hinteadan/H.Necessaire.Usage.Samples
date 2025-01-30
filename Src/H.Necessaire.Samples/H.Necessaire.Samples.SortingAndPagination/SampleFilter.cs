namespace H.Necessaire.Samples.SortingAndPagination
{
    internal class SampleFilter : ISortFilter, IPageFilter
    {
        public PageFilter PageFilter { get; set; }

        public SortFilter[] SortFilters { get; set; }

        public OperationResult ValidateSortFilters() => OperationResult.Win();
    }
}