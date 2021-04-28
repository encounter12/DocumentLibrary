using System.Collections.Generic;

namespace DocumentLibrary.Infrastructure.Sorting
{
    public interface ISortingService
    {
        string BuildSortString(
            string sortBy,
            string sortOrder,
            List<string> allowedColumns,
            string defaultSortColumn);
    }
}