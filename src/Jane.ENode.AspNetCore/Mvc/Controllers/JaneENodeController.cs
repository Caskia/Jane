using Castle.Core.Internal;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using Jane.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;

namespace Jane.ENode.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Jane system.
    /// </summary>
    public abstract class JaneENodeController : JaneController
    {
        protected readonly ICommandService _commandService;

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
                    if (!Request.Headers["access_token"].IsNullOrEmpty())
                    {
                        return Request.Headers["access_token"];
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

        protected Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, CommandReturnType commandReturnType = CommandReturnType.CommandExecuted, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, commandReturnType).TimeoutAfter(millisecondsDelay);
        }

        private Task<AsyncTaskResult> SendCommandAsync(ICommand command)
        {
            return _commandService.SendAsync(command);
        }
    }
}