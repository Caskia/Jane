namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// </summary>
    public interface ICursorResultRequest
    {
        /// <summary>
        /// Max expected id.
        /// </summary>
        int? MaxId { get; set; }

        /// <summary>
        /// Since expected id.
        /// </summary>
        int? SinceId { get; set; }
    }
}