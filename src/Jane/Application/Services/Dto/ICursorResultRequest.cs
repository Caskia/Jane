namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// </summary>
    public interface ICursorResultRequest<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Max expected id.
        /// </summary>
        TPrimaryKey? MaxId { get; set; }

        /// <summary>
        /// Since expected id.
        /// </summary>
        TPrimaryKey? SinceId { get; set; }
    }
}