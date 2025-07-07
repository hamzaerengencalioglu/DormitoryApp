using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using YurtApps.Application.Interfaces;

namespace YurtApps.Api.Authorization
{
    public class OwnsDormitoryHandler : AuthorizationHandler<OwnsDormitoryRequirement, int>
    {
        private readonly IDormitoryService _dormitoryService;

        public OwnsDormitoryHandler(IDormitoryService dormitoryService)
        {
            _dormitoryService = dormitoryService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OwnsDormitoryRequirement requirement,
            int resource)
        {
            var UserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (UserId == null)
                return;

            var ownsDormitory = await _dormitoryService.UserOwnsDormitory(UserId, resource);

            if (ownsDormitory)
            {
                context.Succeed(requirement);
            }
        }
    }
}
