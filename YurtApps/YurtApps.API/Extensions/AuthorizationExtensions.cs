namespace YurtApps.Api.Extensions
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanRead", policy =>
                    policy.RequireClaim("Permission", "Read"));

                options.AddPolicy("CanWrite", policy =>
                    policy.RequireClaim("Permission", "Write"));

            });
        }
    }
}