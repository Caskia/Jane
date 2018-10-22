using Jane.Extensions;
using System;
using System.Linq;

namespace Jane.BackgroundJobs
{
    public class BackgroundJobNameAttribute : Attribute, IBackgroundJobNameProvider
    {
        public BackgroundJobNameAttribute(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public string Name { get; }

        public static string GetName<TJobArgs>()
        {
            return GetName(typeof(TJobArgs));
        }

        public static string GetName(Type jobArgsType)
        {
            if (jobArgsType == null)
            {
                throw new ArgumentNullException(nameof(jobArgsType));
            }

            return jobArgsType
                       .GetCustomAttributes(true)
                       .OfType<IBackgroundJobNameProvider>()
                       .FirstOrDefault()
                       ?.Name
                   ?? jobArgsType.FullName;
        }
    }
}