using Jane.Runtime.Remoting;

namespace Jane.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IJaneSession"/>.
    /// </summary>
    public class NullJaneSession : JaneSessionBase
    {
        private NullJaneSession()
                    : base(
                          new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
                    )
        {
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullJaneSession Instance { get; } = new NullJaneSession();

        public override long? ImpersonatorUserId => null;

        /// <inheritdoc/>
        public override long? UserId => null;
    }
}