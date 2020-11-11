using Identity.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HttpAggregator.Controllers
{
    [ApiExplorerSettings(GroupName = "app")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public abstract class BaseApiController : ControllerBase
    {
        protected string UserId => User.GetUserId();
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
    }
}