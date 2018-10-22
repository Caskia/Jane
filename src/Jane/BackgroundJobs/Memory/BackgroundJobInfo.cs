﻿using System;

namespace Jane.BackgroundJobs
{
    /// <summary>
    /// Represents a background job info that is used to persist jobs.
    /// </summary>
    public class BackgroundJobInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJobInfo"/> class.
        /// </summary>
        public BackgroundJobInfo()
        {
            Priority = BackgroundJobPriority.Normal;
        }

        /// <summary>
        /// Creation time of this job.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        public Guid Id { get; set; }

        /// <summary>
        /// This is true if this job is continously failed and will not be executed again.
        /// </summary>
        public virtual bool IsAbandoned { get; set; }

        /// <summary>
        /// Job arguments as serialized to string.
        /// </summary>
        public virtual string JobArgs { get; set; }

        /// <summary>
        /// Type of the job.
        /// It's AssemblyQualifiedName of job type.
        /// </summary>
        public virtual string JobName { get; set; }

        /// <summary>
        /// Last try time of this job.
        /// </summary>
        public virtual DateTime? LastTryTime { get; set; }

        /// <summary>
        /// Next try time of this job.
        /// </summary>
        public virtual DateTime NextTryTime { get; set; }

        /// <summary>
        /// Priority of this job.
        /// </summary>
        public virtual BackgroundJobPriority Priority { get; set; }

        /// <summary>
        /// Try count of this job.
        /// A job is re-tried if it fails.
        /// </summary>
        public virtual short TryCount { get; set; }
    }
}