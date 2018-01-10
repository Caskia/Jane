using System;

namespace Jane.Runtime.Session
{
    public abstract class JaneSessionBase : IJaneSession
    {
        public const string SessionOverrideContextKey = "Jane.Runtime.Session.Override";

        protected JaneSessionBase(IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        {
            SessionOverrideScopeProvider = sessionOverrideScopeProvider;
        }

        public abstract long? ImpersonatorUserId { get; }

        public abstract long? UserId { get; }
        protected SessionOverride OverridedValue => SessionOverrideScopeProvider.GetValue(SessionOverrideContextKey);
        protected IAmbientScopeProvider<SessionOverride> SessionOverrideScopeProvider { get; }

        public IDisposable Use(long? userId)
        {
            return SessionOverrideScopeProvider.BeginScope(SessionOverrideContextKey, new SessionOverride(userId));
        }
    }
}