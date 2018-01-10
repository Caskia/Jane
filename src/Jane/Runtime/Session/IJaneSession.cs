using System;

namespace Jane.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface IJaneSession
    {
        /// <summary>
        /// UserId of the impersonator.
        /// This is filled if a user is performing actions behalf of the <see cref="UserId"/>.
        /// </summary>
        long? ImpersonatorUserId { get; }

        /// <summary>
        /// Gets current UserId or null.
        /// It can be null if no user logged in.
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// Used to change<see cref="UserId"/> for a limited scope.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDisposable Use(long? userId);
    }
}