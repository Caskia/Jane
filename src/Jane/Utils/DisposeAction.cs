﻿using System;
using System.Threading;

namespace Jane
{
    /// <summary>
    /// This class can be used to provide an action when
    /// Dipose method is called.
    /// </summary>
    public class DisposeAction : IDisposable
    {
        public static readonly DisposeAction Empty = new DisposeAction(null);

        private Action _action;

        /// <summary>
        /// Creates a new <see cref="DisposeAction"/> object.
        /// </summary>
        /// <param name="action">Action to be executed when this object is disposed.</param>
        public DisposeAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            _action = action;
        }

        public void Dispose()
        {
            // Interlocked prevents multiple execution of the _action.
            var action = Interlocked.Exchange(ref _action, null);
            action?.Invoke();
        }
    }
}