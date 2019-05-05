using Jane.Entities;
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
        private TPrimaryKey? _maxId;
        private TPrimaryKey? _sinceId;
        private string _sorting = $"{nameof(IEntity.Id)} desc";

        public virtual TPrimaryKey? MaxId
        {
            get
            {
                return _maxId;
            }
            set
            {
                _maxId = value;
                _sorting = $"{nameof(IEntity.Id)} asc";
            }
        }

        public virtual TPrimaryKey? SinceId
        {
            get
            {
                return _sinceId;
            }
            set
            {
                _sinceId = value;
                _sorting = $"{nameof(IEntity.Id)} desc";
            }
        }

        public virtual string Sorting
        {
            get
            {
                return _sorting;
            }
            set
            {
                _sorting = value;
            }
        }
    }
}