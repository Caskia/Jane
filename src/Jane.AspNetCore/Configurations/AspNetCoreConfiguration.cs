using Jane.AspNetCore.Mvc.Results.Caching;
using Jane.Web.Models;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Jane.Configurations
{
    public class AspNetCoreConfiguration : IAspNetCoreConfiguration
    {
        public AspNetCoreConfiguration()
        {
            DefaultWrapResultAttribute = new WrapResultAttribute();
            DefaultClientCacheAttribute = new NoClientCacheAttribute(false);
            FormBodyBindingIgnoredTypes = new List<Type>();
            RouteConfiguration = new List<Action<IRouteBuilder>>();
            IsValidationEnabledForControllers = true;
            SetNoCacheForAjaxResponses = true;
            IsAuditingEnabled = true;
        }

        public IClientCacheAttribute DefaultClientCacheAttribute { get; set; }
        public WrapResultAttribute DefaultWrapResultAttribute { get; }
        public List<Type> FormBodyBindingIgnoredTypes { get; }
        public bool IsAuditingEnabled { get; set; }
        public bool IsValidationEnabledForControllers { get; set; }
        public List<Action<IRouteBuilder>> RouteConfiguration { get; }
        public bool SetNoCacheForAjaxResponses { get; set; }
    }
}