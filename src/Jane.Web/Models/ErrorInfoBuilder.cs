using Jane.Configurations;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Jane.Web.Models
{
    /// <inheritdoc/>
    public class ErrorInfoBuilder : IErrorInfoBuilder
    {
        /// <inheritdoc/>
        public ErrorInfoBuilder(IWebConfiguration configuration)
        {
            Converter = new DefaultErrorInfoConverter(configuration);
        }

        private IExceptionToErrorInfoConverter Converter { get; set; }

        /// <summary>
        /// Adds an exception converter that is used by <see cref="BuildForException"/> method.
        /// </summary>
        /// <param name="converter">Converter object</param>
        public void AddExceptionConverter(IExceptionToErrorInfoConverter converter)
        {
            converter.Next = Converter;
            Converter = converter;
        }

        /// <inheritdoc/>
        public ErrorInfo BuildForException(Exception exception)
        {
            return Converter.Convert(exception);
        }

        public Dictionary<string, StringValues> BuildHeaders(Exception exception)
        {
            return Converter.ConvertToHeaders(exception);
        }
    }
}