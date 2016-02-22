using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2Versioning.Controllers
{
    //[RoutePrefix("api/tasks")]
    public class TasksV2Controller : ApiController
    {
        [HttpGet]
        //[Route(Name = "AddTaskV2")]
        public string AddTask()
        {
            return "In V2 USING CONTROLLER NAME HTTP CONTROLLER SELECTOR";
        }
    }
}
