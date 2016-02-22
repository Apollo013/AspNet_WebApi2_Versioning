using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2Versioning.Routing;

namespace WebApi2Versioning.Controllers
{
    //[RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        [HttpGet]
        //[Route(Name = "AddTask")]
        public string AddTask()
        {
            return "In V1 USING CONTROLLER NAME HTTP CONTROLLER SELECTOR";
        }
    }
}
