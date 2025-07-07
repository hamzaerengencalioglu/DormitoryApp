using Microsoft.AspNetCore.Authorization;
using YurtApps.Api.Authorization;
using YurtApps.Application.Interfaces;
using YurtApps.Application.Services;

namespace YurtApps.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmin", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("CanRead", policy =>
                    policy.RequireClaim("Permission", "Read"));

                options.AddPolicy("CanWrite", policy =>
                    policy.RequireClaim("Permission", "Write"));

                options.AddPolicy("OwnsDormitory", policy =>
                    policy.Requirements.Add(new OwnsDormitoryRequirement()));
            });
        }
    }
}
