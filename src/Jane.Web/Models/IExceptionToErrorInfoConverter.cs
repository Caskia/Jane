using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Jane.Web.Models
{
    /// <summary>
    /// This interface can be implemented to convert an <see cref="Exception"/> object to an <see cref="ErrorInfo"/> object.
    /// Implements Chain Of Responsibility pattern.
    /// </summary>
    public interface IExceptionToErrorInfoConverter
    {
        /// <summary>
        /// Next converter. If this converter decide this exception is not known, it can call Next.Convert(...).
        /// </summary>
        IExceptionToErrorInfoConverter Next { set; }

        /// <summary>
        /// Converter method.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>Error info or null</returns>
        ErrorInfo Convert(Exception exception);

        /// <summary>
        /// Convert to headers
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>error headers</returns>
        Dictionary<string, StringValues> ConvertToHeaders(Exception exception);
    }
}