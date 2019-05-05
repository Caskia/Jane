namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a paged and cursor result.
    /// </summary>
    public interface IPagedAndCursorResultRequest<TPrimaryKey> : IPagedResultRequest, ICursorResultRequest<TPrimaryKey>
        where TPrimaryKey : struct
    {
    }
}