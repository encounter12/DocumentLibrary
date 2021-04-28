using System.Collections.Generic;

namespace DocumentLibrary.Infrastructure.Sorting
{
    public class SortingService : ISortingService
    {
        public string BuildSortString(
            string sortBy,
            string sort,
            List<string> allowedColumns,
            string defaultSortColumn)
        {
            string sortColumn = sortBy != null ? 
                (allowedColumns.Contains(sortBy) ? sortBy: defaultSortColumn) : defaultSortColumn;
            
            string sortKeyword = sort != null ?
                (sort.ToLowerInvariant() == SortOrder.Desc ? $" {SortOrder.Desc}" : string.Empty) : string.Empty;
            
            string sortDynamicString = $"{sortColumn}{sortKeyword}";

            return sortDynamicString;
        }
    }
}
