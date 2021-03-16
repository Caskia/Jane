using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Jane.Scheduling
{
    public class SampleAccumulator<T>
    {
        private TimeSpan _interval;
        private ConcurrentDictionary<string, ISubject<T>> _maps = new ConcurrentDictionary<string, ISubject<T>>();
        private Func<T, Task> _work;

        public SampleAccumulator(Func<T, Task> work, TimeSpan interval)
        {
            if (work == null)
            {
                throw new ArgumentNullException(nameof(work));
            }

            _interval = interval;
            _work = work;
        }

        public void Enqueue(string key, T element)
        {
            var queue = _maps.GetOrAdd(key, x =>
            {
                var q = Subject.Synchronize(new Subject<T>());

                q.Sample(_interval)
                .Select(e => Observable.FromAsync(() => _work(e)))
                .Concat()
                .Subscribe();

                return q;
            });

            queue.OnNext(element);
        }
    }
}