using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Jane.Scheduling
{
    public class BufferAccumulator<T>
    {
        private int _count;
        private TimeSpan _interval;
        private ConcurrentDictionary<string, ISubject<T>> _maps = new ConcurrentDictionary<string, ISubject<T>>();
        private Func<IList<T>, Task> _work;

        public BufferAccumulator(Func<IList<T>, Task> work, TimeSpan interval, int count)
        {
            if (work == null)
            {
                throw new ArgumentNullException(nameof(work));
            }

            _work = work;
            _interval = interval;
            _count = count;
        }

        public void Enqueue(string key, T element)
        {
            var queue = _maps.GetOrAdd(key, x =>
            {
                var q = Subject.Synchronize(new Subject<T>());

                q.Buffer(_interval, _count)
                .Select(e => Observable.FromAsync(() => _work(e)))
                .Concat()
                .Subscribe();

                return q;
            });

            queue.OnNext(element);
        }
    }
}