using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;

namespace OcelotBasic
{
    public static class ServiceExt
    {
        public static void AddOcelotRouting(this IServiceCollection s)
        {
            s.AddOcelot();

        }

        public static void AddGigyaOidc(this IServiceCollection s)
        {
            var authenticationProviderKey = "TestKey";

            s.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, configureOptions: options =>
                {
                    options.Authority =
                        "https://fidm.us1.gigya.com/oidc/op/v1.0/3__NKd98KtcRCL_Z98TO7bbTtMhZqe83A4hOjMA2wblxL8PAoduwgW9FTvdQ6OqYIB"; // IDENTITY SERVER URL

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://oauth.connect.com/",
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            
        }
        
        
    }
}