using Microsoft.AspNetCore.Mvc;

namespace Core.API.Attributes
{
    public class ApiRoute : RouteAttribute
    {
        public ApiRoute(string template) : base($"c/api/{template}")
        {
        }
    }

    public class ApiAppRoute : ApiRoute
    {
        public ApiAppRoute(string template) : base($"v1/app/{template}")
        {
        }
    }

    public class ApiPanelRoute : ApiRoute
    {
        public ApiPanelRoute(string template) : base($"v1/panel/{template}")
        {
        }
    }
}