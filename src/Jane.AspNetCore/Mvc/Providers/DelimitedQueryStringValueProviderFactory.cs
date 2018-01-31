using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Jane.AspNetCore.Mvc.Providers
{
    /// <summary>
    /// A <see cref="IValueProviderFactory"/> that creates <see cref="IValueProvider"/> instances that
    /// read optionally delimited values from the request query-string.
    /// </summary>
    public class DelimitedQueryStringValueProviderFactory : IValueProviderFactory
    {
        private static readonly char[] DefaultDelimiters = new char[] { ',' };
        private readonly char[] delimiters;

        public DelimitedQueryStringValueProviderFactory()
            : this(DefaultDelimiters)
        {
        }

        public DelimitedQueryStringValueProviderFactory(params char[] delimiters)
        {
            if (delimiters == null || delimiters.Length == 0)
            {
                this.delimiters = DefaultDelimiters;
            }
            else
            {
                this.delimiters = delimiters;
            }
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var valueProvider = new DelimitedQueryStringValueProvider(
                BindingSource.Query,
                context.ActionContext.HttpContext.Request.Query,
                CultureInfo.InvariantCulture,
                this.delimiters);

            context.ValueProviders.Add(valueProvider);

            return Task.CompletedTask;
        }
    }
}