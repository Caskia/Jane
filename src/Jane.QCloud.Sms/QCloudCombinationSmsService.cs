using Jane.Sms;
using Jane.Extensions;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Jane.QCloud.Sms
{
    public class QCloudCombinationSmsService : ISupplySmsService
    {
        private readonly QCloudSmsService _qCloudSmsService;
        private readonly ISubject<SmsMessageContext> _queue = Subject.Synchronize(new Subject<SmsMessageContext>());

        public QCloudCombinationSmsService
            (
                QCloudSmsService qCloudSmsService
            )
        {
            _qCloudSmsService = qCloudSmsService;

            SubscribeMessageQueue();
        }

        public async Task SendSmsAsync(SmsMessage message)
        {
            if (message.GroupKey.IsNullOrEmpty())
            {
                await _qCloudSmsService.SendSmsAsync(message);
            }
            else
            {
                var context = new SmsMessageContext()
                {
                    GroupKey = message.GroupKey,
                    Message = message,
                    TaskCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously)
                };

                _queue.OnNext(context);

                await context.TaskCompletionSource.Task;
            }
        }

        private void SubscribeMessageQueue()
        {
            _queue
                .GroupByUntil(g => g.GroupKey, g => g.Buffer(TimeSpan.FromSeconds(10), 200))
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

                        try
                        {
                            var message = contexts.FirstOrDefault().Message;
                            message.To = contexts
                            .SelectMany(c => c.Message.To)
                            .Distinct(new SmsPhoneNumberEqualityComparer())
                            .ToList();

                            await _qCloudSmsService.SendSmsAsync(message);
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
                                context.TaskCompletionSource.TrySetResult(true);
                            }
                        }
                    }))
                    .Concat()
                    .Subscribe();
                });
        }
    }
}