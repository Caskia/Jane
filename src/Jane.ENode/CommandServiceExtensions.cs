using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using System.Threading.Tasks;

namespace Jane.ENode
{
    public static class CommandServiceExtensions
    {
        public static async Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(this ICommandService commandService, ICommand command, CommandReturnType commandReturnType = CommandReturnType.CommandExecuted, int millisecondsDelay = 10000, bool autoProcessResultException = true)
        {
            var result = await commandService.ExecuteAsync(command, commandReturnType).TimeoutAfter(millisecondsDelay);
            if (autoProcessResultException)
            {
                ProcessExecuteCommandAsyncResultException(result);
            }

            return result;
        }

        public static async Task<AsyncTaskResult> SendCommandAsync(this ICommandService commandService, ICommand command, bool autoProcessResultException = true)
        {
            var result = await commandService.SendAsync(command);
            if (autoProcessResultException)
            {
                ProcessSendCommandAsyncResultException(result);
            }

            return result;
        }

        private static void ProcessExecuteCommandAsyncResultException(AsyncTaskResult<CommandResult> result)
        {
            if (result.Status != AsyncTaskStatus.Success)
            {
                throw new UserFriendlyException("internal network meets problems!", result.ErrorMessage);
            }

            switch (result.Data.Status)
            {
                case CommandStatus.Success:
                    break;

                case CommandStatus.NothingChanged:
                    break;

                case CommandStatus.None:
                case CommandStatus.Failed:
                default:
                    var aggregateExceptionPrefix = "One or more errors occurred. (";
                    var aggregateExceptionSuffix = ")";
                    if (result.Data.ResultType == typeof(JaneSerializableException).Name)
                    {
                        var exceptionWrapper = SerializableExceptionWrapper.Deserialize(result.Data.Result);
                        throw new UserFriendlyException(exceptionWrapper.Code, exceptionWrapper.Message);
                    }
                    else if (result.Data.ResultType == typeof(string).FullName && result.Data.Result.StartsWith(aggregateExceptionPrefix) && result.Data.Result.EndsWith(aggregateExceptionSuffix))
                    {
                        var exceptionResult = result.Data.Result.Substring(aggregateExceptionPrefix.Length, result.Data.Result.Length - aggregateExceptionPrefix.Length - aggregateExceptionSuffix.Length);
                        var exceptionWrapper = SerializableExceptionWrapper.Deserialize(exceptionResult);
                        throw new UserFriendlyException(exceptionWrapper.Code, exceptionWrapper.Message);
                    }
                    else
                    {
                        throw new UserFriendlyException(result.Data.Result);
                    }
            }
        }

        private static void ProcessSendCommandAsyncResultException(AsyncTaskResult result)
        {
            if (result.Status != AsyncTaskStatus.Success)
            {
                throw new UserFriendlyException("internal network meets problems!", result.ErrorMessage);
            }
        }
    }
}