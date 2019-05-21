using Castle.Core.Internal;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Jane.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace Jane.ENode.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Jane system.
    /// </summary>
    public abstract class JaneENodeController : JaneController
    {
        protected readonly ICommandService _commandService;

        protected readonly TimeSpan PollInterval = TimeSpan.FromMilliseconds(500);
        protected readonly TimeSpan WaitTimeout = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Constructor.
        /// </summary>
        protected JaneENodeController(
            ICommandService commandService
            )
        {
            _commandService = commandService;
        }

        #region Authorize Properties

        public string AccessToken
        {
            get
            {
                if (Request.Headers != null)
                {
                    if (!Request.Headers["authorization"].IsNullOrEmpty())
                    {
                        var authorization = Request.Headers["authorization"].ToString();
                        var items = authorization.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (items.Length == 2)
                        {
                            return items[1];
                        }
                    }
                }

                return Request.Query["access_token"];
            }
        }

        public string DeviceToken
        {
            get
            {
                return Request.Query["device_token"];
            }
        }

        public string UserAgent
        {
            get
            {
                if (Request.Headers != null)
                {
                    return Request.Headers["User-Agent"];
                }
                return null;
            }
        }

        public long UserId
        {
            get
            {
                return JaneSession.UserId.Value;
            }
        }

        #endregion Authorize Properties

        protected async Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, CommandReturnType commandReturnType = CommandReturnType.CommandExecuted, int millisecondsDelay = 10000, bool autoProcessResultException = true)
        {
            var result = await _commandService.ExecuteAsync(command, commandReturnType).TimeoutAfter(millisecondsDelay);
            if (autoProcessResultException)
            {
                ProcessExecuteCommandAsyncResultException(result);
            }
            return result;
        }

        protected void ProcessExecuteCommandAsyncResultException(AsyncTaskResult<CommandResult> result)
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

        protected void ProcessSendCommandAsyncResultException(AsyncTaskResult result)
        {
            if (result.Status != AsyncTaskStatus.Success)
            {
                throw new UserFriendlyException("internal network meets problems!", result.ErrorMessage);
            }
        }

        protected async Task<AsyncTaskResult> SendCommandAsync(ICommand command, bool autoProcessResultException = true)
        {
            var result = await _commandService.SendAsync(command);
            if (autoProcessResultException)
            {
                ProcessSendCommandAsyncResultException(result);
            }
            return result;
        }
    }
}