using Jane.Extensions;
using System;
using System.Linq;

namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndSortedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        private string[] _canSortProperties;

        private string _sorting;

        public PagedAndSortedResultRequestDto()
        {
            _sorting = string.Empty;
        }

        public PagedAndSortedResultRequestDto(string[] canSortProperties)
            : this()
        {
            _canSortProperties = canSortProperties;
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
                ValidateSort();
            }
        }

        private void ValidateSort()
        {
            if (!_sorting.IsNullOrEmpty())
            {
                var sorts = _sorting.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sort in sorts)
                {
                    var sortField = sort.Trim().Replace("_", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (_canSortProperties != null && _canSortProperties.FirstOrDefault(s => sortField.ToLower() == s.ToLower()) == null)
                    {
                        throw new UserFriendlyException($"Sort field[{sort.Trim()}] can not allow! ");
                    }
                }
                _sorting = _sorting.Replace("_", "");
            }
        }
    }
}