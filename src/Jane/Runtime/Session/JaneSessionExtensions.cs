namespace Jane.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="IJaneSession"/>.
    /// </summary>
    public static class JaneSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="JaneException"/> if <see cref="IJaneSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this IJaneSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new JaneException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }
    }
}