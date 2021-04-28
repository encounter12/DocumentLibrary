namespace DocumentLibrary.Infrastructure.Paging
{
    public interface IPagingService
    {
        PagingModel GetPagingModel(int? pageNumber, int? itemsPerPage, long allRecordsCount);

        (bool areValuesValid, string errorMessage) ValidatePagingValues(
            int pageNumber, int itemsPerPage, long allRecordsCount);
    }
}