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
            config.MapHttpAttributeRoutes();

            // Uncomment any one of the following
            // ConfigureNamespaceVersioning(config);
            // ConfigureControllerNameVersioning(config);
            ConfigureQueryStringVersioning(config);
        }

        private static void ConfigureNamespaceVersioning(HttpConfiguration config)
        {
            /**** METHOD 01 - USING DIFFERENT NAMESPACE ****/
            // THE FOLLOWING FILES ARE USED
            // (01) WebApi2Versioning.Controllers.V1.TasksController
            // (02) WebApi2Versioning.Controllers.V2.TasksController
            // (03) WebApi2Versioning.Routing.ApiVersion1RoutePrefixAttribute
            // (04) WebApi2Versioning.Routing.ApiVersionConstraint
            // (05) WebApi2Versioning.Routing.NamespaceHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET requests
            // http://localhost:[YOUR_PORT_NUMBER]/api/v1/tasks/
            // http://localhost:[YOUR_PORT_NUMBER]/api/v2/tasks/

            /**** NOTE ****/
            // This particular configuration allows us to use both V1 & V2

            // We need to register our constraint with ASP.NET Web API so that it gets applied to incoming requests.   
            // Our ApiVersionConstraint is registered with a constraint resolver, which the framework uses to find and instantiate the appropriate constraint at runtime. 
            // We also need to configure our custom controller selector, replacing the default, namespace-unaware, controller selector. 
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof (ApiVersionConstraint));
            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));            
        }

        private static void ConfigureControllerNameVersioning(HttpConfiguration config)
        {
            /**** METHOD 02 - USING DIFFERENT CONTROLLER NAME ****/
            // THE FOLLOWING FILES ARE USED
            // (01) WebApi2Versioning.Controllers.TasksController
            // (02) WebApi2Versioning.Controllers.TasksV2Controller
            // (03) WebApi2Versioning.Routing.ControllerNameHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET requests
            // http://localhost:[YOUR_PORT_NUMBER]/api/v1/tasks/
            // http://localhost:[YOUR_PORT_NUMBER]/api/v2/tasks/

            /**** NOTE ****/
            // This particular configuration ONLY allows us to use V2 (see ControllerNameHttpControllerSelector)
            // Alos note for this example, controllers are explicitly specified
            config.Routes.MapHttpRoute(name: "Tasks", routeTemplate: "api/v1/tasks", defaults: new { controller = "tasks" });
            config.Routes.MapHttpRoute(name: "Tasks2", routeTemplate: "api/v2/tasks", defaults: new { controller = "tasksV2" });
            config.Services.Replace(typeof(IHttpControllerSelector), new ControllerNameHttpControllerSelector(config));
        }

        private static void ConfigureQueryStringVersioning(HttpConfiguration config)
        {
            /**** METHOD 02 - USING QUERY STRING WHERE PARAMETER = '?V1' or '?V2' ****/
            // THE FOLLOWING FILES ARE USED
            // (01) WebApi2Versioning.Controllers.TasksController
            // (02) WebApi2Versioning.Controllers.TasksV2Controller
            // (03) WebApi2Versioning.Routing.ControllerNameHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET requests
            // http://localhost:[YOUR_PORT_NUMBER]/api/tasks/?V1
            // http://localhost:[YOUR_PORT_NUMBER]/api/tasks/?V2
            config.Routes.MapHttpRoute(name: "Tasks", routeTemplate: "api/{controller}");
            config.Routes.MapHttpRoute(name: "Tasks2", routeTemplate: "api/{controller}");
            config.Services.Replace(typeof(IHttpControllerSelector), new QueryStringHttpControllerSelector(config));
        }

    }
}
