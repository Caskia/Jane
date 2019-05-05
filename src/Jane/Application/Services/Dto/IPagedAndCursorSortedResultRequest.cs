namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a paged and cursor result.
    /// </summary>
    public interface IPagedAndCursorSortedResultRequest<TPrimaryKey> : IPagedResultRequest, ICursorSortedResultRequest<TPrimaryKey>
        where TPrimaryKey : struct
    {
    }
}