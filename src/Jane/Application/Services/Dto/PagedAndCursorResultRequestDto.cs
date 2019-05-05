using System;
using System.ComponentModel.DataAnnotations;

namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndCursorResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedAndCursorResultRequestDto : PagedResultRequestDto, IPagedAndCursorResultRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int? MaxId { get; set; }

        [Range(1, int.MaxValue)]
        public virtual int? SinceId { get; set; }
    }
}