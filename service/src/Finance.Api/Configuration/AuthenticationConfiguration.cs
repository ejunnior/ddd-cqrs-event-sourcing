namespace Finance.Api.Configuration
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class AuthenticationConfiguration
    {
        public static IServiceCollection RegisterAuthentication(
            this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Environment.GetEnvironmentVariable("Security__IdentityProvider");
                options.Audience = Environment.GetEnvironmentVariable("Security__Audience");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            return services;
        }
    }
}