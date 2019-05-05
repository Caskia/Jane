using System;
using System.ComponentModel.DataAnnotations;

namespace Jane.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// </summary>
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        private int _maxResultCount = 10;

        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount
        {
            get
            {
                return _maxResultCount;
            }
            set
            {
                _maxResultCount = value;
                ValidateMaxResultCount();
            }
        }

        public virtual int MaxResultLimit { get; set; } = 100;

        private void ValidateMaxResultCount()
        {
            if (_maxResultCount < 1)
            {
                throw new ArgumentException($"{nameof(MaxResultCount)} should be greater than zero.");
            }

            if (_maxResultCount > MaxResultLimit)
            {
                throw new ArgumentException($"{nameof(MaxResultCount)} should be less than {MaxResultLimit}.");
            }
        }
    }
}