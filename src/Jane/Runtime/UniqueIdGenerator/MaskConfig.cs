﻿using System;

namespace Jane.Runtime.UniqueIdGenerator
{
    /// <summary>
    /// Specifies the number of bits to use for the different parts of an Id for an <see cref="IdGenerator"/>.
    /// </summary>
    public class MaskConfig
    {
        /// <summary>
        /// Initializes a bitmask configuration for <see cref="IdGenerator"/>s.
        /// </summary>
        /// <param name="timestampBits">Number of bits to use for the timestamp-part of Id's.</param>
        /// <param name="generatorIdBits">Number of bits to use for the generator-id of Id's.</param>
        /// <param name="sequenceBits">Number of bits to use for the sequence-part of Id's.</param>
        public MaskConfig(byte timestampBits, byte generatorIdBits, byte sequenceBits)
        {
            TimestampBits = timestampBits;
            GeneratorIdBits = generatorIdBits;
            SequenceBits = sequenceBits;
        }

        /// <summary>
        /// Gets a default <see cref="MaskConfig"/> with 41 bits for the timestamp part, 10 bits for the generator-id
        /// part and 12 bits for the sequence part of the id.
        /// </summary>
        public static MaskConfig Default => new MaskConfig(41, 10, 12);

        /// <summary>
        /// Gets number of bits to use for the generator-id part of the Id's to generate.
        /// </summary>
        public byte GeneratorIdBits { get; private set; }

        /// <summary>
        /// Returns the maximum number of generators available for this mask configuration.
        /// </summary>
        public long MaxGenerators => (1L << GeneratorIdBits);

        /// <summary>
        /// Returns the maximum number of intervals for this mask configuration.
        /// </summary>
        public long MaxIntervals => (1L << TimestampBits);

        /// <summary>
        /// Returns the maximum number of sequential Id's for a time-interval (e.g. max. number of Id's generated
        /// within a single interval).
        /// </summary>
        public long MaxSequenceIds => (1L << SequenceBits);

        /// <summary>
        /// Gets number of bits to use for the sequence part of the Id's to generate.
        /// </summary>
        public byte SequenceBits { get; private set; }

        /// <summary>
        /// Gets number of bits to use for the timestamp part of the Id's to generate.
        /// </summary>
        public byte TimestampBits { get; private set; }

        /// <summary>
        /// Gets the total number of bits for the <see cref="MaskConfig"/>.
        /// </summary>
        public int TotalBits => TimestampBits + GeneratorIdBits + SequenceBits;

        /// <summary>
        /// Calculates the last date for an Id before a 'wrap around' will occur in the timestamp-part of an Id for the
        /// given <see cref="MaskConfig"/>.
        /// </summary>
        /// <param name="epoch">The used epoch for the <see cref="IdGenerator"/> to use as offset.</param>'
        /// <param name="timeSource">The used <see cref="ITimeSource"/> for the <see cref="IdGenerator"/>.</param>
        /// <returns>The last date for an Id before a 'wrap around' will occur in the timestamp-part of an Id.</returns>
        /// <remarks>
        /// Please note that for dates exceeding the <see cref="DateTimeOffset.MaxValue"/> an
        /// <see cref="ArgumentOutOfRangeException"/> will be thrown.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when any combination of a <see cref="ITimeSource.TickDuration"/> and <see cref="MaxIntervals"/>
        /// results in a date exceeding the <see cref="TimeSpan.MaxValue"/> value.
        /// </exception>
        public DateTimeOffset WraparoundDate(DateTimeOffset epoch, ITimeSource timeSource)
        {
            if (timeSource == null)
                throw new ArgumentNullException(nameof(timeSource));
            return epoch.AddDays(timeSource.TickDuration.TotalDays * MaxIntervals);
        }

        /// <summary>
        /// Calculates the interval at wich a 'wrap around' will occur in the timestamp-part of an Id for the given
        /// <see cref="MaskConfig"/>.
        /// </summary>
        /// <param name="timeSource">The used <see cref="ITimeSource"/> for the <see cref="IdGenerator"/>.</param>
        /// <returns>
        /// The interval at wich a 'wrap around' will occur in the timestamp-part of an Id for the given
        /// <see cref="MaskConfig"/>.
        /// </returns>
        /// <remarks>
        /// Please note that for intervals exceeding the <see cref="TimeSpan.MaxValue"/> an
        /// <see cref="OverflowException"/> will be thrown.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="timeSource"/> is null.
        /// </exception>
        /// <exception cref="OverflowException">
        /// Thrown when any combination of a <see cref="ITimeSource.TickDuration"/> and <see cref="MaxIntervals"/>
        /// results in a TimeSpan exceeding the <see cref="TimeSpan.MaxValue"/> value.
        /// </exception>
        public TimeSpan WraparoundInterval(ITimeSource timeSource)
        {
            if (timeSource == null)
                throw new ArgumentNullException(nameof(timeSource));
            return TimeSpan.FromDays(timeSource.TickDuration.TotalDays * MaxIntervals);
        }
    }
}