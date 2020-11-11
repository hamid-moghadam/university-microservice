using Core.API.Attributes;
using Identity.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.API.Controllers.App
{
    [ApiExplorerSettings(GroupName = "app")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public abstract class BaseAppApiController : ControllerBase
    {
        protected string UserId => User.GetUserId();
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
    }
}