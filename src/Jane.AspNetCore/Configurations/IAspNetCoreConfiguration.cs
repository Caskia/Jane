using Jane.AspNetCore.Mvc.Results.Caching;
using Jane.Dependency;
using Jane.Web.Models;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Jane.Configurations
{
    public interface IAspNetCoreConfiguration : ISingletonDependency
    {
        IClientCacheAttribute DefaultClientCacheAttribute { get; set; }

        WrapResultAttribute DefaultWrapResultAttribute { get; }

        List<Type> FormBodyBindingIgnoredTypes { get; }

        /// <summary>
        /// Used to enable/disable auditing for MVC controllers.
        /// Default: true.
        /// </summary>
        bool IsAuditingEnabled { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// Used to add route config for modules.
        /// </summary>
        List<Action<IRouteBuilder>> RouteConfiguration { get; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool SetNoCacheForAjaxResponses { get; set; }
    }
}