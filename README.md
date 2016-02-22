# Web_API_2_URI_Versioning
A DOTNET Web Api 2 project that demonstrates one technique for API versioning using the route uri in conjunction with route attributes. The project makes use of a custom controller selector that implements 'IHttpControllerSelector', along with custom attribute-based routing & contraints. Requires Fiddler to test.

####Problem 01####
We want to version our web by defining URIs like the following:

/api/v1/tasks/

/api/v2/tasks/


These should map to controllers in the following namespaces:

MyProject.Controllers.V1.TasksController

MyProject.Controllers.V2.TasksController



####To Test####

In Visual Studio 2015, uncomment the line '// ConfigureNamespaceVersioning(config);' in the WebApiConfig class, then simply run the project using ctrl-F5, and then in Fiddler, issue the following GET requests ...

(1) http://localhost:[YOUR_PORT_NUMBER]/api/v1/tasks

(2) http://localhost:[YOUR_PORT_NUMBER]/api/v2/tasks


Both will respond with a message confirming which controller the request was routed to. (You may need to change the port number from 50333 to whatever is relevant on your system)

####Problem 02####
We want to version our web by defining URIs like the following:

/api/v1/tasks/

/api/v2/tasks/


But in this case we want to map to controllers with the following names:

MyProject.Controllers.TasksController

MyProject.Controllers.TasksV2Controller



####To Test####

In Visual Studio 2015, uncomment the line '// ConfigureControllerNameVersioning(config);' in the WebApiConfig class, then simply run the project using ctrl-F5, and then in Fiddler, issue the following GET requests ...

(1) http://localhost:[YOUR_PORT_NUMBER]/api/v1/tasks

(2) http://localhost:[YOUR_PORT_NUMBER]/api/v2/tasks


####Problem 03####
We want to version our web by defining URIs like the following:

/api/tasks/?V1

/api/tasks/?V2


But in this case we want to map to controllers with the following names:

MyProject.Controllers.TasksController

MyProject.Controllers.TasksV2Controller



####To Test####

In Visual Studio 2015, uncomment the line '// ConfigureQueryStringVersioning(config);' in the WebApiConfig class, then simply run the project using ctrl-F5, and then in Fiddler, issue the following GET requests ...

(1) http://localhost:[YOUR_PORT_NUMBER]/api/tasks/?V1

(2) http://localhost:[YOUR_PORT_NUMBER]/api/tasks/?V2





####Information Sources####

(1) ASP.NET Web API 2 - Building a REST service from start to finish, Second Edition; Jamie Kurtz and Brian Wortman

(2) http://bitoftech.net/2013/12/16/asp-net-web-api-versioning-accept-header-query-string

(3) https://blogs.msdn.microsoft.com/webdev/2013/03/07/asp-net-web-api-using-namespaces-to-version-web-apis/

(4) http://www.c-sharpcorner.com/UploadFile/97fc7a/demystify-web-api-versioning/


