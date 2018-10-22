﻿using Jane.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Jane.BackgroundJobs
{
    public class BackgroundJobOptions
    {
        private readonly Dictionary<Type, BackgroundJobConfiguration> _jobConfigurationsByArgsType;
        private readonly Dictionary<string, BackgroundJobConfiguration> _jobConfigurationsByName;

        public BackgroundJobOptions()
        {
            _jobConfigurationsByArgsType = new Dictionary<Type, BackgroundJobConfiguration>();
            _jobConfigurationsByName = new Dictionary<string, BackgroundJobConfiguration>();
        }

        //TODO: Implement for all providers! (Hangfire does not implement yet)
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsJobExecutionEnabled { get; set; } = true;

        public void AddJob<TJob>()
        {
            AddJob(typeof(TJob));
        }

        public void AddJob(Type jobType)
        {
            AddJob(new BackgroundJobConfiguration(jobType));
        }

        public void AddJob(BackgroundJobConfiguration jobConfiguration)
        {
            _jobConfigurationsByArgsType[jobConfiguration.ArgsType] = jobConfiguration;
            _jobConfigurationsByName[jobConfiguration.JobName] = jobConfiguration;
        }

        public BackgroundJobConfiguration GetJob<TArgs>()
        {
            return GetJob(typeof(TArgs));
        }

        public BackgroundJobConfiguration GetJob(Type argsType)
        {
            var jobConfiguration = _jobConfigurationsByArgsType.GetOrDefault(argsType);

            if (jobConfiguration == null)
            {
                throw new JaneException("Undefined background job for the job args type: " + argsType.AssemblyQualifiedName);
            }

            return jobConfiguration;
        }

        public BackgroundJobConfiguration GetJob(string name)
        {
            var jobConfiguration = _jobConfigurationsByName.GetOrDefault(name);

            if (jobConfiguration == null)
            {
                throw new JaneException("Undefined background job for the job name: " + name);
            }

            return jobConfiguration;
        }

        public IReadOnlyList<BackgroundJobConfiguration> GetJobs()
        {
            return _jobConfigurationsByArgsType.Values.ToImmutableList();
        }
    }
}