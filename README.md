# Web_API_2_URI_Versioning
A DOTNET Web Api 2 project that demonstrates one technique for API versioning using the route uri in conjunction with route attributes. The project makes use of a custom controller selector that implements 'IHttpControllerSelector', along with custom attribute-based routing & contraints. Requires Fiddler to test.

####Information Sources####

(1) ASP.NET Web API 2 - Building a REST service from start to finish, Second Edition; Jamie Kurtz and Brian Wortman

(2) http://bitoftech.net/2013/12/16/asp-net-web-api-versioning-accept-header-query-string

(3) https://blogs.msdn.microsoft.com/webdev/2013/03/07/asp-net-web-api-using-namespaces-to-version-web-apis/



####To Test####

Simply run the project from VS2015,  and then in Fiddler, POST the following requests ...

(1) http://localhost:50333//api/v1/tasks

(2) http://localhost:50333//api/v2/tasks


Both will respond with a message confirming which controller the request was routed to.