using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi2Versioning.Routing
{
    public class QueryStringHttpControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public QueryStringHttpControllerSelector(HttpConfiguration config)
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

                // Get the version number as in '?V1'  or  '?V2'
                string versionNum = GetVersionFromQueryString(request);

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
        /// Method to Get Query String Values from URL to get the version number  e.g. '?V1'  or  '?V2'
        /// </summary>  
        /// <param name="request">HttpRequestMessage: Current Request made through Browser or Fiddler</param>  
        /// <returns>Version Number</returns>  
        private string GetVersionFromQueryString(HttpRequestMessage request)
        {
            var versionStr = HttpUtility.ParseQueryString(request.RequestUri.Query);

            if (versionStr[0] != null)
            {
                return versionStr[0];
            }
            return "V1";
        }

        /// <summary>  
        /// Method to Get Query String Values from URL to get the version number  e.g. '?V=1'  or  '?V=2'
        /// </summary>  
        /// <param name="request">HttpRequestMessage: Current Request made through Browser or Fiddler</param>  
        /// <returns>Version Number</returns>  
        private string GetVersionFromQueryStringKey(HttpRequestMessage request)
        {
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);

            var version = query["v"];

            if (version != null)
            {
                return version;
            }

            return "1";

        }
    }
}
