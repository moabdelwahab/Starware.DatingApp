using Microsoft.AspNetCore.Mvc.Filters;
using Starware.DatingApp.API.Extensions;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.ActionFilters
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var excutedContext = await next();

            if (!excutedContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userName = excutedContext.HttpContext.User.GetUserName();

            var userService = excutedContext.HttpContext.RequestServices.GetRequiredService<IUserService>();

            await  userService.LogUserActivity(userName);

        }
    }
}
