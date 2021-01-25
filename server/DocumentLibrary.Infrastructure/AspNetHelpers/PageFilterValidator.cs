using System.Collections.Generic;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;

namespace DocumentLibrary.Infrastructure.AspNetHelpers
{
    public class PageFilterValidator : IPageFilterValidator
    {
        public IEnumerable<string> Validate(int pageNumber, int itemsPerPage, int allRecordsCount)
        {
            if (pageNumber <= 0)
            {
                yield return "The page number cannot be less or equal then zero";
            }
            
            if (itemsPerPage <= 0)
            {
                yield return "The page number cannot be less or equal then zero";
            }
            
            if (pageNumber <= 0)
            {
                yield return "The page number cannot be less or equal then zero";
            }

            if (itemsPerPage < 2)
            {
                yield return "The minimum number of items per page are 5";
            }
            
            var calculatedMaxPagesCount = allRecordsCount % itemsPerPage > 0 ? 
                (allRecordsCount / itemsPerPage) + 1 : (allRecordsCount / itemsPerPage);

            if (pageNumber > calculatedMaxPagesCount)
            {
                yield return $"The page number from client exceeds the maximum page number {calculatedMaxPagesCount}";
            }
        }
    }
}