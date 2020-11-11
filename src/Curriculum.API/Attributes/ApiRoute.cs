using Microsoft.AspNetCore.Mvc;

namespace Curriculum.API.Attributes
{
    public class ApiRoute : RouteAttribute
    {
        public ApiRoute(string template) : base($"sc/api/v1/{template}")
        {
        }
    }
}