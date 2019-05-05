using System;

namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndCursorResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedAndCursorResultRequestDto<TPrimaryKey> : PagedResultRequestDto, IPagedAndCursorResultRequest<TPrimaryKey>
        where TPrimaryKey : struct
    {
        public virtual TPrimaryKey? MaxId { get; set; }

        public virtual TPrimaryKey? SinceId { get; set; }
    }
}