using Microsoft.AspNetCore.Mvc;

namespace HttpAggregator.Attributes
{
    public class ApiRoute : RouteAttribute
    {
        public ApiRoute(string template) : base($"a/api/v1/{template}")
        {
        }
    }
}