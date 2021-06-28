﻿using System;
using System.Collections.Generic;

namespace Jane.Runtime.UniqueIdGenerator
{
    /// <summary>
    /// Provides the interface for Id-generators.
    /// </summary>
    /// <typeparam name="T">The type for the generated ID's.</typeparam>
#pragma warning disable CA1710 // Identifiers should have correct suffix

    public interface IIdGenerator<T> : IEnumerable<T>
#pragma warning restore CA1710 // Identifiers should have correct suffix
    {
        /// <summary>
        /// Creates a new Id.
        /// </summary>
        /// <returns>Returns an Id.</returns>
        T CreateId();

        /// <summary>
        /// Gets the <see cref="ITimeSource"/> for the <see cref="IIdGenerator{T}"/>.
        /// </summary>
        ITimeSource TimeSource { get; }

        /// <summary>
        /// Gets the epoch for the <see cref="IIdGenerator{T}"/>.
        /// </summary>
        DateTimeOffset Epoch { get; }

        /// <summary>
        /// Gets the <see cref="MaskConfig"/> for the <see cref="IIdGenerator{T}"/>.
        /// </summary>
        MaskConfig MaskConfig { get; }
    }
}