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
    public class AcceptHeaderParamHttpControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public AcceptHeaderParamHttpControllerSelector(HttpConfiguration config)
            : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            try
            {
                // Get a dictionary of all API Controllers which derive from 'ApiController' class
                var controllers = GetControllerMapping();
                // Retrieve the route data from the request and look for the controller name in this request.
                var routeData = request.GetRouteData();
                var controllerName = routeData.Values["controller"].ToString();
                HttpControllerDescriptor controllerDescriptor;

                // Get the version number as in 'V1'  or  'V2' BASED ON THE CONTENT TYPE
                string versionNum = GetVersionFromAcceptHeader(request);

                if (versionNum == "v1")
                {
                    if (controllers.TryGetValue(controllerName, out controllerDescriptor))
                    {
                        return controllerDescriptor;
                    }
                }
                else
                {
                    controllerName = string.Concat(controllerName, "V2");
                    if (controllers.TryGetValue(controllerName, out controllerDescriptor))
                    {
                        return controllerDescriptor;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Method to Get Accept Header Values.  
        /// </summary>  
        /// <param name="request">HttpRequestMessage: Current Request made through Browser or Fiddler</param>  
        /// <returns>Version Number</returns>  
        private string GetVersionFromAcceptHeader(HttpRequestMessage request)
        {
            var acceptHeader = request.Headers.Accept;

            foreach (var mime in acceptHeader)
            {
                if (mime.MediaType == "application/json")
                {
                    return "V2";
                }
                else if (mime.MediaType == "application/xml")
                {
                    return "V1";
                }
                else { return "V1"; }

            }
            return "V1";
        }

    }
}
