using System;
using System.Collections.Generic;

namespace DocumentLibrary.Infrastructure.Paging
{
    public class PagingService : IPagingService
    {
        public PagingModel GetPagingModel(int? pageNumber, int? itemsPerPage, long allRecordsCount)
        {
            int defaultPageNumber = 1;
            int defaultItemsPerPage = 10;

            int currentPageNumber = pageNumber ?? defaultPageNumber;
            int currentItemsPerPage = itemsPerPage ?? defaultItemsPerPage;
            
            (bool areValuesValid, string errorMessage) = 
                ValidatePagingValues(currentPageNumber, currentItemsPerPage, allRecordsCount);

            if (!areValuesValid)
            {
                throw new ArgumentException(errorMessage);
            }
            
            int skip = currentPageNumber * currentItemsPerPage - currentItemsPerPage;

            long pagesCount = allRecordsCount % currentItemsPerPage > 0 ?
                (allRecordsCount / currentItemsPerPage) + 1 : (allRecordsCount / currentItemsPerPage);

            var pagingModel = new PagingModel
            {
                PageNumber = currentPageNumber,
                ItemsPerPage = currentItemsPerPage,
                Skip = skip,
                PagesCount = pagesCount
            };

            return pagingModel;
        }

        public (bool areValuesValid, string errorMessage) ValidatePagingValues(
            int pageNumber, int itemsPerPage, long allRecordsCount)
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
                return (false, string.Join(". ", validationErrors));
            }

            int minItemsPerPage = 2;

            if (itemsPerPage < minItemsPerPage)
            {
                validationErrors.Add($"The minimum number of items per page is: {minItemsPerPage}");
                return (false, string.Join(". ", validationErrors));
            }

            int maxAllowedItemsPerPage = 150;
            
            if (itemsPerPage > maxAllowedItemsPerPage)
            {
                validationErrors.Add(
                    $"The itemsPerPage: {itemsPerPage} exceeds the maximum allowed items per page: {maxAllowedItemsPerPage}.");
                return (false, string.Join(". ", validationErrors));
            }
            
            var calculatedMaxPagesCount = allRecordsCount % itemsPerPage > 0 ? 
                (allRecordsCount / itemsPerPage) + 1 : (allRecordsCount / itemsPerPage);

            if (pageNumber > calculatedMaxPagesCount && calculatedMaxPagesCount > 0)
            {
                validationErrors.Add(
                    $"The page number from client exceeds the maximum page number {calculatedMaxPagesCount}");
                
                return (false, string.Join(". ", validationErrors));
            }
            
            return (true, null);
        }
    }
}