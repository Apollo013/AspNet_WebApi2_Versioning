using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi2Versioning.Routing
{
    public class ControllerNameHttpControllerSelector : DefaultHttpControllerSelector
    {

        private HttpConfiguration _config;

        public ControllerNameHttpControllerSelector(HttpConfiguration config)
            : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // Get a dictionary of all API Controllers which derive from 'ApiController' class
            var controllers = GetControllerMapping(); //Will ignore any controllers with same name even if they are in different namepsace

            // Retrieve the route data from the request and look for the controller name in this request.
            var routeData = request.GetRouteData();
            var controllerName = routeData.Values["controller"].ToString();
            HttpControllerDescriptor controllerDescriptor;

            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                var version = "2";            
                var versionedControllerName = string.Concat(controllerName, "V", version);

                HttpControllerDescriptor versionedControllerDescriptor;
                if (controllers.TryGetValue(versionedControllerName, out versionedControllerDescriptor))
                {
                    return versionedControllerDescriptor;
                }

                return controllerDescriptor;
            }

            return null;

        }
    }
}
