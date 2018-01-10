using Jane.Runtime.Remoting;

namespace Jane.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IJaneSession"/>.
    /// </summary>
    public class NullSession : SessionBase
    {
        private NullSession()
                    : base(
                          new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
                    )
        {
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullSession Instance { get; } = new NullSession();

        public override long? ImpersonatorUserId => null;

        /// <inheritdoc/>
        public override long? UserId => null;
    }
}