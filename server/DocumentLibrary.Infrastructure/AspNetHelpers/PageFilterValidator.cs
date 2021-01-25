using System.Collections.Generic;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;

namespace DocumentLibrary.Infrastructure.AspNetHelpers
{
    public class PageFilterValidator : IPageFilterValidator
    {
        public IEnumerable<string> Validate(int pageNumber, int itemsPerPage, int allRecordsCount)
        {
            var validationErrors = new List<string>();

            bool isArgumentNegativeNumber = false;
            
            if (pageNumber <= 0)
            {
                validationErrors.Add($"The page number: {pageNumber} cannot be less or equal than zero");
                isArgumentNegativeNumber = true;
            }
            
            if (itemsPerPage <= 0)
            {
                validationErrors.Add($"The items per page: {itemsPerPage} cannot be less or equal than zero");
                isArgumentNegativeNumber = true;
            }

            if (isArgumentNegativeNumber)
            {
                return validationErrors;
            }

            int minItemsPerPage = 2;

            if (itemsPerPage < minItemsPerPage)
            {
                validationErrors.Add($"The minimum number of items per page is: {minItemsPerPage}");
                return validationErrors;
            }

            int maxAllowedItemsPerPage = 50;
            
            if (itemsPerPage > maxAllowedItemsPerPage)
            {
                validationErrors.Add(
                    $"The itemsPerPage: {itemsPerPage} exceeds the maximum allowed items per page: {maxAllowedItemsPerPage}.");
                return validationErrors;
            }
            
            var calculatedMaxPagesCount = allRecordsCount % itemsPerPage > 0 ? 
                (allRecordsCount / itemsPerPage) + 1 : (allRecordsCount / itemsPerPage);

            if (pageNumber > calculatedMaxPagesCount)
            {
                validationErrors.Add($"The page number from client exceeds the maximum page number {calculatedMaxPagesCount}");
            }

            return validationErrors;
        }
    }
}