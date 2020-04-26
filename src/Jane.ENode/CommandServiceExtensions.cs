using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using System;
using System.Threading.Tasks;

namespace Jane.ENode
{
    public static class CommandServiceExtensions
    {
        public static async Task<CommandResult> ExecuteCommandAsync(this ICommandService commandService, ICommand command, CommandReturnType commandReturnType = CommandReturnType.CommandExecuted, int millisecondsDelay = 10000, bool autoProcessResultException = true)
        {
            CommandResult result;

            try
            {
                result = await commandService.ExecuteAsync(command, commandReturnType).TimeoutAfter(millisecondsDelay);
            }
            catch (IOException ioException)
            {
                throw new UserFriendlyException("internal network meets problems.", ioException);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("unknown problems.", ex);
            }

            if (autoProcessResultException)
            {
                ProcessExecuteCommandAsyncResultException(result);
            }
            return result;
        }

        public static async Task SendCommandAsync(this ICommandService commandService, ICommand command)
        {
            try
            {
                await commandService.SendAsync(command);
            }
            catch (IOException ioException)
            {
                throw new UserFriendlyException("internal network meets problems.", ioException);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("unknown problems.", ex);
            }
        }

        private static void ProcessExecuteCommandAsyncResultException(CommandResult result)
        {
            switch (result.Status)
            {
                case CommandStatus.Success:
                case CommandStatus.NothingChanged:
                    break;

                case CommandStatus.None:
                case CommandStatus.Failed:
                default:
                    var aggregateExceptionPrefix = "One or more errors occurred. (";
                    var aggregateExceptionSuffix = ")";
                    if (result.ResultType == typeof(JaneSerializableException).Name)
                    {
                        var exceptionWrapper = SerializableExceptionWrapper.Deserialize(result.Result);
                        throw new UserFriendlyException(exceptionWrapper.Code, exceptionWrapper.Message);
                    }
                    else if (result.ResultType == typeof(string).FullName && result.Result.StartsWith(aggregateExceptionPrefix) && result.Result.EndsWith(aggregateExceptionSuffix))
                    {
                        var exceptionResult = result.Result.Substring(aggregateExceptionPrefix.Length, result.Result.Length - aggregateExceptionPrefix.Length - aggregateExceptionSuffix.Length);
                        var exceptionWrapper = SerializableExceptionWrapper.Deserialize(exceptionResult);
                        throw new UserFriendlyException(exceptionWrapper.Code, exceptionWrapper.Message);
                    }
                    else
                    {
                        throw new UserFriendlyException(result.Result);
                    }
            }
        }
    }
}