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
            // ConfigureQueryStringVersioning(config);
            // ConfigureCustomHeaderVersioning(config);
            ConfigureAcceptHeaderVersioning(config);

        }

        private static void ConfigureNamespaceVersioning(HttpConfiguration config)
        {
            /**** METHOD 01 - USING DIFFERENT NAMESPACE ****/
            // THE FOLLOWING CLASSES ARE IN PLAY ...
            // (01) WebApi2Versioning.Controllers.V1.TasksController            /* NOTE THE DIFFERENT NAMESPACES */
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
            // THE FOLLOWING CLASSES ARE IN PLAY ...
            // (01) WebApi2Versioning.Controllers.TasksController            /* NOTE THE DIFFERENT CONTROLLER NAMES IN THE SAME NAMESPACE */
            // (02) WebApi2Versioning.Controllers.TasksV2Controller
            // (03) WebApi2Versioning.Routing.ControllerNameHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET requests
            // http://localhost:[YOUR_PORT_NUMBER]/api/v1/tasks/
            // http://localhost:[YOUR_PORT_NUMBER]/api/v2/tasks/

            /**** NOTE ****/
            // This particular configuration ONLY allows us to use V2 (see why => ControllerNameHttpControllerSelector)
            // Also note for this example, controllers are explicitly specified
            config.Routes.MapHttpRoute(name: "Tasks", routeTemplate: "api/v1/tasks", defaults: new { controller = "tasks" });
            config.Routes.MapHttpRoute(name: "Tasks2", routeTemplate: "api/v2/tasks", defaults: new { controller = "tasksV2" });
            config.Services.Replace(typeof(IHttpControllerSelector), new ControllerNameHttpControllerSelector(config));
        }

        private static void ConfigureQueryStringVersioning(HttpConfiguration config)
        {
            /**** METHOD 03 - USING QUERY STRING WHERE PARAMETER = '?V1' or '?V2' ****/
            // THE FOLLOWING CLASSES ARE IN PLAY ...
            // (01) WebApi2Versioning.Controllers.TasksController
            // (02) WebApi2Versioning.Controllers.TasksV2Controller
            // (03) WebApi2Versioning.Routing.QueryStringHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET requests
            // http://localhost:[YOUR_PORT_NUMBER]/api/tasks/?V1
            // http://localhost:[YOUR_PORT_NUMBER]/api/tasks/?V2
            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}");
            config.Services.Replace(typeof(IHttpControllerSelector), new QueryStringHttpControllerSelector(config));
        }

        private static void ConfigureCustomHeaderVersioning(HttpConfiguration config)
        {
            /**** METHOD 04 - USING CUSTOM REQUEST HEADER PARAMETER  ****/
            // THE FOLLOWING CLASSES ARE IN PLAY ...
            // (01) WebApi2Versioning.Controllers.TasksController
            // (02) WebApi2Versioning.Controllers.TasksV2Controller
            // (03) WebApi2Versioning.Routing.CustomHeaderParamHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET request
            // http://localhost:[YOUR_PORT_NUMBER]/api/tasks/

            /* In the header sepcify ...
                User-Agent: Fiddler
                Version-Num: V1

            OR

                User-Agent: Fiddler
                Version-Num: V2
            */
            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}");
            config.Services.Replace(typeof(IHttpControllerSelector), new CustomHeaderParamHttpControllerSelector(config));
        }

        private static void ConfigureAcceptHeaderVersioning(HttpConfiguration config)
        {
            /**** METHOD 05 - USING CONTENT NEGOTIATION WITH THE ACCEPT HEADER PARAMETER  ****/
            // THE FOLLOWING CLASSES ARE IN PLAY ...
            // (01) WebApi2Versioning.Controllers.TasksController
            // (02) WebApi2Versioning.Controllers.TasksV2Controller
            // (03) WebApi2Versioning.Routing.AcceptHeaderParamHttpControllerSelector

            /**** TO TEST ****/
            // Use FIDDLER or POSTMAN and try the following GET request
            // http://localhost:[YOUR_PORT_NUMBER]/api/tasks/

            /* In the header sepcify ...
                User-Agent: Fiddler
                Content-Type: application/json

            OR

                User-Agent: Fiddler
                Content-Type: application/xml
            */
            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}");
            config.Services.Replace(typeof(IHttpControllerSelector), new AcceptHeaderParamHttpControllerSelector(config));
        }

    }
}
