using Jane.Push;
using Jane.Extensions;
using Jane.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Jane.Mob.Push
{
    public class MobCombinationPushService : IPushService
    {
        private readonly ILogger _logger;
        private readonly MobPushService _mobPushService;
        private readonly ISubject<PushMessageContext> _queue = Subject.Synchronize(new Subject<PushMessageContext>());

        public MobCombinationPushService
            (
                ILoggerFactory loggerFactory,
                MobPushService mobPushService
            )
        {
            _logger = loggerFactory.Create(nameof(MobCombinationPushService));
            _mobPushService = mobPushService;

            SubscribeMessageQueue();
        }

        public Task<PushMessageOutput> PushAsync(PushMessage message)
        {
            return PushAsync(message, new List<PushPlatform>() { PushPlatform.Android, PushPlatform.IOS });
        }

        public Task<PushMessageOutput> PushAsync(PushMessage message, PushPlatform platform)
        {
            return PushAsync(message, new List<PushPlatform>() { platform });
        }

        private async Task<PushMessageOutput> PushAsync(PushMessage message, List<PushPlatform> pushPlatforms)
        {
            var pushMessageOutput = default(PushMessageOutput);

            if (message.GroupKey.IsNullOrEmpty())
            {
                pushMessageOutput = await _mobPushService.PushAsync(message, new List<string>() { $"{message.AccountType}_{message.AccountId}" }, pushPlatforms);
            }
            else
            {
                var context = new PushMessageContext()
                {
                    GroupKey = $"{message.GroupKey}_{string.Join(",", pushPlatforms.Select(p => p.ToString()))}",
                    Message = message,
                    PushPlatforms = pushPlatforms,
                    TaskCompletionSource = new TaskCompletionSource<PushMessageOutput>(TaskCreationOptions.RunContinuationsAsynchronously)
                };

                _queue.OnNext(context);

                pushMessageOutput = await context.TaskCompletionSource.Task;
            }

            return pushMessageOutput;
        }

        private void SubscribeMessageQueue()
        {
            _queue
                .GroupByUntil(g => g.GroupKey, g => g.Buffer(TimeSpan.FromSeconds(10), 100))
                .Subscribe(group =>
                {
                    group
                    .ToList()
                    .Select(contexts => Observable.FromAsync(async () =>
                    {
                        if (contexts.Count == 0)
                        {
                            return;
                        }

                        var pushMessageOutput = default(PushMessageOutput);
                        try
                        {
                            var message = contexts.FirstOrDefault().Message;
                            var platforms = contexts.FirstOrDefault().PushPlatforms;
                            var tags = contexts.Select(c => $"{c.Message.AccountType}_{c.Message.AccountId}").ToList();

                            pushMessageOutput = await _mobPushService.PushAsync(message, tags, platforms);
                        }
                        catch (Exception ex)
                        {
                            foreach (var context in contexts)
                            {
                                context.TaskCompletionSource.TrySetException(ex);
                            }
                            return;
                        }
                        finally
                        {
                            foreach (var context in contexts)
                            {
                                context.TaskCompletionSource.TrySetResult(pushMessageOutput);
                            }
                        }
                    }))
                    .Concat()
                    .Subscribe();
                });
        }
    }
}