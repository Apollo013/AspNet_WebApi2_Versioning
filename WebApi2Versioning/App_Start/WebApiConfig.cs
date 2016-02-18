using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using WebApi2Versioning.Routing;

namespace WebApi2Versioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // We need to register our constraint with ASP.NET Web API so that it gets applied to incoming requests.   
            // Our ApiVersionConstraint is registered with a constraint resolver, which the framework uses to find and instantiate the appropriate constraint at runtime.         
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof (ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);

            // We also need to configure our custom controller selector, replacing the default, namespace-unaware, controller selector.
            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));

        }
    }
}
