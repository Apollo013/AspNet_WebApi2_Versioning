using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace WebApi2Versioning.Routing
{
    public class ApiVersionConstraint : IHttpRouteConstraint
    {
        public string AllowedVersion { get; private set; }

        public ApiVersionConstraint(string allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        /// <summary>
        /// Determines if the specified version is permitted
        /// </summary>
        /// <param name="request"></param>
        /// <param name="route"></param>
        /// <param name="parameterName"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection">true if the specified parameter name equals the AllowedVersion property, false otherwise</param>
        /// <returns></returns>
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if(values.TryGetValue(parameterName, out value) && value != null)
            {
                return AllowedVersion.Equals(value.ToString().ToLowerInvariant());
            }
            return false;
        }
    }
}
