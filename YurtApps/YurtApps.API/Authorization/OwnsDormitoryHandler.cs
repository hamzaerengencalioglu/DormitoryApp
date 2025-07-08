using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using YurtApps.Application.Interfaces;

namespace YurtApps.Api.Authorization
{
    public class OwnsDormitoryHandler : AuthorizationHandler<OwnsDormitoryRequirement>
    {
        private readonly IDormitoryService _dormitoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OwnsDormitoryHandler(IDormitoryService dormitoryService, IHttpContextAccessor httpContextAccessor)
        {
            _dormitoryService = dormitoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OwnsDormitoryRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return;

            if (!httpContext.Request.RouteValues.TryGetValue("id", out var idValue))
                return;

            if (!int.TryParse(idValue?.ToString(), out int dormitoryId))
                return;

            var ownsDormitory = await _dormitoryService.UserOwnsDormitory(userId, dormitoryId);
            if (ownsDormitory)
            {
                context.Succeed(requirement);
            }
        }
    }
}
