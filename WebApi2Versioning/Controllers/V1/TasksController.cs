using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2Versioning.Routing;

/*
    For versioning here, we’re using the RoutePrefix attribute directly rather than subclassing it
*/

namespace WebApi2Versioning.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    public class TasksController : ApiController
    {
        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        public string AddTask(HttpRequestMessage requestMessage)
        {
            return "In v1, Task Added";
        }
    }
}
