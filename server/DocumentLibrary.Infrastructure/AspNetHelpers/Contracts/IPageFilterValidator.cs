using System.Collections.Generic;

namespace DocumentLibrary.Infrastructure.AspNetHelpers.Contracts
{
    public interface IPageFilterValidator
    {
        IEnumerable<string> Validate(int pageNumber, int itemsPerPage, int allRecordsCount);
    }
}