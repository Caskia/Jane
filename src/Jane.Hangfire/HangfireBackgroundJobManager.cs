﻿using Jane.BackgroundJobs;
using Jane.BackgroundWorkers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jane.Hangfire
{
    //public class HangfireBackgroundJobManager : BackgroundWorkerBase, IBackgroundJobManager
    //{
    //    public Task<bool> DeleteAsync(string jobId)
    //    {
    //        if (string.IsNullOrWhiteSpace(jobId))
    //        {
    //            throw new ArgumentNullException(nameof(jobId));
    //        }

    //        bool successfulDeletion = HangfireBackgroundJob.Delete(jobId);
    //        return Task.FromResult(successfulDeletion);
    //    }

    //    public Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
    //                TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
    //    {
    //        string jobUniqueIdentifier = string.Empty;

    //        if (!delay.HasValue)
    //        {
    //            jobUniqueIdentifier = HangfireBackgroundJob.Enqueue<TJob>(job => job.Execute(args));
    //        }
    //        else
    //        {
    //            jobUniqueIdentifier = HangfireBackgroundJob.Schedule<TJob>(job => job.Execute(args), delay.Value);
    //        }

    //        return Task.FromResult(jobUniqueIdentifier);
    //    }

    //    public Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}