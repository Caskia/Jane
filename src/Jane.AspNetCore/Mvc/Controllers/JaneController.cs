using Jane.Dependency;
using Jane.Runtime.Session;
using Microsoft.AspNetCore.Mvc;

namespace Jane.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Jane system.
    /// </summary>
    public abstract class JaneController : Controller
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected JaneController()
        {
            JaneSession = ObjectContainer.Resolve<IJaneSession>();
        }

        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IJaneSession JaneSession { get; set; }
    }
}